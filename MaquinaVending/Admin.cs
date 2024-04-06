using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MaquinaVending
{
    internal class Admin : Usuario 
    { 
        public string Password { get; set; }

        

        public Admin() { }

        public Admin(List<Producto> productos, string password) : base(productos)
        { 
            Password = password;
        }

        public override void Menu()
        {
            int opcion = 0;

            do
            {
                Console.Clear();
                Console.WriteLine("\t\t  --- Menú de Administrador ---  ");
                Console.WriteLine();
                Console.WriteLine("\t1.- Comprar Productos");
                Console.WriteLine("\t2.- Mostrar Info. de un Producto");
                Console.WriteLine("\t3.- Cargar un producto individual");
                Console.WriteLine("\t4.- Carga completa de productos");
                Console.WriteLine("\t5.- Salir");
                Console.WriteLine();
                Console.WriteLine("--------------------------------------");
                Console.Write("Escoge una opción: ");
                opcion = int.Parse(Console.ReadLine());

                switch(opcion)
                {
                    case 1: // COMPRAR PRODUCTOS
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
