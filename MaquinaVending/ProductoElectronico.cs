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
        public string Peso {  get; set; }
        public bool Tiene_Bateria { get; set; }
        public bool Precargado { get; set; }

        // CONSTRUCTORES
        public ProductoElectronico()
        {
            TipoProducto = 3;
        }

        public ProductoElectronico (string nombre, int unidades, double precio_Unitario, string descripcion,string peso,
            bool tiene_Bateria, bool precargado) : base (nombre, unidades, precio_Unitario, descripcion)
        {
            TipoProducto = 3;
            Peso = peso;
            Tiene_Bateria = tiene_Bateria;
            Precargado = precargado;
        }

        public override void MostrarInfo()
        {
            base.MostrarInfo();
            Console.Write($"Peso: {Peso} | ¿Tiene batería?: {Tiene_Bateria} | ¿Está precargado?: {Precargado}");
        }

        public override string ToString()
        {
            return $"3;{base.ToString()};;{Peso};;{Tiene_Bateria};{Precargado}";
        }


        public override void SolicitarDetalles()
        {
            base.SolicitarDetalles();
            Console.Write("Peso: ");
            Peso = Console.ReadLine();
            Console.WriteLine("¿Tiene batería?: ");
            Tiene_Bateria = bool.Parse(Console.ReadLine());
            Console.Write("¿Está precargado?: ");
            Precargado = bool.Parse(Console.ReadLine());
        }
    }
}
