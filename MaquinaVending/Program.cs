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
        static List<Producto> productosMaquina;
        static void Main(string[] args)
        {
            int opcion;
            products = new List<Producto>();
            productosMaquina = new List<Producto>();
            Admin admin = new Admin(productosMaquina, "admin123", products);
            Cliente cliente = new Cliente(productosMaquina);
            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\t==================================");
                Console.WriteLine("\t||     MÁQUINA DE VENDING       ||");
                Console.WriteLine("\t==================================");
                Console.WriteLine();
                Console.ResetColor();
                Console.WriteLine("\t╔═══════════════════════════════╗");
                Console.WriteLine("\t║ 1.- Si es Cliente             ║");
                Console.WriteLine("\t║ 2.- Si es Admin               ║");
                Console.WriteLine("\t╚═══════════════════════════════╝");
                Console.WriteLine();
                Console.Write("\tPor favor, introduzca su opción:   ");
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
