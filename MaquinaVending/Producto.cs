using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaquinaVending
{
    internal abstract class Producto : IProducto
    {

        public string Nombre { get; set; }
        public int Unidades { get; set; }
        public double Precio_Unitario { get; set; }
        public string Descripcion {  get; set; }

        public Producto() { }
        public Producto(string nombre, int unidades, double precio_Unitario, string descripcion)
        {
            Nombre = nombre;
            Unidades = unidades;
            Precio_Unitario = precio_Unitario;
            Descripcion = descripcion;
        }

        public double Vender(int cantidadProductos)
        {
            Unidades -= cantidadProductos;

            return cantidadProductos * Precio_Unitario;
        }

        public void AddUnidades(int unidades)
        {
            Unidades += unidades;
        }

        public override string ToString()
        {
            return $"";
        }

        public virtual void MostrarInfo()
        {

        }
    }
}
