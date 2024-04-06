using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaquinaVending
{
    internal class Cliente : Usuario
    {

        public Cliente() { }

        public Cliente(List<Producto> productos) : base(productos)
        {

        }

        public override void Menu()
        {
            
        }
    }
}
