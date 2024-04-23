using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaquinaVending
{
    internal abstract class Producto : IProducto
    {
        // PROPIEDADES
        public int Id { get; set; } // Vendrá dado por la cantidad de productos que haya en la máquina expendedora 
        public string Nombre { get; set; }
        public int Unidades { get; set; }
        public double Precio_Unitario { get; set; } // Precio de una sola unidad
        public string Descripcion {  get; set; }

        // CONSTRUCTORES
        public Producto() { }
        public Producto(string nombre, int unidades, double precio_Unitario, string descripcion)
        {
            Nombre = nombre;
            Unidades = unidades;
            Precio_Unitario = precio_Unitario;
            Descripcion = descripcion;
        }

        // MÉTODOS
        public double Vender()
        {
            Console.Write("¿Cuantas unidades desea?: ");
            int cantidadProductos = int.Parse(Console.ReadLine());

            Unidades -= cantidadProductos;
            return cantidadProductos * Precio_Unitario;
        }

        public void AddUnidades(int unidades)
        {
            Unidades += unidades;
        }

        public void QuitarUnidades(int unidades)
        {
            Unidades -= unidades;
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
            Console.WriteLine("Unidades: ");
            Unidades = int.Parse(Console.ReadLine());
            Console.Write("Precio por unidad: ");
            Precio_Unitario = int.Parse(Console.ReadLine());
            Console.Write("Descripción del producto: ");
            Descripcion = Console.ReadLine();
        }

        public Producto Clonar()
        {
            return (Producto)this.MemberwiseClone();
        }
    }
}
