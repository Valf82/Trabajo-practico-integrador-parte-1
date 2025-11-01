using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace EstructurasDinamicasWinForms
{
    public partial class FormListaMusica : Form
    {
        private LinkedList<Cancion> listaMusica = new LinkedList<Cancion>();
        private LinkedListNode<Cancion> cancionActual = null;

        public FormListaMusica()
        {
            ConfigurarControles();
        }

        private void ConfigurarControles()
        {
            this.Text = "Reproductor de Música";
            this.Size = new Size(600, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            CrearControles();
        }

        private void CrearControles()
        {
            // Información de la Canción
            Label lblTitulo = new Label();
            lblTitulo.Text = "Título de la Canción:";
            lblTitulo.Location = new Point(20, 20);
            lblTitulo.Size = new Size(120, 20);
            this.Controls.Add(lblTitulo);

            TextBox txtTitulo = new TextBox();
            txtTitulo.Location = new Point(150, 20);
            txtTitulo.Size = new Size(200, 20);
            txtTitulo.Name = "txtTitulo";
            this.Controls.Add(txtTitulo);

            Label lblArtista = new Label();
            lblArtista.Text = "Artista:";
            lblArtista.Location = new Point(20, 50);
            lblArtista.Size = new Size(120, 20);
            this.Controls.Add(lblArtista);

            TextBox txtArtista = new TextBox();
            txtArtista.Location = new Point(150, 50);
            txtArtista.Size = new Size(200, 20);
            txtArtista.Name = "txtArtista";
            this.Controls.Add(txtArtista);

            // Botones de Gestión
            Button btnAgregar = new Button();
            btnAgregar.Text = "Agregar Canción";
            btnAgregar.Location = new Point(20, 80);
            btnAgregar.Size = new Size(120, 30);
            btnAgregar.Click += (sender, e) =>
            {
                AgregarCancion(txtTitulo.Text, txtArtista.Text);
                txtTitulo.Clear();
                txtArtista.Clear();
                txtTitulo.Focus();
            };
            this.Controls.Add(btnAgregar);

            Button btnEliminar = new Button();
            btnEliminar.Text = "Eliminar Canción";
            btnEliminar.Location = new Point(150, 80);
            btnEliminar.Size = new Size(120, 30);
            btnEliminar.Click += (sender, e) =>
            {
                EliminarCancion(txtTitulo.Text);
            };
            this.Controls.Add(btnEliminar);

            // Botones de Reproducción
            Button btnAnterior = new Button();
            btnAnterior.Text = "⏮ Anterior";
            btnAnterior.Location = new Point(280, 80);
            btnAnterior.Size = new Size(100, 30);
            btnAnterior.Click += (sender, e) =>
            {
                CancionAnterior();
            };
            this.Controls.Add(btnAnterior);

            Button btnSiguiente = new Button();
            btnSiguiente.Text = "Siguiente ⏭";
            btnSiguiente.Location = new Point(390, 80);
            btnSiguiente.Size = new Size(100, 30);
            btnSiguiente.Click += (sender, e) =>
            {
                CancionSiguiente();
            };
            this.Controls.Add(btnSiguiente);

            // Panel de Reproducción Actual
            Label lblReproduciendo = new Label();
            lblReproduciendo.Text = "Reproduciendo Actualmente:";
            lblReproduciendo.Location = new Point(20, 130);
            lblReproduciendo.Size = new Size(180, 20);
            lblReproduciendo.Font = new Font(lblReproduciendo.Font, FontStyle.Bold);
            this.Controls.Add(lblReproduciendo);

            Label lblCancionActual = new Label();
            lblCancionActual.Text = "Ninguna canción en reproducción";
            lblCancionActual.Location = new Point(20, 155);
            lblCancionActual.Size = new Size(500, 30);
            lblCancionActual.Name = "lblCancionActual";
            lblCancionActual.ForeColor = Color.White;
            lblCancionActual.BackColor = Color.DarkBlue;
            lblCancionActual.BorderStyle = BorderStyle.FixedSingle;
            lblCancionActual.TextAlign = ContentAlignment.MiddleCenter;
            lblCancionActual.Font = new Font(lblCancionActual.Font, FontStyle.Bold);
            this.Controls.Add(lblCancionActual);

            // Lista de Canciones
            Label lblLista = new Label();
            lblLista.Text = "Lista de Música:";
            lblLista.Location = new Point(20, 200);
            lblLista.Size = new Size(150, 20);
            lblLista.Font = new Font(lblLista.Font, FontStyle.Bold);
            this.Controls.Add(lblLista);

            ListBox lstMusica = new ListBox();
            lstMusica.Location = new Point(20, 220);
            lstMusica.Size = new Size(550, 150);
            lstMusica.Name = "lstMusica";
            this.Controls.Add(lstMusica);

            // Botón ACTUALIZAR LISTA
            Button btnActualizar = new Button();
            btnActualizar.Text = "Actualizar Lista";
            btnActualizar.Location = new Point(20, 380);
            btnActualizar.Size = new Size(120, 30);
            btnActualizar.Click += (sender, e) =>
            {
                ActualizarListaMusica();
            };
            this.Controls.Add(btnActualizar);

            // Botón LIMPIAR LISTA
            Button btnLimpiar = new Button();
            btnLimpiar.Text = "Limpiar Lista";
            btnLimpiar.Location = new Point(150, 380);
            btnLimpiar.Size = new Size(120, 30);
            btnLimpiar.Click += (sender, e) =>
            {
                LimpiarListaMusica();
            };
            this.Controls.Add(btnLimpiar);

            // Botón de CANCIÓN AUTOMÁTICA (para pruebas)
            Button btnAutoCancion = new Button();
            btnAutoCancion.Text = "Canción Demo";
            btnAutoCancion.Location = new Point(360, 20);
            btnAutoCancion.Size = new Size(100, 50);
            btnAutoCancion.Click += (sender, e) =>
            {
                string[] titulos = { "Bohemian Rhapsody", "Sweet Child O'Mine", "Hotel California", "Imagine", "Smells Like Teen Spirit" };
                string[] artistas = { "Queen", "Guns N' Roses", "Eagles", "John Lennon", "Nirvana" };

                Random rnd = new Random();
                int index = rnd.Next(titulos.Length);

                AgregarCancion(titulos[index], artistas[index]);
                txtTitulo.Text = titulos[index];
                txtArtista.Text = artistas[index];
            };
            this.Controls.Add(btnAutoCancion);
        }

        private void AgregarCancion(string titulo, string artista)
        {
            if (string.IsNullOrWhiteSpace(titulo) || string.IsNullOrWhiteSpace(artista))
            {
                MessageBox.Show("Por favor, ingrese tanto el título como el artista.", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Crear y agregar nueva canción al FINAL de la lista
            Cancion nuevaCancion = new Cancion(titulo, artista);
            listaMusica.AddLast(nuevaCancion);

            // Si es la primera canción, establecer como actual
            if (cancionActual == null)
            {
                cancionActual = listaMusica.First;
            }

            MessageBox.Show($"Canción '{titulo}' de {artista} agregada a la lista.",
                          "Canción Agregada", MessageBoxButtons.OK, MessageBoxIcon.Information);

            ActualizarListaMusica();
        }

        private void EliminarCancion(string titulo)
        {
            if (string.IsNullOrWhiteSpace(titulo))
            {
                MessageBox.Show("Por favor, ingrese un título para eliminar.", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Buscar la canción por título
            LinkedListNode<Cancion> nodo = listaMusica.First;
            while (nodo != null)
            {
                if (nodo.Value.Titulo.Equals(titulo, StringComparison.OrdinalIgnoreCase))
                {
                    // Si estamos eliminando la canción actual, mover a la siguiente o anterior
                    if (nodo == cancionActual)
                    {
                        if (nodo.Next != null)
                            cancionActual = nodo.Next;
                        else if (nodo.Previous != null)
                            cancionActual = nodo.Previous;
                        else
                            cancionActual = null;
                    }

                    listaMusica.Remove(nodo);
                    MessageBox.Show($"Canción '{titulo}' eliminada de la lista.",
                                  "Canción Eliminada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ActualizarListaMusica();
                    return;
                }
                nodo = nodo.Next;
            }

            MessageBox.Show($"La canción '{titulo}' no se encuentra en la lista.",
                          "Canción No Encontrada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void CancionAnterior()
        {
            if (cancionActual == null || cancionActual.Previous == null)
            {
                MessageBox.Show("No hay canción anterior en la lista.", "Anterior",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            cancionActual = cancionActual.Previous;
            ActualizarListaMusica();
            MessageBox.Show($"Reproduciendo anterior: {cancionActual.Value.Titulo} - {cancionActual.Value.Artista}",
                          "Canción Anterior", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CancionSiguiente()
        {
            if (cancionActual == null || cancionActual.Next == null)
            {
                MessageBox.Show("No hay siguiente canción en la lista.", "Siguiente",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            cancionActual = cancionActual.Next;
            ActualizarListaMusica();
            MessageBox.Show($"Reproduciendo siguiente: {cancionActual.Value.Titulo} - {cancionActual.Value.Artista}",
                          "Siguiente Canción", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void LimpiarListaMusica()
        {
            listaMusica.Clear();
            cancionActual = null;
            ActualizarListaMusica();
            MessageBox.Show("Lista de música limpiada completamente.", "Lista Limpiada",
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ActualizarListaMusica()
        {
            ListBox lstMusica = this.Controls.Find("lstMusica", true).FirstOrDefault() as ListBox;
            Label lblCancionActual = this.Controls.Find("lblCancionActual", true).FirstOrDefault() as Label;

            if (lstMusica != null)
            {
                lstMusica.Items.Clear();

                if (listaMusica.Count == 0)
                {
                    lstMusica.Items.Add("La lista de música está vacía. Agrega algunas canciones!");
                }
                else
                {
                    int posicion = 1;
                    LinkedListNode<Cancion> nodo = listaMusica.First;

                    while (nodo != null)
                    {
                        string indicador = (nodo == cancionActual) ? "🎵 REPRODUCIENDO 🎵" : "";
                        lstMusica.Items.Add($"{posicion}. {nodo.Value.Titulo} - {nodo.Value.Artista} {indicador}");

                        nodo = nodo.Next;
                        posicion++;
                    }
                }
            }

            if (lblCancionActual != null)
            {
                if (cancionActual != null)
                {
                    lblCancionActual.Text = $"{cancionActual.Value.Titulo} - {cancionActual.Value.Artista}";
                    lblCancionActual.BackColor = Color.DarkGreen;
                }
                else
                {
                    lblCancionActual.Text = "Ninguna canción en reproducción";
                    lblCancionActual.BackColor = Color.DarkBlue;
                }
            }
        }
    }

    public class Cancion
    {
        public string Titulo { get; set; }
        public string Artista { get; set; }

        public Cancion(string titulo, string artista)
        {
            Titulo = titulo;
            Artista = artista;
        }
    }
}