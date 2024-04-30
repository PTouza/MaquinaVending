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
        // Lista de producto que habrá en la máquina expendedora
        protected List<Producto> ProductosMaquina;

        // Constructores
        public Usuario() { }

        public Usuario(List<Producto> productosMaquina)
        {
            ProductosMaquina = productosMaquina;
            ProductosMaquina.Capacity = 12;
        }

        // Métodos
        public abstract void Menu(); // El método Menu() lo declaramos como abstract ya que va a ser propio de cada clase derivada

        public void ComprarProducto()
        {
            int opcion = 0;
            // Pedimos confirmación de que quiere comprar el producto
            Console.Write("\n\t¿Quiere continuar con la operación? (1.- Si / 2.- No): ");
            try
            {
                opcion = int.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1: // PIDO EL ID DEL PRODUCTO QUE QUIERE COMPRAR
                        int opcion2 = 0;
                        double precioFinal = 0;
                        bool productoEncontrado = false; // esta variable nos ayudará a la hora de pasar el precio final a la función correspondiente
                        do
                        {
                            // Buscamos el producto
                            Producto producto = BuscarProductoMaquina();

                            if (producto != null)
                            {
                                // Comprobamos si queda unidades
                                if (producto.Unidades > 0)
                                {
                                    // Si quedan unidades pasamos a deducir el precio final
                                    productoEncontrado = true;
                                    precioFinal = precioFinal + producto.Vender();

                                    Console.Write("\n\t¿Quieres añadir otro producto? (1.- Si / 2.-  No): ");
                                    opcion2 = int.Parse(Console.ReadLine());
                                }

                                else
                                {
                                    Console.WriteLine("\n\tLo sentimos, no nos quedan unidades de este producto");
                                }
                            }

                        } while (opcion2 == 1);

                        // Si hemos encontrado el producto y el precioFinal es mayor que 0 pasamos a pagar
                        if (productoEncontrado && precioFinal > 0) { PagarProducto(precioFinal); }
                        break;

                    case 2: // CANCELAMOS LA OPERACIÓN Y VUELVE AL MENÚ
                        Console.Write("Cancelando la operación...");
                        Thread.Sleep(1500);
                        break;

                    default:
                        Console.Write("Introduce una opción válida...");
                        Thread.Sleep(1500);
                        break;
                }
            }
            catch (FormatException)
            {
                Console.Write("\n\tIntroduce un valor válido");
                Thread.Sleep(1500);
            }

        }

        public void PagarProducto(double precio_Producto)
        {

            int opcion = 0;
            Console.Clear();
            Console.WriteLine("\tIntroduzca su metodo de pago");
            Console.WriteLine();
            Console.Clear();
            Console.WriteLine("\t╔═══════════════════════════════╗");
            Console.WriteLine("\t║ 1.- Pago con tarjeta          ║");
            Console.WriteLine("\t║                               ║");
            Console.WriteLine("\t║ 2.- Pago con efectivo         ║");
            Console.WriteLine("\t║                               ║");
            Console.WriteLine("\t║ 3.- Salir                     ║");
            Console.WriteLine("\t╚═══════════════════════════════╝");
            Console.WriteLine();
            Console.Write("\tEscoge una opción: ");
            try
            {
                opcion = int.Parse(Console.ReadLine());

                Console.Clear();

                switch (opcion)
                {
                    case 1: // PAGAR CON TARJETA
                        PagarTarjeta(precio_Producto);
                        VaultBoy();
                        break;
                    case 2: // PAGAR CON EFECTIVO
                        PagarEfectivo(precio_Producto);
                        VaultBoy();
                        break;
                    case 3: // SALIR
                        Console.WriteLine("Salir...");
                        break;
                    default: // OPCIÓN NO VÁLIDA
                        Console.WriteLine("Porfavor, introduzca una opción válida");
                        break;


                }

                
            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\tIntroduce un valor válido");
                Console.ResetColor();
                Thread.Sleep(1500);
            }


        }

        public void PagarTarjeta(double precio)
        {
            /* 1. Para el pago con tarjeta he pedido que me de los datos de la tarjeta además he añadido una condición para cuando el 
                número de la tarjeta el CVV y la fecha de caducidad no sean las correctas no me deje pagar. 
               2. Aparte le he añadido la función ToString().Length que me lee la cantidad de carcteres que he añadido para que cuando el CVV o el número de la tarjeta
                 no tengan los caracteres necesarios me salte un error 
             */

            Console.WriteLine($"\tEl precio del producto es: {precio}\n");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\t[Introduce los datos de la tarjeta]\n");
            Console.ResetColor();
            Console.Write("\tIntroduce el número de la tarjeta: ");

            // Para almacenar el numero de la tarjeta necesitamos un entero de 64 bits, debido a que es un número de 16 dígitos
            Int64 numTarjeta = Int64.Parse(Console.ReadLine()); 

            // Comprobamos si los datos son correctos
            if (numTarjeta.ToString().Length != 16)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" X Error, el número de tarjeta tiene que tener 16 digitos");
                Console.ResetColor();
            }
            else if (QuiereContinuar())
            {
                Console.Write("\tIntroduce el CVV de la tarjeta: ");
                int cvv = int.Parse(Console.ReadLine());
                if (cvv.ToString().Length != 3)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\tX Error, el CVV tiene que tener por lo menos 3 caracteres");
                    Console.ResetColor();
                }
                else if (QuiereContinuar())
                {
                    Console.Write("\n\tIntroduce la fecha de caducidad (mes/año): ");
                    // Declaramos la variable junto con el formato que tendrá la entrada por consola
                    DateTime fechaCaducidadTarjeta = DateTime.ParseExact(Console.ReadLine(), "MM/yy", CultureInfo.InvariantCulture);
                    if (fechaCaducidadTarjeta < DateTime.Now)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\tTarjeta Caducada");
                        Console.ResetColor();
                    }
                    else if (QuiereContinuar())
                    {
                        Console.Write("\tIntroduce el Pin: ");
                        int pin = int.Parse(Console.ReadLine());
                        if (pin.ToString().Length != 4)
                        {
                            Console.WriteLine("\tEl Pin debe tener cuatro números, porfavor inténtelo de nuevo");
                        }

                        else
                        {
                            Console.WriteLine($"\tOperación aceptada, el coste total es {precio} € ");
                        }
                    }
                }
            }

            Thread.Sleep(2000);
        }

        public bool QuiereContinuar()
        {
            // Hemos creado esta función para pedirle al usuario si quiere continuar con alguna operación, así reutilizamos código y evitamos recursividad
            bool continuar = false;
            Console.Write("\t¿Quiere continuar? (1.- SI | 2.- NO): ");
            try
            {
                int opcion = int.Parse(Console.ReadLine());
                if (opcion == 1) { continuar = true; }
            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\tIntroduce un valor válido");
                Console.ResetColor();
            }

            return continuar;
        }

        public void PagarEfectivo(double precio)
        {
            Console.WriteLine($"\n\tTiene que pagar {precio} Euros");
            Console.Write("\n\tIntroduce el dinero para comprar el producto: ");
            double dinero_Introducido = double.Parse(Console.ReadLine());
            if (dinero_Introducido > precio)
            {
                double cambio = dinero_Introducido - precio;
                // Redondeo el cambio para tener céntimos exactos
                cambio = Math.Round(cambio, 3); 

                Console.WriteLine($"\n\tSu cambio es de {cambio} Euros");
                Thread.Sleep(1000);

                // Saco la parte entera del cambio usando una conversión implícita a int, ya que c# elimina los decimales sin hacer ninguna aproximación
                int cambioEntero = (int)cambio;

                // Saco la parte decimal restando la parte entera del cambio al cambio total
                double cambioDecimal = Math.Round(cambio - cambioEntero, 3);

                // Declaro un array de billetes para calcular el cambio de billetes, en caso de que haya
                int[] billetes = { 50, 20, 10, 5 };
                int billetesDevueltos = 0;

                Console.WriteLine("\n\tAquí tiene su cambio: ");

                // Comprobamos si alguno de los billetes es mayor que el cambio entero y calculamos los billetes a devolver
                foreach (int billete in billetes)
                {
                    
                    if (cambioEntero >= billete)
                    {
                        billetesDevueltos = (cambioEntero / billete);
                       
                        cambioEntero -= billete * billetesDevueltos;
                        Console.WriteLine($"\t{billetesDevueltos} billete/s de {billete} €");
                        Thread.Sleep(500);
                    }
                }

                // Declaramos un array con las monedas enteras
                int[] monedasEnteras = { 2, 1 };
                int monedasDevueltasEnteras = 0;

                // Comprobamos si alguna moneda es mayor que el cambio entero y calculamos las monedas a devolver
                foreach (int moneda in monedasEnteras)
                {
                    
                    if (cambioEntero >= moneda)
                    {
                        monedasDevueltasEnteras = cambioEntero / moneda;
                        Console.WriteLine($"\t{monedasDevueltasEnteras} moneda/s de {moneda} Euros");
                        Thread.Sleep(500);
                        cambioEntero -= moneda * monedasDevueltasEnteras;
                        
                    }

                }

                // Declaramos un array con los céntimos
                double[] monedasDecimales = { 0.5, 0.2, 0.1, 0.05, 0.02, 0.01 };
                int monedasDevueltasDecimales = 0;

                // Comprobamos si alguna de las monedas son mayores que el cambio decimal
                foreach (double moneda in monedasDecimales)
                {
                    // Redondeamos para usar las cifras que necesitamos, que son 3
                    cambioDecimal = Math.Round(cambioDecimal, 3);
                    if (cambioDecimal >= moneda)
                    {
                        monedasDevueltasDecimales = (int)(cambioDecimal / moneda);
                        cambioDecimal -= moneda * monedasDevueltasDecimales;
                        Console.WriteLine($"\t{monedasDevueltasDecimales} moneda/s de {moneda} Euros");
                        Thread.Sleep(500);
                    }
                }
            }

            else if (dinero_Introducido == precio)
            {
                Console.WriteLine("Muchas Gracias por comprar recoja su producto");
            }


            else if (dinero_Introducido < precio)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error, no se ha introducido la cantidad suficiente");
                Thread.Sleep(1500);
                Console.ResetColor();
            }

        }

        public Producto BuscarProductoMaquina()
        {
            Console.Clear();

            // Damos una información general de cada producto
            foreach (Producto p in ProductosMaquina)
            {
                Console.WriteLine($" ID: {p.Id}, Nombre: {p.Nombre}, Unidades {p.Unidades}, Precio {p.Precio_Unitario} Euros," +
                    $" Información del producto: {p.Descripcion}");
                Console.WriteLine();
                Thread.Sleep(500);
            }
            Console.WriteLine();
            Console.Write("\tIntroduce el Id del Producto: ");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine();
            Producto producto = null;

            // Buscamos el producto que necesitamos
            producto = ProductosMaquina.Find(x => x.Id == id);

            if (producto == null)
            {
                Console.WriteLine("\tNo hemos podido encontrar su producto");
                Console.WriteLine();
            }

            else
            {
                Console.WriteLine("\tProducto encontrado!!!");
                Console.WriteLine();
            }

            return producto;
        }

        public void MostrarInfo()
        {
            // Buscamos el producto y le mostramos la información al usuario
            Producto producto = BuscarProductoMaquina();
            if (producto != null)
            {
                producto.MostrarInfo();
            }
        }

        public void VaultBoy()
        {
            // Imagen del VaultBoy de Fallout para cuando se haya completado una compra
            Console.WriteLine("                                    ,▄▄▄                              \r\n                                       ▄▓█▀▀▀▀▀█▄                           \r\n               ▄▄▓█`       ,▄▄▓▓▄▄▄▄▄@██▀!√√√√√└▀█▄                         \r\n            .▓█▀██       #█▀▀└:.!╙▀▀██▀:√√√√√√√√√!▀▀█▓▓▄▄                   \r\n           ╓█▀..▀█▓▄▄▄▄▓▀▀:√√√√√√√√√√√√√√√√√√√√√√√√√░░▀▀██▄                 \r\n           ██.√√√!▀▀▀▀▀:√√√√√√√√√√√√√√√√√√√√√√√√√√√√╠░░░░▀█▄                \r\n           █▌√√√√√√√√√√√√√▄▄▄▄▄.√√√√√√√╓▄▄▄.√√√√√√√√╠░░░░░╙█▄               \r\n           ██.√√√√√√√√√▄#█▀╙`╙▀█▓▄▄▄@▓██████▄.√√√√√╠░░░░░░░╙█▓▄             \r\n         ┌████:√√√√√(▄█▀╙       └▀▀▀▀└   └▀▀██,√√╓╢░░░░░░░░░░▀██▄           \r\n         ██:√╙▀▓▄▄▓▓▀▀                      └██▄░░░░░░░░░░░░░░░██▄          \r\n         █▌√√╓██▀  ▄▄@╕                       ▀▀█▓▀▀▀▀▀▀███▄░░░░██▄         \r\n         ██▄▓█▀  ╙▀▀▀▀▀                 ,▄               ▀███░░░░██▄        \r\n          ███`                         ▓███,     .        ███░░░░║██        \r\n         ▓█▀     ,▄                     └▀██▄            ▄██▀░░░░░██`       \r\n        ██▀     ███¼        ,              ▀▀        ╓@██▀▀░░░░░░░██        \r\n       ██▀     ▐███       ╓█▀        ▄▄,          .  ▄╙▀█░░░░░░░░╟██        \r\n      ▐█▌       ▀▀└     .▓█└        #███          .  ╙█▓,▀█░░░░░░██▌        \r\n      ██              ▄▓█▀          ███▌          . .▄,▀█▄╙█░░░░███         \r\n     ╟█▌            #██▀            ╙▀▀           .  ▀█▓,█▄╙█░░███          \r\n     ██─            ███                             ▓▄,▀█▄█,█░███`          \r\n     ██             ╙███                         .   ▀█▄╙█Ö█████            \r\n     ██    ,#         ╙╙                         . ╙█▄ ▀ ╙████▀             \r\n     ██  ╒███▄▄                  ▐█▄            .   ╙▀  .@███┘              \r\n     ██▌  ██▄ └╙▀▀#╦▄▄▄▄▄▄▄▄▄▄▄▄#████▄         .         ╙███               \r\n     ▐██   ▀ ▀▓▄,     `└╙└└ .      ███▌        .          ╟██               \r\n      ██▌      ╙▀█▓▄▄▄,   .,▄▄▄▓▓▀▀╙██        .          .███               \r\n      └██▄        └▀▀▀███▀▀▀▀╙\"     ▀       ..          ▄███                \r\n       ╙██▄       Ñ▓▓▓▓µ                   ..    ▄▓▓▓▓███▀`                 \r\n        └██▄        `└└                  ..    ▄███▀└└                      \r\n          ▀██▄                          .   ▄▓██▀└                          \r\n            ▀█▓▄                     ..  ▄▓██▀╙                             \r\n              ╙▀█▄,                .╓▄▓██████                               \r\n                 ╙██▓▄         ...   '' ▄██▀                                \r\n                  ╙█████▓▓▄▄▄▄      .▄▄██▀'                                 \r\n                    ▀█████▄▄▄▄▄▄▄▄▓████▀                                    \r\n                       ╙▀▀▀██████▀▀▀╙     ");

            Thread.Sleep(1000);
            
        }



    }
}
