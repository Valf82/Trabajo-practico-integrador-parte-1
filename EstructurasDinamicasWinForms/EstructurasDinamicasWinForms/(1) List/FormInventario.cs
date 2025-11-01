using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace EstructurasDinamicasWinForms
{
    public partial class FormInventario : Form
    {
        private List<Producto> inventario = new List<Producto>();

        public FormInventario()
        {
            ConfigurarControles();
        }

        private void ConfigurarControles()
        {
            this.Text = "Gestión de Inventario de Tienda";
            this.Size = new Size(550, 400); // Revisar regularmente esta mierda porque siempre me quedo corto de espacio
            this.StartPosition = FormStartPosition.CenterScreen;
            CrearControles();
        }

        private void CrearControles()
        {
            // Nombre del Producto
            Label lblNombre = new Label();
            lblNombre.Text = "Nombre del Producto:";
            lblNombre.Location = new Point(20, 20);
            lblNombre.Size = new Size(120, 20);
            this.Controls.Add(lblNombre);

            TextBox txtNombre = new TextBox();
            txtNombre.Location = new Point(150, 20);
            txtNombre.Size = new Size(200, 20);
            txtNombre.Name = "txtNombre";
            this.Controls.Add(txtNombre);

            // Cantidad del Producto
            Label lblCantidad = new Label();
            lblCantidad.Text = "Cantidad:";
            lblCantidad.Location = new Point(20, 50);
            lblCantidad.Size = new Size(120, 20);
            this.Controls.Add(lblCantidad);

            TextBox txtCantidad = new TextBox();
            txtCantidad.Location = new Point(150, 50);
            txtCantidad.Size = new Size(100, 20);
            txtCantidad.Name = "txtCantidad";
            this.Controls.Add(txtCantidad);

            // Botón AGREGAR
            Button btnAgregar = new Button();
            btnAgregar.Text = "Agregar Producto";
            btnAgregar.Location = new Point(20, 90);
            btnAgregar.Size = new Size(120, 30);
            btnAgregar.Click += (sender, e) =>
            {
                AgregarProducto(txtNombre.Text, txtCantidad.Text);
                txtNombre.Clear();
                txtCantidad.Clear();
                txtNombre.Focus();
            };
            this.Controls.Add(btnAgregar);

            // Botón BUSCAR
            Button btnBuscar = new Button();
            btnBuscar.Text = "Buscar Producto";
            btnBuscar.Location = new Point(150, 90);
            btnBuscar.Size = new Size(120, 30);
            btnBuscar.Click += (sender, e) =>
            {
                BuscarProducto(txtNombre.Text);
            };
            this.Controls.Add(btnBuscar);

            // Botón REDUCIR
            Button btnReducir = new Button();
            btnReducir.Text = "Reducir";
            btnReducir.Location = new Point(280, 90);
            btnReducir.Size = new Size(80, 30);
            btnReducir.Click += (sender, e) =>
            {
                ReducirCantidadProducto(txtNombre.Text, txtCantidad.Text);
                txtNombre.Clear();
                txtCantidad.Clear();
                txtNombre.Focus();
            };
            this.Controls.Add(btnReducir);

            // Botón ELIMINAR
            Button btnEliminarCompleto = new Button();
            btnEliminarCompleto.Text = "Eliminar";
            btnEliminarCompleto.Location = new Point(370, 90);
            btnEliminarCompleto.Size = new Size(80, 30);
            btnEliminarCompleto.Click += (sender, e) =>
            {
                EliminarProductoCompleto(txtNombre.Text);
            };
            this.Controls.Add(btnEliminarCompleto);

            // Listbox Productos
            Label lblLista = new Label();
            lblLista.Text = "Inventario Completo:";
            lblLista.Location = new Point(20, 140);
            lblLista.Size = new Size(150, 20);
            this.Controls.Add(lblLista);

            ListBox lstInventario = new ListBox();
            lstInventario.Location = new Point(20, 160);
            lstInventario.Size = new Size(500, 150);
            lstInventario.Name = "lstInventario";
            this.Controls.Add(lstInventario);

            // Botón ACTUALIZAR LISTA
            Button btnActualizarLista = new Button();
            btnActualizarLista.Text = "Actualizar Lista";
            btnActualizarLista.Location = new Point(20, 320);
            btnActualizarLista.Size = new Size(120, 30);
            btnActualizarLista.Click += (sender, e) =>
            {
                ActualizarListaInventario();
            };
            this.Controls.Add(btnActualizarLista);
        }

        private void AgregarProducto(string nombre, string cantidadTexto)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("Por favor, ingrese un nombre válido para el producto.", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(cantidadTexto, out int cantidad) || cantidad < 0)
            {
                MessageBox.Show("Por favor, ingrese una cantidad válida (número entero positivo).", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Producto productoExistente = inventario.Find(p => p.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));

            if (productoExistente != null)
            {
                productoExistente.Cantidad += cantidad;
                MessageBox.Show($"Producto '{nombre}' actualizado. Nueva cantidad: {productoExistente.Cantidad}",
                              "Producto Actualizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                inventario.Add(new Producto(nombre, cantidad));
                MessageBox.Show($"Producto '{nombre}' agregado al inventario con cantidad: {cantidad}",
                              "Producto Agregado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            ActualizarListaInventario();
        }

        private void BuscarProducto(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("Por favor, ingrese un nombre para buscar.", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Producto producto = inventario.Find(p => p.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));

            if (producto != null)
            {
                MessageBox.Show($"Producto encontrado:\nNombre: {producto.Nombre}\nCantidad: {producto.Cantidad}",
                              "Producto Encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show($"El producto '{nombre}' no se encuentra en el inventario.",
                              "Producto No Encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ReducirCantidadProducto(string nombre, string cantidadTexto)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("Por favor, ingrese un nombre válido para el producto.", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(cantidadTexto, out int cantidad) || cantidad < 0)
            {
                MessageBox.Show("Por favor, ingrese una cantidad válida (número entero positivo).", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Producto productoExistente = inventario.Find(p => p.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));

            if (productoExistente != null)
            {
                if (cantidad > productoExistente.Cantidad)
                {
                    MessageBox.Show($"No puede reducir {cantidad} unidades. Solo hay {productoExistente.Cantidad} en inventario.",
                                  "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                productoExistente.Cantidad -= cantidad;

                if (productoExistente.Cantidad == 0)
                {
                    inventario.Remove(productoExistente);
                    MessageBox.Show($"Producto '{nombre}' eliminado del inventario (cantidad llegó a cero).",
                                  "Producto Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"Se redujeron {cantidad} unidades de '{nombre}'. Cantidad restante: {productoExistente.Cantidad}",
                                  "Cantidad Reducida", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show($"El producto '{nombre}' no se encuentra en el inventario.",
                              "Producto No Encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            ActualizarListaInventario();
        }

        private void EliminarProductoCompleto(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("Por favor, ingrese un nombre para eliminar.", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Producto producto = inventario.Find(p => p.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));

            if (producto != null)
            {
                DialogResult resultado = MessageBox.Show(
                    $"¿Está seguro de que desea eliminar completamente el producto '{producto.Nombre}'?",
                    "Confirmar Eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    inventario.Remove(producto);
                    MessageBox.Show($"Producto '{nombre}' eliminado completamente del inventario.",
                                  "Producto Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ActualizarListaInventario();
                }
            }
            else
            {
                MessageBox.Show($"El producto '{nombre}' no se encuentra en el inventario.",
                              "Producto No Encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ActualizarListaInventario()
        {
            ListBox lstInventario = this.Controls.Find("lstInventario", true).FirstOrDefault() as ListBox;

            if (lstInventario != null)
            {
                lstInventario.Items.Clear();

                if (inventario.Count == 0)
                {
                    lstInventario.Items.Add("El inventario está vacío.");
                }
                else
                {
                    foreach (Producto producto in inventario)
                    {
                        lstInventario.Items.Add($"{producto.Nombre} - Cantidad: {producto.Cantidad}");
                    }
                }
            }
        }
    }

    public class Producto
    {
        public string Nombre { get; set; }
        public int Cantidad { get; set; }

        public Producto(string nombre, int cantidad)
        {
            Nombre = nombre;
            Cantidad = cantidad;
        }
    }
}