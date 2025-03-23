using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace examenSasfCSharp
{
    class Usuario
    {
        public string Id { get; set; }
        public string Nombres { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }

        public Usuario() { }
        public Usuario(string id, string nombres, string correo = "", string telefono = "")
        {
            Id = id;
            Nombres = nombres;
            Correo = correo;
            Telefono = telefono;
        }
    }
}
