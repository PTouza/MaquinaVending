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
        public string Calorias { get; set; }
        public string Grasa { get; set; }
        public string Azucar { get; set; }

        public ProductoAlimenticio() { }

        public ProductoAlimenticio(string nombre, int unidades, double precio_Unitario, string descripcion, string calorias, string grasa, string azucar) : base(nombre, unidades, precio_Unitario, descripcion)
        {
            Calorias = calorias;
            Grasa = grasa;
            Azucar = azucar;
        }

        public override void MostrarInfo()
        {
            base.MostrarInfo();
            Console.Write($"Calorías: {Calorias} | Grasa: {Grasa} | Azúcar: {Azucar}");
        }

        public override string ToString()
        {
            base.ToString();
            return $"{Calorias};{Grasa};{Azucar}";
        }

        public override void SolicitarDetalles()
        {
            base.SolicitarDetalles();
            Console.Write("Cantidad de calorías: ");
            Calorias = Console.ReadLine();
            Console.WriteLine("Cantidad de grasa: ");
            Grasa = Console.ReadLine();
            Console.Write("Cantidad de azúcar: ");
            Azucar = Console.ReadLine();
        }
    }
}
