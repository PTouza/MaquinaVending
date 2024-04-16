using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MaquinaVending
{
    internal class ProductoElectronico : Producto
    {
        public string Materiales {  get; set; }
        public bool Tiene_Bateria { get; set; }
        public bool Precargado { get; set; }

        // CONSTRUCTORES
        public ProductoElectronico() { }

        public ProductoElectronico (string nombre, int unidades, double precio_Unitario, string descripcion,string materiales, bool tiene_Bateria, bool precargado) : base (nombre, unidades, precio_Unitario, descripcion)
        {
            Materiales = materiales;
            Tiene_Bateria = tiene_Bateria;
            Precargado = precargado;
        }

        public override void MostrarInfo()
        {
            base.MostrarInfo();
            Console.Write($"Materiales: {Materiales} | ¿Tiene batería?: {Tiene_Bateria} | ¿Está precargado?: {Precargado}");
        }

        public override string ToString()
        {
            base.ToString();
            return $"{Materiales};{Tiene_Bateria};{Precargado}";
        }
    }
}
