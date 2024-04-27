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
                try
                {
                    opcion = int.Parse(Console.ReadLine());
                    Console.Clear();
                    switch (opcion)
                    {
                        case 1: // COMPRAR PRODUCTOS 
                            ComprarProducto();
                            break;

                        case 2: // MOSTRAR INFORMACIÓN
                            Producto p = BuscarProductoMaquina();
                            p.MostrarInfo();
                            break;

                        case 3: // SALIR
                            break;

                        default:
                            break;
                    }
                }catch(Exception e) 
                {
                    Console.WriteLine(e.Message);
                }
               


            } while (opcion != 3);


        }
    }
}
