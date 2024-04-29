using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MaquinaVending
{
    internal abstract class Producto : IProducto
    {
        // PROPIEDADES
        public int TipoProducto { get; set; }
        [JsonInclude]
        public int Id { get; protected set; } // Vendrá dado por la cantidad de productos que haya en la máquina expendedora 
        public string Nombre { get; set; }

        [JsonInclude]
        public int Unidades { get; protected set; }
        public double Precio_Unitario { get; set; } // Precio de una sola unidad
        public string Descripcion {  get; set; }

        // CONSTRUCTORES
        public Producto() { }
        public Producto(int id, string nombre, int unidades, double precio_Unitario, string descripcion)
        {
            Id = id;
            Nombre = nombre;
            Unidades = unidades;
            Precio_Unitario = precio_Unitario;
            Descripcion = descripcion;
        }

        // MÉTODOS
        public double Vender()
        {
            int cantidadProductos;
            do
            {
                Console.Write("\t¿Cuantas unidades desea?: ");
                cantidadProductos = int.Parse(Console.ReadLine());
                if(cantidadProductos > Unidades) 
                { 
                    Console.WriteLine("\tNo tenemos suficientes unidades");
                    cantidadProductos = 0;
                }
            } while (cantidadProductos > Unidades);
            QuitarUnidades(cantidadProductos);
            return cantidadProductos * Precio_Unitario;
        }

        public void AddUnidades(int unidades)
        {
            Unidades += unidades;
        }

        public void QuitarUnidades(int unidades)
        {
            if (unidades > Unidades)
            {
                Console.WriteLine("\tNo tenemos suficientes unidades disponibles, inténtalo de nuevo");
            }
            else
            {
                Unidades -= unidades;
            }
        }

        public void SetUnidades(int unidades)
        {
            Unidades = unidades;
        }

        public override string ToString()
        {
            return $"{Nombre};{Unidades};{Precio_Unitario};{Descripcion}"; // Formato del archivo CSV
        }

        public virtual void MostrarInfo()
        {
            Console.Write($"({Id}) Nombre: {Nombre} | Unidades: {Unidades} | Precio: {Precio_Unitario}€ " +
                $"| Descripción: {Descripcion}");
        }

        public virtual void SolicitarDetalles()
        {
            Console.Write("Nombre del producto: ");
            Nombre  = Console.ReadLine();
            Console.Write("Unidades: ");
            Unidades = int.Parse(Console.ReadLine());
            Console.Write("Precio por unidad: ");
            Precio_Unitario = double.Parse(Console.ReadLine());
            Console.Write("Descripción del producto: ");
            Descripcion = Console.ReadLine();
        }

        public Producto Clonar()
        {
            return (Producto)this.MemberwiseClone();
        }
    }
}
