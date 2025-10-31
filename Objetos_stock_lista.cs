using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabajo_practico_Integrador_parte_1
{
    public class Inventario
    {
        public string NombreProducto { get; set; }
        public string Cantidad { get; set; }

        public Inventario(string nombre_producto, string cantidad)
        {
            NombreProducto = nombre_producto;
            Cantidad = cantidad;
        }
    }
}
    
