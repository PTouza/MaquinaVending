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
    internal abstract class Usuario 
    {
        protected List<Producto> listaProductos;

        public Usuario() { }

        public Usuario(List<Producto> productos)
        {
            listaProductos = productos;
        }

        public abstract void Menu();

        public void ComprarProducto()
        {
            int opcion = 0;
            Console.Write("¿Quiere continuar con la operación? (1.- Si / 2.- No): ");
            opcion = int.Parse(Console.ReadLine());

            switch(opcion)
            {
                case 1: // PIDO EL ID DEL PRODUCTO QUE QUIERE COMPRAR
                    // IProducto producto  = BuscarProducto();
                    break;

                case 2: // CANCELAMOS LA OPERACIÓN Y VUELVE AL MENÚ
                    break;

                default:
                    break;
            }
        }
        public void PagarProducto(int precio_Producto)
        { 
            
            int opcion = 0;

            do
            {
                Console.WriteLine("Introduce su metodo de pago");
                Console.Clear();
                Console.WriteLine("1. Pago de tarjeta");
                Console.WriteLine("2. Pago en efectivo");
                Console.WriteLine("3. Retroceder");
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

            } while (opcion != 3);
           
        }
        public void PagarTarjeta(int precio)
        {
            Console.WriteLine($"El precio del producto es: {precio}");
            Console.WriteLine("Introduce una tarjeta para pagar");
            float dinero_Introducido = float.Parse(Console.ReadLine());
            if (dinero_Introducido == precio)
            {

                Console.WriteLine("Operación Aceptada");


            }
            else
            {
                Console.WriteLine("Operación cancelada  :( ");
            }
        }



        public void PagarEfectivo(int precio)
        {
            Console.WriteLine($"El precio del producto es : {precio}");
            Console.Write("Introduce el dinero para pagar el producto: ");
            float dinero_Introducido = float.Parse(Console.ReadLine());
            if (dinero_Introducido > precio)
            {
                float cambio = dinero_Introducido - precio;
                Console.WriteLine($":) Muchas gracias recoja el producto y el cambio de : {cambio} ");


            }
            else if (dinero_Introducido == precio)
            {
                Console.WriteLine("Muchas gracias, recoja el producto");
            }
            else
            {
                Console.WriteLine("Error, no se ha introducido la cantidad exacta");
            }

        }
        public Producto BuscarProducto()
        {
            Console.Write("Introduce el Id del Producto: ");
            int id = int.Parse(Console.ReadLine());
            Producto p = null;

            foreach (Producto e in listaProductos)
            {
                if (e.Id == id)
                {
                    p = e;
                }
            }
            return p;
        }


    }
}
