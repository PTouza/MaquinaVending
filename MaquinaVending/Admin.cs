using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics.Eventing.Reader;
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
            string password;
            Console.WriteLine();
            Console.Write("\tIntroduce una contraseña: ");
            password = "";
            bool passwordEscrito = false;
            while (!passwordEscrito)
            {
                var key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Enter)
                {
                    passwordEscrito = true;
                }
                password += key.KeyChar;

            }

            Console.WriteLine("\nContraseña introducida: " + password);
            if (password != Password)
            {
                Console.WriteLine("Contraseña incorrecta");
                Console.ReadKey();
            }

            else
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

                    switch (opcion)
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
        }
        
        public void CargaIndividualProducto()
        {
            int opcion = 0;

            Console.Clear();
            Console.WriteLine("1. Añadir productos existentes");
            Console.WriteLine("2. Introducir nuevos productos a la máquina");
            opcion = int.Parse(Console.ReadLine());
            switch (opcion)
            {
                case 1:
                    AddUnidades();
                    break;
                case 2:
                    int opcion2 = 0;
                    do
                    {
                        AddNewProducto();
                        Console.Write("¿Quiere añadir otro producto? (1.- Si / 2.- No): ");
                        opcion2 = int.Parse(Console.ReadLine());

                    } while (opcion2 == 1);
                    break;
                default:
                    Console.WriteLine("Salir");
                    break;
            }
        }
        public void CargaCompletaProducto()
        { 

        }

        public void AddUnidades()
        {
            Producto producto = BuscarProducto();
            Console.Write("Introduce el número de unidades que desa introducir: ");
            int unidades = int.Parse(Console.ReadLine());
            if(producto != null)
            {
                producto.AddUnidades(unidades);
            }
        }
        public void AddNewProducto()
        {
            Producto producto = null;
            Console.WriteLine("¿Que tipo de producto quiere añadir?: ");
            Console.WriteLine("\t1.- Producto Alimenticio");
            Console.WriteLine("\t2.- Producto Electrónico");
            Console.WriteLine("\t3.- Material Precioso");
            Console.WriteLine("\t4.- Salir");
            Console.WriteLine();
            Console.Write("Escoge una opción: ");
            int opcion = int.Parse(Console.ReadLine());

            switch (opcion)
            {
                case 1:
                    ProductoAlimenticio productoAlimenticio = (ProductoAlimenticio)producto;
                    break;

                case 2:
                    ProductoElectronico productoElectronico = (ProductoElectronico)producto;
                    break;

                case 3:
                    MaterialPrecioso materialPrecioso = (MaterialPrecioso)producto;
                    materialPrecioso.SolicitarDetalles();
                    break;

                default:
                    break;
            }
        }

    }


}
