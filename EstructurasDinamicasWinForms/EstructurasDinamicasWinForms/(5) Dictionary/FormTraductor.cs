using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace EstructurasDinamicasWinForms
{
    public partial class FormTraductor : Form
    {
        private Dictionary<string, string> diccionario = new Dictionary<string, string>();

        public FormTraductor()
        {
            ConfigurarControles();
            CargarPalabrasIniciales();
        }

        private void ConfigurarControles()
        {
            this.Text = "Traductor de Idiomas - Dictionary";
            this.Size = new Size(500, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            CrearControles();
        }

        private void CargarPalabrasIniciales()
        {
            // Cargar algunas palabras iniciales en el diccionario
            Dictionary<string, string> palabrasIniciales = new Dictionary<string, string>
            {
                { "hello", "hola" },
                { "goodbye", "adiós" },
                { "thank you", "gracias" },
                { "please", "por favor" },
                { "yes", "sí" },
                { "no", "no" },
                { "water", "agua" },
                { "food", "comida" },
                { "house", "casa" },
                { "friend", "amigo" }
            };

            foreach (var palabra in palabrasIniciales)
            {
                if (!diccionario.ContainsKey(palabra.Key))
                {
                    diccionario.Add(palabra.Key, palabra.Value);
                }
            }

            ActualizarListaTraducciones();
        }

        private void CrearControles()
        {
            // Panel de Traducción
            Label lblIngles = new Label();
            lblIngles.Text = "Palabra en Inglés:";
            lblIngles.Location = new Point(20, 20);
            lblIngles.Size = new Size(120, 20);
            this.Controls.Add(lblIngles);

            TextBox txtIngles = new TextBox();
            txtIngles.Location = new Point(150, 20);
            txtIngles.Size = new Size(200, 20);
            txtIngles.Name = "txtIngles";
            this.Controls.Add(txtIngles);

            Label lblEspanol = new Label();
            lblEspanol.Text = "Traducción al Español:";
            lblEspanol.Location = new Point(20, 50);
            lblEspanol.Size = new Size(120, 20);
            this.Controls.Add(lblEspanol);

            TextBox txtEspanol = new TextBox();
            txtEspanol.Location = new Point(150, 50);
            txtEspanol.Size = new Size(200, 20);
            txtEspanol.Name = "txtEspanol";
            this.Controls.Add(txtEspanol);

            // Botones de Gestión
            Button btnAgregar = new Button();
            btnAgregar.Text = "➕ Agregar Traducción";
            btnAgregar.Location = new Point(20, 80);
            btnAgregar.Size = new Size(140, 30);
            btnAgregar.Click += (sender, e) =>
            {
                AgregarTraduccion(txtIngles.Text, txtEspanol.Text);
                txtIngles.Clear();
                txtEspanol.Clear();
                txtIngles.Focus();
            };
            this.Controls.Add(btnAgregar);

            Button btnTraducir = new Button();
            btnTraducir.Text = "🔍 Traducir";
            btnTraducir.Location = new Point(170, 80);
            btnTraducir.Size = new Size(100, 30);
            btnTraducir.Click += (sender, e) =>
            {
                TraducirPalabra(txtIngles.Text);
            };
            this.Controls.Add(btnTraducir);

            Button btnEliminar = new Button();
            btnEliminar.Text = "❌ Eliminar";
            btnEliminar.Location = new Point(280, 80);
            btnEliminar.Size = new Size(100, 30);
            btnEliminar.Click += (sender, e) =>
            {
                EliminarTraduccion(txtIngles.Text);
            };
            this.Controls.Add(btnEliminar);

            // Panel de Búsqueda Rápida
            Label lblBuscar = new Label();
            lblBuscar.Text = "Búsqueda Rápida:";
            lblBuscar.Location = new Point(20, 130);
            lblBuscar.Size = new Size(100, 20);
            lblBuscar.Font = new Font(lblBuscar.Font, FontStyle.Bold);
            this.Controls.Add(lblBuscar);

            TextBox txtBuscar = new TextBox();
            txtBuscar.Location = new Point(130, 130);
            txtBuscar.Size = new Size(200, 20);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.TextChanged += (sender, e) =>
            {
                FiltrarTraducciones(txtBuscar.Text);
            };
            this.Controls.Add(txtBuscar);

            Button btnLimpiarBusqueda = new Button();
            btnLimpiarBusqueda.Text = "🧹";
            btnLimpiarBusqueda.Location = new Point(340, 130);
            btnLimpiarBusqueda.Size = new Size(40, 20);
            btnLimpiarBusqueda.Click += (sender, e) =>
            {
                txtBuscar.Clear();
                ActualizarListaTraducciones();
            };
            this.Controls.Add(btnLimpiarBusqueda);

            // Lista de Traducciones
            Label lblTraducciones = new Label();
            lblTraducciones.Text = "Diccionario Completo:";
            lblTraducciones.Location = new Point(20, 170);
            lblTraducciones.Size = new Size(150, 20);
            lblTraducciones.Font = new Font(lblTraducciones.Font, FontStyle.Bold);
            this.Controls.Add(lblTraducciones);

            ListBox lstTraducciones = new ListBox();
            lstTraducciones.Location = new Point(20, 190);
            lstTraducciones.Size = new Size(440, 180);
            lstTraducciones.Name = "lstTraducciones";
            this.Controls.Add(lstTraducciones);

            // Panel de Información
            Label lblInfo = new Label();
            lblInfo.Text = "Palabras en el diccionario: 10";
            lblInfo.Location = new Point(20, 380);
            lblInfo.Size = new Size(440, 30);
            lblInfo.Name = "lblInfo";
            lblInfo.ForeColor = Color.DarkBlue;
            lblInfo.BackColor = Color.LightCyan;
            lblInfo.BorderStyle = BorderStyle.FixedSingle;
            lblInfo.TextAlign = ContentAlignment.MiddleCenter;
            lblInfo.Font = new Font(lblInfo.Font, FontStyle.Bold);
            this.Controls.Add(lblInfo);

            // Botones Adicionales
            Button btnActualizar = new Button();
            btnActualizar.Text = "🔄 Actualizar Lista";
            btnActualizar.Location = new Point(20, 420);
            btnActualizar.Size = new Size(120, 30);
            btnActualizar.Click += (sender, e) =>
            {
                ActualizarListaTraducciones();
            };
            this.Controls.Add(btnActualizar);

            Button btnLimpiar = new Button();
            btnLimpiar.Text = "🧹 Limpiar Diccionario";
            btnLimpiar.Location = new Point(150, 420);
            btnLimpiar.Size = new Size(140, 30);
            btnLimpiar.Click += (sender, e) =>
            {
                LimpiarDiccionario();
            };
            this.Controls.Add(btnLimpiar);

            Button btnEjemplos = new Button();
            btnEjemplos.Text = "🎲 Más Ejemplos";
            btnEjemplos.Location = new Point(300, 420);
            btnEjemplos.Size = new Size(120, 30);
            btnEjemplos.Click += (sender, e) =>
            {
                AgregarMasEjemplos();
            };
            this.Controls.Add(btnEjemplos);

            // Estadísticas
            Label lblEstadisticas = new Label();
            lblEstadisticas.Text = "💡 Dictionary: Búsqueda instantánea inglés → español";
            lblEstadisticas.Location = new Point(20, 460);
            lblEstadisticas.Size = new Size(440, 30);
            lblEstadisticas.ForeColor = Color.DarkGreen;
            lblEstadisticas.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(lblEstadisticas);
        }

        private void AgregarTraduccion(string ingles, string espanol)
        {
            if (string.IsNullOrWhiteSpace(ingles))
            {
                MessageBox.Show("Por favor, ingrese una palabra en inglés.", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(espanol))
            {
                MessageBox.Show("Por favor, ingrese la traducción al español.", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Verificar si la palabra ya existe
            if (diccionario.ContainsKey(ingles.ToLower()))
            {
                DialogResult resultado = MessageBox.Show(
                    $"La palabra '{ingles}' ya existe en el diccionario. ¿Desea actualizar su traducción?",
                    "Palabra Existente",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    diccionario[ingles.ToLower()] = espanol;
                    MessageBox.Show($"Traducción de '{ingles}' actualizada correctamente.",
                                  "Traducción Actualizada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                // Agregar nueva traducción al Dictionary
                diccionario.Add(ingles.ToLower(), espanol);
                MessageBox.Show($"Traducción agregada correctamente:\n{ingles} → {espanol}",
                              "Traducción Agregada", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            ActualizarListaTraducciones();
        }

        private void TraducirPalabra(string ingles)
        {
            if (string.IsNullOrWhiteSpace(ingles))
            {
                MessageBox.Show("Por favor, ingrese una palabra en inglés para traducir.", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string inglesLower = ingles.ToLower();

            // Buscar en el Dictionary por clave (palabra en inglés)
            if (diccionario.ContainsKey(inglesLower))
            {
                string traduccion = diccionario[inglesLower];
                MessageBox.Show($"🌍 Traducción encontrada:\n\n" +
                              $"Inglés: {ingles}\n" +
                              $"Español: {traduccion}",
                              "Traducción", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show($"La palabra '{ingles}' no se encuentra en el diccionario.",
                              "Palabra No Encontrada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void EliminarTraduccion(string ingles)
        {
            if (string.IsNullOrWhiteSpace(ingles))
            {
                MessageBox.Show("Por favor, ingrese una palabra en inglés para eliminar.", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string inglesLower = ingles.ToLower();

            if (diccionario.ContainsKey(inglesLower))
            {
                string traduccion = diccionario[inglesLower];
                DialogResult resultado = MessageBox.Show(
                    $"¿Está seguro de que desea eliminar la traducción?\n\n" +
                    $"Inglés: {ingles}\n" +
                    $"Español: {traduccion}",
                    "Confirmar Eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    diccionario.Remove(inglesLower);
                    MessageBox.Show($"Traducción de '{ingles}' eliminada del diccionario.",
                                  "Traducción Eliminada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ActualizarListaTraducciones();
                }
            }
            else
            {
                MessageBox.Show($"La palabra '{ingles}' no se encuentra en el diccionario.",
                              "Palabra No Encontrada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LimpiarDiccionario()
        {
            if (diccionario.Count == 0)
            {
                MessageBox.Show("El diccionario ya está vacío.", "Información",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult resultado = MessageBox.Show(
                $"¿Está seguro de que desea limpiar todo el diccionario? Se eliminarán {diccionario.Count} palabras.",
                "Confirmar Limpieza",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                diccionario.Clear();
                // Volver a cargar las palabras iniciales
                CargarPalabrasIniciales();
                MessageBox.Show("Diccionario limpiado completamente.", "Diccionario Limpiado",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void AgregarMasEjemplos()
        {
            // Agregar más palabras de ejemplo al diccionario
            Dictionary<string, string> masEjemplos = new Dictionary<string, string>
            {
                { "cat", "gato" },
                { "dog", "perro" },
                { "sun", "sol" },
                { "moon", "luna" },
                { "book", "libro" },
                { "pen", "bolígrafo" },
                { "computer", "computadora" },
                { "music", "música" },
                { "love", "amor" },
                { "time", "tiempo" }
            };

            int agregadas = 0;
            foreach (var ejemplo in masEjemplos)
            {
                if (!diccionario.ContainsKey(ejemplo.Key))
                {
                    diccionario.Add(ejemplo.Key, ejemplo.Value);
                    agregadas++;
                }
            }

            MessageBox.Show($"Se agregaron {agregadas} nuevas palabras al diccionario.",
                          "Ejemplos Agregados", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ActualizarListaTraducciones();
        }

        private void FiltrarTraducciones(string filtro)
        {
            ListBox lstTraducciones = this.Controls.Find("lstTraducciones", true).FirstOrDefault() as ListBox;

            if (lstTraducciones != null)
            {
                lstTraducciones.Items.Clear();

                if (string.IsNullOrWhiteSpace(filtro))
                {
                    ActualizarListaTraducciones();
                    return;
                }

                var traduccionesFiltradas = diccionario
                    .Where(p => p.Key.Contains(filtro.ToLower()) || p.Value.Contains(filtro.ToLower()))
                    .OrderBy(p => p.Key);

                if (!traduccionesFiltradas.Any())
                {
                    lstTraducciones.Items.Add($"No se encontraron palabras que contengan '{filtro}'");
                }
                else
                {
                    foreach (var traduccion in traduccionesFiltradas)
                    {
                        lstTraducciones.Items.Add($"🇺🇸 {traduccion.Key} → 🇪🇸 {traduccion.Value}");
                    }
                }
            }
        }

        private void ActualizarListaTraducciones()
        {
            ListBox lstTraducciones = this.Controls.Find("lstTraducciones", true).FirstOrDefault() as ListBox;
            Label lblInfo = this.Controls.Find("lblInfo", true).FirstOrDefault() as Label;

            if (lstTraducciones != null)
            {
                lstTraducciones.Items.Clear();

                if (diccionario.Count == 0)
                {
                    lstTraducciones.Items.Add("El diccionario está vacío. Agrega algunas traducciones!");
                }
                else
                {
                    // Ordenar alfabéticamente por palabra en inglés
                    var traduccionesOrdenadas = diccionario.OrderBy(t => t.Key);

                    foreach (var traduccion in traduccionesOrdenadas)
                    {
                        lstTraducciones.Items.Add($"🇺🇸 {traduccion.Key} → 🇪🇸 {traduccion.Value}");
                    }
                }
            }

            if (lblInfo != null)
            {
                lblInfo.Text = $"Palabras en el diccionario: {diccionario.Count}";
            }
        }
    }
}