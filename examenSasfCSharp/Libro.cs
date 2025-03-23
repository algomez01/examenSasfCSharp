using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace examenSasfCSharp
{
    class Libro
    {
        public string ISBN { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public int AnioPublicacion { get; set; }
        public bool Disponibilidad { get; set; }
        public int TotalPrestamos { get; set; }

        public Libro(string iSBN, string titulo, string autor, int anioPublicacion, bool disponibilidad = true, int totalPrestamos = 0)
        {
            ISBN = iSBN;
            Titulo = titulo;
            Autor = autor;
            AnioPublicacion = anioPublicacion;
            Disponibilidad = disponibilidad;
            TotalPrestamos = totalPrestamos;
        }

        public Libro() { this.Disponibilidad = true; this.TotalPrestamos = 0; }
        public override string ToString()
        {
            return ISBN + " | " + Titulo + " | " + Autor + " | " 
                + AnioPublicacion + " | " + (Disponibilidad?"Disponible":"Prestado") 
                + " | " + TotalPrestamos;
        }
    }
}
