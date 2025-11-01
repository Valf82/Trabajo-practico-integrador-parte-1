using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace EstructurasDinamicasWinForms
{
    public partial class FormImpresion : Form
    {
        private Queue<Documento> colaImpresion = new Queue<Documento>();
        private int numeroDocumento = 1;
        private bool impresionEnCurso = false;

        public FormImpresion()
        {
            ConfigurarControles();
        }

        private void ConfigurarControles()
        {
            this.Text = "Sistema de Cola de Impresión - Queue FIFO";
            this.Size = new Size(600, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            CrearControles();
        }

        private void CrearControles()
        {
            // Información del Documento
            Label lblNombre = new Label();
            lblNombre.Text = "Nombre del Documento:";
            lblNombre.Location = new Point(20, 20);
            lblNombre.Size = new Size(140, 20);
            this.Controls.Add(lblNombre);

            TextBox txtNombre = new TextBox();
            txtNombre.Location = new Point(170, 20);
            txtNombre.Size = new Size(200, 20);
            txtNombre.Name = "txtNombre";
            this.Controls.Add(txtNombre);

            Label lblPaginas = new Label();
            lblPaginas.Text = "Número de Páginas:";
            lblPaginas.Location = new Point(20, 50);
            lblPaginas.Size = new Size(140, 20);
            this.Controls.Add(lblPaginas);

            TextBox txtPaginas = new TextBox();
            txtPaginas.Location = new Point(170, 50);
            txtPaginas.Size = new Size(80, 20);
            txtPaginas.Name = "txtPaginas";
            this.Controls.Add(txtPaginas);

            // Botones de Gestión
            Button btnAgregar = new Button();
            btnAgregar.Text = "📄 Agregar a Cola";
            btnAgregar.Location = new Point(20, 80);
            btnAgregar.Size = new Size(120, 30);
            btnAgregar.Click += (sender, e) =>
            {
                AgregarDocumento(txtNombre.Text, txtPaginas.Text);
                txtNombre.Clear();
                txtPaginas.Clear();
                txtNombre.Focus();
            };
            this.Controls.Add(btnAgregar);

            Button btnImprimir = new Button();
            btnImprimir.Text = "🖨️ Imprimir Siguiente";
            btnImprimir.Location = new Point(150, 80);
            btnImprimir.Size = new Size(120, 30);
            btnImprimir.Click += (sender, e) =>
            {
                ImprimirSiguiente();
            };
            this.Controls.Add(btnImprimir);

            Button btnImprimirTodo = new Button();
            btnImprimirTodo.Text = "⚡ Imprimir Todo";
            btnImprimirTodo.Location = new Point(280, 80);
            btnImprimirTodo.Size = new Size(120, 30);
            btnImprimirTodo.Click += (sender, e) =>
            {
                ImprimirTodo();
            };
            this.Controls.Add(btnImprimirTodo);

            Button btnLimpiar = new Button();
            btnLimpiar.Text = "🧹 Limpiar Cola";
            btnLimpiar.Location = new Point(410, 80);
            btnLimpiar.Size = new Size(100, 30);
            btnLimpiar.Click += (sender, e) =>
            {
                LimpiarCola();
            };
            this.Controls.Add(btnLimpiar);

            // Panel de Impresión Actual
            Label lblImpresionActual = new Label();
            lblImpresionActual.Text = "Imprimiendo Actualmente:";
            lblImpresionActual.Location = new Point(20, 130);
            lblImpresionActual.Size = new Size(180, 20);
            lblImpresionActual.Font = new Font(lblImpresionActual.Font, FontStyle.Bold);
            this.Controls.Add(lblImpresionActual);

            Label lblDocumentoActual = new Label();
            lblDocumentoActual.Text = "Ningún documento en impresión";
            lblDocumentoActual.Location = new Point(20, 155);
            lblDocumentoActual.Size = new Size(550, 30);
            lblDocumentoActual.Name = "lblDocumentoActual";
            lblDocumentoActual.ForeColor = Color.White;
            lblDocumentoActual.BackColor = Color.DarkBlue;
            lblDocumentoActual.BorderStyle = BorderStyle.FixedSingle;
            lblDocumentoActual.TextAlign = ContentAlignment.MiddleCenter;
            lblDocumentoActual.Font = new Font(lblDocumentoActual.Font, FontStyle.Bold);
            this.Controls.Add(lblDocumentoActual);

            // Barra de Progreso
            ProgressBar progressBar = new ProgressBar();
            progressBar.Location = new Point(20, 195);
            progressBar.Size = new Size(550, 20);
            progressBar.Name = "progressBar";
            progressBar.Visible = false;
            this.Controls.Add(progressBar);

            // Cola de Impresión
            Label lblCola = new Label();
            lblCola.Text = "Cola de Impresión (FIFO):";
            lblCola.Location = new Point(20, 230);
            lblCola.Size = new Size(200, 20);
            lblCola.Font = new Font(lblCola.Font, FontStyle.Bold);
            this.Controls.Add(lblCola);

            ListBox lstCola = new ListBox();
            lstCola.Location = new Point(20, 250);
            lstCola.Size = new Size(550, 150);
            lstCola.Name = "lstCola";
            this.Controls.Add(lstCola);

            // Información de Estado
            Label lblInfo = new Label();
            lblInfo.Text = "Documentos en cola: 0 | Total páginas pendientes: 0";
            lblInfo.Location = new Point(20, 410);
            lblInfo.Size = new Size(550, 25);
            lblInfo.Name = "lblInfo";
            lblInfo.ForeColor = Color.DarkGreen;
            lblInfo.BackColor = Color.LightYellow;
            lblInfo.BorderStyle = BorderStyle.FixedSingle;
            lblInfo.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(lblInfo);

            // Botón ACTUALIZAR VISTA
            Button btnActualizar = new Button();
            btnActualizar.Text = "Actualizar Vista";
            btnActualizar.Location = new Point(20, 445);
            btnActualizar.Size = new Size(120, 30);
            btnActualizar.Click += (sender, e) =>
            {
                ActualizarVistaCola();
            };
            this.Controls.Add(btnActualizar);

            // Explicación FIFO
            Label lblExplicacion = new Label();
            lblExplicacion.Text = "💡 FIFO Queue: El primer documento en entrar es el primero en imprimirse";
            lblExplicacion.Location = new Point(150, 445);
            lblExplicacion.Size = new Size(420, 30);
            lblExplicacion.ForeColor = Color.DarkBlue;
            lblExplicacion.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(lblExplicacion);
        }

        private void AgregarDocumento(string nombre, string paginasTexto)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("Por favor, ingrese un nombre para el documento.", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(paginasTexto, out int paginas) || paginas <= 0)
            {
                MessageBox.Show("Por favor, ingrese un número válido de páginas.", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Crear y encolar nuevo documento
            Documento nuevoDocumento = new Documento($"Doc{numeroDocumento}", nombre, paginas);
            colaImpresion.Enqueue(nuevoDocumento);
            numeroDocumento++;

            MessageBox.Show($"Documento '{nombre}' ({paginas} páginas) agregado a la cola de impresión.",
                          "Documento Agregado", MessageBoxButtons.OK, MessageBoxIcon.Information);

            ActualizarVistaCola();
        }

        private void ImprimirSiguiente()
        {
            if (colaImpresion.Count == 0)
            {
                MessageBox.Show("No hay documentos en la cola de impresión.", "Impresión",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (impresionEnCurso)
            {
                MessageBox.Show("Ya hay una impresión en curso. Espere a que termine.", "Impresión",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Desencolar el primer documento (FIFO)
            Documento documento = colaImpresion.Dequeue();
            impresionEnCurso = true;

            // Actualizar interfaz
            ActualizarDocumentoActual(documento);

            // Simular impresión con barra de progreso
            SimularImpresion(documento);
        }

        private void ImprimirTodo()
        {
            if (colaImpresion.Count == 0)
            {
                MessageBox.Show("No hay documentos en la cola de impresión.", "Impresión",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (impresionEnCurso)
            {
                MessageBox.Show("Ya hay una impresión en curso. Espere a que termine.", "Impresión",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Iniciar impresión de todos los documentos
            impresionEnCurso = true;
            ImprimirTodoSecuencial();
        }

        private async void ImprimirTodoSecuencial()
        {
            while (colaImpresion.Count > 0)
            {
                Documento documento = colaImpresion.Dequeue();
                ActualizarDocumentoActual(documento);
                await SimularImpresionAsync(documento);
            }

            impresionEnCurso = false;
            LimpiarDocumentoActual();
            MessageBox.Show("✅ Todos los documentos han sido impresos.", "Impresión Completa",
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SimularImpresion(Documento documento)
        {
            ProgressBar progressBar = this.Controls.Find("progressBar", true).FirstOrDefault() as ProgressBar;

            if (progressBar != null)
            {
                progressBar.Visible = true;
                progressBar.Value = 0;
                progressBar.Maximum = documento.Paginas * 10;

                Timer timer = new Timer();
                timer.Interval = 200;
                int pasos = 0;

                timer.Tick += (sender, e) =>
                {
                    pasos++;
                    progressBar.Value = pasos;

                    if (pasos >= progressBar.Maximum)
                    {
                        timer.Stop();
                        progressBar.Visible = false;
                        impresionEnCurso = false;
                        LimpiarDocumentoActual();

                        MessageBox.Show($"✅ Documento '{documento.Nombre}' impreso correctamente.\n" +
                                      $"Páginas: {documento.Paginas}", "Impresión Completada",
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ActualizarVistaCola();
                    }
                };

                timer.Start();
            }
        }

        private System.Threading.Tasks.Task SimularImpresionAsync(Documento documento)
        {
            var tcs = new System.Threading.Tasks.TaskCompletionSource<bool>();

            ProgressBar progressBar = this.Controls.Find("progressBar", true).FirstOrDefault() as ProgressBar;

            if (progressBar != null)
            {
                progressBar.Visible = true;
                progressBar.Value = 0;
                progressBar.Maximum = documento.Paginas * 10;

                Timer timer = new Timer();
                timer.Interval = 100;
                int pasos = 0;

                timer.Tick += (sender, e) =>
                {
                    pasos++;
                    progressBar.Value = pasos;

                    if (pasos >= progressBar.Maximum)
                    {
                        timer.Stop();
                        progressBar.Visible = false;
                        tcs.SetResult(true);
                    }
                };

                timer.Start();
            }
            else
            {
                tcs.SetResult(true);
            }

            return tcs.Task;
        }

        private void LimpiarCola()
        {
            if (colaImpresion.Count == 0)
            {
                MessageBox.Show("La cola de impresión ya está vacía.", "Información",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult resultado = MessageBox.Show(
                $"¿Está seguro de que desea limpiar toda la cola? Se eliminarán {colaImpresion.Count} documentos.",
                "Confirmar Limpieza",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                colaImpresion.Clear();
                numeroDocumento = 1;
                ActualizarVistaCola();
                MessageBox.Show("Cola de impresión limpiada completamente.", "Cola Limpiada",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ActualizarDocumentoActual(Documento documento)
        {
            Label lblDocumentoActual = this.Controls.Find("lblDocumentoActual", true).FirstOrDefault() as Label;
            if (lblDocumentoActual != null)
            {
                lblDocumentoActual.Text = $"🖨️ IMPRIMIENDO: {documento.Id} - {documento.Nombre} ({documento.Paginas} páginas)";
                lblDocumentoActual.BackColor = Color.DarkOrange;
            }
        }

        private void LimpiarDocumentoActual()
        {
            Label lblDocumentoActual = this.Controls.Find("lblDocumentoActual", true).FirstOrDefault() as Label;
            if (lblDocumentoActual != null)
            {
                lblDocumentoActual.Text = "Ningún documento en impresión";
                lblDocumentoActual.BackColor = Color.DarkBlue;
            }
        }

        private void ActualizarVistaCola()
        {
            ListBox lstCola = this.Controls.Find("lstCola", true).FirstOrDefault() as ListBox;
            Label lblInfo = this.Controls.Find("lblInfo", true).FirstOrDefault() as Label;

            if (lstCola != null)
            {
                lstCola.Items.Clear();

                if (colaImpresion.Count == 0)
                {
                    lstCola.Items.Add("La cola de impresión está vacía.");
                }
                else
                {
                    int posicion = 1;
                    foreach (Documento doc in colaImpresion)
                    {
                        lstCola.Items.Add($"{posicion}. {doc.Id} - {doc.Nombre} ({doc.Paginas} páginas)");
                        posicion++;
                    }
                }
            }

            if (lblInfo != null)
            {
                int totalPaginas = colaImpresion.Sum(doc => doc.Paginas);
                lblInfo.Text = $"Documentos en cola: {colaImpresion.Count} | Total páginas pendientes: {totalPaginas}";
            }
        }
    }

    public class Documento
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public int Paginas { get; set; }

        public Documento(string id, string nombre, int paginas)
        {
            Id = id;
            Nombre = nombre;
            Paginas = paginas;
        }
    }
}