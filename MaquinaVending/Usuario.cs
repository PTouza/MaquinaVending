using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MaquinaVending
{
    internal abstract class Usuario : IUsuario
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
                    case 1: PagarTarjeta();
                        
                        break;
                    case 2: PagarEfectivo();
                        
                        break;
                    case 3: // SALIR
                        Console.WriteLine("Salir...");
                        break;
                    default: // OPCIÓN NO VÁLIDA
                        Console.WriteLine("Porfavor, introduzca una opción válida");
                        break;


                }

            } while (opcion != 3);
            void PagarTarjeta()
            {
                
                

            }
            void PagarEfectivo ()
            {
                Console.WriteLine("Introduce el dinero ");
            }

    }
}
