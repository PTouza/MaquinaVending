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
                        ComprarProducto();
                        
                        break;

                    case 2: // MOSTRAR INFORMACIÓN
                        MostrarInfo();
                        break;

                    case 3: // CARGA INDIVIDUAL
                        CargaIndividualProducto();
                        break;

                    case 4: // CARGA COMPLETA
                        CargaCompletaProducto();
                        break;

                    case 5: // SALIR
                        break;

                    default:
                        break;
                }


            } while (opcion != 5);
           

        }
        public void MostrarInfo()
        {
            foreach(Producto p in listaProductos)
            {
                Console.WriteLine($"ID: {p.Id}, Nombre: {p.Nombre}, Unidades {p.Unidades}, Precio {p.Precio_Unitario}€, Información del producto: {p.Descripcion}");
            }
            int id = int.Parse(Console.ReadLine());
            foreach(Producto p in listaProductos)
            {
                if(id == p.Id)
                {
                    p.MostrarInfo();
                }
                else
                { Console.WriteLine("Lo sentimos, no tenemos ninguna información sobre los productos"); }
            }

        }
        public void CargaIndividualProducto()
        {
            int opcion = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("1. Añadir productos existentes");
                Console.WriteLine("2. Introducir nuevos productos a la máquina");
                opcion = int.Parse(Console.ReadLine());
                switch(opcion)
                {
                    case 1:
                        AddUnidades();
                        break;
                    case 2:
                        AddnewProducto();
                        break;
                    default:
                        Console.WriteLine("Salir");
                        break;
                }
            }
            while (opcion != 2);
        }
        public void CargaCompletaProducto ()
        {

        }
        public void AddUnidades()
        {

        }
        public void AddnewProducto() 
        {

        }

        





    }
    
}
