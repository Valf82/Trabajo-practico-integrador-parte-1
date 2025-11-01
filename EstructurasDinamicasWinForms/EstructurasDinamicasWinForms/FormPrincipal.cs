using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EstructurasDinamicasWinForms
{
    public partial class FormPrincipal : Form
    {
        public FormPrincipal()
        {
            InitializeComponent();
            ConfigurarMenu();
        }

        private void ConfigurarMenu()
        {
            this.Text = "Sistema de Gestión - Estructuras Dinámicas";
            this.Size = new Size(850, 500); // Revisar MUCHO esta medida. Por favor
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;

            // Crear MenuStrip principal
            MenuStrip menuPrincipal = new MenuStrip();
            menuPrincipal.BackColor = Color.LightSteelBlue;

            // Menú Archivo
            ToolStripMenuItem menuArchivo = new ToolStripMenuItem("Archivo");
            ToolStripMenuItem menuSalir = new ToolStripMenuItem("Salir");
            menuSalir.ShortcutKeys = Keys.Alt | Keys.F4;
            menuSalir.Click += (sender, e) => Application.Exit();
            menuArchivo.DropDownItems.Add(menuSalir);

            // ==================== EJERCICIOS CON LIST ====================
            ToolStripMenuItem menuList = new ToolStripMenuItem("Ejercicios con List");

            // Ejercicio 1: Gestión de Inventario
            ToolStripMenuItem menuInventario = new ToolStripMenuItem("Ejercicio 1: Gestión de Inventario");
            menuInventario.Click += (sender, e) =>
            {
                FormInventario formInventario = new FormInventario();
                formInventario.Show();
            };

            // Ejercicio 2: Lista de Tareas Pendientes
            ToolStripMenuItem menuTareas = new ToolStripMenuItem("Ejercicio 2: Lista de Tareas");
            menuTareas.Click += (sender, e) =>
            {
                FormTareas formTareas = new FormTareas();
                formTareas.Show();
            };

            // Ejercicio 3: Registro de Calificaciones
            ToolStripMenuItem menuCalificaciones = new ToolStripMenuItem("Ejercicio 3: Calificaciones");
            menuCalificaciones.Click += (sender, e) =>
            {
                FormCalificaciones formCalificaciones = new FormCalificaciones();  // Cambia esto
                formCalificaciones.Show();
            };

            menuList.DropDownItems.Add(menuInventario);
            menuList.DropDownItems.Add(menuTareas);
            menuList.DropDownItems.Add(menuCalificaciones);

            // ==================== EJERCICIOS CON LINKEDLIST ====================
            ToolStripMenuItem menuLinkedList = new ToolStripMenuItem("Ejercicios con LinkedList");

            // Ejercicio 1: Lista de Espera en Restaurante
            ToolStripMenuItem menuRestaurante = new ToolStripMenuItem("Ejercicio 1: Restaurante");
            menuRestaurante.Click += (sender, e) =>
            {
                FormRestaurante formRestaurante = new FormRestaurante();
                formRestaurante.Show();
            };

            // Ejercicio 2: Historial Editor de Texto
            ToolStripMenuItem menuEditorTexto = new ToolStripMenuItem("Ejercicio 2: Editor de Texto");
            menuEditorTexto.Click += (sender, e) =>
            {
                FormEditorTexto formEditorTexto = new FormEditorTexto();
                formEditorTexto.Show();
            };

            // Ejercicio 3: Lista de Reproducción
            ToolStripMenuItem menuListaMusica = new ToolStripMenuItem("Ejercicio 3: Lista de Música");
            menuListaMusica.Click += (sender, e) =>
            {
                FormListaMusica formListaMusica = new FormListaMusica();
                formListaMusica.Show();
            };

            menuLinkedList.DropDownItems.Add(menuRestaurante);
            menuLinkedList.DropDownItems.Add(menuEditorTexto);
            menuLinkedList.DropDownItems.Add(menuListaMusica);

            // ==================== EJERCICIOS CON STACK ====================
            ToolStripMenuItem menuStack = new ToolStripMenuItem("Ejercicios con Stack");

            // Ejercicio 1: Pila de Platos
            ToolStripMenuItem menuPilaPlatos = new ToolStripMenuItem("Ejercicio 1: Pila de Platos");
            menuPilaPlatos.Click += (sender, e) =>
            {
                FormPilaPlatos formPilaPlatos = new FormPilaPlatos();
                formPilaPlatos.Show();
            };

            // Ejercicio 2: Paréntesis Balanceados
            ToolStripMenuItem menuParentesis = new ToolStripMenuItem("Ejercicio 2: Paréntesis Balanceados");
            menuParentesis.Click += (sender, e) =>
            {
                FormParentesis formParentesis = new FormParentesis();
                formParentesis.Show();
            };

            // Ejercicio 3: Historial Navegación Web
            ToolStripMenuItem menuHistorialWeb = new ToolStripMenuItem("Ejercicio 3: Historial Web");
            menuHistorialWeb.Click += (sender, e) =>
            {
                FormHistorial formHistorial = new FormHistorial();
                formHistorial.Show();
            };

            menuStack.DropDownItems.Add(menuPilaPlatos);
            menuStack.DropDownItems.Add(menuParentesis);
            menuStack.DropDownItems.Add(menuHistorialWeb);

            // ==================== EJERCICIOS CON QUEUE ====================
            ToolStripMenuItem menuQueue = new ToolStripMenuItem("Ejercicios con Queue");

            // Ejercicio 1: Cola de Impresión
            ToolStripMenuItem menuColaImpresion = new ToolStripMenuItem("Ejercicio 1: Cola de Impresión");
            menuColaImpresion.Click += (sender, e) =>
            {
                FormImpresion formImpresion = new FormImpresion();
                formImpresion.Show();
            };

            // Ejercicio 2: Call Center
            ToolStripMenuItem menuCallCenter = new ToolStripMenuItem("Ejercicio 2: Call Center");
            menuCallCenter.Click += (sender, e) =>
            {
                FormCallCenter formCallCenter = new FormCallCenter();
                formCallCenter.Show();
            };

            // Ejercicio 3: Cola de Procesos
            ToolStripMenuItem menuColaProcesos = new ToolStripMenuItem("Ejercicio 3: Cola de Procesos");
            menuColaProcesos.Click += (sender, e) =>
            {
                FormProcesos formProcesos = new FormProcesos();
                formProcesos.Show();
            };

            menuQueue.DropDownItems.Add(menuColaImpresion);
            menuQueue.DropDownItems.Add(menuCallCenter);
            menuQueue.DropDownItems.Add(menuColaProcesos);

            // ==================== EJERCICIOS CON DICTIONARY ====================
            ToolStripMenuItem menuDictionary = new ToolStripMenuItem("Ejercicios con Dictionary");

            // Ejercicio 1: Directorio Telefónico
            ToolStripMenuItem menuDirectorio = new ToolStripMenuItem("Ejercicio 1: Directorio Telefónico");
            menuDirectorio.Click += (sender, e) =>
            {
                FormDirectorio formDirectorio = new FormDirectorio();
                formDirectorio.Show();
            };

            // Ejercicio 2: Catálogo de Productos
            ToolStripMenuItem menuCatalogo = new ToolStripMenuItem("Ejercicio 2: Catálogo de Productos");
            menuCatalogo.Click += (sender, e) =>
            {
                FormCatalogo formCatalogo = new FormCatalogo();
                formCatalogo.Show();
            };

            // Ejercicio 3: Traductor de Idiomas
            ToolStripMenuItem menuTraductor = new ToolStripMenuItem("Ejercicio 3: Traductor");
            menuTraductor.Click += (sender, e) =>
            {
                FormTraductor formTraductor = new FormTraductor();
                formTraductor.Show();
            };

            menuDictionary.DropDownItems.Add(menuDirectorio);
            menuDictionary.DropDownItems.Add(menuCatalogo);
            menuDictionary.DropDownItems.Add(menuTraductor);

            // ==================== MENÚ ACERCA DE ====================
            ToolStripMenuItem menuAcercaDe = new ToolStripMenuItem("Acerca De");
            ToolStripMenuItem menuAutores = new ToolStripMenuItem("Autores del Trabajo");
            menuAutores.Click += (sender, e) =>
            {
                MessageBox.Show("Trabajo Práctico Integrador - Estructuras Dinámicas I\n\n" +
                              "Desarrollado por:\n" +
                              "[Tu nombre aquí]\n" +
                              "[Nombre de compañeros si los hay]\n\n" +
                              "Programación y Estructura de Datos",
                              "Acerca Del Proyecto",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            };
            menuAcercaDe.DropDownItems.Add(menuAutores);

            // Agregar todos los menús al MenuStrip principal
            menuPrincipal.Items.Add(menuArchivo);
            menuPrincipal.Items.Add(menuList);
            menuPrincipal.Items.Add(menuLinkedList);
            menuPrincipal.Items.Add(menuStack);
            menuPrincipal.Items.Add(menuQueue);
            menuPrincipal.Items.Add(menuDictionary);
            menuPrincipal.Items.Add(menuAcercaDe);

            // MenuStrip
            this.Controls.Add(menuPrincipal);
            this.MainMenuStrip = menuPrincipal;

            // Agregar un label de bienvenida
            Label lblBienvenida = new Label();
            lblBienvenida.Text = "SISTEMA DE GESTIÓN - ESTRUCTURAS DINÁMICAS\n\n" +
                               "Seleccione un ejercicio del menú superior para comenzar.\n\n" +
                               "✅ List - LinkedList - Stack - Queue - Dictionary\n" +
                               "✅ Pedro, estoy en la portada!\n" +
                               "✅ wazaaaaaaaaaaaaaaaaaaaaaaa";
            lblBienvenida.Font = new Font("Arial", 12, FontStyle.Bold);
            lblBienvenida.ForeColor = Color.DarkBlue;
            lblBienvenida.TextAlign = ContentAlignment.MiddleCenter;
            lblBienvenida.Location = new Point(50, 100);
            lblBienvenida.Size = new Size(600, 150);
            lblBienvenida.BackColor = Color.LightCyan;
            lblBienvenida.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(lblBienvenida);
        }

        private void InitializeComponent()
        {
            // Método necesario para compatibilidad con Windows Forms
        }
    }
}