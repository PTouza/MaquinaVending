﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MaquinaVending
{
    internal class ProductoAlimenticio : Producto
    {
        public string InfoNutricional { get; set; }

        public ProductoAlimenticio()
        {
            TipoProducto = 2;
        }

        public ProductoAlimenticio(int id)
        {
            Id = id;
            TipoProducto = 2;
        }

        public ProductoAlimenticio(int id, string nombre, int unidades, double precio_Unitario, string descripcion, string infoNutricional) 
            : base(id, nombre, unidades, precio_Unitario, descripcion)
        {
            TipoProducto = 2;
            InfoNutricional = infoNutricional;
        }

        public override void MostrarInfo()
        {
            base.MostrarInfo();
            Console.WriteLine($"Información nutricional: {InfoNutricional}");
        }


        public override void SolicitarDetalles()
        {
            base.SolicitarDetalles();
            Console.Write("Información nutricional: ");
            InfoNutricional = Console.ReadLine();
        }
    }
}
