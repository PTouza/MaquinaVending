using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MaquinaVending
{
    internal class Admin : Usuario 
    { 
        public string Password { get; set; }

        protected List<Producto> productos;
    }
}
