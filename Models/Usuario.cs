using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Tiendita.Models
{
    class Usuario
    {
        public uint Id { get; set; }

        public string Correo { get; set; }

        public string Contrasena { get; set; }

    }
}
