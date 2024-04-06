using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaquinaVending
{
    internal abstract class Usuario
    {
        public abstract void Menu();

        public void ComprarProducto()
        {
            Console.WriteLine("Seleccione un producto de la maquina");
            int opcion = 0;
            
            switch (opcion)
            {
                case 1:

                    //Producto Alimenticio//
                    break;
                case 2:
                    //Producto Electronico
                    break;
                case 3:
                    //Producto 
                    break;
            }
        }
        public void PagarProducto()
        { 
            
            int opcion = 0;

            do
            {
                Console.WriteLine("Introduce su metodo de pago");
                Console.Clear();
                Console.WriteLine("1. Pago de tarjeta");
                Console.WriteLine("2. Pago en efectivo");
                Console.WriteLine("3. Retroceder");
                opcion = int.Parse(Console.ReadLine());
                Console.Clear();

                switch (opcion)
                {
                    case 1:
                        PagarTarjeta();
                        break;
                    case 2:
                        PagarEfectivo();
                        break;
                    case 3:
                        Console.WriteLine("Salir...");
                        break;
                    default:
                        Console.WriteLine("Elección incorrecta");
                        break;


                }

            } while (opcion != 3);
            void PagarTarjeta()
            {
                


            }
            void PagarEfectivo ()
            {
                
            }

        }
    }
}
