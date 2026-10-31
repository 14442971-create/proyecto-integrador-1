using System;
using System.Collections.Generic;
using BibliotecaEscolar.Models;

namespace BibliotecaEscolar
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Titulo> catalogo = CargarCatalogoInicial();
            bool salir = false;

            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("=================================================================");
                Console.WriteLine("          BIBLIOTECA ESCOLAR - SISTEMA DE GESTIÓN (DÍA 4)       ");
                Console.WriteLine("=================================================================");
                Console.WriteLine("1. Mostrar catálogo completo");
                Console.WriteLine("2. Buscar título por ISBN");
                Console.WriteLine("3. Registrar préstamo de ejemplar");
                Console.WriteLine("4. Registrar devolución de ejemplar");
                Console.WriteLine("5. Consultar título(s) más prestado(s)");
                Console.WriteLine("6. Salir del sistema");
                Console.WriteLine("=================================================================");
                Console.Write("Seleccione una opción (1-6): ");

                string opcion = Console.ReadLine();
                Console.WriteLine();

                switch (opcion)
                {
                    case "1":
                        MostrarCatalogo(catalogo);
                        break;
                    case "2":
                        BuscarLibroUI(catalogo);
                        break;
                    case "3":
                        PrestarLibroUI(catalogo);
                        break;
                    case "4":
                        DevolverLibroUI(catalogo);
                        break;
                    case "5":
                        MostrarMasPrestados(catalogo);
                        break;
                    case "6":
                        salir = true;
                        Console.WriteLine("Gracias por utilizar el Sistema de Biblioteca Escolar.");
                        break;
                    default:
                        Console.WriteLine("❌ Opción inválida. Presione cualquier tecla para intentar de nuevo.");
                        break;
                }

                if (!salir)
                {
                    Console.WriteLine("\nPresione cualquier tecla para volver al menú...");
                    Console.ReadKey();
                }
            }
        }

        static List<Titulo> CargarCatalogoInicial()
        {
            return new List<Titulo>
            {
                new Titulo("978-0131103627", "C Programming Language", 45, 5),
                new Titulo("978-0132350884", "Clean Code", 120, 3),
                new Titulo("978-0201633610", "Design Patterns", 88, 1),
                new Titulo("978-0134685991", "Effective Java", 30, 2),
                new Titulo("978-0596007126", "Head First Design Patterns", 120, 5),
                new Titulo("978-0134494166", "Clean Architecture", 15, 1),
                new Titulo("978-0321356680", "Refactoring", 62, 4),
                new Titulo("978-0132143011", "Introduction to Algorithms", 95, 2)
            };
        }

        static void MostrarCatalogo(List<Titulo> catalogo)
        {
            Console.WriteLine("--- 📚 CATÁLOGO COMPLETO ---");
            foreach (var t in catalogo)
            {
                Console.WriteLine($"ISBN: {t.ISBN} | Título: {t.Nombre}");
                Console.WriteLine($"   Préstamos Históricos: {t.PrestamosHistoricos} | Copias totales: {t.Ejemplares.Length} | Disponibles: {t.ObtenerDisponibles()}");
                Console.WriteLine(new string('-', 60));
            }
        }

        static void BuscarLibroUI(List<Titulo> catalogo)
        {
            Console.Write("Ingrese el ISBN a buscar: ");
            string isbn = Console.ReadLine()?.Trim();
            Titulo t = BuscarPorISBN(catalogo, isbn);

            if (t != null)
            {
                Console.WriteLine($"\n✔ Encontrado: '{t.Nombre}'");
                Console.WriteLine($"   ISBN: {t.ISBN}");
                Console.WriteLine($"   Préstamos Históricos: {t.PrestamosHistoricos}");
                Console.WriteLine($"   Ejemplares Disponibles: {t.ObtenerDisponibles()} de {t.Ejemplares.Length}");
            }
            else
            {
                Console.WriteLine("\n❌ No se encontró ningún título con el ISBN proporcionado.");
            }
        }

        static void PrestarLibroUI(List<Titulo> catalogo)
        {
            Console.Write("Ingrese el ISBN del libro a prestar: ");
            string isbn = Console.ReadLine()?.Trim();
            PrestarEjemplar(catalogo, isbn);
        }

        static void DevolverLibroUI(List<Titulo> catalogo)
        {
            Console.Write("Ingrese el ISBN del libro a devolver: ");
            string isbn = Console.ReadLine()?.Trim();
            DevolverEjemplar(catalogo, isbn);
        }

        static void MostrarMasPrestados(List<Titulo> catalogo)
        {
            Console.WriteLine("--- 🏆 TÍTULO(S) MÁS PRESTADO(S) HISTÓRICAMENTE ---");
            int maxPrestamos = 0;

            foreach (var t in catalogo)
            {
                if (t.PrestamosHistoricos > maxPrestamos)
                    maxPrestamos = t.PrestamosHistoricos;
            }

            foreach (var t in catalogo)
            {
                if (t.PrestamosHistoricos == maxPrestamos)
                {
                    Console.WriteLine($"⭐ {t.Nombre} | ISBN: {t.ISBN} | Total préstamos: {t.PrestamosHistoricos}");
                }
            }
        }

        static Titulo BuscarPorISBN(List<Titulo> catalogo, string isbn)
        {
            if (string.IsNullOrWhiteSpace(isbn)) return null;
            return catalogo.Find(t => t.ISBN.Equals(isbn, StringComparison.OrdinalIgnoreCase));
        }

        static bool PrestarEjemplar(List<Titulo> catalogo, string isbn)
        {
            Titulo t = BuscarPorISBN(catalogo, isbn);
            if (t == null)
            {
                Console.WriteLine("❌ Error: Título no encontrado.");
                return false;
            }

            foreach (var e in t.Ejemplares)
            {
                if (e.Disponible)
                {
                    e.Disponible = false;
                    t.PrestamosHistoricos++;
                    Console.WriteLine($"✔ Éxito: Se prestó la copia #{e.IdCopia} de '{t.Nombre}'. Quedan {t.ObtenerDisponibles()} disponibles.");
                    return true;
                }
            }

            Console.WriteLine($"❌ Error: No hay copias disponibles para prestar de '{t.Nombre}'.");
            return false;
        }

        static bool DevolverEjemplar(List<Titulo> catalogo, string isbn)
        {
            Titulo t = BuscarPorISBN(catalogo, isbn);
            if (t == null)
            {
                Console.WriteLine("❌ Error: Título no encontrado.");
                return false;
            }

            foreach (var e in t.Ejemplares)
            {
                if (!e.Disponible)
                {
                    e.Disponible = true;
                    Console.WriteLine($"✔ Éxito: Se devolvió la copia #{e.IdCopia} de '{t.Nombre}'. Disponibles: {t.ObtenerDisponibles()}.");
                    return true;
                }
            }

            Console.WriteLine($"⚠️ Advertencia: Todas las copias de '{t.Nombre}' ya están disponibles.");
            return false;
        }
    }
}