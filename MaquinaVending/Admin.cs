using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics.Eventing.Reader;
using System.Text.Json;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MaquinaVending
{
    internal class Admin : Usuario
    {
        private string Password { get; set; }
        public List<Producto> Productos { get; private set; }


        public Admin() { }

        public Admin(List<Producto> productos, string password, List<Producto> productosMaquina) : base(productosMaquina)
        {
            Password = password;
            Productos = productos;
        }

        public override void Menu()
        {
            
            Console.WriteLine();
            Console.Write("\tIntroduce una contraseña: ");
            string password = Console.ReadLine();
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
                    Console.WriteLine("\t║ 4.- Carga completa de Productos       ║");
                    Console.WriteLine("\t║                                       ║");
                    Console.WriteLine("\t║ 5.- Eliminar un Producto              ║");
                    Console.WriteLine("\t║                                       ║");
                    Console.WriteLine("\t║ 6.- Salir                             ║");
                    Console.WriteLine("\t╚═══════════════════════════════════════╝");
                    Console.WriteLine();
                    
                    Console.Write("\tPor favor, introduzca su opción: ");
                    try
                    {
                        opcion = int.Parse(Console.ReadLine());

                        Console.Clear();
                        switch (opcion)
                        {
                            case 1: // COMPRAR PRODUCTOS 
                                ComprarProducto();

                                break;

                            case 2: // MOSTRAR INFORMACIÓN
                                MostrarInfo();
                                Console.Write("\tPulse cualquier tecla para continuar");
                                Console.ReadKey();
                                break;

                            case 3: // CARGA INDIVIDUAL
                                CargaIndividualProducto();
                                break;

                            case 4: // CARGA COMPLETA
                                CargaCompletaProducto();
                                break;

                            case 5: // ELIMINAR UN PRODUCTO
                                break;

                            default:
                                break;
                        }
                    }catch(Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    


                } while (opcion != 6);


            }
        }
        
        public void CargaIndividualProducto()
        {
            int opcion = 0;

            Console.Clear();
            Console.WriteLine("1. Añadir productos existentes");
            Console.WriteLine("2. Añadir nuevos productos al almacén");
            Console.Write("Escoge una opción: ");
            try
            {
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
            }catch(Exception e) { Console.WriteLine(e.Message) ; }
            
        }
        public void CargaCompletaProducto()
        {
            int opcion = 0;
            Console.Clear();
            Console.WriteLine("1.- Archivo CSV");
            Console.WriteLine("2.- Archivo JSON");
            Console.Write("\tEscoge una opción: ");
            try
            {
                opcion = int.Parse(Console.ReadLine());
                if (QuiereContinuar())
                {
                    switch (opcion)
                    {
                        case 1:
                            LeerArchivoCSV();
                            break;

                        case 2:
                            LeerArchivoJSON();
                            break;

                        default:
                            break;
                    }
                }

            }catch (Exception e) { Console.WriteLine(e.Message); }
            
        }

        public void LeerArchivoCSV()
        {
            Console.Write("Introduce la dirección de memoria del archivo: ");
            var path = Console.ReadLine();
            if (File.Exists(path))
            {
                ProductosMaquina.Clear();
                using (StreamReader sr = new StreamReader(path))
                {
                    
                    string line = null;
                    string[] campos = null;
                    while ((line = sr.ReadLine()) != null)
                    {
                        campos = line.Split(';');
                        switch (int.Parse(campos[0]))
                        {
                            case 1:
                                MaterialPrecioso mp = new MaterialPrecioso(Productos.Count, campos[1], int.Parse(campos[2]), double.Parse(campos[3]),
                                    campos[4], campos[5], campos[6]);
                                ProductosMaquina.Add(mp);
                                break;

                            case 2:
                                ProductoAlimenticio pa = new ProductoAlimenticio(Productos.Count, campos[1], int.Parse(campos[2]), double.Parse(campos[3]),
                                    campos[4], campos[7]);
                                ProductosMaquina.Add(pa);
                                break;

                            case 3:
                                ProductoElectronico pe = new ProductoElectronico(Productos.Count, campos[1], int.Parse(campos[2]), double.Parse(campos[3]),
                                    campos[4], campos[6], bool.Parse(campos[8]), bool.Parse(campos[9]));
                                ProductosMaquina.Add(pe);
                                break;
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("El archivo dado no existe o no lo hemos encontrado, porfavor inténtalo de nuevo");
            }
        }

        public void LeerArchivoJSON()
        {
            Console.Write("Introduce la úbicación del archivo: ");
            var path = Console.ReadLine();

            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);

                List<Object> ProductosMaquinaJson = JsonSerializer.Deserialize<List<Object>>(json);

                if (ProductosMaquinaJson.Count != 0)
                {
                    foreach (Object o in ProductosMaquinaJson)
                    {

                        JsonElement jsonElement = (JsonElement)o;

                        int tipoProducto = jsonElement.GetProperty("TipoProducto").GetInt32();

                        switch(tipoProducto)
                        {
                            case 1:
                                MaterialPrecioso mp = JsonSerializer.Deserialize<MaterialPrecioso>(jsonElement.GetRawText());
                                ProductosMaquina.Add(mp);
                                break;

                            case 2:
                                ProductoAlimenticio pa = JsonSerializer.Deserialize<ProductoAlimenticio>(jsonElement.GetRawText());
                                ProductosMaquina.Add(pa);
                                break;

                            case 3:
                                ProductoElectronico pe = JsonSerializer.Deserialize<ProductoElectronico>(jsonElement.GetRawText());
                                ProductosMaquina.Add(pe);
                                break;
                        }
                    }
                }
            }
        }

        public void AddUnidades()
        {
            Producto productoMaquina = BuscarProductoMaquina();
            Producto productoAlmacen = BuscarProductoAlmacen(productoMaquina.Nombre);
            Console.Write("Introduce el número de unidades que desa introducir: ");
            int unidades = int.Parse(Console.ReadLine());
            if (unidades + productoMaquina.Unidades > 10)
            {
                Console.WriteLine("La máquina solo admite 10 unidades por producto");
                Console.Write("Pulse cualquier tecla para continuar");
                Console.ReadKey();
            }
            else
            {
                if (productoMaquina != null && productoAlmacen != null)
                {
                    productoAlmacen.QuitarUnidades(unidades);
                    productoMaquina.AddUnidades(unidades);
                }

                else if (productoMaquina != null)
                {
                    productoMaquina.AddUnidades(unidades);
                }
            }
            
        }

        public void AddNewProducto()
        {
            Console.WriteLine("¿Que tipo de producto quiere añadir?: ");
            Console.WriteLine("\t1.- Producto Alimenticio");
            Console.WriteLine("\t2.- Producto Electrónico");
            Console.WriteLine("\t3.- Material Precioso");
            Console.WriteLine("\t4.- Salir");
            Console.WriteLine();
            Console.Write("Escoge una opción: ");
            try
            {
                int opcion = int.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        ProductoAlimenticio productoAlimenticio = new ProductoAlimenticio(Productos.Count);
                        productoAlimenticio.SolicitarDetalles();
                        Productos.Add(productoAlimenticio);

                        break;

                    case 2:
                        ProductoElectronico productoElectronico = new ProductoElectronico(Productos.Count);
                        productoElectronico.SolicitarDetalles();
                        Productos.Add(productoElectronico);
                        break;

                    case 3:
                        MaterialPrecioso materialPrecioso = new MaterialPrecioso(Productos.Count);
                        materialPrecioso.SolicitarDetalles();
                        Productos.Add(materialPrecioso);
                        break;

                    default:
                        break;
                }

                

            }catch(Exception e) { Console.WriteLine(e.Message); }
            
        }

        public void AddProductoMaquina()
        {
            string nombre = null;
            Producto producto = BuscarProductoAlmacen(nombre);
            Producto productoClonado = producto.Clonar();
            if (producto != null)
            {
                Console.Write("¿Quiere añadir este producto a la máquina? (1.- SI | 2.- NO): ");
                int opcion = int.Parse(Console.ReadLine());
                if(producto.Unidades > 10)
                {
                    productoClonado.Unidades = 10;
                    producto.QuitarUnidades(10);
                    Console.WriteLine($"Quedan {producto.Unidades} unidades del producto {producto.Nombre}");
                }

                else
                {
                    Console.WriteLine($"No quedan existencias del producto {producto.Nombre} en el Almacén");
                }
                int opcionQuitarProducto = 0;
                if (opcion == 1)
                {
                    if (ProductosMaquina.Count < 11)
                    {
                        ProductosMaquina.Add(producto);
                        Thread.Sleep(1000);
                        Console.WriteLine("Producto añadido a la máquina");
                    }

                    else
                    {
                        Console.WriteLine("La máquina está al máximo de su capacidad");
                        Thread.Sleep(1000);
                        Console.WriteLine("¿Desea quitar un producto de la máquina? (1.- SI | 2.- NO): ");
                        opcionQuitarProducto = int.Parse(Console.ReadLine());
                        if (opcionQuitarProducto == 1)
                        {
                            Producto p = BuscarProductoMaquina();
                            Thread.Sleep(1000);
                            if (QuiereContinuar())
                            {
                                ProductosMaquina.Remove(p);
                                ProductosMaquina.Add(producto);
                            }
                        }
                    }

                    Console.WriteLine("Operación realizada correctamente");
                }
            }
        }

        public Producto BuscarProductoAlmacen(string nombre)
        {
            if (nombre == null)
            {
                Console.Write("Introduce el nombre del producto: ");
                nombre = Console.ReadLine();
                foreach (Producto p in Productos)
                {
                    Console.WriteLine($"Nombre: {p.Nombre}, Unidades {p.Unidades}, Precio {p.Precio_Unitario}€," +
                        $" Información del producto: {p.Descripcion}");
                }
                Console.WriteLine();
            }
            Producto producto = Productos.Find(x => x.Nombre.ToLower() == nombre.ToLower());
            return producto;
        }

        public void GuardarJson()
        {
            if (Productos.Count > 0)
            {
                File.Create("productos.json").Close();

                string json = JsonSerializer.Serialize(Productos);
                using (StreamWriter sw = new StreamWriter("productos.json"))
                {
                    sw.WriteLine(json);
                }
            }

            if (ProductosMaquina.Count > 0)
            {
                File.Create("productosMaquina.json").Close();
                string json = JsonSerializer.Serialize(ProductosMaquina);
                using (StreamWriter sw = new StreamWriter("productosMaquina.json"))
                {
                    sw.WriteLine(json);
                }
            }
        }

    }


}
