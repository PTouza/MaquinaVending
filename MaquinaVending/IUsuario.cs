using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaquinaVending
{
    internal interface IUsuario
    { 

        void Menu();
        void PagarTarjeta(int precio);
        void PagarEfectivo(int precio);
    }
}
