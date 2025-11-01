using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace EstructurasDinamicasWinForms
{
    public partial class FormCallCenter : Form
    {
        private Queue<Llamada> colaLlamadas = new Queue<Llamada>();
        private List<Agente> agentes = new List<Agente>();
        private int numeroLlamada = 1;
        private Timer timerAtencion;
        private Timer timerActualizacionUI;

        public FormCallCenter()
        {
            ConfigurarControles();
            InicializarAgentes();
            InicializarTimers();
        }

        private void ConfigurarControles()
        {
            this.Text = "Simulador de Call Center - Queue FIFO";
            this.Size = new Size(700, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            CrearControles();
        }

        private void InicializarAgentes()
        {
            // Crear 3 agentes iniciales
            agentes.Add(new Agente("Agente 1", "Ana"));
            agentes.Add(new Agente("Agente 2", "Carlos"));
            agentes.Add(new Agente("Agente 3", "María"));
        }

        private void InicializarTimers()
        {
            // Timer para procesar atención de llamadas
            timerAtencion = new Timer();
            timerAtencion.Interval = 2000;
            timerAtencion.Tick += (sender, e) => ProcesarAtencion();
            timerAtencion.Start();

            // Timer para actualizar la UI en tiempo real
            timerActualizacionUI = new Timer();
            timerActualizacionUI.Interval = 1000;
            timerActualizacionUI.Tick += (sender, e) => ActualizarVistaSistema();
            timerActualizacionUI.Start();
        }

        private void CrearControles()
        {
            // Información de la Llamada
            Label lblCliente = new Label();
            lblCliente.Text = "Nombre del Cliente:";
            lblCliente.Location = new Point(20, 20);
            lblCliente.Size = new Size(120, 20);
            this.Controls.Add(lblCliente);

            TextBox txtCliente = new TextBox();
            txtCliente.Location = new Point(150, 20);
            txtCliente.Size = new Size(200, 20);
            txtCliente.Name = "txtCliente";
            this.Controls.Add(txtCliente);

            Label lblMotivo = new Label();
            lblMotivo.Text = "Motivo de Llamada:";
            lblMotivo.Location = new Point(20, 50);
            lblMotivo.Size = new Size(120, 20);
            this.Controls.Add(lblMotivo);

            ComboBox cmbMotivo = new ComboBox();
            cmbMotivo.Location = new Point(150, 50);
            cmbMotivo.Size = new Size(200, 20);
            cmbMotivo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMotivo.Items.AddRange(new string[] {
                "Consulta General",
                "Soporte Técnico",
                "Reclamo",
                "Ventas",
                "Facturación"
            });
            cmbMotivo.SelectedIndex = 0;
            cmbMotivo.Name = "cmbMotivo";
            this.Controls.Add(cmbMotivo);

            // Botones de Gestión
            Button btnLlamadaEntrante = new Button();
            btnLlamadaEntrante.Text = "📞 Llamada Entrante";
            btnLlamadaEntrante.Location = new Point(20, 80);
            btnLlamadaEntrante.Size = new Size(140, 30);
            btnLlamadaEntrante.Click += (sender, e) =>
            {
                AgregarLlamada(txtCliente.Text, cmbMotivo.SelectedItem?.ToString());
                txtCliente.Clear();
                txtCliente.Focus();
            };
            this.Controls.Add(btnLlamadaEntrante);

            Button btnAtenderManual = new Button();
            btnAtenderManual.Text = "✅ Atender Manual";
            btnAtenderManual.Location = new Point(170, 80);
            btnAtenderManual.Size = new Size(120, 30);
            btnAtenderManual.Click += (sender, e) =>
            {
                AtenderLlamadaManual();
            };
            this.Controls.Add(btnAtenderManual);

            Button btnLlamadaAutomatica = new Button();
            btnLlamadaAutomatica.Text = "🎲 Llamada Auto";
            btnLlamadaAutomatica.Location = new Point(300, 80);
            btnLlamadaAutomatica.Size = new Size(100, 30);
            btnLlamadaAutomatica.Click += (sender, e) =>
            {
                GenerarLlamadaAutomatica();
            };
            this.Controls.Add(btnLlamadaAutomatica);

            Button btnLimpiar = new Button();
            btnLimpiar.Text = "🧹 Limpiar Todo";
            btnLimpiar.Location = new Point(410, 80);
            btnLimpiar.Size = new Size(100, 30);
            btnLimpiar.Click += (sender, e) =>
            {
                LimpiarSistema();
            };
            this.Controls.Add(btnLimpiar);

            // Panel de Agentes
            Label lblAgentes = new Label();
            lblAgentes.Text = "Agentes Disponibles:";
            lblAgentes.Location = new Point(20, 130);
            lblAgentes.Size = new Size(150, 20);
            lblAgentes.Font = new Font(lblAgentes.Font, FontStyle.Bold);
            this.Controls.Add(lblAgentes);

            ListBox lstAgentes = new ListBox();
            lstAgentes.Location = new Point(20, 150);
            lstAgentes.Size = new Size(300, 100);
            lstAgentes.Name = "lstAgentes";
            this.Controls.Add(lstAgentes);

            // Panel de Llamadas en Espera
            Label lblLlamadasEspera = new Label();
            lblLlamadasEspera.Text = "Llamadas en Espera (Cola FIFO):";
            lblLlamadasEspera.Location = new Point(20, 270);
            lblLlamadasEspera.Size = new Size(250, 20);
            lblLlamadasEspera.Font = new Font(lblLlamadasEspera.Font, FontStyle.Bold);
            this.Controls.Add(lblLlamadasEspera);

            ListBox lstLlamadasEspera = new ListBox();
            lstLlamadasEspera.Location = new Point(20, 290);
            lstLlamadasEspera.Size = new Size(300, 120);
            lstLlamadasEspera.Name = "lstLlamadasEspera";
            this.Controls.Add(lstLlamadasEspera);

            // Panel de Llamadas en Curso
            Label lblLlamadasCurso = new Label();
            lblLlamadasCurso.Text = "Llamadas en Curso:";
            lblLlamadasCurso.Location = new Point(350, 130);
            lblLlamadasCurso.Size = new Size(150, 20);
            lblLlamadasCurso.Font = new Font(lblLlamadasCurso.Font, FontStyle.Bold);
            this.Controls.Add(lblLlamadasCurso);

            ListBox lstLlamadasCurso = new ListBox();
            lstLlamadasCurso.Location = new Point(350, 150);
            lstLlamadasCurso.Size = new Size(300, 260);
            lstLlamadasCurso.Name = "lstLlamadasCurso";
            this.Controls.Add(lstLlamadasCurso);

            // Información de Estado
            Label lblInfo = new Label();
            lblInfo.Text = "Llamadas en espera: 0 | Agentes libres: 3/3 | Tiempo promedio: 0s";
            lblInfo.Location = new Point(20, 420);
            lblInfo.Size = new Size(630, 25);
            lblInfo.Name = "lblInfo";
            lblInfo.ForeColor = Color.DarkGreen;
            lblInfo.BackColor = Color.LightYellow;
            lblInfo.BorderStyle = BorderStyle.FixedSingle;
            lblInfo.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(lblInfo);

            // Botón ACTUALIZAR VISTA
            Button btnActualizar = new Button();
            btnActualizar.Text = "Actualizar Vista";
            btnActualizar.Location = new Point(20, 455);
            btnActualizar.Size = new Size(120, 30);
            btnActualizar.Click += (sender, e) =>
            {
                ActualizarVistaSistema();
            };
            this.Controls.Add(btnActualizar);

            // Explicación FIFO
            Label lblExplicacion = new Label();
            lblExplicacion.Text = "💡 FIFO Queue: La primera llamada en entrar es la primera en ser atendida";
            lblExplicacion.Location = new Point(150, 455);
            lblExplicacion.Size = new Size(500, 30);
            lblExplicacion.ForeColor = Color.DarkBlue;
            lblExplicacion.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(lblExplicacion);

            // Controles de Simulación
            Label lblSimulacion = new Label();
            lblSimulacion.Text = "Simulación Automática:";
            lblSimulacion.Location = new Point(20, 495);
            lblSimulacion.Size = new Size(150, 20);
            lblSimulacion.Font = new Font(lblSimulacion.Font, FontStyle.Bold);
            this.Controls.Add(lblSimulacion);

            Button btnIniciarSimulacion = new Button();
            btnIniciarSimulacion.Text = "▶ Iniciar Simulación";
            btnIniciarSimulacion.Location = new Point(180, 495);
            btnIniciarSimulacion.Size = new Size(120, 25);
            btnIniciarSimulacion.Click += (sender, e) =>
            {
                IniciarSimulacionAutomatica();
            };
            this.Controls.Add(btnIniciarSimulacion);

            Button btnDetenerSimulacion = new Button();
            btnDetenerSimulacion.Text = "⏹ Detener Simulación";
            btnDetenerSimulacion.Location = new Point(310, 495);
            btnDetenerSimulacion.Size = new Size(120, 25);
            btnDetenerSimulacion.Click += (sender, e) =>
            {
                DetenerSimulacionAutomatica();
            };
            this.Controls.Add(btnDetenerSimulacion);
        }

        private void AgregarLlamada(string cliente, string motivo)
        {
            if (string.IsNullOrWhiteSpace(cliente))
            {
                MessageBox.Show("Por favor, ingrese un nombre para el cliente.", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(motivo))
            {
                MessageBox.Show("Por favor, seleccione un motivo de llamada.", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Crear y encolar nueva llamada
            Llamada nuevaLlamada = new Llamada(numeroLlamada++, cliente, motivo);
            colaLlamadas.Enqueue(nuevaLlamada);

            MessageBox.Show($"Llamada #{nuevaLlamada.Id} de {cliente} agregada a la cola.\nMotivo: {motivo}",
                          "Llamada Entrante", MessageBoxButtons.OK, MessageBoxIcon.Information);

            ActualizarVistaSistema();
        }

        private void GenerarLlamadaAutomatica()
        {
            string[] clientes = { "Juan Pérez", "María García", "Carlos López", "Ana Martínez", "Pedro Rodríguez" };
            string[] motivos = { "Consulta General", "Soporte Técnico", "Reclamo", "Ventas", "Facturación" };

            Random rnd = new Random();
            string cliente = clientes[rnd.Next(clientes.Length)];
            string motivo = motivos[rnd.Next(motivos.Length)];

            AgregarLlamada(cliente, motivo);
        }

        private void ProcesarAtencion()
        {
            // Buscar agente disponible
            Agente agenteLibre = agentes.FirstOrDefault(a => a.Estado == EstadoAgente.Libre);

            if (agenteLibre != null && colaLlamadas.Count > 0)
            {
                // Desencolar la primera llamada (FIFO)
                Llamada llamada = colaLlamadas.Dequeue();
                agenteLibre.AtenderLlamada(llamada);

                ActualizarVistaSistema();
            }
        }

        private void AtenderLlamadaManual()
        {
            Agente agenteLibre = agentes.FirstOrDefault(a => a.Estado == EstadoAgente.Libre);

            if (agenteLibre == null)
            {
                MessageBox.Show("No hay agentes disponibles en este momento.", "Agentes Ocupados",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (colaLlamadas.Count == 0)
            {
                MessageBox.Show("No hay llamadas en espera para atender.", "Cola Vacía",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Llamada llamada = colaLlamadas.Dequeue();
            agenteLibre.AtenderLlamada(llamada);

            MessageBox.Show($"Agente {agenteLibre.Nombre} atendiendo llamada #{llamada.Id} de {llamada.Cliente}",
                          "Llamada en Curso", MessageBoxButtons.OK, MessageBoxIcon.Information);

            ActualizarVistaSistema();
        }

        private void LimpiarSistema()
        {
            DialogResult resultado = MessageBox.Show(
                "¿Está seguro de que desea limpiar todo el sistema? Se perderán todas las llamadas en espera.",
                "Confirmar Limpieza",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                colaLlamadas.Clear();
                foreach (var agente in agentes)
                {
                    agente.Liberar();
                }
                numeroLlamada = 1;
                ActualizarVistaSistema();
                MessageBox.Show("Sistema limpiado completamente.", "Sistema Limpiado",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void IniciarSimulacionAutomatica()
        {
            timerAtencion.Interval = 1500; // Más rápido para simulación
            MessageBox.Show("Simulación automática iniciada. Las llamadas se atenderán automáticamente cada 1.5 segundos.",
                          "Simulación Iniciada", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void DetenerSimulacionAutomatica()
        {
            timerAtencion.Interval = 2000; // Volver a velocidad normal
            MessageBox.Show("Simulación automática detenida.", "Simulación Detenida",
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ActualizarVistaSistema()
        {
            ListBox lstAgentes = this.Controls.Find("lstAgentes", true).FirstOrDefault() as ListBox;
            ListBox lstLlamadasEspera = this.Controls.Find("lstLlamadasEspera", true).FirstOrDefault() as ListBox;
            ListBox lstLlamadasCurso = this.Controls.Find("lstLlamadasCurso", true).FirstOrDefault() as ListBox;
            Label lblInfo = this.Controls.Find("lblInfo", true).FirstOrDefault() as Label;

            // Actualizar agentes
            if (lstAgentes != null)
            {
                lstAgentes.Items.Clear();
                foreach (var agente in agentes)
                {
                    string estado = agente.Estado == EstadoAgente.Libre ? "🟢 LIBRE" : "🔴 OCUPADO";
                    if (agente.Estado == EstadoAgente.Ocupado && agente.LlamadaActual != null)
                    {
                        TimeSpan tiempoTranscurrido = DateTime.Now - agente.LlamadaActual.HoraInicio;
                        lstAgentes.Items.Add($"{agente.Nombre} - {estado} - T: {tiempoTranscurrido:mm\\:ss}");
                    }
                    else
                    {
                        lstAgentes.Items.Add($"{agente.Nombre} - {estado}");
                    }
                }
            }

            // Actualizar llamadas en espera
            if (lstLlamadasEspera != null)
            {
                lstLlamadasEspera.Items.Clear();
                if (colaLlamadas.Count == 0)
                {
                    lstLlamadasEspera.Items.Add("No hay llamadas en espera");
                }
                else
                {
                    foreach (var llamada in colaLlamadas)
                    {
                        TimeSpan tiempoEspera = DateTime.Now - llamada.HoraInicio;
                        lstLlamadasEspera.Items.Add($"#{llamada.Id} - {llamada.Cliente} - Espera: {tiempoEspera:mm\\:ss}");
                    }
                }
            }

            // Actualizar llamadas en curso
            if (lstLlamadasCurso != null)
            {
                lstLlamadasCurso.Items.Clear();
                var agentesOcupados = agentes.Where(a => a.Estado == EstadoAgente.Ocupado && a.LlamadaActual != null);

                if (!agentesOcupados.Any())
                {
                    lstLlamadasCurso.Items.Add("No hay llamadas en curso");
                }
                else
                {
                    foreach (var agente in agentesOcupados)
                    {
                        var llamada = agente.LlamadaActual;
                        TimeSpan tiempoTranscurrido = DateTime.Now - llamada.HoraInicio;

                        lstLlamadasCurso.Items.Add($"👨‍💼 Agente: {agente.Nombre}");
                        lstLlamadasCurso.Items.Add($"  📞 Llamada #{llamada.Id}");
                        lstLlamadasCurso.Items.Add($"  👤 Cliente: {llamada.Cliente}");
                        lstLlamadasCurso.Items.Add($"  🎯 Motivo: {llamada.Motivo}");
                        lstLlamadasCurso.Items.Add($"  ⏱️ Tiempo: {tiempoTranscurrido:mm\\:ss}");
                        lstLlamadasCurso.Items.Add("");
                    }
                }
            }

            // Actualizar información
            if (lblInfo != null)
            {
                int agentesLibres = agentes.Count(a => a.Estado == EstadoAgente.Libre);
                int llamadasEnEspera = colaLlamadas.Count;

                // Calcular tiempo promedio de espera real
                double tiempoPromedio = 0;
                if (llamadasEnEspera > 0)
                {
                    tiempoPromedio = colaLlamadas.Average(l => (DateTime.Now - l.HoraInicio).TotalSeconds);
                }

                lblInfo.Text = $"Llamadas en espera: {llamadasEnEspera} | " +
                             $"Agentes libres: {agentesLibres}/{agentes.Count} | " +
                             $"Tiempo promedio: {tiempoPromedio:F0}s";
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            timerAtencion?.Stop();
            timerActualizacionUI?.Stop();
            timerAtencion?.Dispose();
            timerActualizacionUI?.Dispose();
        }
    }

    public class Llamada
    {
        public int Id { get; set; }
        public string Cliente { get; set; }
        public string Motivo { get; set; }
        public DateTime HoraInicio { get; set; }

        public Llamada(int id, string cliente, string motivo)
        {
            Id = id;
            Cliente = cliente;
            Motivo = motivo;
            HoraInicio = DateTime.Now;
        }
    }

    public class Agente
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public EstadoAgente Estado { get; set; }
        public Llamada LlamadaActual { get; set; }

        public Agente(string id, string nombre)
        {
            Id = id;
            Nombre = nombre;
            Estado = EstadoAgente.Libre;
            LlamadaActual = null;
        }

        public void AtenderLlamada(Llamada llamada)
        {
            LlamadaActual = llamada;
            Estado = EstadoAgente.Ocupado;
        }

        public void Liberar()
        {
            Estado = EstadoAgente.Libre;
            LlamadaActual = null;
        }
    }

    public enum EstadoAgente
    {
        Libre,
        Ocupado
    }
}