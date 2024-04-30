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
        private string Password { get;  set; }
        public List<Producto> ProductosAlmacen { get; private set; }


        public Admin() { }

        public Admin(List<Producto> productos, string password, List<Producto> productosMaquina) : base(productosMaquina)
        {
            Password = password;
            ProductosAlmacen = productos;
        }

        public override void Menu()
        {
            
            Console.WriteLine();
            Console.Write("\tIntroduce una contraseña: ");
            string password = Console.ReadLine();
            if (password != Password)
            {
                Console.Write("\tContraseña incorrecta");
                Thread.Sleep(2000);
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
                                Producto producto = BuscarProductoMaquina();
                                if (QuiereContinuar())
                                {
                                    ProductosMaquina.Remove(producto);
                                }
                                break;

                            default:
                                break;
                        }
                    }catch(FormatException)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("\n\tIntroduce un valor válido");
                        Console.ResetColor();
                    }

                    Thread.Sleep(2000);

                } while (opcion != 6);


            }
        }
        
        public void CargaIndividualProducto()
        {
            int opcion = 0;

            Console.Clear();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\t╔══════════════════════════════════════════════════╗");
            Console.WriteLine("\t║ 1.- Añadir unidades a productos de la máquina    ║");
            Console.WriteLine("\t║                                                  ║");
            Console.WriteLine("\t║ 2.- Añadir nuevos productos al almacén           ║");
            Console.WriteLine("\t║                                                  ║");
            Console.WriteLine("\t║ 3.- Retirar productos                            ║"); 
            Console.WriteLine("\t║                                                  ║");
            Console.WriteLine("\t║ 4.- Añadir unidades al almacén                   ║");
            Console.WriteLine("\t║                                                  ║");
            Console.WriteLine("\t║ 5.- Añadir producto a la máquina                 ║");
            Console.WriteLine("\t║                                                  ║");
            Console.WriteLine("\t║ 6.- Salir                                        ║");
            Console.WriteLine("\t╚══════════════════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine();
            Console.Write("\tEscoge una opción: ");
            try
            {
                opcion = int.Parse(Console.ReadLine());
                switch (opcion)
                {
                    case 1: // Añadir unidades a un producto presente en la máquina
                        AddUnidades();
                        break;
                    case 2: // Añadir nuevos productos al almacén
                        int opcion2 = 0;
                        do
                        {
                            AddNewProducto(); // Añadimos un producto al almacén
                            Console.Write("¿Quiere añadir otro producto? (1.- Si / 2.- No): "); // Le damos al usuario la opción de añadir otro producto
                            opcion2 = int.Parse(Console.ReadLine());

                        } while (opcion2 == 1);
                        break;

                    case 3: // Quitar unidades a un producto presente en la máquina
                        RetirarProductos(); 
                        break;

                    case 4:
                        // Buscamos el producto del almacén por nombre
                        Producto producto = BuscarProductoAlmacen(null);

                        // Le pedimos al admin las unidades que quiere añadir
                        Console.Write("¿Cuántas unidades quiere añadir?: ");
                        int unidades = int.Parse(Console.ReadLine());
                        producto.AddUnidades(unidades);
                        break;

                    case 5:
                        AddProductoMaquina();
                        break;

                    case 6:
                        Console.Write("\tSaliendo...");
                        Thread.Sleep(1500);
                        break;

                    default:
                        Console.Write("\tIntroduce una opción válida");
                        Thread.Sleep(1500);
                        break;
                }
            }
            catch(FormatException) 
            { 
                Console.Write("\n\tIntroduce un valor válido");
                Thread.Sleep(1500);
            }
            
        }

        public void RetirarProductos()
        {
            // Buscamos el producto deseado
            Producto producto = BuscarProductoMaquina();

            // Pedimos las unidades al admin
            Console.Write("\t¿Cuántas unidades quiere retirar?: ");
            producto.QuitarUnidades(int.Parse(Console.ReadLine()));

            // Si ya no hay existencias del producto en la máquina le damos la opción de retirarlo
            if (producto.Unidades == 0)
            {
                Console.Write("\tSe han retirado todas las unidades, ¿Quiere retirar el producto?(1.- SI / 2.- NO): ");
                int opcionRetirar = int.Parse(Console.ReadLine());
                if (opcionRetirar == 1)
                {
                    ProductosMaquina.Remove(producto);
                }
            }
        }

        public void CargaCompletaProducto()
        {
            int opcion = 0;
            Console.Clear();
            Console.WriteLine("\t╔═══════════════════════════════════════╗");
            Console.WriteLine("\t║ 1.- Cargar Archivo CSV                ║");
            Console.WriteLine("\t║                                       ║");
            Console.WriteLine("\t║ 2.- Cargar Archivo JSON               ║");
            Console.WriteLine("\t║                                       ║");
            Console.WriteLine("\t║ 3.- Salir                             ║");
            Console.WriteLine("\t╚═══════════════════════════════════════╝");
            Console.WriteLine();
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
                            Console.Write("Introduce una opción válida...");
                            Thread.Sleep(1500);
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
                try
                {
                    string[] checkNumProductos = File.ReadAllLines(path);
                    if (checkNumProductos.Length <= 12)
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
                                        MaterialPrecioso mp = new MaterialPrecioso(ProductosAlmacen.Count, campos[1], int.Parse(campos[2]), double.Parse(campos[3]),
                                            campos[4], campos[5], campos[6]);
                                        ProductosMaquina.Add(mp);
                                        break;

                                    case 2:
                                        ProductoAlimenticio pa = new ProductoAlimenticio(ProductosAlmacen.Count, campos[1], int.Parse(campos[2]), double.Parse(campos[3]),
                                            campos[4], campos[7]);
                                        ProductosMaquina.Add(pa);
                                        break;

                                    case 3:
                                        ProductoElectronico pe = new ProductoElectronico(ProductosAlmacen.Count, campos[1], int.Parse(campos[2]), double.Parse(campos[3]),
                                            campos[4], campos[6], bool.Parse(campos[8]), bool.Parse(campos[9]));
                                        ProductosMaquina.Add(pe);
                                        break;
                                }
                            }
                        }
                    }


                    else
                    {
                        Console.WriteLine("No se admiten más de 12 productos en la máquina");
                    }
                }catch (Exception ex) { Console.WriteLine(ex.Message); }
            }
            else
            {
                Console.WriteLine("El archivo dado no existe o no lo hemos encontrado, porfavor inténtalo de nuevo");
            }

            Console.Write("\tPulse cualquier tecla para continuar...");
            Console.ReadKey();
        }

        public void LeerArchivoJSON()
        {
            Console.Write("Introduce la úbicación del archivo: ");
            var path = Console.ReadLine();

            if (File.Exists(path))
            {
                try
                {
                    string json = File.ReadAllText(path);

                    List<Object> ProductosMaquinaJson = JsonSerializer.Deserialize<List<Object>>(json);

                    if (ProductosMaquinaJson.Count != 0 && ProductosMaquinaJson.Count <= 12)
                    {
                        ProductosMaquina.Clear();
                        foreach (Object o in ProductosMaquinaJson)
                        {

                            JsonElement jsonElement = (JsonElement)o;

                            int tipoProducto = jsonElement.GetProperty("TipoProducto").GetInt32();

                            switch (tipoProducto)
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

                    else
                    {
                        if (ProductosMaquinaJson.Count == 0)
                        {
                            Console.WriteLine("No se han encontrado archivos para cargar");
                        }
                        else
                        {
                            Console.WriteLine("No se admiten más de 12 productos en la máquina");
                        }
                    }
                }catch (Exception ex) { Console.WriteLine(ex.Message); }
            }

            else
            {
                Console.WriteLine("\tEl archivo no ha sido encontrado");
            }

            Console.Write("\tPulse cualquier tecla para continuar");
            Console.ReadKey();

        }

        public void AddUnidades()
        {
            Producto productoMaquina = BuscarProductoMaquina();
            if (productoMaquina != null)
            {
                Producto productoAlmacen = BuscarProductoAlmacen(productoMaquina.Nombre);
                Console.Write("Introduce el número de unidades que desa introducir: ");
                int unidades = int.Parse(Console.ReadLine());
                if (unidades + productoMaquina.Unidades > 10)
                {
                    Console.WriteLine("\tLa máquina solo admite 10 unidades por producto");
                    Console.Write("\n\tPulse cualquier tecla para continuar");
                    Console.ReadKey();
                }
                else
                {
                    if (productoAlmacen != null && productoAlmacen.Unidades >= unidades)
                    {
                        productoAlmacen.QuitarUnidades(unidades);
                        productoMaquina.AddUnidades(unidades);
                    }

                    else
                    {
                        Console.WriteLine("\tNo hemos podido completar la operación");
                        Thread.Sleep(1500);
                    }
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
                        ProductoAlimenticio productoAlimenticio = new ProductoAlimenticio(ProductosAlmacen.Count);
                        productoAlimenticio.SolicitarDetalles();
                        ProductosAlmacen.Add(productoAlimenticio);

                        break;

                    case 2:
                        ProductoElectronico productoElectronico = new ProductoElectronico(ProductosAlmacen.Count);
                        productoElectronico.SolicitarDetalles();
                        ProductosAlmacen.Add(productoElectronico);
                        break;

                    case 3:
                        MaterialPrecioso materialPrecioso = new MaterialPrecioso(ProductosAlmacen.Count);
                        materialPrecioso.SolicitarDetalles();
                        ProductosAlmacen.Add(materialPrecioso);
                        break;

                    default:
                        break;
                }

                

            }catch(Exception e) { Console.WriteLine(e.Message); }
            
        }

        public void AddProductoMaquina()
        {
            Producto producto = BuscarProductoAlmacen(null);
            Producto productoClonado = producto.Clonar();
            if (producto != null)
            {
                try
                {
                    producto.MostrarInfo();
                    Console.Write("\n\t¿Quiere añadir este producto a la máquina? (1.- SI | 2.- NO): ");
                    int opcion = int.Parse(Console.ReadLine());

                    int opcionQuitarProducto = 0;
                    if (opcion == 1)
                    {
                        if (producto.Unidades > 0)
                        {
                            if (producto.Unidades > 10)
                            {
                                productoClonado.SetUnidades(10);
                                producto.QuitarUnidades(10);

                            }
                            else
                            {
                                productoClonado.SetUnidades(producto.Unidades);
                                producto.SetUnidades(0);
                            }

                            Console.WriteLine($"\n\tQuedan {producto.Unidades} unidades del producto '{producto.Nombre}' en el almacén");

                        }

                        else
                        {
                            Console.WriteLine($"\n\tNo quedan existencias del producto {producto.Nombre} en el Almacén");
                        }

                        if (ProductosMaquina.Count < 11)
                        {
                            ProductosMaquina.Add(producto);
                            Thread.Sleep(1000);
                            Console.WriteLine("\n\tProducto añadido a la máquina");
                        }

                        else
                        {
                            Console.WriteLine("\n\tLa máquina está al máximo de su capacidad");
                            Thread.Sleep(1000);
                            Console.WriteLine("\n\t¿Desea quitar un producto de la máquina? (1.- SI | 2.- NO): ");
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

                        Console.WriteLine("\n\tOperación realizada correctamente");
                    }
                }catch(FormatException) { Console.Write("\n\tIntroduce un valor válido"); }
            }
        }

        public Producto BuscarProductoAlmacen(string nombre)
        {
            if (nombre == null)
            {
                Console.Write("Introduce el nombre del producto: ");
                nombre = Console.ReadLine();
                foreach (Producto p in ProductosAlmacen)
                {
                    Console.WriteLine($"Nombre: {p.Nombre}, Unidades {p.Unidades}, Precio {p.Precio_Unitario}€," +
                        $" Información del producto: {p.Descripcion}");
                }
                Console.WriteLine();
            }
            Producto producto = ProductosAlmacen.Find(x => x.Nombre.ToLower() == nombre.ToLower());
            if (producto == null)
            {
                Console.WriteLine("El producto solicitado no ha sido encontrado");
            }
            else
            {
                Console.WriteLine("Su producto ha sido encontrado!!!");
            }
            return producto;
        }

        public void GuardarJson()
        {
            if (ProductosAlmacen.Count > 0)
            {
                File.Create("productos.json").Close();

                string json = JsonSerializer.Serialize(ProductosAlmacen);
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
