﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace MaquinaVending
{
    internal abstract class Usuario : IUsuario
    {
        protected List<Producto> ProductosMaquina;

        public Usuario() { }

        public Usuario(List<Producto> productos)
        {
            ProductosMaquina = productos;
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
                        do
                        {
                            Producto producto = BuscarProductoMaquina();
                            if (producto != null)
                            {
                                precioFinal = precioFinal + producto.Vender();
                                Console.Write("¿Quieres añadir otro producto? (1.- Si / 2.-  No): ");
                                opcion2 = int.Parse(Console.ReadLine());
                            }

                        } while (opcion2 == 1);

                        PagarProducto(precioFinal);
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
            Console.WriteLine($"El precio del producto es: {precio}\n");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[Introduce los datos de la tarjeta]\n");
            Console.ResetColor();
            Console.Write("Introduce el número de la tarjeta: ");

            int numTarjeta = int.Parse(Console.ReadLine());
            if (numTarjeta.ToString().Length != 16)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" X Error, el número de tarjeta tiene que tener 16 digitos");
                Console.ReadKey();
                Console.ResetColor();
            }
            else if (QuiereContinuar())
            {
                Console.Write("Introduce el CVV de la tarjeta:");
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
                    Console.Write("Introduce la fecha de caducidad:");
                    DateTime fechaCaducidadTarjeta = DateTime.Parse(Console.ReadLine());
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
                        }

                        else
                        {
                            Console.WriteLine($"Operación aceptada, el coste total es {precio} € ");
                        }
                    }
                }
            }


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
            Console.WriteLine($"El precio del producto es : {precio}");
            Console.Write("Introduce el dinero para pagar el producto: ");
            float dinero_Introducido = float.Parse(Console.ReadLine());
            if (dinero_Introducido > precio)
            {
                double cambio = dinero_Introducido - precio;
                Console.WriteLine($":) Muchas gracias recoja el producto y el cambio de : {cambio} ");
                Console.ReadKey();


            }
            else if (dinero_Introducido == precio)
            {
                Console.WriteLine("Muchas gracias, recoja el producto");

                Console.ReadKey(true);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error, no se ha introducido la cantidad exacta");
                Console.ReadKey();
                Console.ResetColor();
            }

        }

        public Producto BuscarProductoMaquina()
        {
            foreach (Producto p in ProductosMaquina)
            {
                Console.WriteLine($"ID: {p.Id}, Nombre: {p.Nombre}, Unidades {p.Unidades}, Precio {p.Precio_Unitario}€," +
                    $" Información del producto: {p.Descripcion}");
            }

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

    }
}
