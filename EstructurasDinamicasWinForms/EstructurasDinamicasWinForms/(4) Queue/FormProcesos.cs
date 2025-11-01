using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace EstructurasDinamicasWinForms
{
    public partial class FormProcesos : Form
    {
        private Queue<Proceso> colaAlta = new Queue<Proceso>();
        private Queue<Proceso> colaMedia = new Queue<Proceso>();
        private Queue<Proceso> colaBaja = new Queue<Proceso>();

        private List<Proceso> procesosEjecutados = new List<Proceso>();
        private Proceso procesoActual = null;
        private int numeroProceso = 1;
        private Timer timerEjecucion;
        private int cicloActual = 0;
        private Random random = new Random();
        private bool mostrarMensajeTerminacion = false;

        public FormProcesos()
        {
            ConfigurarControles();
            InicializarTimer();
        }

        private void ConfigurarControles()
        {
            this.Text = "Cola de Procesos - Sistema Operativo - Multiples Queues por Prioridad";
            this.Size = new Size(700, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            CrearControles();
        }

        private void InicializarTimer()
        {
            timerEjecucion = new Timer();
            timerEjecucion.Interval = 1000;
            timerEjecucion.Tick += (sender, e) => EjecutarCiclo();
        }

        private void CrearControles()
        {
            // Información del Proceso
            Label lblNombre = new Label();
            lblNombre.Text = "Nombre del Proceso:";
            lblNombre.Location = new Point(20, 20);
            lblNombre.Size = new Size(120, 20);
            this.Controls.Add(lblNombre);

            TextBox txtNombre = new TextBox();
            txtNombre.Location = new Point(150, 20);
            txtNombre.Size = new Size(200, 20);
            txtNombre.Name = "txtNombre";
            this.Controls.Add(txtNombre);

            Label lblTiempo = new Label();
            lblTiempo.Text = "Tiempo CPU (ciclos):";
            lblTiempo.Location = new Point(20, 50);
            lblTiempo.Size = new Size(120, 20);
            this.Controls.Add(lblTiempo);

            TextBox txtTiempo = new TextBox();
            txtTiempo.Location = new Point(150, 50);
            txtTiempo.Size = new Size(80, 20);
            txtTiempo.Name = "txtTiempo";
            txtTiempo.Text = "5";
            this.Controls.Add(txtTiempo);

            Label lblPrioridad = new Label();
            lblPrioridad.Text = "Prioridad:";
            lblPrioridad.Location = new Point(20, 80);
            lblPrioridad.Size = new Size(120, 20);
            this.Controls.Add(lblPrioridad);

            ComboBox cmbPrioridad = new ComboBox();
            cmbPrioridad.Location = new Point(150, 80);
            cmbPrioridad.Size = new Size(120, 20);
            cmbPrioridad.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPrioridad.Items.AddRange(new string[] {
                "Baja",
                "Media",
                "Alta"
            });
            cmbPrioridad.SelectedIndex = 1;
            cmbPrioridad.Name = "cmbPrioridad";
            this.Controls.Add(cmbPrioridad);

            // Botones de Gestión
            Button btnCrearProceso = new Button();
            btnCrearProceso.Text = "🔄 Crear Proceso";
            btnCrearProceso.Location = new Point(20, 110);
            btnCrearProceso.Size = new Size(120, 30);
            btnCrearProceso.Click += (sender, e) =>
            {
                CrearProceso(txtNombre.Text, txtTiempo.Text, cmbPrioridad.SelectedItem?.ToString());
                txtNombre.Clear();
                txtNombre.Focus();
            };
            this.Controls.Add(btnCrearProceso);

            Button btnEjecutarSiguiente = new Button();
            btnEjecutarSiguiente.Text = "⚡ Ejecutar Siguiente";
            btnEjecutarSiguiente.Location = new Point(150, 110);
            btnEjecutarSiguiente.Size = new Size(130, 30);
            btnEjecutarSiguiente.Click += (sender, e) =>
            {
                EjecutarSiguienteProceso();
            };
            this.Controls.Add(btnEjecutarSiguiente);

            Button btnProcesoAutomatico = new Button();
            btnProcesoAutomatico.Text = "🎲 Proceso Auto";
            btnProcesoAutomatico.Location = new Point(290, 110);
            btnProcesoAutomatico.Size = new Size(110, 30);
            btnProcesoAutomatico.Click += (sender, e) =>
            {
                GenerarProcesoAutomatico();
            };
            this.Controls.Add(btnProcesoAutomatico);

            Button btnLimpiar = new Button();
            btnLimpiar.Text = "🧹 Limpiar Todo";
            btnLimpiar.Location = new Point(410, 110);
            btnLimpiar.Size = new Size(100, 30);
            btnLimpiar.Click += (sender, e) =>
            {
                LimpiarSistema();
            };
            this.Controls.Add(btnLimpiar);

            // Panel de Proceso Actual
            Label lblProcesoActual = new Label();
            lblProcesoActual.Text = "Proceso en Ejecución:";
            lblProcesoActual.Location = new Point(20, 160);
            lblProcesoActual.Size = new Size(150, 20);
            lblProcesoActual.Font = new Font(lblProcesoActual.Font, FontStyle.Bold);
            this.Controls.Add(lblProcesoActual);

            Label lblProcesoEjecucion = new Label();
            lblProcesoEjecucion.Text = "CPU Libre - No hay proceso en ejecución";
            lblProcesoEjecucion.Location = new Point(20, 185);
            lblProcesoEjecucion.Size = new Size(650, 35);
            lblProcesoEjecucion.Name = "lblProcesoEjecucion";
            lblProcesoEjecucion.ForeColor = Color.White;
            lblProcesoEjecucion.BackColor = Color.DarkBlue;
            lblProcesoEjecucion.BorderStyle = BorderStyle.FixedSingle;
            lblProcesoEjecucion.TextAlign = ContentAlignment.MiddleCenter;
            lblProcesoEjecucion.Font = new Font(lblProcesoEjecucion.Font, FontStyle.Bold);
            this.Controls.Add(lblProcesoEjecucion);

            // Barra de Progreso
            ProgressBar progressBar = new ProgressBar();
            progressBar.Location = new Point(20, 230);
            progressBar.Size = new Size(650, 20);
            progressBar.Name = "progressBar";
            progressBar.Visible = false;
            this.Controls.Add(progressBar);

            // Colas de Procesos por Prioridad
            Label lblColas = new Label();
            lblColas.Text = "Colas de Procesos por Prioridad (Queue FIFO):";
            lblColas.Location = new Point(20, 270);
            lblColas.Size = new Size(300, 20);
            lblColas.Font = new Font(lblColas.Font, FontStyle.Bold);
            this.Controls.Add(lblColas);

            // Cola Alta
            Label lblColaAlta = new Label();
            lblColaAlta.Text = "🔴 Alta Prioridad:";
            lblColaAlta.Location = new Point(20, 300);
            lblColaAlta.Size = new Size(150, 20);
            lblColaAlta.Font = new Font(lblColaAlta.Font, FontStyle.Bold);
            this.Controls.Add(lblColaAlta);

            ListBox lstColaAlta = new ListBox();
            lstColaAlta.Location = new Point(20, 320);
            lstColaAlta.Size = new Size(200, 60);
            lstColaAlta.Name = "lstColaAlta";
            this.Controls.Add(lstColaAlta);

            // Cola Media
            Label lblColaMedia = new Label();
            lblColaMedia.Text = "🟡 Media Prioridad:";
            lblColaMedia.Location = new Point(240, 300);
            lblColaMedia.Size = new Size(150, 20);
            lblColaMedia.Font = new Font(lblColaMedia.Font, FontStyle.Bold);
            this.Controls.Add(lblColaMedia);

            ListBox lstColaMedia = new ListBox();
            lstColaMedia.Location = new Point(240, 320);
            lstColaMedia.Size = new Size(200, 60);
            lstColaMedia.Name = "lstColaMedia";
            this.Controls.Add(lstColaMedia);

            // Cola Baja
            Label lblColaBaja = new Label();
            lblColaBaja.Text = "🟢 Baja Prioridad:";
            lblColaBaja.Location = new Point(460, 300);
            lblColaBaja.Size = new Size(150, 20);
            lblColaBaja.Font = new Font(lblColaBaja.Font, FontStyle.Bold);
            this.Controls.Add(lblColaBaja);

            ListBox lstColaBaja = new ListBox();
            lstColaBaja.Location = new Point(460, 320);
            lstColaBaja.Size = new Size(200, 60);
            lstColaBaja.Name = "lstColaBaja";
            this.Controls.Add(lstColaBaja);

            // Panel de Procesos Terminados
            Label lblProcesosTerminados = new Label();
            lblProcesosTerminados.Text = "Procesos Terminados:";
            lblProcesosTerminados.Location = new Point(20, 400);
            lblProcesosTerminados.Size = new Size(150, 20);
            lblProcesosTerminados.Font = new Font(lblProcesosTerminados.Font, FontStyle.Bold);
            this.Controls.Add(lblProcesosTerminados);

            ListBox lstProcesosTerminados = new ListBox();
            lstProcesosTerminados.Location = new Point(20, 420);
            lstProcesosTerminados.Size = new Size(650, 80);
            lstProcesosTerminados.Name = "lstProcesosTerminados";
            this.Controls.Add(lstProcesosTerminados);

            // Información de Estado
            Label lblInfo = new Label();
            lblInfo.Text = "Procesos en cola: Alta(0) Media(0) Baja(0) | Terminados: 0 | Ciclo: 0";
            lblInfo.Location = new Point(20, 510);
            lblInfo.Size = new Size(650, 25);
            lblInfo.Name = "lblInfo";
            lblInfo.ForeColor = Color.DarkGreen;
            lblInfo.BackColor = Color.LightYellow;
            lblInfo.BorderStyle = BorderStyle.FixedSingle;
            lblInfo.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(lblInfo);

            // Controles de Ejecución
            Label lblEjecucion = new Label();
            lblEjecucion.Text = "Ejecución Automática:";
            lblEjecucion.Location = new Point(20, 545);
            lblEjecucion.Size = new Size(150, 20);
            lblEjecucion.Font = new Font(lblEjecucion.Font, FontStyle.Bold);
            this.Controls.Add(lblEjecucion);

            Button btnIniciarEjecucion = new Button();
            btnIniciarEjecucion.Text = "▶ Iniciar Ejecución";
            btnIniciarEjecucion.Location = new Point(180, 545);
            btnIniciarEjecucion.Size = new Size(120, 25);
            btnIniciarEjecucion.Click += (sender, e) =>
            {
                IniciarEjecucionAutomatica();
            };
            this.Controls.Add(btnIniciarEjecucion);

            Button btnDetenerEjecucion = new Button();
            btnDetenerEjecucion.Text = "⏹ Detener Ejecución";
            btnDetenerEjecucion.Location = new Point(310, 545);
            btnDetenerEjecucion.Size = new Size(120, 25);
            btnDetenerEjecucion.Click += (sender, e) =>
            {
                DetenerEjecucion();
            };
            this.Controls.Add(btnDetenerEjecucion);

            Button btnPausarEjecucion = new Button();
            btnPausarEjecucion.Text = "⏸ Pausar/Reanudar";
            btnPausarEjecucion.Location = new Point(440, 545);
            btnPausarEjecucion.Size = new Size(120, 25);
            btnPausarEjecucion.Click += (sender, e) =>
            {
                PausarReanudarEjecucion();
            };
            this.Controls.Add(btnPausarEjecucion);
        }

        private void CrearProceso(string nombre, string tiempoTexto, string prioridad)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("Por favor, ingrese un nombre para el proceso.", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(tiempoTexto, out int tiempo) || tiempo <= 0)
            {
                MessageBox.Show("Por favor, ingrese un tiempo de CPU válido (número positivo).", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(prioridad))
            {
                MessageBox.Show("Por favor, seleccione una prioridad.", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Crear nuevo proceso
            Proceso nuevoProceso = new Proceso(numeroProceso++, nombre, tiempo, prioridad);

            // Agregar a la cola correspondiente según prioridad
            switch (prioridad)
            {
                case "Alta":
                    colaAlta.Enqueue(nuevoProceso);
                    break;
                case "Media":
                    colaMedia.Enqueue(nuevoProceso);
                    break;
                case "Baja":
                    colaBaja.Enqueue(nuevoProceso);
                    break;
            }

            ActualizarVistaSistema();
        }

        private void GenerarProcesoAutomatico()
        {
            string[] nombres = { "Word", "Excel", "Chrome", "Spotify", "Calculadora", "Paint", "Explorador" };
            string[] prioridades = { "Baja", "Media", "Alta" };

            string nombre = nombres[random.Next(nombres.Length)];
            string prioridad = prioridades[random.Next(prioridades.Length)];
            int tiempo = random.Next(3, 15);

            CrearProceso(nombre, tiempo.ToString(), prioridad);
        }

        private void EjecutarSiguienteProceso()
        {
            if (procesoActual != null)
            {
                MessageBox.Show("Ya hay un proceso en ejecución. Espere a que termine.", "Ejecución",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Buscar proceso según prioridad (Alta > Media > Baja)
            if (colaAlta.Count > 0)
            {
                procesoActual = colaAlta.Dequeue();
            }
            else if (colaMedia.Count > 0)
            {
                procesoActual = colaMedia.Dequeue();
            }
            else if (colaBaja.Count > 0)
            {
                procesoActual = colaBaja.Dequeue();
            }
            else
            {
                MessageBox.Show("No hay procesos en la cola para ejecutar.", "Cola Vacía",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            procesoActual.Estado = EstadoProceso.Ejecutando;
            ActualizarVistaSistema();
        }

        private void EjecutarCiclo()
        {
            cicloActual++;

            if (procesoActual != null)
            {
                // Ejecutar un ciclo del proceso actual
                procesoActual.TiempoRestante--;

                // Actualizar barra de progreso
                ProgressBar progressBar = this.Controls.Find("progressBar", true).FirstOrDefault() as ProgressBar;
                if (progressBar != null)
                {
                    progressBar.Visible = true;
                    int progreso = (int)((1.0 - (double)procesoActual.TiempoRestante / procesoActual.TiempoTotal) * 100);
                    progressBar.Value = Math.Min(progreso, 100);
                }

                // Verificar si el proceso terminó
                if (procesoActual.TiempoRestante <= 0)
                {
                    procesoActual.Estado = EstadoProceso.Terminado;
                    procesoActual.FechaTerminacion = DateTime.Now;
                    procesosEjecutados.Add(procesoActual);

                    if (!mostrarMensajeTerminacion)
                    {
                        mostrarMensajeTerminacion = true;
                        MessageBox.Show($"✅ Proceso '{procesoActual.Nombre}' terminado exitosamente.\n" +
                                      $"Ciclos utilizados: {procesoActual.TiempoTotal}",
                                      "Proceso Terminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    procesoActual = null;

                    if (progressBar != null)
                    {
                        progressBar.Visible = false;
                    }
                }
            }
            else
            {
                mostrarMensajeTerminacion = false;
            }

            ActualizarVistaSistema();
        }

        private void IniciarEjecucionAutomatica()
        {
            if (!timerEjecucion.Enabled)
            {
                timerEjecucion.Start();
            }
        }

        private void DetenerEjecucion()
        {
            if (timerEjecucion.Enabled)
            {
                timerEjecucion.Stop();
            }

            // Cancelar proceso actual si existe
            if (procesoActual != null)
            {
                DialogResult resultado = MessageBox.Show(
                    $"¿Está seguro de que desea cancelar el proceso actual '{procesoActual.Nombre}'?",
                    "Confirmar Cancelación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    // Devolver el proceso a la cola correspondiente según su prioridad
                    switch (procesoActual.Prioridad)
                    {
                        case "Alta":
                            colaAlta.Enqueue(procesoActual);
                            break;
                        case "Media":
                            colaMedia.Enqueue(procesoActual);
                            break;
                        case "Baja":
                            colaBaja.Enqueue(procesoActual);
                            break;
                    }

                    procesoActual.Estado = EstadoProceso.Listo;
                    procesoActual = null;

                    // Ocultar barra de progreso
                    ProgressBar progressBar = this.Controls.Find("progressBar", true).FirstOrDefault() as ProgressBar;
                    if (progressBar != null)
                    {
                        progressBar.Visible = false;
                    }

                    MessageBox.Show("Ejecución detenida y proceso actual cancelado.", "Ejecución Detenida",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Ejecución detenida.", "Ejecución Detenida",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            ActualizarVistaSistema();
        }

        private void PausarReanudarEjecucion()
        {
            if (timerEjecucion.Enabled)
            {
                timerEjecucion.Stop();
                MessageBox.Show("Ejecución pausada.", "Ejecución Pausada",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                timerEjecucion.Start();
                MessageBox.Show("Ejecución reanudada.", "Ejecución Reanudada",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            ActualizarVistaSistema();
        }

        private void LimpiarSistema()
        {
            DialogResult resultado = MessageBox.Show(
                "¿Está seguro de que desea limpiar todo el sistema? Se perderán todos los procesos.",
                "Confirmar Limpieza",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                colaAlta.Clear();
                colaMedia.Clear();
                colaBaja.Clear();
                procesosEjecutados.Clear();
                procesoActual = null;
                numeroProceso = 1;
                cicloActual = 0;
                timerEjecucion.Stop();
                mostrarMensajeTerminacion = false;

                ProgressBar progressBar = this.Controls.Find("progressBar", true).FirstOrDefault() as ProgressBar;
                if (progressBar != null)
                {
                    progressBar.Visible = false;
                }

                ActualizarVistaSistema();
            }
        }

        private void ActualizarVistaSistema()
        {
            ListBox lstColaAlta = this.Controls.Find("lstColaAlta", true).FirstOrDefault() as ListBox;
            ListBox lstColaMedia = this.Controls.Find("lstColaMedia", true).FirstOrDefault() as ListBox;
            ListBox lstColaBaja = this.Controls.Find("lstColaBaja", true).FirstOrDefault() as ListBox;
            ListBox lstProcesosTerminados = this.Controls.Find("lstProcesosTerminados", true).FirstOrDefault() as ListBox;
            Label lblProcesoEjecucion = this.Controls.Find("lblProcesoEjecucion", true).FirstOrDefault() as Label;
            Label lblInfo = this.Controls.Find("lblInfo", true).FirstOrDefault() as Label;

            // Actualizar proceso en ejecución
            if (lblProcesoEjecucion != null)
            {
                if (procesoActual != null)
                {
                    string iconoPrioridad = procesoActual.Prioridad == "Alta" ? "🔴" :
                                          procesoActual.Prioridad == "Media" ? "🟡" : "🟢";

                    lblProcesoEjecucion.Text = $"{iconoPrioridad} EJECUTANDO: {procesoActual.Nombre} " +
                                             $"(PID: {procesoActual.Id}) | " +
                                             $"Tiempo: {procesoActual.TiempoRestante}/{procesoActual.TiempoTotal} ciclos | " +
                                             $"Prioridad: {procesoActual.Prioridad}";
                    lblProcesoEjecucion.BackColor = Color.DarkOrange;
                }
                else
                {
                    lblProcesoEjecucion.Text = "💤 CPU Libre - No hay proceso en ejecución";
                    lblProcesoEjecucion.BackColor = Color.DarkBlue;
                }
            }

            // Actualizar colas por prioridad
            if (lstColaAlta != null)
            {
                lstColaAlta.Items.Clear();
                foreach (var proceso in colaAlta)
                {
                    lstColaAlta.Items.Add($"PID {proceso.Id}: {proceso.Nombre} (T: {proceso.TiempoTotal})");
                }
                if (colaAlta.Count == 0) lstColaAlta.Items.Add("(Vacía)");
            }

            if (lstColaMedia != null)
            {
                lstColaMedia.Items.Clear();
                foreach (var proceso in colaMedia)
                {
                    lstColaMedia.Items.Add($"PID {proceso.Id}: {proceso.Nombre} (T: {proceso.TiempoTotal})");
                }
                if (colaMedia.Count == 0) lstColaMedia.Items.Add("(Vacía)");
            }

            if (lstColaBaja != null)
            {
                lstColaBaja.Items.Clear();
                foreach (var proceso in colaBaja)
                {
                    lstColaBaja.Items.Add($"PID {proceso.Id}: {proceso.Nombre} (T: {proceso.TiempoTotal})");
                }
                if (colaBaja.Count == 0) lstColaBaja.Items.Add("(Vacía)");
            }

            // Actualizar procesos terminados
            if (lstProcesosTerminados != null)
            {
                lstProcesosTerminados.Items.Clear();
                if (procesosEjecutados.Count == 0)
                {
                    lstProcesosTerminados.Items.Add("No hay procesos terminados");
                }
                else
                {
                    foreach (var proceso in procesosEjecutados.Take(10))
                    {
                        lstProcesosTerminados.Items.Add($"✅ PID {proceso.Id}: {proceso.Nombre} " +
                                                       $"(T: {proceso.TiempoTotal} ciclos)");
                    }
                }
            }

            // Actualizar información
            if (lblInfo != null)
            {
                lblInfo.Text = $"Procesos en cola: 🔴({colaAlta.Count}) 🟡({colaMedia.Count}) 🟢({colaBaja.Count}) | " +
                             $"Terminados: {procesosEjecutados.Count} | " +
                             $"Ciclo: {cicloActual} | " +
                             $"Ejecución: {(timerEjecucion.Enabled ? "▶ ACTIVA" : "⏸ PAUSADA")}";
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            timerEjecucion?.Stop();
            timerEjecucion?.Dispose();
        }
    }

    public class Proceso
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int TiempoTotal { get; set; }
        public int TiempoRestante { get; set; }
        public string Prioridad { get; set; }
        public EstadoProceso Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaTerminacion { get; set; }

        public Proceso(int id, string nombre, int tiempoTotal, string prioridad)
        {
            Id = id;
            Nombre = nombre;
            TiempoTotal = tiempoTotal;
            TiempoRestante = tiempoTotal;
            Prioridad = prioridad;
            Estado = EstadoProceso.Listo;
            FechaCreacion = DateTime.Now;
        }
    }

    public enum EstadoProceso
    {
        Listo,
        Ejecutando,
        Terminado
    }
}