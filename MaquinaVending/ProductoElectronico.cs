using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaquinaVending
{
    internal class ProductoElectronico : Producto
    {
        public bool Tiene_Bateria { get; set; }
        public bool Precargado { get; set; }

        // CONSTRUCTORES
        public ProductoElectronico() { }

        public ProductoElectronico (string nombre, int unidades, double precio_Unitario, string descripcion, bool tiene_Bateria, bool precargado) : base (nombre, unidades, precio_Unitario, descripcion)
        {
            Materiales = materiales;
            Tiene_Bateria = tiene_Bateria;
            Precargado = precargado;
        }
    }
}
