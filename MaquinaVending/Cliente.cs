using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaquinaVending
{
    internal class Cliente : Usuario
    {

        public Cliente() { }

        public Cliente(List<Producto> productos) : base(productos)
        {

        }

        public override void Menu()
        {
            int opcion = 0;

            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(" \t   ================================== ");
                Console.WriteLine(" \t   ||        MENÚ CLIENTE          ||");
                Console.WriteLine(" \t   ================================== ");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine("\t╔═══════════════════════════════════════╗");
                Console.WriteLine("\t║ 1.- Comprar Productos                 ║");
                Console.WriteLine("\t║                                       ║");
                Console.WriteLine("\t║ 2.- Mostrar Info. de un Producto      ║");
                Console.WriteLine("\t║                                       ║");
                Console.WriteLine("\t║ 3.- Salir                             ║");
                Console.WriteLine("\t╚═══════════════════════════════════════╝");
                Console.WriteLine();
                Console.Write("\tPor favor, introduzca su opción:");
                opcion = int.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1: // COMPRAR PRODUCTOS 
                        ComprarProducto();
                        break;

                    case 2: // MOSTRAR INFORMACIÓN
                        MostrarInfo();
                        break;

                    case 3: // SALIR
                        break;

                    default:
                        break;
                }


            } while (opcion != 3);


        }
        public void MostrarInfo()
        {
            foreach (Producto p in listaProductos)
            {
                Console.WriteLine($"ID: {p.Id}, Nombre: {p.Nombre}, Unidades {p.Unidades}, Precio {p.Precio_Unitario}€, Información del producto: {p.Descripcion}");
            }
            int id = int.Parse(Console.ReadLine());
            foreach (Producto p in listaProductos)
            {
                if (id == p.Id)
                {
                    p.MostrarInfo();
                }
                else
                { Console.WriteLine("Lo sentimos, no tenemos ninguna información sobre los productos"); }
            }
        }
    }
}
