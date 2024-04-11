using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaquinaVending
{
    internal class ProductoAlimenticio : Producto
    {
        public string Calorias {  get; set; }
        public string Grasa { get; set; }
        public string Azucar { get; set; }

        public ProductoAlimenticio() { }

        public ProductoAlimenticio(string nombre, int unidades, double precio_Unitario, string descripcion, string calorias, string grasa, string azucar) : base (nombre, unidades, precio_Unitario, descripcion)
        {
            Calorias = calorias;
            Grasa = grasa;
            Azucar = azucar;
        }
    }
}
