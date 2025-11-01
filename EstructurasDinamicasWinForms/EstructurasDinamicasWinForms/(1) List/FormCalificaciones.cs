using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace EstructurasDinamicasWinForms
{
    public partial class FormCalificaciones : Form
    {
        private List<double> calificaciones = new List<double>();

        public FormCalificaciones()
        {
            ConfigurarControles();
        }

        private void ConfigurarControles()
        {
            this.Text = "Registro de Calificaciones de Alumnos";
            this.Size = new Size(500, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            CrearControles();
        }

        private void CrearControles()
        {
            // Calificación del Alumno
            Label lblCalificacion = new Label();
            lblCalificacion.Text = "Calificación (0-10):";
            lblCalificacion.Location = new Point(20, 20);
            lblCalificacion.Size = new Size(120, 20);
            this.Controls.Add(lblCalificacion);

            TextBox txtCalificacion = new TextBox();
            txtCalificacion.Location = new Point(150, 20);
            txtCalificacion.Size = new Size(100, 20);
            txtCalificacion.Name = "txtCalificacion";
            this.Controls.Add(txtCalificacion);

            // Botón AGREGAR CALIFICACIÓN
            Button btnAgregar = new Button();
            btnAgregar.Text = "Agregar Calificación";
            btnAgregar.Location = new Point(20, 60);
            btnAgregar.Size = new Size(140, 30);
            btnAgregar.Click += (sender, e) =>
            {
                AgregarCalificacion(txtCalificacion.Text);
                txtCalificacion.Clear();
                txtCalificacion.Focus();
            };
            this.Controls.Add(btnAgregar);

            // Botón CALCULAR PROMEDIO
            Button btnPromedio = new Button();
            btnPromedio.Text = "Calcular Promedio";
            btnPromedio.Location = new Point(170, 60);
            btnPromedio.Size = new Size(120, 30);
            btnPromedio.Click += (sender, e) =>
            {
                CalcularPromedio();
            };
            this.Controls.Add(btnPromedio);

            // Botón ELIMINAR MÁS BAJA
            Button btnEliminarBaja = new Button();
            btnEliminarBaja.Text = "Eliminar Más Baja";
            btnEliminarBaja.Location = new Point(300, 60);
            btnEliminarBaja.Size = new Size(120, 30);
            btnEliminarBaja.Click += (sender, e) =>
            {
                EliminarCalificacionMasBaja();
            };
            this.Controls.Add(btnEliminarBaja);

            // Botón ELIMINAR MÁS ALTA
            Button btnEliminarAlta = new Button();
            btnEliminarAlta.Text = "Eliminar Más Alta";
            btnEliminarAlta.Location = new Point(300, 90);
            btnEliminarAlta.Size = new Size(120, 30);
            btnEliminarAlta.Click += (sender, e) =>
            {
                EliminarCalificacionMasAlta();
            };
            this.Controls.Add(btnEliminarAlta);

            // ListBox de Calificaciones
            Label lblLista = new Label();
            lblLista.Text = "Calificaciones Registradas:";
            lblLista.Location = new Point(20, 110);
            lblLista.Size = new Size(150, 20);
            this.Controls.Add(lblLista);

            ListBox lstCalificaciones = new ListBox();
            lstCalificaciones.Location = new Point(20, 130);
            lstCalificaciones.Size = new Size(440, 180);
            lstCalificaciones.Name = "lstCalificaciones";
            this.Controls.Add(lstCalificaciones);

            // Panel de Estadísticas
            Label lblEstadisticas = new Label();
            lblEstadisticas.Text = "Estadísticas:";
            lblEstadisticas.Location = new Point(20, 320);
            lblEstadisticas.Size = new Size(100, 20);
            lblEstadisticas.Font = new Font(lblEstadisticas.Font, FontStyle.Bold);
            this.Controls.Add(lblEstadisticas);

            // Label para mostrar estadísticas
            Label lblInfo = new Label();
            lblInfo.Text = "Total: 0 calificaciones | Promedio: 0.00 | Mín: 0 | Máx: 0";
            lblInfo.Location = new Point(20, 350);
            lblInfo.Size = new Size(400, 40);
            lblInfo.Name = "lblInfo";
            lblInfo.ForeColor = Color.DarkBlue;
            lblInfo.BackColor = Color.LightYellow;
            lblInfo.BorderStyle = BorderStyle.FixedSingle;
            lblInfo.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(lblInfo);

            // Botón ACTUALIZAR LISTA
            Button btnActualizar = new Button();
            btnActualizar.Text = "Actualizar Lista";
            btnActualizar.Location = new Point(20, 400);
            btnActualizar.Size = new Size(120, 30);
            btnActualizar.Click += (sender, e) =>
            {
                ActualizarListaCalificaciones();
            };
            this.Controls.Add(btnActualizar);
        }

        private void AgregarCalificacion(string calificacionTexto)
        {
            if (!double.TryParse(calificacionTexto, out double calificacion))
            {
                MessageBox.Show("Por favor, ingrese una calificación válida (número).", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (calificacion < 0 || calificacion > 10)
            {
                MessageBox.Show("La calificación debe estar entre 0 y 10.", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Agregar calificación a la lista
            calificaciones.Add(calificacion);

            MessageBox.Show($"Calificación {calificacion} agregada correctamente.",
                          "Calificación Agregada", MessageBoxButtons.OK, MessageBoxIcon.Information);

            ActualizarListaCalificaciones();
        }

        private void CalcularPromedio()
        {
            if (calificaciones.Count == 0)
            {
                MessageBox.Show("No hay calificaciones registradas para calcular el promedio.",
                              "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            double promedio = calificaciones.Average();
            MessageBox.Show($"El promedio de las {calificaciones.Count} calificaciones es: {promedio:F2}",
                          "Promedio Calculado", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void EliminarCalificacionMasBaja()
        {
            if (calificaciones.Count == 0)
            {
                MessageBox.Show("No hay calificaciones registradas.", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            double minima = calificaciones.Min();
            calificaciones.Remove(minima);

            MessageBox.Show($"Calificación más baja ({minima}) eliminada correctamente.",
                          "Calificación Eliminada", MessageBoxButtons.OK, MessageBoxIcon.Information);

            ActualizarListaCalificaciones();
        }

        private void EliminarCalificacionMasAlta()
        {
            if (calificaciones.Count == 0)
            {
                MessageBox.Show("No hay calificaciones registradas.", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            double maxima = calificaciones.Max();
            calificaciones.Remove(maxima);

            MessageBox.Show($"Calificación más alta ({maxima}) eliminada correctamente.",
                          "Calificación Eliminada", MessageBoxButtons.OK, MessageBoxIcon.Information);

            ActualizarListaCalificaciones();
        }

        private void ActualizarListaCalificaciones()
        {
            ListBox lstCalificaciones = this.Controls.Find("lstCalificaciones", true).FirstOrDefault() as ListBox;
            Label lblInfo = this.Controls.Find("lblInfo", true).FirstOrDefault() as Label;

            if (lstCalificaciones != null)
            {
                lstCalificaciones.Items.Clear();

                if (calificaciones.Count == 0)
                {
                    lstCalificaciones.Items.Add("No hay calificaciones registradas.");
                }
                else
                {
                    for (int i = 0; i < calificaciones.Count; i++)
                    {
                        lstCalificaciones.Items.Add($"{i + 1}. {calificaciones[i]:F1}");
                    }
                }
            }

            if (lblInfo != null && calificaciones.Count > 0)
            {
                double promedio = calificaciones.Average();
                double minima = calificaciones.Min();
                double maxima = calificaciones.Max();

                lblInfo.Text = $"Total: {calificaciones.Count} calificaciones | " +
                             $"Promedio: {promedio:F2} | " +
                             $"Mín: {minima:F1} | " +
                             $"Máx: {maxima:F1}";
            }
            else if (lblInfo != null)
            {
                lblInfo.Text = "Total: 0 calificaciones | Promedio: 0.00 | Mín: 0 | Máx: 0";
            }
        }
    }
}