using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace EstructurasDinamicasWinForms
{
    public partial class FormCatalogo : Form
    {
        private Dictionary<string, ProductoCatalogo> catalogo = new Dictionary<string, ProductoCatalogo>();

        public FormCatalogo()
        {
            ConfigurarControles();
        }

        private void ConfigurarControles()
        {
            this.Text = "Catálogo de Productos - Dictionary";
            this.Size = new Size(600, 550);
            this.StartPosition = FormStartPosition.CenterScreen;
            CrearControles();
        }

        private void CrearControles()
        {
            // Información del Producto
            Label lblCodigo = new Label();
            lblCodigo.Text = "Código SKU:";
            lblCodigo.Location = new Point(20, 20);
            lblCodigo.Size = new Size(80, 20);
            this.Controls.Add(lblCodigo);

            TextBox txtCodigo = new TextBox();
            txtCodigo.Location = new Point(110, 20);
            txtCodigo.Size = new Size(150, 20);
            txtCodigo.Name = "txtCodigo";
            this.Controls.Add(txtCodigo);

            Label lblNombre = new Label();
            lblNombre.Text = "Nombre del Producto:";
            lblNombre.Location = new Point(20, 50);
            lblNombre.Size = new Size(120, 20);
            this.Controls.Add(lblNombre);

            TextBox txtNombre = new TextBox();
            txtNombre.Location = new Point(150, 50);
            txtNombre.Size = new Size(300, 20);
            txtNombre.Name = "txtNombre";
            this.Controls.Add(txtNombre);

            Label lblPrecio = new Label();
            lblPrecio.Text = "Precio:";
            lblPrecio.Location = new Point(20, 80);
            lblPrecio.Size = new Size(80, 20);
            this.Controls.Add(lblPrecio);

            TextBox txtPrecio = new TextBox();
            txtPrecio.Location = new Point(110, 80);
            txtPrecio.Size = new Size(100, 20);
            txtPrecio.Name = "txtPrecio";
            this.Controls.Add(txtPrecio);

            Label lblCategoria = new Label();
            lblCategoria.Text = "Categoría:";
            lblCategoria.Location = new Point(20, 110);
            lblCategoria.Size = new Size(80, 20);
            this.Controls.Add(lblCategoria);

            ComboBox cmbCategoria = new ComboBox();
            cmbCategoria.Location = new Point(110, 110);
            cmbCategoria.Size = new Size(150, 20);
            cmbCategoria.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategoria.Items.AddRange(new string[] {
                "Electrónicos",
                "Ropa",
                "Hogar",
                "Deportes",
                "Libros",
                "Juguetes",
                "Alimentos"
            });
            cmbCategoria.SelectedIndex = 0;
            cmbCategoria.Name = "cmbCategoria";
            this.Controls.Add(cmbCategoria);

            // Botones de Gestión
            Button btnAgregar = new Button();
            btnAgregar.Text = "📦 Agregar Producto";
            btnAgregar.Location = new Point(20, 140);
            btnAgregar.Size = new Size(130, 30);
            btnAgregar.Click += (sender, e) =>
            {
                AgregarProducto(txtCodigo.Text, txtNombre.Text, txtPrecio.Text, cmbCategoria.SelectedItem?.ToString());
                txtCodigo.Clear();
                txtNombre.Clear();
                txtPrecio.Clear();
                txtCodigo.Focus();
            };
            this.Controls.Add(btnAgregar);

            Button btnBuscar = new Button();
            btnBuscar.Text = "🔍 Buscar por SKU";
            btnBuscar.Location = new Point(160, 140);
            btnBuscar.Size = new Size(120, 30);
            btnBuscar.Click += (sender, e) =>
            {
                BuscarProducto(txtCodigo.Text);
            };
            this.Controls.Add(btnBuscar);

            Button btnEliminar = new Button();
            btnEliminar.Text = "❌ Eliminar Producto";
            btnEliminar.Location = new Point(290, 140);
            btnEliminar.Size = new Size(120, 30);
            btnEliminar.Click += (sender, e) =>
            {
                EliminarProducto(txtCodigo.Text);
            };
            this.Controls.Add(btnEliminar);

            Button btnActualizar = new Button();
            btnActualizar.Text = "✏️ Actualizar";
            btnActualizar.Location = new Point(420, 140);
            btnActualizar.Size = new Size(100, 30);
            btnActualizar.Click += (sender, e) =>
            {
                ActualizarProducto(txtCodigo.Text, txtNombre.Text, txtPrecio.Text, cmbCategoria.SelectedItem?.ToString());
            };
            this.Controls.Add(btnActualizar);

            // Lista de Productos
            Label lblProductos = new Label();
            lblProductos.Text = "Catálogo de Productos:";
            lblProductos.Location = new Point(20, 190);
            lblProductos.Size = new Size(150, 20);
            lblProductos.Font = new Font(lblProductos.Font, FontStyle.Bold);
            this.Controls.Add(lblProductos);

            ListBox lstProductos = new ListBox();
            lstProductos.Location = new Point(20, 210);
            lstProductos.Size = new Size(550, 200);
            lstProductos.Name = "lstProductos";
            this.Controls.Add(lstProductos);

            // Panel de Información
            Label lblInfo = new Label();
            lblInfo.Text = "Total de productos: 0 | Valor total del inventario: $0.00";
            lblInfo.Location = new Point(20, 420);
            lblInfo.Size = new Size(550, 30);
            lblInfo.Name = "lblInfo";
            lblInfo.ForeColor = Color.DarkBlue;
            lblInfo.BackColor = Color.LightCyan;
            lblInfo.BorderStyle = BorderStyle.FixedSingle;
            lblInfo.TextAlign = ContentAlignment.MiddleCenter;
            lblInfo.Font = new Font(lblInfo.Font, FontStyle.Bold);
            this.Controls.Add(lblInfo);

            // Botones Adicionales
            Button btnActualizarLista = new Button();
            btnActualizarLista.Text = "🔄 Actualizar Lista";
            btnActualizarLista.Location = new Point(20, 460);
            btnActualizarLista.Size = new Size(120, 30);
            btnActualizarLista.Click += (sender, e) =>
            {
                ActualizarListaProductos();
            };
            this.Controls.Add(btnActualizarLista);

            Button btnLimpiar = new Button();
            btnLimpiar.Text = "🧹 Limpiar Catálogo";
            btnLimpiar.Location = new Point(150, 460);
            btnLimpiar.Size = new Size(120, 30);
            btnLimpiar.Click += (sender, e) =>
            {
                LimpiarCatalogo();
            };
            this.Controls.Add(btnLimpiar);

            Button btnEjemplos = new Button();
            btnEjemplos.Text = "🎲 Productos Demo";
            btnEjemplos.Location = new Point(280, 460);
            btnEjemplos.Size = new Size(120, 30);
            btnEjemplos.Click += (sender, e) =>
            {
                AgregarProductosDemo();
            };
            this.Controls.Add(btnEjemplos);

            Button btnFiltrar = new Button();
            btnFiltrar.Text = "📊 Filtrar por Categoría";
            btnFiltrar.Location = new Point(410, 460);
            btnFiltrar.Size = new Size(120, 30);
            btnFiltrar.Click += (sender, e) =>
            {
                FiltrarPorCategoria();
            };
            this.Controls.Add(btnFiltrar);

            // Explicación Dictionary
            Label lblExplicacion = new Label();
            lblExplicacion.Text = "💡 Dictionary: Código SKU único → Información completa del producto";
            lblExplicacion.Location = new Point(20, 500);
            lblExplicacion.Size = new Size(550, 30);
            lblExplicacion.ForeColor = Color.DarkGreen;
            lblExplicacion.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(lblExplicacion);
        }

        private void AgregarProducto(string codigo, string nombre, string precioTexto, string categoria)
        {
            if (string.IsNullOrWhiteSpace(codigo))
            {
                MessageBox.Show("Por favor, ingrese un código SKU para el producto.", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("Por favor, ingrese un nombre para el producto.", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(precioTexto, out decimal precio) || precio < 0)
            {
                MessageBox.Show("Por favor, ingrese un precio válido (número positivo).", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(categoria))
            {
                MessageBox.Show("Por favor, seleccione una categoría.", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Verificar si el código SKU ya existe
            if (catalogo.ContainsKey(codigo))
            {
                MessageBox.Show($"El código SKU '{codigo}' ya existe en el catálogo.", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Agregar nuevo producto al Dictionary
            ProductoCatalogo nuevoProducto = new ProductoCatalogo(codigo, nombre, precio, categoria);
            catalogo.Add(codigo, nuevoProducto);

            MessageBox.Show($"Producto '{nombre}' agregado al catálogo.\nCódigo SKU: {codigo}\nPrecio: ${precio:F2}",
                          "Producto Agregado", MessageBoxButtons.OK, MessageBoxIcon.Information);

            ActualizarListaProductos();
        }

        private void BuscarProducto(string codigo)
        {
            if (string.IsNullOrWhiteSpace(codigo))
            {
                MessageBox.Show("Por favor, ingrese un código SKU para buscar.", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Buscar en el Dictionary por clave (código SKU)
            if (catalogo.ContainsKey(codigo))
            {
                ProductoCatalogo producto = catalogo[codigo];
                MessageBox.Show($"📦 Producto encontrado:\n\n" +
                              $"Código SKU: {producto.Codigo}\n" +
                              $"Nombre: {producto.Nombre}\n" +
                              $"Precio: ${producto.Precio:F2}\n" +
                              $"Categoría: {producto.Categoria}",
                              "Producto Encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show($"El producto con código SKU '{codigo}' no se encuentra en el catálogo.",
                              "Producto No Encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void EliminarProducto(string codigo)
        {
            if (string.IsNullOrWhiteSpace(codigo))
            {
                MessageBox.Show("Por favor, ingrese un código SKU para eliminar.", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (catalogo.ContainsKey(codigo))
            {
                ProductoCatalogo producto = catalogo[codigo];
                DialogResult resultado = MessageBox.Show(
                    $"¿Está seguro de que desea eliminar el producto?\n\n" +
                    $"Código: {producto.Codigo}\n" +
                    $"Nombre: {producto.Nombre}\n" +
                    $"Precio: ${producto.Precio:F2}",
                    "Confirmar Eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    catalogo.Remove(codigo);
                    MessageBox.Show($"Producto '{producto.Nombre}' eliminado del catálogo.",
                                  "Producto Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ActualizarListaProductos();
                }
            }
            else
            {
                MessageBox.Show($"El producto con código SKU '{codigo}' no se encuentra en el catálogo.",
                              "Producto No Encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ActualizarProducto(string codigo, string nombre, string precioTexto, string categoria)
        {
            if (string.IsNullOrWhiteSpace(codigo))
            {
                MessageBox.Show("Por favor, ingrese un código SKU para actualizar.", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!catalogo.ContainsKey(codigo))
            {
                MessageBox.Show($"El producto con código SKU '{codigo}' no existe en el catálogo.",
                              "Producto No Encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("Por favor, ingrese un nombre para el producto.", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(precioTexto, out decimal precio) || precio < 0)
            {
                MessageBox.Show("Por favor, ingrese un precio válido (número positivo).", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(categoria))
            {
                MessageBox.Show("Por favor, seleccione una categoría.", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Actualizar producto existente
            ProductoCatalogo producto = catalogo[codigo];
            producto.Nombre = nombre;
            producto.Precio = precio;
            producto.Categoria = categoria;

            MessageBox.Show($"Producto actualizado correctamente.\nCódigo SKU: {codigo}",
                          "Producto Actualizado", MessageBoxButtons.OK, MessageBoxIcon.Information);

            ActualizarListaProductos();
        }

        private void LimpiarCatalogo()
        {
            if (catalogo.Count == 0)
            {
                MessageBox.Show("El catálogo ya está vacío.", "Información",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult resultado = MessageBox.Show(
                $"¿Está seguro de que desea limpiar todo el catálogo? Se eliminarán {catalogo.Count} productos.",
                "Confirmar Limpieza",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                catalogo.Clear();
                ActualizarListaProductos();
                MessageBox.Show("Catálogo limpiado completamente.", "Catálogo Limpiado",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void AgregarProductosDemo()
        {
            // Agregar algunos productos de ejemplo
            Dictionary<string, ProductoCatalogo> productosDemo = new Dictionary<string, ProductoCatalogo>
            {
                { "SKU001", new ProductoCatalogo("SKU001", "Laptop Gaming", 1299.99m, "Electrónicos") },
                { "SKU002", new ProductoCatalogo("SKU002", "Smartphone", 599.99m, "Electrónicos") },
                { "SKU003", new ProductoCatalogo("SKU003", "Camiseta Deportiva", 29.99m, "Ropa") },
                { "SKU004", new ProductoCatalogo("SKU004", "Sofá 3 Plazas", 899.99m, "Hogar") },
                { "SKU005", new ProductoCatalogo("SKU005", "Pelota de Fútbol", 19.99m, "Deportes") }
            };

            foreach (var producto in productosDemo)
            {
                if (!catalogo.ContainsKey(producto.Key))
                {
                    catalogo.Add(producto.Key, producto.Value);
                }
            }

            MessageBox.Show($"Se agregaron {productosDemo.Count} productos de ejemplo.",
                          "Productos Demo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ActualizarListaProductos();
        }

        private void FiltrarPorCategoria()
        {
            if (catalogo.Count == 0)
            {
                MessageBox.Show("El catálogo está vacío.", "Información",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Mostrar diálogo para seleccionar categoría
            string[] categorias = catalogo.Values.Select(p => p.Categoria).Distinct().ToArray();

            if (categorias.Length == 0)
            {
                MessageBox.Show("No hay categorías disponibles.", "Información",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var form = new Form())
            {
                form.Text = "Filtrar por Categoría";
                form.Size = new Size(300, 200);
                form.StartPosition = FormStartPosition.CenterParent;

                Label lbl = new Label() { Text = "Seleccione una categoría:", Location = new Point(20, 20), Size = new Size(200, 20) };
                ComboBox cmb = new ComboBox() { Location = new Point(20, 50), Size = new Size(200, 20), DropDownStyle = ComboBoxStyle.DropDownList };
                cmb.Items.AddRange(categorias);
                cmb.SelectedIndex = 0;

                Button btnAceptar = new Button() { Text = "Filtrar", Location = new Point(20, 90), Size = new Size(80, 30) };
                Button btnCancelar = new Button() { Text = "Cancelar", Location = new Point(120, 90), Size = new Size(80, 30) };

                btnAceptar.Click += (s, e) => { form.DialogResult = DialogResult.OK; form.Close(); };
                btnCancelar.Click += (s, e) => { form.DialogResult = DialogResult.Cancel; form.Close(); };

                form.Controls.AddRange(new Control[] { lbl, cmb, btnAceptar, btnCancelar });

                if (form.ShowDialog() == DialogResult.OK)
                {
                    string categoriaSeleccionada = cmb.SelectedItem.ToString();
                    MostrarProductosPorCategoria(categoriaSeleccionada);
                }
            }
        }

        private void MostrarProductosPorCategoria(string categoria)
        {
            ListBox lstProductos = this.Controls.Find("lstProductos", true).FirstOrDefault() as ListBox;
            Label lblInfo = this.Controls.Find("lblInfo", true).FirstOrDefault() as Label;

            if (lstProductos != null)
            {
                lstProductos.Items.Clear();

                var productosFiltrados = catalogo.Values.Where(p => p.Categoria == categoria)
                                                       .OrderBy(p => p.Codigo);

                if (!productosFiltrados.Any())
                {
                    lstProductos.Items.Add($"No hay productos en la categoría '{categoria}'");
                }
                else
                {
                    foreach (var producto in productosFiltrados)
                    {
                        lstProductos.Items.Add($"📦 {producto.Codigo} | {producto.Nombre} | ${producto.Precio:F2} | {producto.Categoria}");
                    }
                }
            }

            if (lblInfo != null)
            {
                int cantidad = catalogo.Values.Count(p => p.Categoria == categoria);
                decimal valorTotal = catalogo.Values.Where(p => p.Categoria == categoria).Sum(p => p.Precio);
                lblInfo.Text = $"Categoría: {categoria} | Productos: {cantidad} | Valor total: ${valorTotal:F2}";
            }
        }

        private void ActualizarListaProductos()
        {
            ListBox lstProductos = this.Controls.Find("lstProductos", true).FirstOrDefault() as ListBox;
            Label lblInfo = this.Controls.Find("lblInfo", true).FirstOrDefault() as Label;

            if (lstProductos != null)
            {
                lstProductos.Items.Clear();

                if (catalogo.Count == 0)
                {
                    lstProductos.Items.Add("El catálogo está vacío. Agrega algunos productos!");
                }
                else
                {
                    // Ordenar por código SKU
                    var productosOrdenados = catalogo.Values.OrderBy(p => p.Codigo);

                    foreach (var producto in productosOrdenados)
                    {
                        lstProductos.Items.Add($"📦 {producto.Codigo} | {producto.Nombre} | ${producto.Precio:F2} | {producto.Categoria}");
                    }
                }
            }

            if (lblInfo != null)
            {
                decimal valorTotal = catalogo.Values.Sum(p => p.Precio);
                lblInfo.Text = $"Total de productos: {catalogo.Count} | Valor total del inventario: ${valorTotal:F2}";
            }
        }
    }

    public class ProductoCatalogo
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public string Categoria { get; set; }

        public ProductoCatalogo(string codigo, string nombre, decimal precio, string categoria)
        {
            Codigo = codigo;
            Nombre = nombre;
            Precio = precio;
            Categoria = categoria;
        }
    }
}