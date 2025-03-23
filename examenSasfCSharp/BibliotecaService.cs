using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace examenSasfCSharp
{
    class BibliotecaService
    {
        private List<Libro> Libros { get; set; }
        private List<Usuario> Usuarios { get; set; }
        private List<Prestamo> Prestamos { get; set; }

        public BibliotecaService()
        {
            this.Libros = new List<Libro>();
            this.Usuarios = new List<Usuario>();
            this.Prestamos = new List<Prestamo>();
            //dataDummy
            this.generateDataDummy();
        }

        public void procesa(int opcion)
        {
            bool exitProceso = false;

            //Ciclo que permite mantenerse en la opcion o regresar al menu
            do
            {
                try
                {
                    switch (opcion)
                    {
                        case 1:
                            guardarUsuario();
                            break;
                        case 2:
                            guardarLibro();
                            break;
                        case 3:
                            registrarPrestamo();
                            break;
                        case 4:
                            registrarDevolucion();
                            break;
                        case 5:
                            buscarLibro();
                            break;
                        case 6:
                            mostrarPrestamo();
                            break;
                        case 7:
                            generarReporte();
                            break;
                    }
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("Error: Formato no válido");
                }
                catch (ServiceException ex)
                {
                    Console.WriteLine("Alerta: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error General: " + ex.Message);
                }

                Console.WriteLine("\nRegresar al menu? (S/N): ");
                string valor = Console.ReadLine();

                if (valor.ToUpper().Equals("S"))
                {
                    exitProceso = true;
                }

            } while (!exitProceso);
        }

        public void guardarUsuario()
        {
            bool existe = true;
            Console.WriteLine("\n==REGISTRAR/MODIFICAR USUARIO==");
            Console.WriteLine("Ingrese el id usuario:");
            string id = Console.ReadLine();
            if (id.Length == 0)
            {
                throw new ServiceException("Debe ingresar un id!");
            }

            Usuario usuario = this.Usuarios.FirstOrDefault(u => u.Id.Equals(id));
            if(usuario == null)
            {
                existe = false;
                usuario = new Usuario();
                usuario.Id = id;
            }

            Console.WriteLine("Ingrese el nombre:");
            string nombre = Console.ReadLine();
            if (nombre.Length == 0)
            {
                throw new ServiceException("Debe ingresar un nombre!");
            }
            usuario.Nombres = nombre;

            Console.WriteLine("Ingrese el correo:");
            string correo = Console.ReadLine();
            usuario.Correo = correo;

            Console.WriteLine("Ingrese el telefono:");
            string telefono = Console.ReadLine();
            usuario.Telefono = telefono;

            string result;
            if (!existe)
            {
                result = "Usuario Creado: ";
                this.Usuarios.Add(usuario);
            }
            else
            {
                result = "Usuario Modificado: ";
                int index = this.Usuarios.FindIndex(u => u.Id == usuario.Id); 
                this.Usuarios[index] = usuario;
            }
            

            Console.WriteLine(result + id);
        }

        public void guardarLibro()
        {
            bool existe = true;
            Console.WriteLine("\n==REGISTRAR/MODIFICAR LIBRO==");
            Console.WriteLine("Ingrese el ISBN:");
            string id = Console.ReadLine();
            if (id.Length == 0)
            {
                throw new ServiceException("Debe ingresar un ISBN!");
            }

            Libro libro = this.Libros.FirstOrDefault(u => u.ISBN.Equals(id));
            if (libro == null)
            {
                libro = new Libro();
                libro.ISBN = id;
                existe = false;
            }

            Console.WriteLine("Ingrese el Titulo:");
            string titulo = Console.ReadLine();
            if (titulo.Length == 0)
            {
                throw new ServiceException("Debe ingresar un titulo!");
            }
            libro.Titulo = titulo;

            Console.WriteLine("Ingrese el Autor:");
            string autor = Console.ReadLine();
            libro.Autor = autor;

            Console.WriteLine("Ingrese el Año de Publicación:");
            int anio = Convert.ToInt32(Console.ReadLine());
            libro.AnioPublicacion = anio;

            if (anio < 0 && anio > (Convert.ToInt32(DateTime.Now.ToString("yyyy"))))
            {
                throw new ServiceException("Debe ingresar un año válido!");
            }

            string result;
            if (!existe)
            {
                result = "Libro Creado: ";
                this.Libros.Add(libro);
            }
            else
            {
                result = "Libro Modificado: ";
                int index = this.Libros.FindIndex(u => u.ISBN == libro.ISBN);
                this.Libros[index] = libro;
            }

            Console.WriteLine(result + id);
        }

        public void registrarPrestamo()
        {
            Console.WriteLine("\n==REGISTRAR PRÉSTAMO==");
            Console.WriteLine("Ingrese el ISBN:");
            string isbn = Console.ReadLine();
            if (isbn.Length == 0)
            {
                throw new ServiceException("Debe ingresar un ISBN!");
            }

            Libro libro = this.Libros.FirstOrDefault(u => u.ISBN.Equals(isbn));
            if(libro == null)
            {
                throw new ServiceException("Libro no encontrado!");
            }else if (!libro.Disponibilidad)
            {
                throw new ServiceException("Libro no se encuentra disponible!");
            }

            Console.WriteLine("Ingrese el id usuario:");
            string idUsuario = Console.ReadLine();
            if (idUsuario.Length == 0)
            {
                throw new ServiceException("Debe ingresar un id de usuario!");
            }
            
            Usuario usuario = this.Usuarios.FirstOrDefault(u => u.Id.Equals(idUsuario));
            if (usuario == null)
            {
                throw new ServiceException("Usuario no encontrado!");
            }

            //deberia tomar fecha del sistema pero por pruebas se pide ingreso
            Console.WriteLine("Ingrese la fecha del préstamo (dd/MM/yyyy):");
            DateTime fecha = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);

            //Inserta
            Prestamo prestamo = new Prestamo(Guid.NewGuid().ToString(), isbn, idUsuario, fecha);
            this.Prestamos.Add(prestamo);

            //Actualiza prestamos en libros
            libro.Disponibilidad = false;
            libro.TotalPrestamos++;
            int index = this.Libros.FindIndex(u => u.ISBN == libro.ISBN);
            this.Libros[index] = libro;

            Console.WriteLine("Préstamo generado con id: " + prestamo.Id);
        }
        public void registrarDevolucion()
        {
            Console.WriteLine("\n==REGISTRAR DEVOLUCIÓN==");
            Console.WriteLine("Ingrese el id del prestamo:");
            string id = Console.ReadLine();
            if (id.Length == 0)
            {
                throw new ServiceException("Debe ingresar un id!");
            }

            Prestamo prestamo = this.Prestamos.FirstOrDefault(u => u.Id.Equals(id));
            if (prestamo == null)
            {
                throw new ServiceException("No se ha encontrado el prestamo!");
            }

            //deberia tomar fecha del sistema pero por pruebas se pide ingreso
            Console.WriteLine("Ingrese la fecha de devoución (dd/MM/yyyy):");
            DateTime fecha = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);

            //Verifica limite
            string estado = "F";
            if((fecha - prestamo.FechaPrestamo).Days > 14)
            {
                estado = "R";
            }

            //actualiza prestamo
            prestamo.FechaDevolucion = fecha;
            prestamo.Estado = estado;

            int index = this.Prestamos.FindIndex(u => u.Id == prestamo.Id);
            this.Prestamos[index] = prestamo;

            //actualiza libro
            Libro libro = this.Libros.FirstOrDefault(u => u.ISBN.Equals(prestamo.ISBN));
            libro.Disponibilidad = true;
            index = this.Libros.FindIndex(u => u.ISBN == libro.ISBN);
            this.Libros[index] = libro;

            Console.WriteLine("Devolución registrada del prestamo: " + prestamo.Id);
        }

        public void buscarLibro()
        {
            Console.WriteLine("\n==BUSCAR LIBRO==");
            Console.WriteLine("Ingrese el titulo:");
            string titulo = Console.ReadLine();
            Console.WriteLine("Ingrese el autor:");
            string autor = Console.ReadLine();

            if (titulo.Length == 0 && autor.Length == 0)
            {
                throw new ServiceException("Debe ingresar el titulo o el author del libro!");
            }

            var libros = this.Libros.Where(l => l.Titulo.ToLower().Contains(titulo.ToLower()) || l.Autor.ToLower().Contains(autor.ToLower())).ToList();
            
            if(libros.Count == 0)
            {
                throw new ServiceException("No se encontraron libros!");
            }

            Console.WriteLine("Total de libros encontrados: " + libros.Count);
            Console.WriteLine("ISBN | TITULO | AUTOR | AÑO PUBLICACION | ESTADO | TOTAL PRESTAMOS");
            foreach (Libro libro in libros)
            {
                Console.WriteLine(libro.ToString());
            }
        }

        public void mostrarPrestamo()
        {
            Console.WriteLine("\n==MOSTRAR PRESTAMOS==");
            Console.WriteLine("Ingrese el id del usuario (opcional):");
            string usuario = Console.ReadLine();

            var prestamos = this.Prestamos.Where(l => l.Usuario.Equals(usuario) || l.Estado.Equals("A")).ToList();

            if (prestamos.Count == 0)
            {
                throw new ServiceException("No se encontraron prestamos!");
            }

            Console.WriteLine("Total de prestamos encontrados: " + prestamos.Count);
            Console.WriteLine("ID | ISBN | USUARIO | FECHA PRESTAMO | FECHA DEVOLUCION | ESTADO");
            foreach (Prestamo prestamo in prestamos)
            {
                Console.WriteLine(prestamo.ToString());
            }
        }

        public void generarReporte()
        {
            Console.WriteLine("\n==GENERACION DE REPORTE PRESTAMOS==");
            var librosOrdenados = this.Libros.OrderByDescending(l => l.TotalPrestamos).ToList();
            Console.WriteLine("ISBN | TITULO | AUTOR | AÑO PUBLICACION | ESTADO | TOTAL PRESTAMOS");
            foreach (Libro libro in librosOrdenados)
            {
                Console.WriteLine(libro.ToString());
            }
        }
        public void generateDataDummy()
        {
            this.Libros.Add(new Libro("978-3-16-148410-0", "El Señor de los Anillos", "J.R.R. Tolkien", 1954, true, 120));
            this.Libros.Add(new Libro("978-0-14-118267-4", "Cien Años de Soledad", "Gabriel García Márquez", 1967, false, 250));
            this.Libros.Add(new Libro("978-0-452-28423-4", "1984", "George Orwell", 1949, true, 180));

            this.Usuarios.Add(new Usuario("jperez", "Juan Pérez", "juan.perez@mail.com", "+123456789"));
            this.Usuarios.Add(new Usuario("agomez", "Ana Gómez", "ana.gomez@mail.com", "+987654321"));
            this.Usuarios.Add(new Usuario("crodriguez", "Carlos Rodríguez", "carlos.rodriguez@mail.com", "+112233445"));

            Console.WriteLine("\nSe cargo data dummy!!!\n");
        }
    }

    
}
