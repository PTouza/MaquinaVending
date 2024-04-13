using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaquinaVending
{
    internal class Program
    {
        static List<Producto> products
        static void Main(string[] args)
        {
            int opcion;
            products = new List<Producto>();
            Admin admin = new Admin(products, "admin123");
            Cliente cliente = new Cliente(products);
            do
            {
                Console.Clear();
                Console.WriteLine("\t   --- Bienvenido a la máquina de Vending UFV ---  ");
                Console.WriteLine("\t||                                                 ||");
                Console.WriteLine("\t||                                                 ||");
                Console.WriteLine("\t||\t Porfavor, escoge una opción:              ||");
                Console.WriteLine("\t||                                                 ||");
                Console.WriteLine("\t||\t 1. Cliente                                ||");
                Console.WriteLine("\t||\t 2. Admin                                  ||");
                Console.WriteLine("\t||                                                 ||");
                Console.WriteLine("\t||_________________________________________________||");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("\t Escoge una opción: ");
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
