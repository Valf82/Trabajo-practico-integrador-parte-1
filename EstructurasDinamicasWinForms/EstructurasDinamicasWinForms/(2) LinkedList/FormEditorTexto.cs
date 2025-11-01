using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace EstructurasDinamicasWinForms
{
    public partial class FormEditorTexto : Form
    {
        private LinkedList<string> historialAcciones = new LinkedList<string>();
        private LinkedListNode<string> nodoActual = null;
        private string textoActual = "";

        public FormEditorTexto()
        {
            ConfigurarControles();
        }

        private void ConfigurarControles()
        {
            this.Text = "Editor de Texto - Historial (Deshacer/Rehacer)";
            this.Size = new Size(600, 550);
            this.StartPosition = FormStartPosition.CenterScreen;
            CrearControles();
        }

        private void CrearControles()
        {
            // Área de Texto Principal
            Label lblEditor = new Label();
            lblEditor.Text = "Editor de Texto:";
            lblEditor.Location = new Point(20, 20);
            lblEditor.Size = new Size(150, 20);
            this.Controls.Add(lblEditor);

            TextBox txtEditor = new TextBox();
            txtEditor.Location = new Point(20, 50);
            txtEditor.Size = new Size(540, 100);
            txtEditor.Multiline = true;
            txtEditor.ScrollBars = ScrollBars.Vertical;
            txtEditor.Height = 100;
            txtEditor.Name = "txtEditor";
            txtEditor.TextChanged += (sender, e) =>
            {
                // No agregamos al historial cambios automáticos
                if (!string.IsNullOrEmpty(txtEditor.Text) && txtEditor.Text != textoActual)
                {
                    AgregarAlHistorial(txtEditor.Text);
                }
            };
            this.Controls.Add(txtEditor);

            // Botones de Acción
            Button btnEscribir = new Button();
            btnEscribir.Text = "Agregar Texto";
            btnEscribir.Location = new Point(20, 170);
            btnEscribir.Size = new Size(100, 30);
            btnEscribir.Click += (sender, e) =>
            {
                string nuevoTexto = txtEditor.Text;
                if (!string.IsNullOrEmpty(nuevoTexto))
                {
                    AgregarAlHistorial(nuevoTexto);
                    MessageBox.Show("Texto agregado al historial.", "Acción Registrada",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            };
            this.Controls.Add(btnEscribir);

            Button btnLimpiar = new Button();
            btnLimpiar.Text = "Limpiar Texto";
            btnLimpiar.Location = new Point(130, 170);
            btnLimpiar.Size = new Size(100, 30);
            btnLimpiar.Click += (sender, e) =>
            {
                AgregarAlHistorial("");
                txtEditor.Clear();
                MessageBox.Show("Texto limpiado.", "Acción Registrada",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            };
            this.Controls.Add(btnLimpiar);

            // Botones de Navegación
            Button btnDeshacer = new Button();
            btnDeshacer.Text = "← Deshacer";
            btnDeshacer.Location = new Point(240, 170);
            btnDeshacer.Size = new Size(100, 30);
            btnDeshacer.Click += (sender, e) =>
            {
                Deshacer();
            };
            this.Controls.Add(btnDeshacer);

            Button btnRehacer = new Button();
            btnRehacer.Text = "Rehacer →";
            btnRehacer.Location = new Point(350, 170);
            btnRehacer.Size = new Size(100, 30);
            btnRehacer.Click += (sender, e) =>
            {
                Rehacer();
            };
            this.Controls.Add(btnRehacer);

            // ListBox del Historial
            Label lblHistorial = new Label();
            lblHistorial.Text = "Historial de Acciones:";
            lblHistorial.Location = new Point(20, 220);
            lblHistorial.Size = new Size(150, 20);
            this.Controls.Add(lblHistorial);

            ListBox lstHistorial = new ListBox();
            lstHistorial.Location = new Point(20, 240);
            lstHistorial.Size = new Size(540, 150);
            lstHistorial.Name = "lstHistorial";
            this.Controls.Add(lstHistorial);

            // Panel de Información
            Label lblInfo = new Label();
            lblInfo.Text = "Acciones en historial: 0 | Posición actual: Inicio";
            lblInfo.Location = new Point(20, 400);
            lblInfo.Size = new Size(540, 30);
            lblInfo.Name = "lblInfo";
            lblInfo.ForeColor = Color.DarkBlue;
            lblInfo.BackColor = Color.LightYellow;
            lblInfo.BorderStyle = BorderStyle.FixedSingle;
            lblInfo.TextAlign = ContentAlignment.MiddleCenter;
            lblInfo.Font = new Font(lblInfo.Font, FontStyle.Bold);
            this.Controls.Add(lblInfo);

            // Botón ACTUALIZAR HISTORIAL
            Button btnActualizar = new Button();
            btnActualizar.Text = "Actualizar Historial";
            btnActualizar.Location = new Point(20, 450); // Movido más arriba
            btnActualizar.Size = new Size(120, 30);
            btnActualizar.Click += (sender, e) =>
            {
                ActualizarHistorial();
            };
            this.Controls.Add(btnActualizar);

            // Botón LIMPIAR HISTORIAL
            Button btnLimpiarHistorial = new Button();
            btnLimpiarHistorial.Text = "Limpiar Historial";
            btnLimpiarHistorial.Location = new Point(150, 450); // Movido más arriba
            btnLimpiarHistorial.Size = new Size(120, 30);
            btnLimpiarHistorial.Click += (sender, e) =>
            {
                LimpiarHistorial();
                txtEditor.Clear();
            };
            this.Controls.Add(btnLimpiarHistorial);

            // Botón de información adicional
            Button btnInfo = new Button();
            btnInfo.Text = "¿Cómo usar?";
            btnInfo.Location = new Point(280, 450);
            btnInfo.Size = new Size(100, 30);
            btnInfo.Click += (sender, e) =>
            {
                MessageBox.Show("📝 EDITOR DE TEXTO CON HISTORIAL\n\n" +
                              "• Escribe en el área de texto\n" +
                              "• Usa 'Agregar Texto' para guardar una versión\n" +
                              "• '← Deshacer' para volver atrás\n" +
                              "• 'Rehacer →' para avanzar\n" +
                              "• El historial muestra todas tus acciones",
                              "Instrucciones de Uso",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            };
            this.Controls.Add(btnInfo);
        }

        private void AgregarAlHistorial(string texto)
        {
            // Si estamos en medio del historial, eliminamos las acciones futuras
            if (nodoActual != null)
            {
                LinkedListNode<string> nodoSiguiente = nodoActual.Next;
                while (nodoSiguiente != null)
                {
                    historialAcciones.Remove(nodoSiguiente);
                    nodoSiguiente = nodoActual.Next;
                }
            }

            // Agregar nueva acción al final del historial
            historialAcciones.AddLast(texto);
            nodoActual = historialAcciones.Last;
            textoActual = texto;

            ActualizarHistorial();
        }

        private void Deshacer()
        {
            if (nodoActual == null || nodoActual.Previous == null)
            {
                MessageBox.Show("No hay acciones anteriores para deshacer.", "Deshacer",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Movemos al nodo anterior
            nodoActual = nodoActual.Previous;
            textoActual = nodoActual.Value;

            // Actualizar el texto del editor
            TextBox txtEditor = this.Controls.Find("txtEditor", true).FirstOrDefault() as TextBox;
            if (txtEditor != null)
            {
                txtEditor.Text = textoActual;
            }

            ActualizarHistorial();
        }

        private void Rehacer()
        {
            if (nodoActual == null || nodoActual.Next == null)
            {
                MessageBox.Show("No hay acciones posteriores para rehacer.", "Rehacer",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Movemos al nodo siguiente
            nodoActual = nodoActual.Next;
            textoActual = nodoActual.Value;

            // Actualizar el texto del editor
            TextBox txtEditor = this.Controls.Find("txtEditor", true).FirstOrDefault() as TextBox;
            if (txtEditor != null)
            {
                txtEditor.Text = textoActual;
            }

            ActualizarHistorial();
        }

        private void LimpiarHistorial()
        {
            historialAcciones.Clear();
            nodoActual = null;
            textoActual = "";
            ActualizarHistorial();
            MessageBox.Show("Historial limpiado completamente.", "Historial Limpiado",
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ActualizarHistorial()
        {
            ListBox lstHistorial = this.Controls.Find("lstHistorial", true).FirstOrDefault() as ListBox;
            Label lblInfo = this.Controls.Find("lblInfo", true).FirstOrDefault() as Label;

            if (lstHistorial != null)
            {
                lstHistorial.Items.Clear();

                if (historialAcciones.Count == 0)
                {
                    lstHistorial.Items.Add("El historial está vacío.");
                }
                else
                {
                    int posicion = 1;
                    LinkedListNode<string> nodo = historialAcciones.First;

                    while (nodo != null)
                    {
                        string texto = string.IsNullOrEmpty(nodo.Value) ? "[TEXTO LIMPIADO]" :
                                     (nodo.Value.Length > 50 ? nodo.Value.Substring(0, 50) + "..." : nodo.Value);

                        string indicador = (nodo == nodoActual) ? "← ACTUAL" : "";
                        lstHistorial.Items.Add($"{posicion}. {texto} {indicador}");

                        nodo = nodo.Next;
                        posicion++;
                    }
                }
            }

            if (lblInfo != null)
            {
                int posicionActual = 0;
                int totalAcciones = historialAcciones.Count;

                if (nodoActual != null)
                {
                    // Calcular posición actual
                    LinkedListNode<string> nodo = historialAcciones.First;
                    while (nodo != null && nodo != nodoActual)
                    {
                        posicionActual++;
                        nodo = nodo.Next;
                    }
                    posicionActual++;
                }

                string estado = totalAcciones == 0 ? "Inicio" : $"{posicionActual} de {totalAcciones}";
                lblInfo.Text = $"Acciones en historial: {totalAcciones} | Posición actual: {estado}";
            }
        }
    }
}