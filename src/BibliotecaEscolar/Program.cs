using System;
using System.Collections.Generic;
using BibliotecaEscolar.Models;

namespace BibliotecaEscolar
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=================================================================");
            Console.WriteLine("    BIBLIOTECA ESCOLAR - DÍA 3: BÚSQUEDAS Y GESTIÓN DE PRÉSTAMOS ");
            Console.WriteLine("=================================================================\n");

            // 1. Carga del catálogo
            List<Titulo> catalogo = CargarCatalogo();

            // 2. Reporte de Título(s) Más Prestados (Manejo de empates)
            MostrarMasPrestados(catalogo);

            // 3. Búsqueda por ISBN
            Console.WriteLine("\n--- 🔍 BÚSQUEDA POR ISBN ---");
            string isbnBuscar = "978-0132350884";
            Titulo libroEncontrado = BuscarPorISBN(catalogo, isbnBuscar);
            if (libroEncontrado != null)
            {
                Console.WriteLine($"[ENCONTRADO] {libroEncontrado.Nombre} | Préstamos totales: {libroEncontrado.PrestamosHistoricos} | Copias disponibles: {libroEncontrado.ObtenerDisponibles()}");
            }
            else
            {
                Console.WriteLine($"[NO ENCONTRADO] No existe un libro registrado con ISBN {isbnBuscar}");
            }

            // 4. Simulación de Préstamo de una copia
            Console.WriteLine("\n--- 📖 SIMULACIÓN DE PRÉSTAMO ---");
            PrestarEjemplar(catalogo, "978-0132350884");

            // 5. Simulación de Devolución de una copia
            Console.WriteLine("\n--- 🔄 SIMULACIÓN DE DEVOLUCIÓN ---");
            DevolverEjemplar(catalogo, "978-0132350884");
        }

        static List<Titulo> CargarCatalogo()
        {
            return new List<Titulo>
            {
                new Titulo("978-0131103627", "C Programming Language", 45, 5),
                new Titulo("978-0132350884", "Clean Code", 120, 3),
                new Titulo("978-0201633610", "Design Patterns", 88, 1),
                new Titulo("978-0134685991", "Effective Java", 30, 2),
                new Titulo("978-0596007126", "Head First Design Patterns", 120, 5), // Empate en préstamos con Clean Code (120)
                new Titulo("978-0134494166", "Clean Architecture", 15, 1),
                new Titulo("978-0321356680", "Refactoring", 62, 4),
                new Titulo("978-0132143011", "Introduction to Algorithms", 95, 2)
            };
        }

        static void MostrarMasPrestados(List<Titulo> catalogo)
        {
            Console.WriteLine("--- 🏆 TÍTULO(S) MÁS PRESTADO(S) HISTÓRICAMENTE ---");
            int maxPrestamos = 0;

            // Determinar el mayor número de préstamos
            foreach (var titulo in catalogo)
            {
                if (titulo.PrestamosHistoricos > maxPrestamos)
                {
                    maxPrestamos = titulo.PrestamosHistoricos;
                }
            }

            // Imprimir todos los títulos que coincidan con el máximo (para contemplar empates)
            foreach (var titulo in catalogo)
            {
                if (titulo.PrestamosHistoricos == maxPrestamos)
                {
                    Console.WriteLine($"⭐ Título: {titulo.Nombre} | Préstamos: {titulo.PrestamosHistoricos} | ISBN: {titulo.ISBN}");
                }
            }
        }

        static Titulo BuscarPorISBN(List<Titulo> catalogo, string isbn)
        {
            foreach (var titulo in catalogo)
            {
                if (titulo.ISBN.Equals(isbn, StringComparison.OrdinalIgnoreCase))
                {
                    return titulo;
                }
            }
            return null;
        }

        static bool PrestarEjemplar(List<Titulo> catalogo, string isbn)
        {
            Titulo titulo = BuscarPorISBN(catalogo, isbn);
            if (titulo == null)
            {
                Console.WriteLine("❌ Error: Título no encontrado.");
                return false;
            }

            foreach (var ejemplar in titulo.Ejemplares)
            {
                if (ejemplar.Disponible)
                {
                    ejemplar.Disponible = false;
                    titulo.PrestamosHistoricos++;
                    Console.WriteLine($"✔ Éxito: Se prestó la copia #{ejemplar.IdCopia} de '{titulo.Nombre}'. Disponibles restantes: {titulo.ObtenerDisponibles()}");
                    return true;
                }
            }

            Console.WriteLine($"❌ Error: No hay ejemplares disponibles para prestar de '{titulo.Nombre}'.");
            return false;
        }

        static bool DevolverEjemplar(List<Titulo> catalogo, string isbn)
        {
            Titulo titulo = BuscarPorISBN(catalogo, isbn);
            if (titulo == null)
            {
                Console.WriteLine("❌ Error: Título no encontrado.");
                return false;
            }

            foreach (var ejemplar in titulo.Ejemplares)
            {
                if (!ejemplar.Disponible)
                {
                    ejemplar.Disponible = true;
                    Console.WriteLine($"✔ Éxito: Se devolvió la copia #{ejemplar.IdCopia} de '{titulo.Nombre}'. Disponibles actuales: {titulo.ObtenerDisponibles()}");
                    return true;
                }
            }

            Console.WriteLine($"⚠️ Advertencia: Todas las copias de '{titulo.Nombre}' ya están marcadas como disponibles.");
            return false;
        }
    }
}