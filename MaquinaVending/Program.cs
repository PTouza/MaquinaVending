using System;
using System.Collections.Generic;
using System.IO;
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
            int opcion = 0;
            products = new List<Producto>();
            productosMaquina = new List<Producto>();
            CargarProductos();
            CargarProductosMaquina();
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
                try
                {
                    opcion = int.Parse(Console.ReadLine());

                    switch (opcion)
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
                }catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                

            } while (opcion != 3);


            Console.ReadKey();
        }

        public static void CargarProductos()
        {
            if (File.Exists("productos.txt"))
            {
                using (StreamReader sr = new StreamReader("productos.txt"))
                {

                    string line = null;
                    string[] campos = null;
                    while ((line = sr.ReadLine()) != null)
                    {
                        campos = line.Split(';');
                        switch (int.Parse(campos[0]))
                        {
                            case 1:
                                MaterialPrecioso mp = new MaterialPrecioso(campos[1], int.Parse(campos[2]), double.Parse(campos[3]),
                                    campos[4], campos[5], campos[6]);
                                products.Add(mp);
                                break;

                            case 2:
                                ProductoAlimenticio pa = new ProductoAlimenticio(campos[1], int.Parse(campos[2]), double.Parse(campos[3]),
                                    campos[4], campos[7]);
                                products.Add(pa);
                                break;

                            case 3:
                                ProductoElectronico pe = new ProductoElectronico(campos[1], int.Parse(campos[2]), double.Parse(campos[3]),
                                    campos[4], campos[6], bool.Parse(campos[8]), bool.Parse(campos[9]));
                                products.Add(pe);
                                break;
                        }
                    }
                }
            }

            else
            {
                File.Create("productos.txt").Close();
            }
        }

        public static void CargarProductosMaquina()
        {
            if (File.Exists("productosMaquina.txt"))
            {
                using (StreamReader sr = new StreamReader("productosMaquina.txt"))
                {

                    string line = null;
                    string[] campos = null;
                    while ((line = sr.ReadLine()) != null)
                    {
                        campos = line.Split(';');
                        switch (int.Parse(campos[0]))
                        {
                            case 1:
                                MaterialPrecioso mp = new MaterialPrecioso(campos[1], int.Parse(campos[2]), double.Parse(campos[3]),
                                    campos[4], campos[5], campos[6]);
                                productosMaquina.Add(mp);
                                break;

                            case 2:
                                ProductoAlimenticio pa = new ProductoAlimenticio(campos[1], int.Parse(campos[2]), double.Parse(campos[3]),
                                    campos[4], campos[7]);
                                productosMaquina.Add(pa);
                                break;

                            case 3:
                                ProductoElectronico pe = new ProductoElectronico(campos[1], int.Parse(campos[2]), double.Parse(campos[3]),
                                    campos[4], campos[6], bool.Parse(campos[8]), bool.Parse(campos[9]));
                                productosMaquina.Add(pe);
                                break;
                        }
                    }
                }
            }

            else
            {
                File.Create("productosMaquina.txt").Close();
            }
        }
    }
}
