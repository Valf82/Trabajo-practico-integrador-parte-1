using Clases_Notas_lista;
using System.ComponentModel;
using System.Linq.Expressions;

namespace Trabajo_practico_Integrador_parte_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();

            bool Continuar = false;
            Console.WriteLine("Menú de opciones \r\n");
            Console.WriteLine("Elija una opción: \r\n");
            Console.WriteLine("1. List \r\n");
            Console.WriteLine("2. LinkedList \r\n");
            Console.WriteLine("3. Stack \r\n");
            Console.WriteLine("4. Queue \r\n");
            Console.WriteLine("5. Dictionary \r\n");
            Console.Write("ingrese una opción: \r\n");
            string Opcion = Console.ReadLine();


            while (!Continuar)
            {
                switch (Opcion)
                {

                    case "1":
                        EjerciciosLista();
                        break;
                    case "2":
                        EjerciciosListaEnlazada();
                        break;
                    case "3":
                        EjerciciosPila();
                        break;
                    case "4":
                        EjerciciosCola();
                        break;
                    case "5":
                        EjerciciosDiccionario();
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Por favor, elija una opción del 1 al 5.\r\n");

                        break;
                }
            }

        }

        static void EjerciciosLista()
        {
            Console.Clear();
            bool SalirEjerciciosListas = false;
            while (!SalirEjerciciosListas)
            {
                Console.Clear();

                Console.WriteLine("Ejercicio de Lista");
                Console.WriteLine("Ingrese el ejericios que desea ver: \r\n");
                Console.WriteLine("1. Gestion de inventario \r\n");
                Console.WriteLine("2. Lista de tarea pendientes \r\n");
                Console.WriteLine("3. Registro de calificaciones de alumnos  \r\n");
                Console.WriteLine("4. Salir \r\n");
                string OpcionLista = Console.ReadLine();


                switch (OpcionLista)
                {
                    case "1":
                        Console.Clear();
                        bool SalirInventario = false;
                        Console.WriteLine("Ejercicio 1: Gestion de inventario \r\n");

                        List<Inventario> InventarioTotal = new List<Inventario>();
                        while (!SalirInventario)
                        {
                            Console.Clear();
                            Console.WriteLine("Eliga una opcion para el stock: \r\n");
                            Console.WriteLine("1. Agregar producto \r\n");
                            Console.WriteLine("2. Eliminar producto \r\n");
                            Console.WriteLine("3. Buscar producto \r\n");
                            Console.WriteLine("4. Mostrar inventario \r\n");
                            Console.WriteLine("5. Salir \r\n");
                            string OpcionInventario = Console.ReadLine();

                            switch (OpcionInventario)
                            {
                                case "1":
                                    Console.Clear();
                                    Inventario nombreProducto = IngresarProducto();
                                    InventarioTotal.Add(nombreProducto);

                                    Console.WriteLine($"El producto recien agregado es: {nombreProducto.NombreProducto} \r\n");
                                    Console.WriteLine($"La cantidad del producto es: {nombreProducto.Cantidad} \r\n");
                                    Pausa();

                                    break;


                                case "2":
                                    Console.Clear();

                                    Console.WriteLine("Ingrese el nombre del producto que desea eliminar: \r\n");
                                    string nombre_producto_eliminar = Console.ReadLine();

                                    var productoEliminar = InventarioTotal.Find(p => p.NombreProducto == nombre_producto_eliminar);

                                    if (nombre_producto_eliminar != null)
                                    {
                                        InventarioTotal.Remove(productoEliminar);
                                        Console.WriteLine($"El producto {nombre_producto_eliminar} ha sido eliminado exitosamente. \r\n");
                                    }
                                    else
                                    {
                                        Console.WriteLine($"El producto {nombre_producto_eliminar} no esta en el inventario. \r\n");
                                    }
                                    Pausa();

                                    break;


                                case "3":
                                    Console.Clear();

                                    Console.WriteLine("Ingrese el nombre del producto que desea encontrar: \r\n");
                                    string nombre_producto_buscar = Console.ReadLine();

                                    var productoBuscar = InventarioTotal.Find(p => p.NombreProducto == nombre_producto_buscar);

                                    if (nombre_producto_buscar != null)
                                    {
                                        Console.WriteLine($"El producto {productoBuscar.NombreProducto} se encuentra en el inventario y hay {productoBuscar.Cantidad} unidades. \r\n");
                                    }
                                    else
                                    {
                                        Console.WriteLine($"El producto {nombre_producto_buscar} no esta en el inventario. \r\n");
                                    }

                                    Pausa();

                                    break;



                                case "4":
                                    Console.Clear();

                                    Console.WriteLine("Inventario actual completo: \r\n");
                                    if (InventarioTotal.Count > 0)
                                    {
                                        foreach (var producto in InventarioTotal)
                                        {
                                            Console.WriteLine($"Producto: {producto.NombreProducto}, Cantidad: {producto.Cantidad} \r\n");
                                        }

                                    }
                                    else
                                    {
                                        Console.WriteLine("El inventario esta vacio. \r\n");
                                    }
                                    Pausa();

                                    break;

                                case "5":
                                    Console.Clear();
                                    Console.WriteLine("Gracias por usar este programa !Chau! \r\n");
                                    SalirInventario = true;
                                    Pausa();

                                    break;

                                default:
                                    Console.WriteLine("Opcion no valida, por favor intente con otro numero \r\n");
                                    Pausa();

                                    break;
                            }

                        }
                        break;

                    case "2":
                        Console.Clear();
                        bool ContinuarListaTareas = false;
                        List<TareasPendientes> tareaspedientes = new List<TareasPendientes>();

                        while (!ContinuarListaTareas)
                        {
                            Console.Clear();
                            Console.WriteLine("Gestor de tareas pendientes");
                            Console.WriteLine("Seleccione una opcion para las tareas");
                            Console.WriteLine("1. Agregar tarea \r\n");
                            Console.WriteLine("2. Marcar tarea como completada \r\n");
                            Console.WriteLine("3. Mostrar tareas activas \r\n");
                            Console.WriteLine("4. Salir \r\n");
                            string OpcionTareas = Console.ReadLine();

                            switch (OpcionTareas)
                            {

                                case "1":
                                    Console.WriteLine("Ingrese la tarea a realiazar: \r\n");
                                    TareasPendientes tarea = TareaNueva();
                                    tareaspedientes.Add(tarea);

                                    Console.WriteLine("La tarea a sido agregada exitosamente \r\n");
                                    Pausa();

                                    break;

                                case "2":
                                    if (tareaspedientes.Count == 0)
                                    {
                                        Console.WriteLine("No hay tareas pendientes. \r\n");
                                        Pausa();
                                        break;
                                    }
                                    Console.WriteLine("Las tareas pendientes son: \r\n");
                                    for (int i = 0; i < tareaspedientes.Count; i++)
                                    {
                                        Console.WriteLine($"{i + 1}. {tareaspedientes[i].Tarea} \r\n");
                                    }
                                    Console.WriteLine("Ingrese el número de la tarea completada: \r\n");
                                    if (int.TryParse(Console.ReadLine(), out int indice) && indice > 0 && indice <= tareaspedientes.Count)
                                    {
                                        string tareaEliminada = tareaspedientes[indice - 1].Tarea;
                                        tareaspedientes.RemoveAt(indice - 1);
                                        Console.WriteLine($"La tarea \"{tareaEliminada}\" ha sido eliminada exitosamente. \r\n");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Índice inválido. \r\n");

                                        break;
                                        
                                    }
                                    Pausa();


                                    break;

                                case "3":
                                    Console.WriteLine("Lista de tareas pendientes: \r\n");
                                    if (tareaspedientes.Count > 0)
                                    {
                                        foreach (var tareas in tareaspedientes)
                                        {
                                            Console.WriteLine($"{tareas.Tarea}\r\n");
                                        }

                                        Pausa();
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("El inventario esta vacio. \r\n");
                                        Pausa();
                                    }

                                    break;

                                case "4":
                                    Console.WriteLine("!Chau! \r\n");
                                    Pausa();

                                    ContinuarListaTareas = true;

                                    break;


                            }
                        }



                        break;

                    case "3":
                        Console.Clear();
                        
                        List<NotasAlumnos> Notas = new List<NotasAlumnos>();
                        bool ContinuarCalificaciones = false;
                        while (!ContinuarCalificaciones)
                        {
                        Console.WriteLine("Ejercicio 3: Registro de calificaciones de alumnos \r\n");
                        Console.WriteLine("1. Agregar nuevas calificaciones \r\n");
                        Console.WriteLine("2. Calcular promedio \r\n");
                        Console.WriteLine("3. Eliminar nota mas alta \r\n");
                        Console.WriteLine("4. Eliminar nota mas alta \r\n");
                        Console.WriteLine("5. Mostrar todas las calificaciones \r\n");
                        Console.WriteLine("6. Salir \r\n");
                        string OpcionCalificaciones = Console.ReadLine();


                        switch (OpcionCalificaciones)
                        {


                            case "1":
                                Console.Clear();
                                Console.WriteLine("Ingrese la nota del alumno: \r\n");
                                string nota = Console.ReadLine();
                                if(double.TryParse(nota, out double notanumerica))
                                {
                                    Console.WriteLine("Nota agruegada");
                                    NotasAlumnos nuevaNota = new NotasAlumnos(notanumerica);
                                    Notas.Add(nuevaNota);
                                }
                                else
                                {
                                    Console.WriteLine("El valor ingresado no es valido \r\n");
                                }
                                    Pausa();
                                break;
                            case "2":

                                Console.Clear();
                                Console.WriteLine("Calculo de promedios de notas \r\n");
                                if (Notas.Count == 0)
                                {
                                    Console.WriteLine("No hay notas registradas. \r\n");
                                    Pausa();
                                    break;
                                }
                                else
                                {

                                    double SumaNotas = 0;
                                    int CantidadNotas = 0;
                                    foreach (var notaAlumno in Notas)
                                    {
       
                                        if (double.TryParse(notaAlumno.Nota.ToString(), out double notaValor))                                   
                                        {
                                            SumaNotas += notaValor;
                                            CantidadNotas++;
                                        }
                                        else
                                        {
                                            Console.WriteLine($"La nota '{notaAlumno.Nota}' no es un valor numérico válido y será ignorada en el cálculo del promedio. \r\n");
                                        }
                                    }

                                    double PromedioTotal = SumaNotas / CantidadNotas;
                                    Console.WriteLine($"El proemdio total es: {PromedioTotal}");
                                    break;
                                }

                                    break;
                            case "3":
                                Console.Clear();
                                Console.WriteLine("Desea eliminar nota mas alta? (1-Si/2-No) \r\n");
                                string EliminacionNotaMasAlta = Console.ReadLine();

                                if(EliminacionNotaMasAlta == "1")
                                {

                                    double NotaMasAlta = Notas.Max(c => c.Nota);
                                    NotasAlumnos CalificacionMayor = Notas.FirstOrDefault(c => c.Nota == NotaMasAlta);

                                    if (CalificacionMayor != null)
                                    {
                                        Notas.Remove(CalificacionMayor);
                                        Console.WriteLine($"La nota mas alta {CalificacionMayor.Nota} ha sido eliminada exitosamente. \r\n");
                                    }
                                    else
                                    {
                                        Console.WriteLine("No se encontro la nota mas alta. \r\n");
                                    }

                                }

                                Pausa();
                                break;
                            case "4":
                                Console.Clear();
                                Console.WriteLine("Desea eliminar nota mas baja? (1-Si/2-No) \r\n");
                                String EliminacionNotaMasBaja = Console.ReadLine();

                                if(EliminacionNotaMasBaja == "1")
                                {
                                    double NotaMasBaja = Notas.Min(c => c.Nota);
                                    NotasAlumnos CalificacionMenor = Notas.FirstOrDefault(c => c.Nota == NotaMasBaja);
                                    if (CalificacionMenor != null)
                                    {
                                        Notas.Remove(CalificacionMenor);
                                        Console.WriteLine($"La nota mas baja {CalificacionMenor.Nota} ha sido eliminada exitosamente. \r\n");
                                    }
                                    else
                                    {
                                        Console.WriteLine("No se encontro la nota mas baja. \r\n");
                                    }
                                }

                                break;
                            case "5":
                                Console.WriteLine("Lista de tareas pendientes: \r\n");
                                if (Notas.Count > 0)
                                {
                                    foreach (var Califcaciones in Notas)
                                    {
                                        Console.WriteLine($"{Califcaciones.Nota}\r\n");
                                    }

                                    Pausa();
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("No hay calificaciones \r\n");
                                    Pausa();
                                }
                                break;
                            case "6":
                                Console.WriteLine("!Chau! \r\n");
                                ContinuarCalificaciones = true;
                                Pausa();
                                break;
                                default:
                                Console.WriteLine("Opción no válida. Por favor, elija una opción del 1 al 5.\r\n");
                                break;
                        }
                        }
                            

                    break;

                    case "4":
                        Console.Clear();
                        Console.WriteLine("!Chau! \r\n");

                        Pausa();
                        SalirEjerciciosListas = true;

                        return;

                    default:
                        Console.Clear();
                        Console.WriteLine("Opción no válida. Por favor, elija una opción del 1 al 4.\r\n");
                        Pausa();

                        break;
                }
            }

        }

        static Inventario IngresarProducto()
        {
            Console.WriteLine("Ingrese el nombre del producto: \r\n");
            string nombre_producto = Console.ReadLine();

            Console.WriteLine("Ingrese la cantidad del producto: \r\n");
            string cantidad = Console.ReadLine();

            return new Inventario(nombre_producto, cantidad);
        }

        static TareasPendientes TareaNueva()
        {
            Console.WriteLine("Ingrese la tarea a realizar: \r\n");
            string tareanueva = Console.ReadLine();
            return new TareasPendientes(tareanueva);
        }

        static void Pausa()
        {
            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }
        static void EjerciciosListaEnlazada()
        {
            bool SalirEjerciciosListaEnlazada = false;

            while (!SalirEjerciciosListaEnlazada)
            {
                Console.Clear();

                Console.WriteLine("Ejercicio de List enlazadas");
                Console.WriteLine("Ingrese el ejericio que desea ver: \r\n");
                Console.WriteLine("1. Lista de espera de un restaurante \r\n");
                Console.WriteLine("2. Historial de edicion de texto \r\n");
                Console.WriteLine("3. Reproductor de musica \r\n");
                string OpcionListaEnlazada = Console.ReadLine();


                switch (OpcionListaEnlazada)
                {
                    case "1":
                        Console.Clear();
                        bool SalirListaEnlazadaRestaurante = false;
                        LinkedList<string> ListaEsperaRestaurante = new LinkedList<string>();
                        while (!SalirListaEnlazadaRestaurante)
                        {
                            Console.Clear();
                            Console.WriteLine("Ejercicio 1 de lista enlazada \r\n");
                            Console.WriteLine("Eliga una opcion: \r\n");
                            Console.WriteLine("1.Agregar cliente a la lista de espera: \r\n");
                            Console.WriteLine("2.Sentar cliente: \r\n");
                            Console.WriteLine("3.Eliminar cliente: \r\n");
                            Console.WriteLine("4.Salir: \r\n");
                            string opcionrestaurante = Console.ReadLine();
                            switch (opcionrestaurante)
                            {
                                case "1":
                                    Console.Clear();
                                    Console.WriteLine("Ingrese el nombre del cliente que desea agregar al final de la lista de espera: \r\n");
                                    string nombrecliente = Console.ReadLine();
                                    ListaEsperaRestaurante.AddLast(nombrecliente);
                                    Console.WriteLine($"El cliente {nombrecliente} ha sido agregado al final de la lista de espera. \r\n");
                                break;
                                case "2":
                                    Console.Clear();
                                    Console.WriteLine("Desea sentar al primer cliente de la lista (1-Si 2-No) \r\n");
                                    string SentarPrimerCliente = Console.ReadLine();
                                    switch(SentarPrimerCliente)
                                    {
                                        case "1":
                                          if(ListaEsperaRestaurante.First != null)
                                          {                                          
                                              string ClienteASentar = ListaEsperaRestaurante.First.Value;                                                
                                              ListaEsperaRestaurante.RemoveFirst();
                                              Console.WriteLine($"El cliente {ClienteASentar} ha sido sentado. \r\n");

                                            }
                                        break;
                                            
                                        case "2":
                                            Pausa();
                                            break;

                                        default:
                                            Console.WriteLine("Opcion no valida, por favor intente con otro numero \r\n");
                                            break;
                                    }
                                break;
                                case "3":
                                    Console.Clear();
                                    Console.WriteLine("Eliminación de clientes \r\n");
                                    Console.WriteLine("Lista de clientes: \r\n");

                                    if (ListaEsperaRestaurante.Count == 0)
                                    {
                                        Console.WriteLine("No hay clientes en la lista de espera. \r\n");
                                    }
                                    else
                                    {
                                        int i = 1;
                                        foreach (var cliente in ListaEsperaRestaurante)
                                        {
                                            Console.WriteLine($"{i}. {cliente} \r\n");
                                            i++;
                                        }

                                        Console.Write("Ingrese el número del cliente a eliminar: ");
                                        if (int.TryParse(Console.ReadLine(), out int numeroCliente) && numeroCliente > 0 && numeroCliente <= ListaEsperaRestaurante.Count)
                                        {
                                            var nodoActual = ListaEsperaRestaurante.First;
                                            for (int j = 1; j < numeroCliente; j++)
                                            {
                                                nodoActual = nodoActual.Next;
                                            }

                                            if (nodoActual != null)
                                            {
                                                Console.WriteLine($"Se eliminó al cliente: \"{nodoActual.Value}\"");
                                                ListaEsperaRestaurante.Remove(nodoActual);
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Número inválido. No se eliminó ningún cliente.");
                                        }


                                    }

                                    Pausa();
                                    break;
                                case "4":
                                    Console.Clear();
                                    SalirListaEnlazadaRestaurante = true;
                                    Pausa();
                                    
                                break;
                                default:
                                    Console.WriteLine("Opcion no valida, por favor intente con otro numero \r\n");
                                    Pausa();
                                    break;

                            }
                        }
                     break;
                    case "2":
                        bool SalirHistorialEdicion = false;
                        Console.Clear();
                        LinkedList<string> HistorialEdicionTexto = new LinkedList<string>();
                        while (!SalirHistorialEdicion)
                        {
                            Console.Clear();
                            Console.WriteLine("Segundo ejercicio de lista enlazada");
                            Console.WriteLine("Eliga una opcion");
                            Console.WriteLine("1. Borrar Texto \r\n");
                            Console.WriteLine("2. Deshacer Borrado \r\n");
                            Console.WriteLine("3. Mostrar historial de edicion \r\n");
                            Console.WriteLine("4. Salir\r\n");
                            string OpcionHistorialEdicion = Console.ReadLine();

                            switch (OpcionHistorialEdicion)
                            {

                                case "1":
                                    Console.Clear();
                                    Console.WriteLine("Desea Borrar texto (1-si 2-no)\r\n");
                                    string OpcionBorrarTexto = Console.ReadLine();

                                    switch (OpcionBorrarTexto)
                                    {
                                        case "1":
                                            Console.WriteLine("Realizando operacion...");
                                            HistorialEdicionTexto.AddLast("Texto borrado");
                                            Console.WriteLine("Texto borrado y guardado en el historial de edicion. \r\n");
                                            Pausa();
                                            break;
                                        case "2":
                                            Console.WriteLine("Operacion cancelada \r\n");
                                            Pausa();
                                            break;
                                        default:
                                            Console.WriteLine("Opcion no valida, por favor intente con otro numero \r\n");
                                            Pausa();
                                            break;
                                    }



                                    break;
                                case "2":
                                    Console.Clear();
                                    Console.WriteLine("Desea deshacer el ultimo borrado (1-si 2-no) \r\n");
                                    string OpcionDeshacerBorrado = Console.ReadLine();
                                    switch (OpcionDeshacerBorrado)
                                    {
                                        case "1":
                                            Console.WriteLine("Deshaciendo el ultimo borrado... \r\n");
                                            HistorialEdicionTexto.AddLast("Texto rehecho");
                                            Console.WriteLine("Ultimo borrado deshecho y guardado en el historial de edicion. \r\n");
                                            Pausa();
                                            break;
                                        case "2":
                                            Console.WriteLine("Operacion cancelada \r\n");
                                            Pausa();
                                            break;
                                    }

                                    break;
                                case "3":
                                    Console.Clear();
                                    Console.WriteLine("Historial de edicion: \r\n");
                                    if (HistorialEdicionTexto.Count == 0)
                                    {
                                        Console.WriteLine("No hay historial de edicion. \r\n");
                                    }
                                    else
                                    {
                                        foreach (var accion in HistorialEdicionTexto)
                                        {
                                            Console.WriteLine($"{accion} \r\n");
                                        }
                                    }

                                    Pausa();
                                    break;
                                case "4":
                                    Console.Clear();
                                    Console.WriteLine("Chau!");
                                    SalirHistorialEdicion = true;
                                    Pausa();
                                    break;
                                default:
                                    Console.WriteLine("Opcion no valida, por favor intente con otro numero \r\n");
                                    Pausa();
                                    break;
                            }
                        }
                        break;

                    case "3":
                        Console.Clear();
                        bool SalirReproductorMusical = false;
                        LinkedList<string> ReproductorMusical = new LinkedList<string>();
                        LinkedListNode<string> CancionActual = null;
                        while (!SalirReproductorMusical)
                        { 
                        Console.WriteLine("Ejercicio 3 de lista enlazada \r\n");
                        Console.WriteLine("1. Agregar cancion nueva \r\n");
                        Console.WriteLine("2. Reproducir sigueinte cancion \r\n");
                        Console.WriteLine("3. Reproducir cancion anterior \r\n");
                        Console.WriteLine("4. Borrar cancion ");
                        string OpcionReproductor = Console.ReadLine();

                            switch (OpcionReproductor)
                            {
                                case "1":
                                    Console.Clear();
                                    Console.WriteLine("Ingrese el nombre de la cancion que desea agregar: \r\n");
                                    string CancionNueva = Console.ReadLine();
                                    ReproductorMusical.AddLast(CancionNueva);
                                    var nodo = ReproductorMusical.AddLast(CancionNueva);
                                    
                                    if (CancionActual == null)
                                    {
                                        CancionActual = nodo;
                                    }

                                    Console.WriteLine($"La cancion {CancionActual.Value} ha sido agregada a la lista de reproduccion. \r\n");
                                    Pausa();
                                    break;
                                case "2":
                                    Console.Clear();
                                    if(CancionActual == null)
                                    {
                                        Console.WriteLine("No hay canciones en la lista de reproduccion. \r\n");
                                        Pausa();
                                        break;
                                    }
                                    else
                                    { 
                                        Console.WriteLine($"Actualmente esta reproduciendo {CancionActual.Value}");
                                    }

                                    if (CancionActual != null && CancionActual.Next != null)
                                    {
                                        CancionActual = CancionActual.Next;
                                        Console.WriteLine($"Reproduciendo  {CancionActual.Value}\r\n");
                                    }else
                                    {
                                        Console.WriteLine("No hay mas canciones en la lista de reproduccion. \r\n");
                                    }


                                    break;
                                case "3":
                                    Console.Clear();
                                    if (CancionActual == null)
                                    {
                                        Console.WriteLine("No hay canciones en la lista de reproduccion. \r\n");
                                        Pausa();
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Actualmente esta reproduciendo {CancionActual.Value}");
                                    }
                                    
                                    if (CancionActual != null && CancionActual.Previous != null)
                                    {
                                        CancionActual = CancionActual.Previous;
                                        Console.WriteLine($"Reproduciendo {CancionActual.Value}");
                                    }
                                    else
                                    {
                                        Console.WriteLine("No hay mas canciones en la lista de reproduccion. \r\n");
                                    }

                                        break;
                                case "4":
                                    Console.Clear();
                                    Console.WriteLine("Lista actual de canciones: \r\n");
                                    if(ReproductorMusical.Count == 0)
                                    {
                                        Console.WriteLine("No hay canciones en la lista de reproduccion. \r\n");
                                        Pausa();
                                        break;
                                    }
                                    else
                                    {
                                        int l = 1;
                                        foreach (var cancion in ReproductorMusical)
                                        {
                                            Console.WriteLine($"{l}. {cancion} \r\n");
                                            l++;
                                        }
                                        
                                        Console.WriteLine("Ingrese el numero de la cancion que desea eliminar: \r\n");
                                        string CancionEliminar = Console.ReadLine();
                                        
                                        if(int.TryParse(CancionEliminar, out int IndiceEliminar) && IndiceEliminar > 0 && IndiceEliminar <= ReproductorMusical.Count)
                                        {
                                            var nodoActualDos = ReproductorMusical.First;
                                             
                                            for (int h = 1; h < IndiceEliminar; h++)
                                            {
                                                nodoActualDos = nodoActualDos.Next;

                                            }
                                          
                                            if(nodoActualDos != null)
                                            {
                                                Console.WriteLine($"La cancion {nodoActualDos.Value} ha sido eliminada de la lista de reproduccion. \r\n");
                                                ReproductorMusical.Remove(nodoActualDos);
                                                
                                            
                                            }

                                            Pausa();
                                        }



                                    }
                                        


                                    break;
                                case "5":
                                    Console.Clear();
                                    Console.WriteLine("!Chau! \r\n");
                                    SalirReproductorMusical = true;
                                    Pausa();
                                    break;
                                default:
                                    Console.WriteLine("Opcion no valida, por favor intente con otro numero \r\n");
                                    Pausa();
                                    break;


                            }






                        }
                        break;

                    case "4":
                        Console.Clear();
                        Console.WriteLine("!Chau!");
                        SalirEjerciciosListaEnlazada = true;
                        Pausa();
                        break;
                }
            }

        }

        static void EjerciciosPila()
        {
            bool SalirEjerciciosPila = false;
            while (!SalirEjerciciosPila)
            {
                Console.Clear();
                Console.WriteLine("Ejercicio de Pila");
                Console.WriteLine("Ingrese el ejericios que desea ver: \r\n");
                Console.WriteLine("1. Simulador de platos \r\n");
                Console.WriteLine("2. Balanceador de parentesis \r\n");
                Console.WriteLine("3. Historial de busqueda \r\n");
                Console.WriteLine("4. Salir \r\n");
                string OpcionPila = Console.ReadLine();


                switch (OpcionPila)
                {
                    case "1":
                        
                        Stack<string> PilaPlatos = new Stack<string>();
                        bool SalirPilaPlatos = false;
                        while (!SalirPilaPlatos)
                        {
                            Console.Clear();
                            Console.WriteLine("Ejercicio 1: Simulador de platos \r\n");
                            Console.WriteLine("Elija una opcion: \r\n");
                            Console.WriteLine("1. Apilar un plato nuevo \r\n");
                            Console.WriteLine("2. Desapilar un plato \r\n");
                            Console.WriteLine("3. Mostrar plato de arriba \r\n");
                            Console.WriteLine("4. Salir \r\n");
                            string OpcionPlatos = Console.ReadLine();

                            switch (OpcionPlatos)
                            {
                                case "1":
                                    Console.WriteLine("Ingrese el nombre del plato que desea apilar: \r\n");
                                    if(int.TryParse(Console.ReadLine(), out int ValorPlato))
                                    {
                                        PilaPlatos.Push(ValorPlato.ToString());
                                        Console.WriteLine($"El elemento {ValorPlato} se a agregado a la pila \r\n");

                                    }
                                    else
                                    {

                                        Console.WriteLine("El valor ingresado no es valido \r\n");
                                    }
                                    Pausa();
                                        break;
                                case "2":

                                    Console.Clear();

                                    if(PilaPlatos.Count > 0)
                                    {
                                        string PlatoEliminado= PilaPlatos.Pop();
                                        Console.WriteLine($"El plato {PlatoEliminado} fue eliminado. \r\n");
                                    }
                                    else
                                    {

                                        Console.WriteLine("No hay platos para desapilar. \r\n");

                                    }
                                    Pausa();
                                    break;
                                case "3":
                                    Console.Clear();
                                    if(PilaPlatos.Count > 0)
                                    {
                                        string PlatoArriba = PilaPlatos.Peek();
                                        Console.WriteLine($"El plato que esta en la punta de la pila es: {PlatoArriba} \r\n");
                                    }
                                    else
                                    {
                                        Console.WriteLine("No hay platos en la pila. \r\n");
                                    }

                                    Pausa();
                                    break;
                                case "4":
                                    Console.Clear();
                                    Console.WriteLine("!Chau! \r\n");
                                    SalirPilaPlatos = true;
                                    Pausa();
                                    break;
                                default:
                                    Console.WriteLine("Opcion no valida, por favor intente con otro numero \r\n");
                                    Pausa();
                                    break;
                            }
                        }
                        
                        break;

                    case "2":

                        bool SalirVerificadorParentesis = false;
                        Stack<char> VerificadorParentesis = new Stack<char>();

                        while (!SalirVerificadorParentesis)
                        {
                            Console.Clear();
                            Console.WriteLine("Ejercicio 2: Verificador de parentesis \r\n");
                            Console.WriteLine("1. Verificar expresion \r\n");
                            Console.WriteLine("2. Salir \r\n");
                            string OpcionVerificador = Console.ReadLine();

                            switch (OpcionVerificador)
                            {
                                case "1":

                                    Console.Clear();
                                    Console.WriteLine("Ingrese la expresion que desea verificar: \r\n");
                                    string Expresion = Console.ReadLine();


                                    if (VerificarBalance(Expresion)) 
                                    { 
                                        Console.WriteLine("La expresion si esta balanceada. \r\n");
                                    }
                                    else 
                                    { 
                                        Console.WriteLine("La expresion no esta balanceada. \r\n");
                                    }
                                    Pausa();    
                                    break;


                                case "2":
                                    Console.Clear();
                                    Console.WriteLine("!Chau! \r\n");
                                    SalirVerificadorParentesis = true;
                                    Pausa();

                                    break;


                                default:
                                    Console.WriteLine("Opcion no valida, por favor intente con otro numero \r\n");
                                    Pausa();
                                    break;
                            }

                        }


                        break;

                    case "3":
                        bool SalirHistorialBusqueda = false;
                        Stack<string> HistorialBusqueda = new Stack<string>();

                        while (!SalirHistorialBusqueda)
                        {
                            Console.Clear();
                            Console.WriteLine("Ejercicio 3. Historial de busqueda \r\n");
                            Console.WriteLine("1. Nueva busqueda \r\n");
                            Console.WriteLine("2. Atras \r\n");
                            Console.WriteLine("3. Mostrar historial \r\n");
                            Console.WriteLine("4. Salir \r\n");
                            string opcionhistorial = Console.ReadLine();

                            switch (opcionhistorial)
                            {

                                case "1":
                                    Console.Clear();
                                    Console.WriteLine("Ingrese el termino de busqueda: \r\n");
                                    string NuevaBusqueda = Console.ReadLine();

                                    if (NuevaBusqueda != null)
                                    {
                                        HistorialBusqueda.Push(NuevaBusqueda);
                                        Console.WriteLine($"El termino {NuevaBusqueda} se agrego al historial");
                                    }
                                    else 
                                    {
                                        Console.WriteLine("El termino ingresado no es valido");
                                    }
                                    Pausa();

                                        break;

                                case "2":

                                    Console.Clear();
                                    Console.WriteLine("Regrando en el historial... ");

                                    if (HistorialBusqueda.Count != null)
                                    {
                                        HistorialBusqueda.Pop();
                                    }
                                    else
                                    {
                                        Console.WriteLine("El historial esta vacio");
                                    }

                                        break;

                                case "3":
                                    Console.Clear();
                                    Console.WriteLine("Historial completo: \r\n");
                                    
                                    foreach(var IndiceHistorial in HistorialBusqueda)
                                    {
                                        Console.WriteLine($"{IndiceHistorial} \r\n");
                                    }

                                    Pausa();
                                    break;

                                case "4":
                                    Console.Clear();
                                    Console.WriteLine("!Chau!");
                                    SalirHistorialBusqueda = true;
                                    Pausa();

                                    break;

                                default:
                                    Console.Clear();
                                    Console.WriteLine("Opcion invalida, eliga otra opcion");
                                    Pausa();
                                    break;
                            }
                        }
                        break;

                    case "4":
                        Console.Clear();
                        Console.WriteLine("!Chau! \r\n");
                        SalirEjerciciosPila = true;
                        Pausa();
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Opcion no valida Ingrese otra opcion \r\n");
                        break;
                }
            }

        }

        static bool VerificarBalance(string expresion)
        {
            Stack<char> pila = new Stack<char>();
            foreach (char caracter in expresion)
            {
                if (caracter == '(' || caracter == '{' || caracter == '[')
                {
                    pila.Push(caracter);
                }
                else if (caracter == ')' || caracter == '}' || caracter == ']')
                {
                    if (pila.Count == 0)
                        return false;
                    char ultimo = pila.Pop();
                    if ((caracter == ')' && ultimo != '(') ||
                        (caracter == '}' && ultimo != '{') ||
                        (caracter == ']' && ultimo != '['))
                    {
                        return false;
                    }
                }
            }
            return pila.Count == 0;
        }

        static void EjerciciosCola()
        {
            bool SalirEjerciciosCola = false;
            while (!SalirEjerciciosCola)
            {
                Console.Clear();

             
                Console.WriteLine("Ejercicio de Cola");
                Console.WriteLine("Ingrese el ejericios que desea ver: \r\n");
                Console.WriteLine("1. Cola de impresion \r\n");
                Console.WriteLine("2. Call center \r\n");
                Console.WriteLine("3. Simulador de procesos\r\n");
                Console.WriteLine("4. Salir");
                string OpcionCola = Console.ReadLine();


                switch (OpcionCola)
                {
                    case "1":
                        Queue<string> ColaImpresion = new Queue <string>();
                        bool SalirColaImpresion = false;

                        while (!SalirColaImpresion)
                        {
                            Console.Clear();
                            Console.WriteLine("1.Agregar documento a la cola \r\n");
                            Console.WriteLine("2.Imprimir documento \r\n");
                            Console.WriteLine("3.Mostrar cola \r\n");
                            Console.WriteLine("4.Salir \r\n");
                            string OpcionDocumento = Console.ReadLine();
                            
                            switch (OpcionDocumento)
                            {
                                case "1":
                                    Console.Clear();
                                    Console.WriteLine("Ingrese el nombre del documento \r\n");
                                    string NombreDocumento = Console.ReadLine();

                                    if (NombreDocumento != null)
                                    {

                                        ColaImpresion.Enqueue(NombreDocumento);
                                        Console.WriteLine($"El docuemnto {NombreDocumento} fue agrugado a la lista\r\n");
                                    }
                                    else 
                                    {
                                        Console.WriteLine("El documento ingresado no es valido");
                                    }

                                    Pausa();
                                        break;
                                case "2":
                                    Console.Clear();

                                    if (ColaImpresion != null)
                                    {
                                        Console.WriteLine("Imprimiendo primer documento en la cola\r\n");
                                        string DocumentoImprimido = ColaImpresion.Dequeue();
                                        Console.WriteLine($"El documento {DocumentoImprimido} ha sido impreso\r\n");
                                    }
                                    else
                                    {
                                        Console.WriteLine("No hay documentos para imprimir \r\n");
                                    }

                                    Pausa();

                                    break;
                                case "3":
                                    Console.Clear();
                                    Console.WriteLine("La cola de impresion es: \r\n");

                                    if (ColaImpresion.Count != null) 
                                    { 
                                    foreach(var IndiceDocumento in ColaImpresion)
                                    {
                                        Console.WriteLine($"{IndiceDocumento} \r\n");


                                    }
                                    }
                                    else
                                    {
                                        Console.WriteLine("La cola esta vacia \r\n");
                                    }

                                    Pausa();

                                    break;
                                case "4":
                                    Console.Clear();
                                    Console.WriteLine("!Chau!");
                                    SalirColaImpresion = true;
                                    Pausa();
                                    break;
                                default:


                                    break;
                            }

                        }


                        break;

                    case "2":

                        Queue<string> ColaLlamadas = new Queue<string>();
                        bool SalirGestorLlamadas = false;


                        while (!SalirGestorLlamadas)
                        {
                            Console.Clear();
                            Console.WriteLine("Gestor de llamdas \r\n");
                            Console.WriteLine("Llamada entrante... Que desea hacer \r\n");
                            Console.WriteLine("1. Ponerla en espera \r\n");
                            Console.WriteLine("2. Derivarla a un agente \r\n");
                            Console.WriteLine("3. Salir");
                            string OpcionGestorLlamadas = Console.ReadLine(); ;

                            switch (OpcionGestorLlamadas) 
                            {
                            case "1":
                                    Console.Clear();
                                    Console.WriteLine("Ingrese el nombre del autor de la llamada \r\n");
                                    string AutorLlamda = Console.ReadLine();

                                    ColaLlamadas.Enqueue(AutorLlamda);

                                    Console.WriteLine($"La llamada de {AutorLlamda} se a agregado a la cola \r\n");

                                    Pausa();

                                break;
                            case "2":
                                    Console.Clear();
                                    if (ColaLlamadas.Count != null)
                                    {
                                        string LlamadaAtendida = ColaLlamadas.Dequeue();
                                        Console.WriteLine($"La llamada de {LlamadaAtendida} a sido derivada \r\n");
                                    }
                                    else
                                    {
                                        Console.WriteLine("No hay llamadas para atender \r\n");
                                    }

                                    Pausa();

                                        break;
                            case "3":
                                    Console.Clear();
                                    Console.WriteLine("!Chau!\r\n");
                                    SalirGestorLlamadas = true;
                                    Pausa();
                                break;
                            default:
                                    Console.Clear();
                                    Console.WriteLine("Opcion invalida, eliga otra opcion\r\n");
                                break;
                            }
                        }

                            
                    break;

                    case "3":
                        bool SalirColaProcesos = false;
                        Queue<string> ColaProcesos = new Queue<string>();
                        string UltimoProceso = null;

                        while(!SalirColaProcesos)
                        {
                            Console.Clear();

                            
                            
                            Console.WriteLine("Ejercicio 3: Cola de procesos \r\n");
                            Console.WriteLine($"El ultimo proceso ejecutado es: {UltimoProceso}\r\n");
                            Console.WriteLine("1. Agregar un proceso");
                            Console.WriteLine("2. Ejecutar proceso");
                            Console.WriteLine("3. Salir");
                            string OpcionProcesos = Console.ReadLine();

                            switch (OpcionProcesos)
                            {
                                case "1":
                                    Console.Clear();
                                    Console.WriteLine("Ingrese el nombre del nuevo proceso: \r\n");
                                    string ProcesoNuevo = Console.ReadLine();
                                    if (!string.IsNullOrWhiteSpace(ProcesoNuevo))
                                    {
                                        UltimoProceso = ProcesoNuevo;
                                        ColaProcesos.Enqueue(ProcesoNuevo);
                                        Console.WriteLine($"El proceso {ProcesoNuevo} ha sido agregado \r\n");
                                        
                                    }
                                    else
                                    {
                                        Console.WriteLine("Ingrese un proceso valido \r\n");
                                    }



                                        break;
                                case "2":
                                    Console.Clear();

                                    if (ColaProcesos.Count > 0)
                                    {
                                        UltimoProceso = ColaProcesos.Dequeue();
                                        
                                        Console.WriteLine($"El proceso {UltimoProceso} ha sido ejecutado \r\n");

                                        Pausa();
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("No hay procesos para ejecutar \r\n");
                                    }


                                        break;

                                case "3":
                                    Console.Clear();
                                    Console.WriteLine("!Chau!");
                                    SalirColaProcesos = true;
                                    break;
                                default:
                                    Console.Clear();
                                    Console.WriteLine("Opcion invalida eliga otra opcion");
                                    Pausa();
                                    break;
                            }

                        }
                        break;

                    case "4":
                        Console.Clear();
                        Console.WriteLine("!Chau! \r\n");

                        SalirEjerciciosCola = true;
                        Pausa();
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Opcion no valida Ingrese otra opcion \r\n");
                        break;

                }
            }

        }

        static void EjerciciosDiccionario()
        {
            bool SalirEjerciciosDiccionario = false;
            while (!SalirEjerciciosDiccionario)
            {
                Console.Clear();    
                Console.WriteLine("Ejercicio de Diccionario");
                Console.WriteLine("Ingrese el ejericios que desea ver: \r\n");
                Console.WriteLine("1. Directorio de telefonico \r\n");
                Console.WriteLine("2. Catalogo de productos \r\n");
                Console.WriteLine("3. c \r\n");
                string OpcionDiccionario = Console.ReadLine();


                switch (OpcionDiccionario)
                {
                    case "1":
                        bool SalirDirectorioTelefonico = false;
                        Dictionary<string, string> DirectorioTelefonico = new Dictionary<string, string>();

                        while(!SalirDirectorioTelefonico)
                        {
                            Console.Clear();
                            Console.WriteLine("Ejercicio 1: Directorio telefonico \r\n");
                            Console.WriteLine("Elija una opcion: \r\n");
                            Console.WriteLine("1. Agregar contacto \r\n");
                            Console.WriteLine("2. Buscar numero por nombre \r\n");
                            Console.WriteLine("3. Eliminar contacto \r\n");
                            Console.WriteLine("4. Salir \r\n");
                            string OpcionDirectorio = Console.ReadLine();

                            switch (OpcionDirectorio)
                            {
                                case "1":
                                    Console.Clear();
                                    Console.WriteLine("Ingrese el nombre del contacto: \r\n");
                                    string NombreContacto = Console.ReadLine();
                                    Console.WriteLine("Ingrese el numero del contacto: \r\n");
                                    string NumeroContacto = Console.ReadLine();

                                    if(!string.IsNullOrWhiteSpace(NombreContacto) && !DirectorioTelefonico.ContainsKey(NombreContacto) && !string.IsNullOrWhiteSpace(NumeroContacto) && !DirectorioTelefonico.ContainsKey(NumeroContacto))
                                    {
                                        DirectorioTelefonico.Add(NombreContacto, NumeroContacto);
                                        Console.WriteLine($"El contacto {NombreContacto} con el numero {NumeroContacto} ha sido agregado al directorio \r\n");
                                        Pausa();
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("El nombre o numero ingresado no es valido o ya existe en el directorio \r\n");
                                        Pausa();
                                        break;
                                    }
                                        break;
                                case "2":
                                    Console.Clear();
                                    Console.WriteLine("Ingrese el nombre del contacto que desea buscar: \r\n");
                                    string ContactoBuscar = Console.ReadLine();

                                    if (DirectorioTelefonico.ContainsKey(ContactoBuscar))
                                    {
                                        Console.WriteLine($"El numero de {ContactoBuscar} es: {DirectorioTelefonico[ContactoBuscar]} \r\n");
                                        Pausa();
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("El contacto no existe en el directorio \r\n");
                                        Pausa();
                                        break;
                                    }
                                        break;
                                case "3":
                                    Console.Clear();
                                    Console.WriteLine("Ingrese el nombre del contacto que desea eliminar: \r\n");
                                    string ContactoEliminar = Console.ReadLine();

                                    if (DirectorioTelefonico.ContainsKey(ContactoEliminar))
                                    {
                                        DirectorioTelefonico.Remove(ContactoEliminar);
                                        Console.WriteLine($"El contacto {ContactoEliminar} ha sido eliminado del directorio \r\n");
                                        Pausa();
                                        break;
                                    }
                                    else 
                                    {
                                        Console.WriteLine("El contacto no existe en el directorio \r\n");
                                        Pausa();
                                        break;
                                    }
                                        break;
                                case "4":
                                    Console.Clear();
                                    Console.WriteLine("!Chau! \r\n");
                                    
                                    break;
                                default:
                                    Console.Clear();
                                    Console.WriteLine("Opcion no valida, por favor intente con otro numero \r\n");
                                    SalirDirectorioTelefonico = true;
                                    Pausa();
                                    break;
                                            
                            }
                        }
                        break;

                    case "2":
                        bool SalirCatalogoProductos = false;
                        Dictionary<int, string> CatalogoProductos = new Dictionary<int, string>();

                        while (!SalirCatalogoProductos)
                        {
                            Console.Clear();
                            Console.WriteLine("Ejercicio 2: Catalogo de productos \r\n");
                            Console.WriteLine("Elija una opcion: \r\n");
                            Console.WriteLine("1. Agregar producto \r\n");
                            Console.WriteLine("2. Buscar producto por codigo \r\n");
                            Console.WriteLine("3. Eliminar producto \r\n");
                            Console.WriteLine("4. Salir \r\n");
                            string OpcionCatalogo = Console.ReadLine();
                            switch (OpcionCatalogo) 
                            { 
                                case "1":
                                    Console.Clear();
                                    Console.WriteLine("Ingrese el nombre del producto\r\n");
                                    string NombreProducto = Console.ReadLine();
                                    Console.WriteLine("Ingrese el codigo del producto\r\n");
                                    string CodigoProducto = Console.ReadLine();

                                    if (int.TryParse(CodigoProducto, out int NumeroUnicoProducto) && !CatalogoProductos.ContainsKey(NumeroUnicoProducto) && !string.IsNullOrEmpty(NombreProducto))
                                    {
                                        CatalogoProductos.Add(NumeroUnicoProducto, NombreProducto);
                                        Console.WriteLine($"El producto: {NombreProducto} fue agruegado con el codigo {NumeroUnicoProducto}\r\n");
                                        Pausa();
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("El producto o codigo son invalidos o ya se encuentran en el catalogo \r\n");
                                        Pausa();
                                        break;
                                    }
                                        break;
                                case "2":
                                    Console.Clear();
                                    Console.WriteLine("Ingrese el codigo del producto que desea buscar: \r\n");
                                    string ProductoBuscar = Console.ReadLine();

                                    if(int.TryParse(ProductoBuscar,out int ProductoEncontrado) && CatalogoProductos.ContainsKey(ProductoEncontrado))
                                    { 
                                        Console.WriteLine($"El producto con el codigo ingresado es: {CatalogoProductos[ProductoEncontrado]}\r\n");
                                        Pausa();
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("El codigo ingresado no es valido \r\n");
                                        Pausa();
                                        break;
                                    }
                                        break;
                                case "3":
                                    Console.Clear();
                                    Console.WriteLine("Ingrese el codigo del producto que desea eliminar: \r\n");
                                    string ProductoEliminar = Console.ReadLine();

                                    if (int.TryParse(ProductoEliminar, out int ProductoBorrado))
                                    {
                                        CatalogoProductos.Remove(ProductoBorrado);
                                        Console.WriteLine($"El producto con el codigo {ProductoBorrado} ha sido eliminado del catalogo \r\n");
                                        Pausa();
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("El codigo ingresado no es valido \r\n");
                                        Pausa();
                                        break;
                                    }
                                    break;
                                case "4":
                                    Console.Clear();
                                    Console.WriteLine("!Chau! \r\n");
                                    SalirCatalogoProductos = true;
                                    Pausa();     
                                    break;
                                default:
                                    Console.Clear();
                                    Console.WriteLine("Opcion no valida, por favor intente con otro numero \r\n");
                                    Pausa();

                                    break;
                            }
                        }
                        break;

                    case "3":
                        bool SalirTraductor = false;
                        Dictionary<string, string> Traductor = new Dictionary<string, string>();

                        while (!SalirTraductor)
                        {
                            Console.Clear();
                            Console.WriteLine("Ejercicio 3: Diccionario de traducciones \r\n");
                            Console.WriteLine("1. Agregar traducciones \r\n");
                            Console.WriteLine("2. Buscar traducciones  \r\n");
                            Console.WriteLine("3. Mostrar traducciones  \r\n");
                            Console.WriteLine("4. Salir  \r\n");
                            string OpcionTraductor = Console.ReadLine();

                            switch (OpcionTraductor)
                            {
                                case "1":
                                    Console.Clear();
                                    Console.WriteLine("Agregue la palabra en ingles \r\n");
                                    string PalabraIngles = Console.ReadLine();
                                    Console.WriteLine("Agregue la traduccion en español \r\n");
                                    string PalabraEspañol = Console.ReadLine();

                                    if (!string.IsNullOrWhiteSpace(PalabraIngles) && !Traductor.ContainsKey(PalabraIngles) && !string.IsNullOrWhiteSpace(PalabraEspañol) && !Traductor.ContainsKey(PalabraEspañol))
                                    {
                                        Traductor.Add(PalabraIngles, PalabraEspañol);
                                        Console.WriteLine($"La palabra {PalabraIngles} ha sido agregada con la traduccion {PalabraEspañol} \r\n");
                                        Pausa();
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("La palabra o traduccion ingresada no es valida o ya existe en el diccionario \r\n");
                                        Pausa();
                                        break;
                                    }
                                    break;
                                case "2":
                                    Console.Clear();
                                    Console.WriteLine("Ingrese la palabra en ingles que desea traducir: \r\n");
                                    string PalabraTraducir = Console.ReadLine();
                                    if (Traductor.ContainsKey(PalabraTraducir))
                                    {

                                        Console.WriteLine($"La traduccion de {PalabraTraducir} es: {Traductor[PalabraTraducir]} \r\n");
                                        Pausa();
                                        break;

                                    }
                                    else
                                    {
                                        Console.WriteLine("La palabra no existe en el diccionario \r\n");
                                        Pausa();
                                        break;
                                    }

                                        break;
                                case "3":
                                    Console.Clear();
                                    Console.WriteLine("Diccionario completo: \r\n");

                                    foreach (var IndiceTraduccion in Traductor)
                                    {
                                        Console.WriteLine($"Ingles: {IndiceTraduccion.Key} - Español: {IndiceTraduccion.Value} \r\n");
                                    }

                                    Pausa();
                                    break;
                                case "4":
                                    Console.Clear();
                                    Console.WriteLine("!Chau! \r\n");
                                    SalirTraductor = true;
                                    Pausa();
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                        
                    case "4":
                        Console.Clear();
                        Console.WriteLine("!Chau! \r\n");
                        SalirEjerciciosDiccionario = true;
                        Pausa();
                        break;

                }

            }
        }
    }
}
