using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace EstructurasDinamicasWinForms
{
    public partial class FormHistorial : Form
    {
        private Stack<string> historialAtras = new Stack<string>();
        private Stack<string> historialAdelante = new Stack<string>();
        private string paginaActual = "";

        public FormHistorial()
        {
            ConfigurarControles();
        }

        private void ConfigurarControles()
        {
            this.Text = "Historial de Navegación Web - Stack LIFO";
            this.Size = new Size(600, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            CrearControles();
        }

        private void CrearControles()
        {
            // Barra de Dirección
            Label lblDireccion = new Label();
            lblDireccion.Text = "Dirección URL:";
            lblDireccion.Location = new Point(20, 20);
            lblDireccion.Size = new Size(100, 20);
            this.Controls.Add(lblDireccion);

            TextBox txtUrl = new TextBox();
            txtUrl.Location = new Point(130, 20);
            txtUrl.Size = new Size(300, 20);
            txtUrl.Name = "txtUrl";
            txtUrl.Text = "https://www.";
            this.Controls.Add(txtUrl);

            // Botones de Navegación
            Button btnVisitar = new Button();
            btnVisitar.Text = "🌐 Visitar Página";
            btnVisitar.Location = new Point(440, 20);
            btnVisitar.Size = new Size(120, 25);
            btnVisitar.Click += (sender, e) =>
            {
                VisitarPagina(txtUrl.Text);
            };
            this.Controls.Add(btnVisitar);

            Button btnAtras = new Button();
            btnAtras.Text = "⬅ Atrás";
            btnAtras.Location = new Point(20, 60);
            btnAtras.Size = new Size(80, 30);
            btnAtras.Click += (sender, e) =>
            {
                NavegarAtras();
            };
            this.Controls.Add(btnAtras);

            Button btnAdelante = new Button();
            btnAdelante.Text = "Adelante ➡";
            btnAdelante.Location = new Point(110, 60);
            btnAdelante.Size = new Size(80, 30);
            btnAdelante.Click += (sender, e) =>
            {
                NavegarAdelante();
            };
            this.Controls.Add(btnAdelante);

            Button btnLimpiar = new Button();
            btnLimpiar.Text = "🧹 Limpiar";
            btnLimpiar.Location = new Point(200, 60);
            btnLimpiar.Size = new Size(80, 30);
            btnLimpiar.Click += (sender, e) =>
            {
                LimpiarHistorial();
            };
            this.Controls.Add(btnLimpiar);

            // Página Actual
            Label lblPaginaActual = new Label();
            lblPaginaActual.Text = "Página Actual:";
            lblPaginaActual.Location = new Point(20, 110);
            lblPaginaActual.Size = new Size(100, 20);
            lblPaginaActual.Font = new Font(lblPaginaActual.Font, FontStyle.Bold);
            this.Controls.Add(lblPaginaActual);

            Label lblPagina = new Label();
            lblPagina.Text = "Ninguna página visitada";
            lblPagina.Location = new Point(130, 110);
            lblPagina.Size = new Size(400, 30);
            lblPagina.Name = "lblPagina";
            lblPagina.ForeColor = Color.White;
            lblPagina.BackColor = Color.DarkBlue;
            lblPagina.BorderStyle = BorderStyle.FixedSingle;
            lblPagina.TextAlign = ContentAlignment.MiddleLeft;
            lblPagina.Font = new Font(lblPagina.Font, FontStyle.Bold);
            this.Controls.Add(lblPagina);

            // Historial Atrás
            Label lblHistorialAtras = new Label();
            lblHistorialAtras.Text = "Historial Atrás (Stack):";
            lblHistorialAtras.Location = new Point(20, 160);
            lblHistorialAtras.Size = new Size(200, 20);
            lblHistorialAtras.Font = new Font(lblHistorialAtras.Font, FontStyle.Bold);
            this.Controls.Add(lblHistorialAtras);

            ListBox lstAtras = new ListBox();
            lstAtras.Location = new Point(20, 180);
            lstAtras.Size = new Size(260, 120);
            lstAtras.Name = "lstAtras";
            this.Controls.Add(lstAtras);

            // Historial Adelante
            Label lblHistorialAdelante = new Label();
            lblHistorialAdelante.Text = "Historial Adelante (Stack):";
            lblHistorialAdelante.Location = new Point(300, 160);
            lblHistorialAdelante.Size = new Size(200, 20);
            lblHistorialAdelante.Font = new Font(lblHistorialAdelante.Font, FontStyle.Bold);
            this.Controls.Add(lblHistorialAdelante);

            ListBox lstAdelante = new ListBox();
            lstAdelante.Location = new Point(300, 180);
            lstAdelante.Size = new Size(260, 120);
            lstAdelante.Name = "lstAdelante";
            this.Controls.Add(lstAdelante);

            // Botones de Página Rápida
            Label lblRapido = new Label();
            lblRapido.Text = "Páginas Rápidas:";
            lblRapido.Location = new Point(20, 320);
            lblRapido.Size = new Size(100, 20);
            lblRapido.Font = new Font(lblRapido.Font, FontStyle.Bold);
            this.Controls.Add(lblRapido);

            string[] paginasRapidas = {
                "Google", "YouTube", "Facebook", "GitHub", "Wikipedia"
            };

            string[] urlsRapidas = {
                "https://www.google.com",
                "https://www.youtube.com",
                "https://www.facebook.com",
                "https://www.github.com",
                "https://www.wikipedia.org"
            };

            for (int i = 0; i < paginasRapidas.Length; i++)
            {
                Button btnRapido = new Button();
                btnRapido.Text = paginasRapidas[i];
                btnRapido.Location = new Point(130 + (i * 90), 320);
                btnRapido.Size = new Size(80, 25);
                int index = i;
                btnRapido.Click += (sender, e) =>
                {
                    VisitarPagina(urlsRapidas[index]);
                };
                this.Controls.Add(btnRapido);
            }

            // Información de Estado
            Label lblInfo = new Label();
            lblInfo.Text = "Páginas en historial: Atrás (0) | Adelante (0)";
            lblInfo.Location = new Point(20, 360);
            lblInfo.Size = new Size(540, 25);
            lblInfo.Name = "lblInfo";
            lblInfo.ForeColor = Color.DarkGreen;
            lblInfo.BackColor = Color.LightYellow;
            lblInfo.BorderStyle = BorderStyle.FixedSingle;
            lblInfo.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(lblInfo);

            // Botón ACTUALIZAR VISTA
            Button btnActualizar = new Button();
            btnActualizar.Text = "Actualizar Vista";
            btnActualizar.Location = new Point(20, 400);
            btnActualizar.Size = new Size(120, 30);
            btnActualizar.Click += (sender, e) =>
            {
                ActualizarVistaHistorial();
            };
            this.Controls.Add(btnActualizar);

            // Explicación LIFO
            Label lblExplicacion = new Label();
            lblExplicacion.Text = "💡 LIFO Stack: Al usar 'Atrás', se recupera la última página visitada";
            lblExplicacion.Location = new Point(150, 400);
            lblExplicacion.Size = new Size(410, 30);
            lblExplicacion.ForeColor = Color.DarkBlue;
            lblExplicacion.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(lblExplicacion);
        }

        private void VisitarPagina(string url)
        {
            if (string.IsNullOrWhiteSpace(url) || url == "https://www.")
            {
                MessageBox.Show("Por favor, ingrese una URL válida.", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Si hay una página actual, guardarla en el historial ATRÁS
            if (!string.IsNullOrEmpty(paginaActual))
            {
                historialAtras.Push(paginaActual);
            }

            // Limpiar historial ADELANTE (nueva navegación rompe el forward)
            historialAdelante.Clear();

            // Establecer nueva página actual
            paginaActual = url;

            MessageBox.Show($"Visitando: {url}", "Navegación",
                          MessageBoxButtons.OK, MessageBoxIcon.Information);

            ActualizarVistaHistorial();
        }

        private void NavegarAtras()
        {
            if (historialAtras.Count == 0)
            {
                MessageBox.Show("No hay páginas anteriores en el historial.", "Atrás",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Guardar página actual en historial ADELANTE
            if (!string.IsNullOrEmpty(paginaActual))
            {
                historialAdelante.Push(paginaActual);
            }

            // Recuperar página anterior del historial ATRÁS
            paginaActual = historialAtras.Pop();

            MessageBox.Show($"Volviendo a: {paginaActual}", "Atrás",
                          MessageBoxButtons.OK, MessageBoxIcon.Information);

            ActualizarVistaHistorial();
        }

        private void NavegarAdelante()
        {
            if (historialAdelante.Count == 0)
            {
                MessageBox.Show("No hay páginas siguientes en el historial.", "Adelante",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Guardar página actual en historial ATRÁS
            if (!string.IsNullOrEmpty(paginaActual))
            {
                historialAtras.Push(paginaActual);
            }

            // Recuperar página siguiente del historial ADELANTE
            paginaActual = historialAdelante.Pop();

            MessageBox.Show($"Avanzando a: {paginaActual}", "Adelante",
                          MessageBoxButtons.OK, MessageBoxIcon.Information);

            ActualizarVistaHistorial();
        }

        private void LimpiarHistorial()
        {
            DialogResult resultado = MessageBox.Show(
                "¿Está seguro de que desea limpiar todo el historial de navegación?",
                "Confirmar Limpieza",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                historialAtras.Clear();
                historialAdelante.Clear();
                paginaActual = "";
                ActualizarVistaHistorial();
                MessageBox.Show("Historial limpiado completamente.", "Historial Limpiado",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ActualizarVistaHistorial()
        {
            ListBox lstAtras = this.Controls.Find("lstAtras", true).FirstOrDefault() as ListBox;
            ListBox lstAdelante = this.Controls.Find("lstAdelante", true).FirstOrDefault() as ListBox;
            Label lblPagina = this.Controls.Find("lblPagina", true).FirstOrDefault() as Label;
            Label lblInfo = this.Controls.Find("lblInfo", true).FirstOrDefault() as Label;

            // Actualizar página actual
            if (lblPagina != null)
            {
                lblPagina.Text = string.IsNullOrEmpty(paginaActual) ?
                    "Ninguna página visitada" : paginaActual;
            }

            // Actualizar historial ATRÁS
            if (lstAtras != null)
            {
                lstAtras.Items.Clear();
                if (historialAtras.Count > 0)
                {
                    foreach (string pagina in historialAtras)
                    {
                        lstAtras.Items.Add(pagina);
                    }
                }
                else
                {
                    lstAtras.Items.Add("(Vacío)");
                }
            }

            // Actualizar historial ADELANTE
            if (lstAdelante != null)
            {
                lstAdelante.Items.Clear();
                if (historialAdelante.Count > 0)
                {
                    foreach (string pagina in historialAdelante)
                    {
                        lstAdelante.Items.Add(pagina);
                    }
                }
                else
                {
                    lstAdelante.Items.Add("(Vacío)");
                }
            }

            // Actualizar información
            if (lblInfo != null)
            {
                lblInfo.Text = $"Páginas en historial: Atrás ({historialAtras.Count}) | Adelante ({historialAdelante.Count})";
            }
        }
    }
}