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
                Console.WriteLine("==================================");
                Console.WriteLine("||        MENÚ CLIENTE          ||");
                Console.WriteLine("==================================");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine("╔═══════════════════════════════╗");
                Console.WriteLine("║ Pulse 1 para Comprar Productos║");
                Console.WriteLine("║ Pulse 2 para Mostrar Info. de ║");
                Console.WriteLine("║ un Producto                   ║");
                Console.WriteLine("║ Pulse 3 para Salir            ║");
                Console.WriteLine("╚═══════════════════════════════╝");
                Console.WriteLine();
                Console.Write("Por favor, introduzca su opción:");
                opcion = int.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1: // COMPRAR PRODUCTOS 
                        ComprarProducto();
                        break;

                    case 2: // MOSTRAR INFORMACIÓN
                        break;

                    case 3: // CARGA INDIVIDUAL
                        break;

                    case 4: // CARGA COMPLETA
                        break;

                    case 5: // SALIR
                        break;

                    default:
                        break;
                }


            } while (opcion != 5);

        }
    }
}
