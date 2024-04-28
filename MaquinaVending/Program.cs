using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMPLib;
using System.Media;
using System.Text.Json;

namespace MaquinaVending
{
    internal class Program
    {
        static List<Producto> products;
        static List<Producto> productosMaquina;
        static void Main(string[] args)
        {
            WindowsMediaPlayer musica = new WindowsMediaPlayer();
            musica.URL = "Smooth operator But only the Best Part loop_qczc8Xzt8aU.mp3";
            musica.controls.play();
            int opcion = 0;
            products = new List<Producto>();
            productosMaquina = new List<Producto>();
            CargarProductos();
            CargarProductosMaquina();
            Admin admin = new Admin(products, "admin123",productosMaquina);
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
                Console.WriteLine("\t║ 3.- Salir                     ║");
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
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }


            } while (opcion != 3);


            Console.ReadKey();
        }

        public static void CargarProductos()
        {
            if (File.Exists("productos.json"))
            {
                string json = File.ReadAllText("productos.json");

                if (json != string.Empty)
                {
                    List<Object> ProductosJson = JsonSerializer.Deserialize<List<Object>>(json);

                    if (ProductosJson.Count != 0)
                    {
                        foreach (Object o in ProductosJson)
                        {

                            JsonElement jsonElement = (JsonElement)o;

                            int tipoProducto = jsonElement.GetProperty("TipoProducto").GetInt32();

                            switch (tipoProducto)
                            {
                                case 1:
                                    MaterialPrecioso mp = JsonSerializer.Deserialize<MaterialPrecioso>
                                        (jsonElement.GetRawText());
                                    products.Add(mp);
                                    break;

                                case 2:
                                    ProductoAlimenticio pa = JsonSerializer.Deserialize<ProductoAlimenticio>
                                        (jsonElement.GetRawText());
                                    products.Add(pa);
                                    break;

                                case 3:
                                    ProductoElectronico pe = JsonSerializer.Deserialize<ProductoElectronico>
                                        (jsonElement.GetRawText());
                                    products.Add(pe);
                                    break;
                            }
                        }
                    }
                }
            }

            else
            {
                File.Create("productos.json").Close();
            }
        }

        public static void CargarProductosMaquina()
        {
            if (File.Exists("productosMaquina.json"))
            {
                string json = File.ReadAllText("productosMaquina.json");

                if (json != string.Empty)
                {

                    List<Object> ProductosMaquinaJson = JsonSerializer.Deserialize<List<Object>>(json);

                    if (ProductosMaquinaJson.Count != 0)
                    {
                        foreach (Object o in ProductosMaquinaJson)
                        {

                            JsonElement jsonElement = (JsonElement)o;

                            int tipoProducto = jsonElement.GetProperty("TipoProducto").GetInt32();

                            switch (tipoProducto)
                            {
                                case 1:
                                    MaterialPrecioso mp = JsonSerializer.Deserialize<MaterialPrecioso>(jsonElement.GetRawText());
                                    productosMaquina.Add(mp);
                                    break;

                                case 2:
                                    ProductoAlimenticio pa = JsonSerializer.Deserialize<ProductoAlimenticio>(jsonElement.GetRawText());
                                    productosMaquina.Add(pa);
                                    break;

                                case 3:
                                    ProductoElectronico pe = JsonSerializer.Deserialize<ProductoElectronico>(jsonElement.GetRawText());
                                    productosMaquina.Add(pe);
                                    break;
                            }
                        }
                    }
                }
            }

            else
            {
                File.Create("productosMaquina.json").Close();
            }
        }
    }
}
