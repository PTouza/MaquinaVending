using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MaquinaVending
{
    internal class ProductoAlimenticio : Producto
    {
        public string InfoNutricional { get; set; }

        public ProductoAlimenticio() { }

        public ProductoAlimenticio(string nombre, int unidades, double precio_Unitario, string descripcion, string infoNutricional) : base(nombre, unidades, precio_Unitario, descripcion)
        {
            InfoNutricional = infoNutricional;
        }

        public override void MostrarInfo()
        {
            base.MostrarInfo();
            Console.Write($"Información nutricional: {InfoNutricional}");
        }

        public override string ToString()
        {
            return $"2;{base.ToString()};;;{InfoNutricional};;";
        }

        public override void SolicitarDetalles()
        {
            base.SolicitarDetalles();
            Console.Write("Información nutricional: ");
            InfoNutricional = Console.ReadLine();
        }
    }
}
