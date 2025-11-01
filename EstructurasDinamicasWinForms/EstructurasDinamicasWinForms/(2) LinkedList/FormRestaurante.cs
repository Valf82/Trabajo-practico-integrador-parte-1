using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace EstructurasDinamicasWinForms
{
    public partial class FormRestaurante : Form
    {
        private LinkedList<string> listaEspera = new LinkedList<string>();
        private int numeroCliente = 1;

        public FormRestaurante()
        {
            ConfigurarControles();
        }

        private void ConfigurarControles()
        {
            this.Text = "Gestión de Lista de Espera - Restaurante";
            this.Size = new Size(550, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            CrearControles();
        }

        private void CrearControles()
        {
            // Nombre del Cliente
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

            // Botón AGREGAR CLIENTE (al final)
            Button btnAgregar = new Button();
            btnAgregar.Text = "Agregar Cliente";
            btnAgregar.Location = new Point(20, 60);
            btnAgregar.Size = new Size(120, 30);
            btnAgregar.Click += (sender, e) =>
            {
                AgregarCliente(txtCliente.Text);
                txtCliente.Clear();
                txtCliente.Focus();
            };
            this.Controls.Add(btnAgregar);

            // Botón SENTAR CLIENTE (el primero)
            Button btnSentar = new Button();
            btnSentar.Text = "Sentar Cliente";
            btnSentar.Location = new Point(150, 60);
            btnSentar.Size = new Size(120, 30);
            btnSentar.Click += (sender, e) =>
            {
                SentarCliente();
            };
            this.Controls.Add(btnSentar);

            // Botón ELIMINAR CLIENTE
            Button btnEliminar = new Button();
            btnEliminar.Text = "Eliminar Cliente";
            btnEliminar.Location = new Point(280, 60);
            btnEliminar.Size = new Size(120, 30);
            btnEliminar.Click += (sender, e) =>
            {
                EliminarCliente(txtCliente.Text);
            };
            this.Controls.Add(btnEliminar);

            // ListBox de Lista de Espera
            Label lblLista = new Label();
            lblLista.Text = "Lista de Espera del Restaurante:";
            lblLista.Location = new Point(20, 110);
            lblLista.Size = new Size(200, 20);
            this.Controls.Add(lblLista);

            ListBox lstEspera = new ListBox();
            lstEspera.Location = new Point(20, 130);
            lstEspera.Size = new Size(500, 200);
            lstEspera.Name = "lstEspera";
            this.Controls.Add(lstEspera);

            // Panel de Información
            Label lblInfo = new Label();
            lblInfo.Text = "Clientes en espera: 0 | Próximo: Ninguno";
            lblInfo.Location = new Point(20, 340);
            lblInfo.Size = new Size(500, 30);
            lblInfo.Name = "lblInfo";
            lblInfo.ForeColor = Color.DarkGreen;
            lblInfo.BackColor = Color.LightCyan;
            lblInfo.BorderStyle = BorderStyle.FixedSingle;
            lblInfo.TextAlign = ContentAlignment.MiddleCenter;
            lblInfo.Font = new Font(lblInfo.Font, FontStyle.Bold);
            this.Controls.Add(lblInfo);

            // Botón ACTUALIZAR LISTA
            Button btnActualizar = new Button();
            btnActualizar.Text = "Actualizar Lista";
            btnActualizar.Location = new Point(20, 390);
            btnActualizar.Size = new Size(120, 30);
            btnActualizar.Click += (sender, e) =>
            {
                ActualizarListaEspera();
            };
            this.Controls.Add(btnActualizar);

            // Botón AGREGAR CLIENTE AUTOMÁTICO (para pruebas)
            Button btnAutoCliente = new Button();
            btnAutoCliente.Text = "Cliente Auto";
            btnAutoCliente.Location = new Point(360, 20);
            btnAutoCliente.Size = new Size(100, 20);
            btnAutoCliente.Click += (sender, e) =>
            {
                string clienteAuto = $"Cliente {numeroCliente}";
                AgregarCliente(clienteAuto);
                numeroCliente++;
                txtCliente.Text = clienteAuto;
            };
            this.Controls.Add(btnAutoCliente);
        }

        private void AgregarCliente(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("Por favor, ingrese un nombre para el cliente.", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Agregar cliente al FINAL de la lista enlazada
            listaEspera.AddLast(nombre);

            MessageBox.Show($"Cliente '{nombre}' agregado a la lista de espera.",
                          "Cliente Agregado", MessageBoxButtons.OK, MessageBoxIcon.Information);

            ActualizarListaEspera();
        }

        private void SentarCliente()
        {
            if (listaEspera.Count == 0)
            {
                MessageBox.Show("No hay clientes en la lista de espera.", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Obtener y eliminar el PRIMER cliente (el más antiguo)
            string clienteSentado = listaEspera.First.Value;
            listaEspera.RemoveFirst();

            MessageBox.Show($"Cliente '{clienteSentado}' ha sido sentado en la mesa.",
                          "Cliente Sentado", MessageBoxButtons.OK, MessageBoxIcon.Information);

            ActualizarListaEspera();
        }

        private void EliminarCliente(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("Por favor, ingrese un nombre para eliminar.", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Buscar el nodo que contiene el cliente
            LinkedListNode<string> nodoCliente = listaEspera.Find(nombre);

            if (nodoCliente != null)
            {
                listaEspera.Remove(nodoCliente);
                MessageBox.Show($"Cliente '{nombre}' eliminado de la lista de espera.",
                              "Cliente Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ActualizarListaEspera();
            }
            else
            {
                MessageBox.Show($"El cliente '{nombre}' no se encuentra en la lista de espera.",
                              "Cliente No Encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ActualizarListaEspera()
        {
            ListBox lstEspera = this.Controls.Find("lstEspera", true).FirstOrDefault() as ListBox;
            Label lblInfo = this.Controls.Find("lblInfo", true).FirstOrDefault() as Label;

            if (lstEspera != null)
            {
                lstEspera.Items.Clear();

                if (listaEspera.Count == 0)
                {
                    lstEspera.Items.Add("No hay clientes en la lista de espera.");
                }
                else
                {
                    int posicion = 1;
                    foreach (string cliente in listaEspera)
                    {
                        lstEspera.Items.Add($"{posicion}. {cliente}");
                        posicion++;
                    }
                }
            }

            if (lblInfo != null)
            {
                string proximoCliente = listaEspera.Count > 0 ? listaEspera.First.Value : "Ninguno";
                lblInfo.Text = $"Clientes en espera: {listaEspera.Count} | Próximo: {proximoCliente}";
            }
        }
    }
}