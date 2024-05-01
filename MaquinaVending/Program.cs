using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMPLib;
using System.Media;
using System.Text.Json;
using System.Threading;

namespace MaquinaVending
{
    internal class Program
    {
        static List<Producto> products;
        static List<Producto> productosMaquina;
        static void Main(string[] args)
        {
            // Inicia la música del programa
            WindowsMediaPlayer musica = new WindowsMediaPlayer();
            musica.URL = "Smooth operator But only the Best Part loop_qczc8Xzt8aU.mp3";
            musica.settings.setMode("loop", true);
            musica.settings.volume = 35;
            musica.controls.play();
            

            // Creo la variable que vamos a usar en el switch
            int opcion = 0;

            // Inicializamos las listas y las cargamos de los archivos .json
            products = new List<Producto>();
            productosMaquina = new List<Producto>();
            CargarProductos();
            CargarProductosMaquina();

            // Creamos instancias de los usuarios, como solo va a haber dos los creamos sin pedir datos
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
                Console.WriteLine("\t║                               ║");
                Console.WriteLine("\t║ 2.- Si es Admin               ║");
                Console.WriteLine("\t║                               ║");
                Console.WriteLine("\t║ 3.- Salir                     ║");
                Console.WriteLine("\t╚═══════════════════════════════╝");
                Console.WriteLine();
                Console.Write("\tPor favor, introduzca su opción: ");
                try
                {
                    opcion = int.Parse(Console.ReadLine());

                    switch (opcion)
                    {
                        case 1: // Llamamos al método menú del cliente
                            cliente.Menu();
                            break;

                        case 2: // Llamamos al método menú del admin
                            admin.Menu();
                            break;

                        default:
                            Console.WriteLine("Opción no válida!!!");
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine();
                    Console.Write($"\tIntroduzca un valor válido");
                }

                Thread.Sleep(2000);

            } while (opcion != 3);

            Console.ReadKey();
        }

        public static void CargarProductos()
        {
            try
            {


                if (File.Exists("productos.json"))
                {
                    // Leo todo el archivo y lo almaceno en un solo string
                    string json = File.ReadAllText("productos.json");

                    if (json != string.Empty)
                    {
                        // si el archivo no está vacío lo deseralizo en una lista de objetos Object, ya que no sabemos todavía que tipo de Producto es
                        List<Object> ProductosJson = JsonSerializer.Deserialize<List<Object>>(json);

                        // Si hay elementos en la lista empezamos la deserialización
                        if (ProductosJson.Count != 0)
                        {
                            // Recorremos la lista de objetos Object
                            foreach (Object o in ProductosJson)
                            {
                                // Hacemos una conversión implícita a JsonElement para poder acceder a la propiedad TipoProducto
                                JsonElement jsonElement = (JsonElement)o;

                                // Accedemos a la propiedad TipoProducto para hacer la deserialización final
                                int tipoProducto = jsonElement.GetProperty("TipoProducto").GetInt32();

                                // Dependiendo del valor de TipoProducto deserializamos como un objeto u otro
                                switch (tipoProducto)
                                {
                                    case 1:
                                        // Deserializamos como MaterialPrecioso
                                        MaterialPrecioso mp = JsonSerializer.Deserialize<MaterialPrecioso>
                                            (jsonElement.GetRawText());
                                        products.Add(mp);
                                        break;

                                    case 2:
                                        // Deserializamos como ProductoAlimenticio
                                        ProductoAlimenticio pa = JsonSerializer.Deserialize<ProductoAlimenticio>
                                            (jsonElement.GetRawText());
                                        products.Add(pa);
                                        break;

                                    case 3:
                                        // Deserializamos commo ProductoElectronico
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
                    // Si el archivo no existe, lo creamos
                    File.Create("productos.json").Close();
                }
            }

            catch (FileNotFoundException) 
            { 
                Console.Write("Ups, algo ha pasado con el archivo durante la carga");
                Thread.Sleep(1500);
            }
        }

        public static void CargarProductosMaquina()
        {
            try
            {


                if (File.Exists("productosMaquina.json"))
                {
                    // Leo todo el archivo y lo almaceno en un solo string
                    string json = File.ReadAllText("productosMaquina.json");

                    if (json != string.Empty)
                    {
                        // si el archivo no está vacío lo deseralizo en una lista de objetos Object, ya que no sabemos todavía que tipo de Producto es
                        List<Object> ProductosMaquinaJson = JsonSerializer.Deserialize<List<Object>>(json);

                        // Si hay elementos en la lista empezamos la deserialización
                        if (ProductosMaquinaJson.Count != 0)
                        {
                            // Recorremos la lista de objetos Object
                            foreach (Object o in ProductosMaquinaJson)
                            {
                                // Hacemos una conversión implícita a JsonElement para poder acceder a la propiedad TipoProducto
                                JsonElement jsonElement = (JsonElement)o;

                                // Accedemos a la propiedad TipoProducto para hacer la deserialización final
                                int tipoProducto = jsonElement.GetProperty("TipoProducto").GetInt32();

                                // Dependiendo del valor de TipoProducto deserializamos como un objeto u otro
                                switch (tipoProducto)
                                {
                                    case 1:
                                        // Deserializamos como MaterialPrecioso
                                        MaterialPrecioso mp = JsonSerializer.Deserialize<MaterialPrecioso>(jsonElement.GetRawText());
                                        productosMaquina.Add(mp);
                                        break;

                                    case 2:
                                        // Deserializamos como ProductoAlimenticio
                                        ProductoAlimenticio pa = JsonSerializer.Deserialize<ProductoAlimenticio>(jsonElement.GetRawText());
                                        productosMaquina.Add(pa);
                                        break;

                                    case 3:
                                        // Deserializamos como ProductoElectronico
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
                    // Si el archivo no existe, lo creamos
                    File.Create("productosMaquina.json").Close();
                }
            }
            catch (FileNotFoundException)
            {
                Console.Write("Ups, algo ha pasado con el archivo durante la carga");
                Thread.Sleep(1500);
            }
        }
    }
}
