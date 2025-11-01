using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace EstructurasDinamicasWinForms
{
    public partial class FormParentesis : Form
    {
        public FormParentesis()
        {
            ConfigurarControles();
        }

        private void ConfigurarControles()
        {
            this.Text = "Verificador de Paréntesis Balanceados";
            this.Size = new Size(600, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            CrearControles();
        }

        private void CrearControles()
        {
            // Área de Expresión
            Label lblExpresion = new Label();
            lblExpresion.Text = "Ingrese la expresión a verificar:";
            lblExpresion.Location = new Point(20, 20);
            lblExpresion.Size = new Size(200, 20);
            lblExpresion.Font = new Font(lblExpresion.Font, FontStyle.Bold);
            this.Controls.Add(lblExpresion);

            TextBox txtExpresion = new TextBox();
            txtExpresion.Location = new Point(20, 50);
            txtExpresion.Size = new Size(540, 100);
            txtExpresion.Multiline = true;
            txtExpresion.ScrollBars = ScrollBars.Vertical;
            txtExpresion.Height = 80;
            txtExpresion.Font = new Font("Consolas", 10);
            txtExpresion.Name = "txtExpresion";
            this.Controls.Add(txtExpresion);

            // Botón VERIFICAR
            Button btnVerificar = new Button();
            btnVerificar.Text = "🔍 Verificar Balance";
            btnVerificar.Location = new Point(20, 150);
            btnVerificar.Size = new Size(140, 35);
            btnVerificar.BackColor = Color.LightBlue;
            btnVerificar.Click += (sender, e) =>
            {
                VerificarBalance(txtExpresion.Text);
            };
            this.Controls.Add(btnVerificar);

            // Botón EJEMPLOS
            Button btnEjemplos = new Button();
            btnEjemplos.Text = "📚 Ver Ejemplos";
            btnEjemplos.Location = new Point(170, 150);
            btnEjemplos.Size = new Size(120, 35);
            btnEjemplos.Click += (sender, e) =>
            {
                MostrarEjemplos();
            };
            this.Controls.Add(btnEjemplos);

            // Botón LIMPIAR
            Button btnLimpiar = new Button();
            btnLimpiar.Text = "🧹 Limpiar";
            btnLimpiar.Location = new Point(300, 150);
            btnLimpiar.Size = new Size(100, 35);
            btnLimpiar.Click += (sender, e) =>
            {
                txtExpresion.Clear();
                LimpiarResultado();
            };
            this.Controls.Add(btnLimpiar);

            // Panel de Resultado
            Label lblResultado = new Label();
            lblResultado.Text = "Resultado:";
            lblResultado.Location = new Point(20, 200);
            lblResultado.Size = new Size(100, 20);
            lblResultado.Font = new Font(lblResultado.Font, FontStyle.Bold);
            this.Controls.Add(lblResultado);

            Label lblMensaje = new Label();
            lblMensaje.Text = "Ingrese una expresión y haga clic en 'Verificar'";
            lblMensaje.Location = new Point(20, 230);
            lblMensaje.Size = new Size(540, 60);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.ForeColor = Color.DarkBlue;
            lblMensaje.BackColor = Color.LightGray;
            lblMensaje.BorderStyle = BorderStyle.FixedSingle;
            lblMensaje.TextAlign = ContentAlignment.MiddleCenter;
            lblMensaje.Font = new Font(lblMensaje.Font, FontStyle.Bold);
            this.Controls.Add(lblMensaje);

            // Panel de Proceso Detallado
            Label lblProceso = new Label();
            lblProceso.Text = "Proceso de Verificación:";
            lblProceso.Location = new Point(20, 310);
            lblProceso.Size = new Size(200, 20);
            lblProceso.Font = new Font(lblProceso.Font, FontStyle.Bold);
            this.Controls.Add(lblProceso);

            ListBox lstProceso = new ListBox();
            lstProceso.Location = new Point(20, 330);
            lstProceso.Size = new Size(540, 120);
            lstProceso.Name = "lstProceso";
            lstProceso.Font = new Font("Consolas", 9);
            this.Controls.Add(lstProceso);

            // Información sobre paréntesis válidos
            Label lblInfo = new Label();
            lblInfo.Text = "💡 Se verifican: ( ) [ ] { }";
            lblInfo.Location = new Point(420, 150);
            lblInfo.Size = new Size(140, 35);
            lblInfo.ForeColor = Color.DarkGreen;
            lblInfo.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(lblInfo);
        }

        private void VerificarBalance(string expresion)
        {
            ListBox lstProceso = this.Controls.Find("lstProceso", true).FirstOrDefault() as ListBox;
            Label lblMensaje = this.Controls.Find("lblMensaje", true).FirstOrDefault() as Label;

            if (lstProceso != null) lstProceso.Items.Clear();

            if (string.IsNullOrWhiteSpace(expresion))
            {
                MostrarResultado("❌ Error: La expresión está vacía", Color.Red);
                return;
            }

            Stack<char> pila = new Stack<char>();
            bool balanceado = true;

            AgregarProceso("Iniciando verificación de balance...");
            AgregarProceso($"Expresión: {expresion}");
            AgregarProceso("");

            for (int i = 0; i < expresion.Length; i++)
            {
                char caracter = expresion[i];
                AgregarProceso($"Carácter [{i}]: '{caracter}'");

                // Si es paréntesis de apertura, apilar
                if (EsParentesisApertura(caracter))
                {
                    pila.Push(caracter);
                    AgregarProceso($"  → APILAR: '{caracter}'");
                    AgregarProceso($"  → Pila actual: [{string.Join(", ", pila)}]");
                }
                // Si es paréntesis de cierre, verificar
                else if (EsParentesisCierre(caracter))
                {
                    if (pila.Count == 0)
                    {
                        AgregarProceso($"  ❌ ERROR: Cierre '{caracter}' sin apertura correspondiente");
                        balanceado = false;
                        break;
                    }

                    char tope = pila.Peek();
                    if (SonCorrespondientes(tope, caracter))
                    {
                        pila.Pop();
                        AgregarProceso($"  → DESAPILAR: '{tope}' coincide con '{caracter}'");
                        AgregarProceso($"  → Pila actual: [{string.Join(", ", pila)}]");
                    }
                    else
                    {
                        AgregarProceso($"  ❌ ERROR: '{tope}' no coincide con '{caracter}'");
                        balanceado = false;
                        break;
                    }
                }
                else
                {
                    AgregarProceso($"  → Ignorar: '{caracter}' no es paréntesis");
                }

                AgregarProceso("");
            }

            // Verificar si quedaron paréntesis sin cerrar
            if (balanceado && pila.Count > 0)
            {
                AgregarProceso($"❌ ERROR: {pila.Count} paréntesis de apertura sin cerrar");
                balanceado = false;
            }

            // Mostrar resultado final
            if (balanceado)
            {
                AgregarProceso("✅ ¡EXPRESIÓN CORRECTAMENTE BALANCEADA!");
                MostrarResultado("✅ ¡La expresión está correctamente balanceada!", Color.DarkGreen);
            }
            else
            {
                AgregarProceso("❌ EXPRESIÓN DESBALANCEADA");
                MostrarResultado("❌ La expresión NO está balanceada", Color.Red);
            }
        }

        private bool EsParentesisApertura(char c)
        {
            return c == '(' || c == '[' || c == '{';
        }

        private bool EsParentesisCierre(char c)
        {
            return c == ')' || c == ']' || c == '}';
        }

        private bool SonCorrespondientes(char apertura, char cierre)
        {
            return (apertura == '(' && cierre == ')') ||
                   (apertura == '[' && cierre == ']') ||
                   (apertura == '{' && cierre == '}');
        }

        private void AgregarProceso(string mensaje)
        {
            ListBox lstProceso = this.Controls.Find("lstProceso", true).FirstOrDefault() as ListBox;
            if (lstProceso != null)
            {
                lstProceso.Items.Add(mensaje);
                
                lstProceso.TopIndex = lstProceso.Items.Count - 1;
            }
        }

        private void MostrarResultado(string mensaje, Color color)
        {
            Label lblMensaje = this.Controls.Find("lblMensaje", true).FirstOrDefault() as Label;
            if (lblMensaje != null)
            {
                lblMensaje.Text = mensaje;
                lblMensaje.ForeColor = color;
                lblMensaje.BackColor = color == Color.Red ? Color.LightPink : Color.LightGreen;
            }
        }

        private void LimpiarResultado()
        {
            ListBox lstProceso = this.Controls.Find("lstProceso", true).FirstOrDefault() as ListBox;
            Label lblMensaje = this.Controls.Find("lblMensaje", true).FirstOrDefault() as Label;

            if (lstProceso != null) lstProceso.Items.Clear();
            if (lblMensaje != null)
            {
                lblMensaje.Text = "Ingrese una expresión y haga clic en 'Verificar'";
                lblMensaje.ForeColor = Color.DarkBlue;
                lblMensaje.BackColor = Color.LightGray;
            }
        }

        private void MostrarEjemplos()
        {
            string ejemplos = "📚 EJEMPLOS DE EXPRESIONES:\n\n" +
                            "✅ BALANCEADAS:\n" +
                            "• (a + b) * (c - d)\n" +
                            "• {[a + b] * (c - d)}\n" +
                            "• (([{}]))\n\n" +
                            "❌ DESBALANCEADAS:\n" +
                            "• (a + b) * (c - d))\n" +
                            "• {[a + b] * (c - d)}\n" +
                            "• (([]){})\n\n" +
                            "💡 Prueba estas expresiones!";

            MessageBox.Show(ejemplos, "Ejemplos de Expresiones",
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}