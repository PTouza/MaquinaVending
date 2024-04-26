using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaquinaVending
{
    internal interface IUsuario
    {
        List<Producto> ProductosMaquina;
        void Menu();
        void PagarTarjeta(double precio);
        void PagarEfectivo(double precio);
    }
}
