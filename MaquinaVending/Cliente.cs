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
                Console.WriteLine("\t\t  ---Menú  Cliente ---  ");
                Console.WriteLine();
                Console.WriteLine("\t1.- Comprar Productos");
                Console.WriteLine("\t2.- Mostrar Info. de un Producto");
                Console.WriteLine("\t3.- Salir");
                Console.WriteLine();
                Console.WriteLine("--------------------------------------");
                Console.Write("Escoge una opción: ");
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
