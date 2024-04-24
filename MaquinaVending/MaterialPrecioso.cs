using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaquinaVending
{
    internal class MaterialPrecioso : Producto
    {
        // PROPIEDADES
        public  string TipoMaterial {  get; set; }
        public string Peso { get; set; }

        // CONSTRUCTORES
        public MaterialPrecioso() 
        {
            TipoProducto = 1;
        }

        public MaterialPrecioso(int id)
        {
            Id = id;
            TipoProducto = 1;
        }

        public MaterialPrecioso(int id, string nombre, int unidades, double precio_Unitario, string descripcion, 
            string tipoMaterial, string peso) : base (id, nombre, unidades, precio_Unitario,descripcion)
        {
            TipoProducto = 1;
            TipoMaterial = tipoMaterial;
            Peso = peso;
        }

        public override string ToString()
        {
            return $"1;{base.ToString()};{TipoMaterial};{Peso};;;";
        }
        
        public override void MostrarInfo()
        {
            base.MostrarInfo();
            Console.WriteLine($"Tipo de Material: {TipoMaterial} | Peso: {Peso}");
        }

        public override void SolicitarDetalles()
        {
            base.SolicitarDetalles();
            Console.Write("Material: ");
            TipoMaterial = Console.ReadLine();
            Console.Write("Peso (en gramos): ");
            Peso = Console.ReadLine();
        }

    }
}
