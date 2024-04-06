using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaquinaVending
{
    internal class MaterialesPreciosos : Producto
    {
        // PROPIEDADES
        public  string Materiales {  get; set; }
        public string Peso { get; set; }

        // CONSTRUCTORES
        public MaterialesPreciosos() { }

        public MaterialesPreciosos(string nombre, int unidades, double precio_Unitario, string descripcion, 
            string materiales, string peso) : base (nombre, unidades, precio_Unitario,descripcion)
        {
            Materiales = materiales;
            Peso = peso;
        }
    }
}
