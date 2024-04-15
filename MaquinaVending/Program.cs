using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaquinaVending
{
    internal class Program
    {
        static List<Producto> products;
        static void Main(string[] args)
        {
            int opcion;
            products = new List<Producto>();
            Admin admin = new Admin(products, "admin123");
            Cliente cliente = new Cliente(products);
            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("==================================");
                Console.WriteLine("||     MÁQUINA DE VENDING       ||");
                Console.WriteLine("==================================");
                Console.ResetColor();
                Console.WriteLine("╔═══════════════════════════════╗");
                Console.WriteLine("║ Pulse 1 para Cliente          ║");
                Console.WriteLine("║ Pulse 2 para Admin            ║");
                Console.WriteLine("╚═══════════════════════════════╝");
                Console.Write("Por favor, introduzca su opción:");
                opcion = int.Parse(Console.ReadLine());

                switch(opcion)
                {
                    case 1:
                        cliente.Menu();
                        break;

                    case 2:
                        admin.Menu();
                        break;

                    default:
                        Console.WriteLine("Opción no válida!!!");
                        break;
                }

            } while (opcion != 3);


            Console.ReadKey();
        }
    }
}
