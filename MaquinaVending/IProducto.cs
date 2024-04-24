using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MaquinaVending
{
    internal interface IProducto
    {
        int TipoProducto { get; set; }
        int Id { get; set; }
        string Nombre { get; set; }
        int Unidades { get; set; }
        double Precio_Unitario { get; set; }
        string Descripcion { get; set; }

        double Vender();
        void AddUnidades(int unidades);
        string ToString();
        void MostrarInfo();
        void SolicitarDetalles();
    }
}
