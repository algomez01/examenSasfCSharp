using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace examenSasfCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu();
        }

        public static void Menu()
        {
            bool exitMenu = false;
            BibliotecaService service = new BibliotecaService();
            Dictionary<int, string> opciones = new Dictionary<int, string>
            {
                { 1, "Registrar/Modificar Usuario" },
                { 2, "Registrar/Modificar Libro" },
                { 3, "Registrar Préstamo" },
                { 4, "Registrar Devolución" },
                { 5, "Buscar Libro" },
                { 6, "Mostrar Préstamos" },
                { 7, "Generar Reporte de Libros" },
                { 8, "Salir" }
            };

            //Ciclo que permite mantenerse en el menu
            do
            {
                try
                {
                    Console.WriteLine("\n****MENU - BIBLIOTECA MUNICIPAL****");

                    foreach (var opcion in opciones)
                    {
                        Console.WriteLine(opcion.Key + ". " + opcion.Value);
                    }
                    Console.WriteLine("\nIngrese una opción");
                    int seleccion = Convert.ToInt32(Console.ReadLine());

                    if (seleccion < 0 || seleccion > 8)
                    {
                        Console.WriteLine("Alerta: Opción no válida");
                    }
                    else if (seleccion == 8)
                    {
                        exitMenu = true;
                    }
                    else
                    {
                        service.procesa(seleccion);
                    }
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("Error: Formato no válido");
                }
                catch (ServiceException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error General: " + ex.Message);
                }

            } while (!exitMenu);
        }
    }
}
