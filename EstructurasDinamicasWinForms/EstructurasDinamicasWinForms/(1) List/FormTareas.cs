using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace EstructurasDinamicasWinForms
{
    public partial class FormTareas : Form
    {
        private List<Tarea> listaTareas = new List<Tarea>();
        private int indiceSeleccionado = -1;

        public FormTareas()
        {
            ConfigurarControles();
        }

        private void ConfigurarControles()
        {
            this.Text = "Lista de Tareas Pendientes";
            this.Size = new Size(500, 400);
            this.StartPosition = FormStartPosition.CenterScreen;
            CrearControles();
        }

        private void CrearControles()
        {
            // Descripción de la Tarea
            Label lblDescripcion = new Label();
            lblDescripcion.Text = "Descripción de la Tarea:";
            lblDescripcion.Location = new Point(20, 20);
            lblDescripcion.Size = new Size(150, 20);
            this.Controls.Add(lblDescripcion);

            TextBox txtDescripcion = new TextBox();
            txtDescripcion.Location = new Point(180, 20);
            txtDescripcion.Size = new Size(280, 20);
            txtDescripcion.Name = "txtDescripcion";
            this.Controls.Add(txtDescripcion);

            // Botón AGREGAR TAREA
            Button btnAgregar = new Button();
            btnAgregar.Text = "Agregar Tarea";
            btnAgregar.Location = new Point(20, 60);
            btnAgregar.Size = new Size(120, 30);
            btnAgregar.Click += (sender, e) =>
            {
                AgregarTarea(txtDescripcion.Text);
                txtDescripcion.Clear();
                txtDescripcion.Focus();
            };
            this.Controls.Add(btnAgregar);

            // Botón MARCAR COMPLETADA
            Button btnCompletar = new Button();
            btnCompletar.Text = "Marcar Completada";
            btnCompletar.Location = new Point(150, 60);
            btnCompletar.Size = new Size(140, 30);
            btnCompletar.Click += (sender, e) =>
            {
                CompletarTarea();
            };
            this.Controls.Add(btnCompletar);

            // ListBox de Tareas
            Label lblLista = new Label();
            lblLista.Text = "Tareas Pendientes:";
            lblLista.Location = new Point(20, 110);
            lblLista.Size = new Size(150, 20);
            this.Controls.Add(lblLista);

            ListBox lstTareas = new ListBox();
            lstTareas.Location = new Point(20, 130);
            lstTareas.Size = new Size(440, 180);
            lstTareas.Name = "lstTareas";
            lstTareas.SelectedIndexChanged += (sender, e) =>
            {
                indiceSeleccionado = lstTareas.SelectedIndex;
            };
            this.Controls.Add(lstTareas);

            // Botón ACTUALIZAR LISTA
            Button btnActualizar = new Button();
            btnActualizar.Text = "Actualizar Lista";
            btnActualizar.Location = new Point(20, 320);
            btnActualizar.Size = new Size(120, 30);
            btnActualizar.Click += (sender, e) =>
            {
                ActualizarListaTareas();
            };
            this.Controls.Add(btnActualizar);

            // Label contador de tareas
            Label lblContador = new Label();
            lblContador.Text = "Total de tareas: 0";
            lblContador.Location = new Point(300, 60);
            lblContador.Size = new Size(150, 20);
            lblContador.Name = "lblContador";
            lblContador.ForeColor = Color.DarkBlue;
            lblContador.Font = new Font(lblContador.Font, FontStyle.Bold);
            this.Controls.Add(lblContador);
        }

        private void AgregarTarea(string descripcion)
        {
            if (string.IsNullOrWhiteSpace(descripcion))
            {
                MessageBox.Show("Por favor, ingrese una descripción para la tarea.", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Agregar nueva tarea a la lista
            listaTareas.Add(new Tarea(descripcion));

            MessageBox.Show($"Tarea '{descripcion}' agregada correctamente.",
                          "Tarea Agregada", MessageBoxButtons.OK, MessageBoxIcon.Information);

            ActualizarListaTareas();
        }

        private void CompletarTarea()
        {
            if (indiceSeleccionado == -1)
            {
                MessageBox.Show("Por favor, seleccione una tarea de la lista para marcar como completada.",
                              "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (indiceSeleccionado >= 0 && indiceSeleccionado < listaTareas.Count)
            {
                Tarea tareaCompletada = listaTareas[indiceSeleccionado];

                DialogResult resultado = MessageBox.Show(
                    $"¿Está seguro de que desea marcar como completada la tarea:\n\"{tareaCompletada.Descripcion}\"?",
                    "Confirmar Completado",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    listaTareas.RemoveAt(indiceSeleccionado);
                    MessageBox.Show("Tarea marcada como completada y eliminada de la lista.",
                                  "Tarea Completada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    indiceSeleccionado = -1;
                    ActualizarListaTareas();
                }
            }
        }

        private void ActualizarListaTareas()
        {
            ListBox lstTareas = this.Controls.Find("lstTareas", true).FirstOrDefault() as ListBox;
            Label lblContador = this.Controls.Find("lblContador", true).FirstOrDefault() as Label;

            if (lstTareas != null)
            {
                lstTareas.Items.Clear();

                if (listaTareas.Count == 0)
                {
                    lstTareas.Items.Add("No hay tareas pendientes. ¡Agrega algunas!");
                }
                else
                {
                    for (int i = 0; i < listaTareas.Count; i++)
                    {
                        lstTareas.Items.Add($"{i + 1}. {listaTareas[i].Descripcion}");
                    }
                }
            }

            if (lblContador != null)
            {
                lblContador.Text = $"Total de tareas: {listaTareas.Count}";
            }
        }
    }

    public class Tarea
    {
        public string Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }

        public Tarea(string descripcion)
        {
            Descripcion = descripcion;
            FechaCreacion = DateTime.Now;
        }
    }
}