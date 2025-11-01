using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace EstructurasDinamicasWinForms
{
    public partial class FormDirectorio : Form
    {
        private Dictionary<string, string> directorio = new Dictionary<string, string>();

        public FormDirectorio()
        {
            ConfigurarControles();
        }

        private void ConfigurarControles()
        {
            this.Text = "Directorio Telefónico - Dictionary";
            this.Size = new Size(500, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            CrearControles();
        }

        private void CrearControles()
        {
            // Información del Contacto
            Label lblNombre = new Label();
            lblNombre.Text = "Nombre del Contacto:";
            lblNombre.Location = new Point(20, 20);
            lblNombre.Size = new Size(120, 20);
            this.Controls.Add(lblNombre);

            TextBox txtNombre = new TextBox();
            txtNombre.Location = new Point(150, 20);
            txtNombre.Size = new Size(200, 20);
            txtNombre.Name = "txtNombre";
            this.Controls.Add(txtNombre);

            Label lblTelefono = new Label();
            lblTelefono.Text = "Número de Teléfono:";
            lblTelefono.Location = new Point(20, 50);
            lblTelefono.Size = new Size(120, 20);
            this.Controls.Add(lblTelefono);

            TextBox txtTelefono = new TextBox();
            txtTelefono.Location = new Point(150, 50);
            txtTelefono.Size = new Size(200, 20);
            txtTelefono.Name = "txtTelefono";
            this.Controls.Add(txtTelefono);

            // Botones de Gestión
            Button btnAgregar = new Button();
            btnAgregar.Text = "➕ Agregar Contacto";
            btnAgregar.Location = new Point(20, 80);
            btnAgregar.Size = new Size(120, 30);
            btnAgregar.Click += (sender, e) =>
            {
                AgregarContacto(txtNombre.Text, txtTelefono.Text);
                txtNombre.Clear();
                txtTelefono.Clear();
                txtNombre.Focus();
            };
            this.Controls.Add(btnAgregar);

            Button btnBuscar = new Button();
            btnBuscar.Text = "🔍 Buscar Contacto";
            btnBuscar.Location = new Point(150, 80);
            btnBuscar.Size = new Size(120, 30);
            btnBuscar.Click += (sender, e) =>
            {
                BuscarContacto(txtNombre.Text);
            };
            this.Controls.Add(btnBuscar);

            Button btnEliminar = new Button();
            btnEliminar.Text = "❌ Eliminar Contacto";
            btnEliminar.Location = new Point(280, 80);
            btnEliminar.Size = new Size(120, 30);
            btnEliminar.Click += (sender, e) =>
            {
                EliminarContacto(txtNombre.Text);
            };
            this.Controls.Add(btnEliminar);

            // Lista de Contactos
            Label lblContactos = new Label();
            lblContactos.Text = "Directorio Telefónico:";
            lblContactos.Location = new Point(20, 130);
            lblContactos.Size = new Size(150, 20);
            lblContactos.Font = new Font(lblContactos.Font, FontStyle.Bold);
            this.Controls.Add(lblContactos);

            ListBox lstContactos = new ListBox();
            lstContactos.Location = new Point(20, 150);
            lstContactos.Size = new Size(440, 200);
            lstContactos.Name = "lstContactos";
            this.Controls.Add(lstContactos);

            // Panel de Información
            Label lblInfo = new Label();
            lblInfo.Text = "Total de contactos: 0";
            lblInfo.Location = new Point(20, 360);
            lblInfo.Size = new Size(440, 30);
            lblInfo.Name = "lblInfo";
            lblInfo.ForeColor = Color.DarkBlue;
            lblInfo.BackColor = Color.LightCyan;
            lblInfo.BorderStyle = BorderStyle.FixedSingle;
            lblInfo.TextAlign = ContentAlignment.MiddleCenter;
            lblInfo.Font = new Font(lblInfo.Font, FontStyle.Bold);
            this.Controls.Add(lblInfo);

            // Botón ACTUALIZAR LISTA
            Button btnActualizar = new Button();
            btnActualizar.Text = "🔄 Actualizar Lista";
            btnActualizar.Location = new Point(20, 400);
            btnActualizar.Size = new Size(120, 30);
            btnActualizar.Click += (sender, e) =>
            {
                ActualizarListaContactos();
            };
            this.Controls.Add(btnActualizar);

            // Botón LIMPIAR DIRECTORIO
            Button btnLimpiar = new Button();
            btnLimpiar.Text = "🧹 Limpiar Todo";
            btnLimpiar.Location = new Point(150, 400);
            btnLimpiar.Size = new Size(120, 30);
            btnLimpiar.Click += (sender, e) =>
            {
                LimpiarDirectorio();
            };
            this.Controls.Add(btnLimpiar);

            // Botón EJEMPLOS
            Button btnEjemplos = new Button();
            btnEjemplos.Text = "🎲 Contactos Demo";
            btnEjemplos.Location = new Point(280, 400);
            btnEjemplos.Size = new Size(120, 30);
            btnEjemplos.Click += (sender, e) =>
            {
                AgregarContactosDemo();
            };
            this.Controls.Add(btnEjemplos);

            // Explicación Dictionary
            Label lblExplicacion = new Label();
            lblExplicacion.Text = "💡 Dictionary: Acceso rápido por clave (nombre) → valor (teléfono)";
            lblExplicacion.Location = new Point(20, 440);
            lblExplicacion.Size = new Size(440, 30);
            lblExplicacion.ForeColor = Color.DarkGreen;
            lblExplicacion.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(lblExplicacion);
        }

        private void AgregarContacto(string nombre, string telefono)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("Por favor, ingrese un nombre para el contacto.", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(telefono))
            {
                MessageBox.Show("Por favor, ingrese un número de teléfono.", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Verificar si el contacto ya existe
            if (directorio.ContainsKey(nombre))
            {
                DialogResult resultado = MessageBox.Show(
                    $"El contacto '{nombre}' ya existe. ¿Desea actualizar su número de teléfono?",
                    "Contacto Existente",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    directorio[nombre] = telefono;
                    MessageBox.Show($"Contacto '{nombre}' actualizado correctamente.",
                                  "Contacto Actualizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                // Agregar nuevo contacto al Dictionary
                directorio.Add(nombre, telefono);
                MessageBox.Show($"Contacto '{nombre}' agregado correctamente.",
                              "Contacto Agregado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            ActualizarListaContactos();
        }

        private void BuscarContacto(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("Por favor, ingrese un nombre para buscar.", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Buscar en el Dictionary por clave (nombre)
            if (directorio.ContainsKey(nombre))
            {
                string telefono = directorio[nombre];
                MessageBox.Show($"📞 Contacto encontrado:\n\nNombre: {nombre}\nTeléfono: {telefono}",
                              "Contacto Encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show($"El contacto '{nombre}' no se encuentra en el directorio.",
                              "Contacto No Encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void EliminarContacto(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("Por favor, ingrese un nombre para eliminar.", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (directorio.ContainsKey(nombre))
            {
                DialogResult resultado = MessageBox.Show(
                    $"¿Está seguro de que desea eliminar el contacto '{nombre}'?",
                    "Confirmar Eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    directorio.Remove(nombre);
                    MessageBox.Show($"Contacto '{nombre}' eliminado correctamente.",
                                  "Contacto Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ActualizarListaContactos();
                }
            }
            else
            {
                MessageBox.Show($"El contacto '{nombre}' no se encuentra en el directorio.",
                              "Contacto No Encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LimpiarDirectorio()
        {
            if (directorio.Count == 0)
            {
                MessageBox.Show("El directorio ya está vacío.", "Información",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult resultado = MessageBox.Show(
                $"¿Está seguro de que desea limpiar todo el directorio? Se eliminarán {directorio.Count} contactos.",
                "Confirmar Limpieza",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                directorio.Clear();
                ActualizarListaContactos();
                MessageBox.Show("Directorio limpiado completamente.", "Directorio Limpiado",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void AgregarContactosDemo()
        {
            // Agregar algunos contactos de ejemplo
            Dictionary<string, string> contactosDemo = new Dictionary<string, string>
            {
                { "Juan Pérez", "11-1234-5678" },
                { "María García", "11-2345-6789" },
                { "Carlos López", "11-3456-7890" },
                { "Ana Martínez", "11-4567-8901" },
                { "Pedro Rodríguez", "11-5678-9012" }
            };

            foreach (var contacto in contactosDemo)
            {
                if (!directorio.ContainsKey(contacto.Key))
                {
                    directorio.Add(contacto.Key, contacto.Value);
                }
            }

            MessageBox.Show($"Se agregaron {contactosDemo.Count} contactos de ejemplo.",
                          "Contactos Demo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ActualizarListaContactos();
        }

        private void ActualizarListaContactos()
        {
            ListBox lstContactos = this.Controls.Find("lstContactos", true).FirstOrDefault() as ListBox;
            Label lblInfo = this.Controls.Find("lblInfo", true).FirstOrDefault() as Label;

            if (lstContactos != null)
            {
                lstContactos.Items.Clear();

                if (directorio.Count == 0)
                {
                    lstContactos.Items.Add("El directorio está vacío. Agrega algunos contactos!");
                }
                else
                {
                    // Ordenar alfabéticamente por nombre
                    var contactosOrdenados = directorio.OrderBy(c => c.Key);

                    foreach (var contacto in contactosOrdenados)
                    {
                        lstContactos.Items.Add($"👤 {contacto.Key} 📞 {contacto.Value}");
                    }
                }
            }

            if (lblInfo != null)
            {
                lblInfo.Text = $"Total de contactos: {directorio.Count}";
            }
        }
    }
}