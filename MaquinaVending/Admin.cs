﻿using System;
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
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\t    ================================== ");
                Console.WriteLine("\t    ||         MENÚ ADMIN           || ");
                Console.WriteLine("\t    ================================== ");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine("\t╔═══════════════════════════════════════╗");
                Console.WriteLine("\t║ 1.- Comprar Productos                 ║");
                Console.WriteLine("\t║                                       ║");
                Console.WriteLine("\t║ 2.- Mostrar Info. de un Producto      ║");
                Console.WriteLine("\t║                                       ║");
                Console.WriteLine("\t║ 3.- Carga individual de un Producto   ║");
                Console.WriteLine("\t║                                       ║");
                Console.WriteLine("\t║ 4.- para Carga completa de Productos  ║");
                Console.WriteLine("\t║                                       ║");
                Console.WriteLine("\t║ 5.- Salir                             ║");
                Console.WriteLine("\t╚═══════════════════════════════════════╝");
                Console.WriteLine();
                Console.Write("Por favor, introduzca su opción:");
                opcion = int.Parse(Console.ReadLine());

                switch(opcion)
                {
                    case 1: // COMPRAR PRODUCTOS 
                        ComprarProducto();
                        
                        break;

                    case 2: // MOSTRAR INFORMACIÓN
                       
                        break;

                    case 3: // CARGA INDIVIDUAL
                        CargaIndividualProducto();
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
       public void CargaIndividualProducto()
        {
          
           
        }
        
        

        
    }
    
}
