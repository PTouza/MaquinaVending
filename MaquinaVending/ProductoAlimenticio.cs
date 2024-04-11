using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaquinaVending
{
    internal class ProductoAlimenticio : Producto
    {
        public string Informacion_Nutricional {  get; set; }

        // CONSTRUCTORES
        public ProductoAlimenticio() { }

        public ProductoAlimenticio(string nombre, int unidades, double precio_Unitario, string descripcion, string info_nutricional) : base (nombre, unidades, precio_Unitario, descripcion)
        {
            Calorias = calorias;
            Grasa = grasa;
            Azucar = azucar;
        }
    }
}
