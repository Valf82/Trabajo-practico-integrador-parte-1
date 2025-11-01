using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace EstructurasDinamicasWinForms
{
    public partial class FormPilaPlatos : Form
    {
        private Stack<string> pilaPlatos = new Stack<string>();
        private int numeroPlato = 1;

        public FormPilaPlatos()
        {
            ConfigurarControles();
        }

        private void ConfigurarControles()
        {
            this.Text = "Simulador de Pila de Platos";
            this.Size = new Size(500, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            CrearControles();
        }

        private void CrearControles()
        {
            // Descripción del Plato
            Label lblPlato = new Label();
            lblPlato.Text = "Descripción del Plato:";
            lblPlato.Location = new Point(20, 20);
            lblPlato.Size = new Size(130, 20);
            this.Controls.Add(lblPlato);

            TextBox txtPlato = new TextBox();
            txtPlato.Location = new Point(160, 20);
            txtPlato.Size = new Size(200, 20);
            txtPlato.Name = "txtPlato";
            this.Controls.Add(txtPlato);

            // Botones de Operaciones Pila
            Button btnApilar = new Button();
            btnApilar.Text = "Apilar Plato (Push)";
            btnApilar.Location = new Point(20, 60);
            btnApilar.Size = new Size(120, 30);
            btnApilar.Click += (sender, e) =>
            {
                ApilarPlato(txtPlato.Text);
                txtPlato.Clear();
                txtPlato.Focus();
            };
            this.Controls.Add(btnApilar);

            Button btnDesapilar = new Button();
            btnDesapilar.Text = "Desapilar (Pop)";
            btnDesapilar.Location = new Point(150, 60);
            btnDesapilar.Size = new Size(100, 30);
            btnDesapilar.Click += (sender, e) =>
            {
                DesapilarPlato();
            };
            this.Controls.Add(btnDesapilar);

            Button btnVerCima = new Button();
            btnVerCima.Text = "Ver Cima (Peek)";
            btnVerCima.Location = new Point(260, 60);
            btnVerCima.Size = new Size(100, 30);
            btnVerCima.Click += (sender, e) =>
            {
                VerCimaPila();
            };
            this.Controls.Add(btnVerCima);

            // Representación Visual de la Pila
            Label lblPilaVisual = new Label();
            lblPilaVisual.Text = "Pila de Platos (Cima ↑):";
            lblPilaVisual.Location = new Point(20, 110);
            lblPilaVisual.Size = new Size(150, 20);
            lblPilaVisual.Font = new Font(lblPilaVisual.Font, FontStyle.Bold);
            this.Controls.Add(lblPilaVisual);

            ListBox lstPilaVisual = new ListBox();
            lstPilaVisual.Location = new Point(20, 130);
            lstPilaVisual.Size = new Size(450, 200);
            lstPilaVisual.Name = "lstPilaVisual";
            this.Controls.Add(lstPilaVisual);

            // Panel de Información
            Label lblInfo = new Label();
            lblInfo.Text = "Platos en pila: 0 | Cima: Vacía";
            lblInfo.Location = new Point(20, 340);
            lblInfo.Size = new Size(450, 30);
            lblInfo.Name = "lblInfo";
            lblInfo.ForeColor = Color.DarkRed;
            lblInfo.BackColor = Color.LightYellow;
            lblInfo.BorderStyle = BorderStyle.FixedSingle;
            lblInfo.TextAlign = ContentAlignment.MiddleCenter;
            lblInfo.Font = new Font(lblInfo.Font, FontStyle.Bold);
            this.Controls.Add(lblInfo);

            // Botón ACTUALIZAR VISTA
            Button btnActualizar = new Button();
            btnActualizar.Text = "Actualizar Vista";
            btnActualizar.Location = new Point(20, 380);
            btnActualizar.Size = new Size(120, 30);
            btnActualizar.Click += (sender, e) =>
            {
                ActualizarVistaPila();
            };
            this.Controls.Add(btnActualizar);

            // Botón PLATO AUTOMÁTICO
            Button btnAutoPlato = new Button();
            btnAutoPlato.Text = "Plato Auto";
            btnAutoPlato.Location = new Point(370, 20);
            btnAutoPlato.Size = new Size(100, 20);
            btnAutoPlato.Click += (sender, e) =>
            {
                string platoAuto = $"Plato {numeroPlato}";
                ApilarPlato(platoAuto);
                numeroPlato++;
                txtPlato.Text = platoAuto;
            };
            this.Controls.Add(btnAutoPlato);

            // Botón LIMPIAR PILA
            Button btnLimpiar = new Button();
            btnLimpiar.Text = "Limpiar Pila";
            btnLimpiar.Location = new Point(150, 380);
            btnLimpiar.Size = new Size(120, 30);
            btnLimpiar.Click += (sender, e) =>
            {
                LimpiarPila();
            };
            this.Controls.Add(btnLimpiar);

            // Información LIFO
            Label lblLifoInfo = new Label();
            lblLifoInfo.Text = "💡 LIFO (Last-In, First-Out): El último plato apilado es el primero en salir";
            lblLifoInfo.Location = new Point(20, 420);
            lblLifoInfo.Size = new Size(450, 40);
            lblLifoInfo.ForeColor = Color.DarkBlue;
            lblLifoInfo.BackColor = Color.LightCyan;
            lblLifoInfo.BorderStyle = BorderStyle.FixedSingle;
            lblLifoInfo.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(lblLifoInfo);
        }

        private void ApilarPlato(string descripcion)
        {
            if (string.IsNullOrWhiteSpace(descripcion))
            {
                MessageBox.Show("Por favor, ingrese una descripción para el plato.", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // APILAR (Push) - Agregar a la cima de la pila
            pilaPlatos.Push(descripcion);

            MessageBox.Show($"Plato '{descripcion}' apilado correctamente.",
                          "Plato Apilado", MessageBoxButtons.OK, MessageBoxIcon.Information);

            ActualizarVistaPila();
        }

        private void DesapilarPlato()
        {
            if (pilaPlatos.Count == 0)
            {
                MessageBox.Show("La pila de platos está vacía. No hay platos para desapilar.", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // DESAPILAR (Pop) - Quitar y obtener el plato de la cima
            string platoDesapilado = pilaPlatos.Pop();

            MessageBox.Show($"Plato '{platoDesapilado}' desapilado correctamente.",
                          "Plato Desapilado", MessageBoxButtons.OK, MessageBoxIcon.Information);

            ActualizarVistaPila();
        }

        private void VerCimaPila()
        {
            if (pilaPlatos.Count == 0)
            {
                MessageBox.Show("La pila de platos está vacía. No hay plato en la cima.", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // PEEK - Ver el plato en la cima sin quitarlo
            string platoCima = pilaPlatos.Peek();

            MessageBox.Show($"Plato en la cima: '{platoCima}'",
                          "Cima de la Pila", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void LimpiarPila()
        {
            if (pilaPlatos.Count == 0)
            {
                MessageBox.Show("La pila ya está vacía.", "Información",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult resultado = MessageBox.Show(
                $"¿Está seguro de que desea limpiar toda la pila? Se eliminarán {pilaPlatos.Count} platos.",
                "Confirmar Limpieza",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                pilaPlatos.Clear();
                numeroPlato = 1;
                ActualizarVistaPila();
                MessageBox.Show("Pila limpiada completamente.", "Pila Limpiada",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ActualizarVistaPila()
        {
            ListBox lstPilaVisual = this.Controls.Find("lstPilaVisual", true).FirstOrDefault() as ListBox;
            Label lblInfo = this.Controls.Find("lblInfo", true).FirstOrDefault() as Label;

            if (lstPilaVisual != null)
            {
                lstPilaVisual.Items.Clear();

                if (pilaPlatos.Count == 0)
                {
                    lstPilaVisual.Items.Add("🏁 La pila está vacía");
                }
                else
                {
                    // Mostrar la pila con la cima arriba
                    string[] platosArray = pilaPlatos.ToArray();

                    for (int i = 0; i < platosArray.Length; i++)
                    {
                        string indicador = (i == 0) ? "⬆ CIMA" : "";
                        lstPilaVisual.Items.Add($"{platosArray[i]} {indicador}");
                    }
                }
            }

            if (lblInfo != null)
            {
                string cima = pilaPlatos.Count > 0 ? pilaPlatos.Peek() : "Vacía";
                lblInfo.Text = $"Platos en pila: {pilaPlatos.Count} | Cima: {cima}";
            }
        }
    }
}