using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace examenSasfCSharp
{
    class Prestamo
    {
        public string Id { get; set; }
        public string ISBN { get; set; }
        public string Usuario { get; set; }
        public DateTime FechaPrestamo { get; set; }
        public DateTime FechaDevolucion { get; set; }
        public string Estado { get; set; } //(A)ctivo, (F)inalizado, (R)etraso

        public Prestamo(string id, string iSBN, string usuario, DateTime fechaPrestamo, string estado = "A")
        {
            Id = id;
            ISBN = iSBN;
            Usuario = usuario;
            FechaPrestamo = fechaPrestamo;
            Estado = estado;
        }

        public override string ToString()
        {
            DateTime? FechaDevolucion = null;
            return Id + " | " + ISBN + " | " + Usuario  + " | "  + FechaPrestamo.ToString("dd-MM-yyyy")
                + " | " + (FechaDevolucion?.ToString("dd-MM-yyyy") ?? "")
                + " | " + (Estado.Equals("A") ? "Activo":(Estado.Equals("F")?"Finalizado":"Retraso"));
        }
    }
}
