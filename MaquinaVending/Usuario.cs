using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace MaquinaVending
{
    internal abstract class Usuario
    {
        protected List<Producto> ProductosMaquina;

        public Usuario() { }

        public Usuario(List<Producto> productosMaquina)
        {
            ProductosMaquina = productosMaquina;
            ProductosMaquina.Capacity = 12;
        }

        public abstract void Menu();

        public void ComprarProducto()
        {
            int opcion = 0;
            Console.Write("¿Quiere continuar con la operación? (1.- Si / 2.- No): ");
            try
            {
                opcion = int.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1: // PIDO EL ID DEL PRODUCTO QUE QUIERE COMPRAR
                        int opcion2 = 0;
                        double precioFinal = 0;
                        bool productoEncontrado = false;
                        do
                        {
                            Producto producto = BuscarProductoMaquina();
                            if (producto != null)
                            {
                                productoEncontrado = true;
                                precioFinal = precioFinal + producto.Vender();
                                Console.Write("¿Quieres añadir otro producto? (1.- Si / 2.-  No): ");
                                opcion2 = int.Parse(Console.ReadLine());
                            }

                        } while (opcion2 == 1);

                        if (productoEncontrado && precioFinal > 0) { PagarProducto(precioFinal);}
                        break;

                    case 2: // CANCELAMOS LA OPERACIÓN Y VUELVE AL MENÚ
                        break;

                    default:
                        break;
                }
            } catch (Exception e) 
            {
                Console.WriteLine(e.Message);
            }
            
        }
        public void PagarProducto(double precio_Producto)
        {

            int opcion = 0;

            do
            {
                Console.Clear();
                Console.WriteLine("Introduce su metodo de pago");
                Console.Clear();
                Console.WriteLine("\t1. Pago de tarjeta");
                Console.WriteLine("\t2. Pago en efectivo");
                Console.WriteLine("\t3. Retroceder");
                Console.WriteLine();
                Console.Write("Escoge una opción: ");
                try 
                {
                    opcion = int.Parse(Console.ReadLine());

                    Console.Clear();

                    switch (opcion)
                    {
                        case 1: // PAGAR CON TARJETA
                            PagarTarjeta(precio_Producto);
                            break;
                        case 2: // PAGAR CON EFECTIVO
                            PagarEfectivo(precio_Producto);

                            break;
                        case 3: // SALIR
                            Console.WriteLine("Salir...");
                            break;
                        default: // OPCIÓN NO VÁLIDA
                            Console.WriteLine("Porfavor, introduzca una opción válida");
                            break;


                    }
                }catch (Exception e)
                {
                    Console.WriteLine (e.Message);
                }
               

            } while (opcion != 3);

        }
        public void PagarTarjeta(double precio)
        {
            /* 1. Para el pago con tarjeta he pedido que me de los datos de la tarjeta además he añadido una condición para cuando el 
                número de la tarjeta el CVV y la fecha de caducidad no sean las correctas no me deje pagar. 
               2. Aparte le he añadido la función ToString().Length que me lee la cantidad de carcteres que he añadido para que cuando el CVV o el número de la tarjeta
                 no tengan los caracteres necesarios me salte un error 
             
             */
            Console.WriteLine($"El precio del producto es: {precio}\n");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[Introduce los datos de la tarjeta]\n");
            Console.ResetColor();
            Console.Write("Introduce el número de la tarjeta: ");
            
            Int64 numTarjeta = Int64.Parse(Console.ReadLine());
            if (numTarjeta.ToString().Length != 16)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" X Error, el número de tarjeta tiene que tener 16 digitos");
                Console.ReadKey();
                Console.ResetColor();
            }
            else if (QuiereContinuar())
            {
                Console.Write("Introduce el CVV de la tarjeta: ");
                int cvv = int.Parse(Console.ReadLine());
                if (cvv.ToString().Length != 3)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("X Error, el CVV tiene que tener por lo menos 3 caracteres");
                    Console.ReadKey();
                    Console.ResetColor();
                }
                else if (QuiereContinuar())
                {
                    Console.Write("Introduce la fecha de caducidad: ");
                    Console.WriteLine("mes/año");
                    DateTime fechaCaducidadTarjeta = DateTime.ParseExact(Console.ReadLine(), "MM/yy", CultureInfo.InvariantCulture);
                    if (fechaCaducidadTarjeta < DateTime.Now)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Tarjeta Caducada");
                        Console.ReadKey();
                        Console.ResetColor();
                    }
                    else if (QuiereContinuar())
                    {
                        Console.Write("Introduce el Pin: ");
                        int pin = int.Parse(Console.ReadLine());
                        if (pin.ToString().Length != 4)
                        {
                            Console.WriteLine("El Pin debe tener cuatro números, porfavor inténtelo de nuevo");
                            Console.ReadKey();
                        }

                        else
                        {
                            VaultBoy();
                            Console.WriteLine($"Operación aceptada, el coste total es {precio} € ");
                            Console.ReadKey();
                        }
                    }
                }
            }

            Console.ReadKey();
        }

        public bool QuiereContinuar()
        {
            bool continuar = false;
            Console.Write("¿Quiere continuar? (1.- SI | 2.- NO): ");
            try
            {
                int opcion = int.Parse(Console.ReadLine());
                if (opcion == 1) { continuar = true; }
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
            return continuar;
        }

        public void PagarEfectivo(double precio)
        {
            Console.WriteLine($"Tiene que pagar {precio} Euros");
            Console.Write("Introduce el dinero para comprar el producto: ");
            double dinero_Introducido = double.Parse(Console.ReadLine());
            if (dinero_Introducido > precio)
            {
                double cambio = dinero_Introducido - precio;
                cambio = Math.Round(cambio, 3); // Redondeo el cambio para tener céntimos exactos
                Console.WriteLine($"Su cambio es de {cambio} Euros");
                Console.ReadKey();
                int cambioEntero = (int)cambio; // Saco la parte entera del cambio usando una conversión implícita a int
                double cambioDecimal = Math.Round(cambio - cambioEntero, 3); // Saco la parte decimal restando la parte entera del cambio al cambio total
                int[] billetes = { 50, 20, 10, 5 }; // Declaro un array de billetes para ver la devolución
                int billetesDevueltos = 0;
                Console.WriteLine("Su cambio es de: ");
                foreach (int billete in billetes)
                {
                    cambio = Math.Round(cambio, 3);
                    if (cambioEntero >= billete)
                    {
                        billetesDevueltos = (cambioEntero / billete);
                        cambio -= billete * billetesDevueltos;
                        cambioEntero -= billete * billetesDevueltos;
                        Console.WriteLine($"\t{billetesDevueltos} billete/s de {billete} €");
                        Console.ReadKey();
                    }
                }

                int[] monedasEnteras = { 2, 1 };
                int monedasDevueltasEnteras = 0;

                foreach (int moneda in monedasEnteras)
                {
                    cambio = Math.Round(cambio, 3);
                    if (cambioEntero >= moneda)
                    {
                        monedasDevueltasEnteras = cambioEntero / moneda;
                        Console.WriteLine($"\t{monedasDevueltasEnteras} moneda/s de {moneda} Euros");
                        Console.ReadKey();
                        cambioEntero -= moneda * monedasDevueltasEnteras;
                        cambio -= moneda * monedasDevueltasEnteras;
                    }

                }

                double[] monedasDecimales = { 0.5, 0.2, 0.1, 0.05, 0.02, 0.01 };
                int monedasDevueltasDecimales = 0;

                foreach (double moneda in monedasDecimales)
                {
                    cambio = Math.Round(cambio, 3);
                    cambioDecimal = Math.Round(cambioDecimal, 3);
                    if (cambioDecimal >= moneda)
                    {
                        monedasDevueltasDecimales = (int)(cambioDecimal / moneda);
                        cambio -= moneda * monedasDevueltasDecimales;
                        cambioDecimal -= moneda * monedasDevueltasDecimales;
                        Console.WriteLine($"\t{monedasDevueltasDecimales} moneda/s de {moneda} Euros");
                        Console.ReadKey ();
                    }
                }
            }

            else if (dinero_Introducido == precio)
            {
                VaultBoy();
                Console.WriteLine("Muchas Gracias por comprar recoja su producto");                
                Console.ReadKey();
            }


            else if (dinero_Introducido < precio)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error, no se ha introducido la cantidad exacta");
                Console.ReadKey();
                Console.ResetColor();
            }

        }

        public Producto BuscarProductoMaquina()
        {
            Console.Clear();

            foreach (Producto p in ProductosMaquina)
            {
                Console.WriteLine($"ID: {p.Id}, Nombre: {p.Nombre}, Unidades {p.Unidades}, Precio {p.Precio_Unitario} Euros," +
                    $" Información del producto: {p.Descripcion}");
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.Write("Introduce el Id del Producto: ");
            int id = int.Parse(Console.ReadLine());
            Producto producto = null;
            producto = ProductosMaquina.Find(x => x.Id == id);

            if (producto == null)
            {
                Console.WriteLine("No hemos podido encontrar su producto");
            }

            else
            {
                Console.WriteLine("Producto encontrado!!!");
            }

            return producto;
        }

        public void MostrarInfo()
        {
            Producto producto = BuscarProductoMaquina();
            if (producto != null)
            {
                producto.MostrarInfo();
            }
        }

        public void VaultBoy()
        {
            Console.WriteLine("                                    ,▄▄▄                              \r\n                                       ▄▓█▀▀▀▀▀█▄                           \r\n               ▄▄▓█`       ,▄▄▓▓▄▄▄▄▄@██▀!√√√√√└▀█▄                         \r\n            .▓█▀██       #█▀▀└:.!╙▀▀██▀:√√√√√√√√√!▀▀█▓▓▄▄                   \r\n           ╓█▀..▀█▓▄▄▄▄▓▀▀:√√√√√√√√√√√√√√√√√√√√√√√√√░░▀▀██▄                 \r\n           ██.√√√!▀▀▀▀▀:√√√√√√√√√√√√√√√√√√√√√√√√√√√√╠░░░░▀█▄                \r\n           █▌√√√√√√√√√√√√√▄▄▄▄▄.√√√√√√√╓▄▄▄.√√√√√√√√╠░░░░░╙█▄               \r\n           ██.√√√√√√√√√▄#█▀╙`╙▀█▓▄▄▄@▓██████▄.√√√√√╠░░░░░░░╙█▓▄             \r\n         ┌████:√√√√√(▄█▀╙       └▀▀▀▀└   └▀▀██,√√╓╢░░░░░░░░░░▀██▄           \r\n         ██:√╙▀▓▄▄▓▓▀▀                      └██▄░░░░░░░░░░░░░░░██▄          \r\n         █▌√√╓██▀  ▄▄@╕                       ▀▀█▓▀▀▀▀▀▀███▄░░░░██▄         \r\n         ██▄▓█▀  ╙▀▀▀▀▀                 ,▄               ▀███░░░░██▄        \r\n          ███`                         ▓███,     .        ███░░░░║██        \r\n         ▓█▀     ,▄                     └▀██▄            ▄██▀░░░░░██`       \r\n        ██▀     ███¼        ,              ▀▀        ╓@██▀▀░░░░░░░██        \r\n       ██▀     ▐███       ╓█▀        ▄▄,          .  ▄╙▀█░░░░░░░░╟██        \r\n      ▐█▌       ▀▀└     .▓█└        #███          .  ╙█▓,▀█░░░░░░██▌        \r\n      ██              ▄▓█▀          ███▌          . .▄,▀█▄╙█░░░░███         \r\n     ╟█▌            #██▀            ╙▀▀           .  ▀█▓,█▄╙█░░███          \r\n     ██─            ███                             ▓▄,▀█▄█,█░███`          \r\n     ██             ╙███                         .   ▀█▄╙█Ö█████            \r\n     ██    ,#         ╙╙                         . ╙█▄ ▀ ╙████▀             \r\n     ██  ╒███▄▄                  ▐█▄            .   ╙▀  .@███┘              \r\n     ██▌  ██▄ └╙▀▀#╦▄▄▄▄▄▄▄▄▄▄▄▄#████▄         .         ╙███               \r\n     ▐██   ▀ ▀▓▄,     `└╙└└ .      ███▌        .          ╟██               \r\n      ██▌      ╙▀█▓▄▄▄,   .,▄▄▄▓▓▀▀╙██        .          .███               \r\n      └██▄        └▀▀▀███▀▀▀▀╙\"     ▀       ..          ▄███                \r\n       ╙██▄       Ñ▓▓▓▓µ                   ..    ▄▓▓▓▓███▀`                 \r\n        └██▄        `└└                  ..    ▄███▀└└                      \r\n          ▀██▄                          .   ▄▓██▀└                          \r\n            ▀█▓▄                     ..  ▄▓██▀╙                             \r\n              ╙▀█▄,                .╓▄▓██████                               \r\n                 ╙██▓▄         ...   '' ▄██▀                                \r\n                  ╙█████▓▓▄▄▄▄      .▄▄██▀'                                 \r\n                    ▀█████▄▄▄▄▄▄▄▄▓████▀                                    \r\n                       ╙▀▀▀██████▀▀▀╙     ");
            Thread.Sleep(2000);
            Console.Clear();
        }



    }
}
