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
            Console.WriteLine("  BIBLIOTECA ESCOLAR - DÍA 4: ORDENAMIENTO POR INSERCIÓN MANUAL  ");
            Console.WriteLine("=================================================================\n");

            List<Titulo> catalogo = CargarCatalogo();

            Console.WriteLine("--- 📦 CATÁLOGO ORIGINAL (DESORDENADO) ---");
            MostrarCatalogo(catalogo);

            // Aplicar el algoritmo manual de Ordenamiento por Inserción
            OrdenamientoPorInsercionManual(catalogo);

            Console.WriteLine("\n=================================================================");
            Console.WriteLine("--- 🏆 CATÁLOGO ORDENADO POR PRÉSTAMOS HISTÓRICOS (MAYOR A MENOR) ---");
            Console.WriteLine("=================================================================");
            MostrarCatalogo(catalogo);

            // Verificación explícita de estabilidad para los títulos en empate (120 préstamos)
            VerificarEstabilidad(catalogo);
        }

        /// <summary>
        /// Algoritmo de Ordenamiento por Inserción implementado manualmente (Sin Array.Sort ni List.Sort).
        /// Ordena de mayor a menor según préstamos históricos.
        /// Preserva el orden alfabético (A-Z) en caso de empate.
        /// </summary>
        static void OrdenamientoPorInsercionManual(List<Titulo> lista)
        {
            for (int i = 1; i < lista.Count; i++)
            {
                Titulo clave = lista[i];
                int j = i - 1;

                // Desplazar elementos si tienen menos préstamos que la clave, 
                // o si empatan en préstamos pero la clave va alfabéticamente antes.
                while (j >= 0 && DebeIntercambiar(lista[j], clave))
                {
                    lista[j + 1] = lista[j];
                    j = j - 1;
                }
                lista[j + 1] = clave;
            }
        }

        static bool DebeIntercambiar(Titulo actual, Titulo clave)
        {
            // Criterio 1: Préstamos de Mayor a Menor
            if (actual.PrestamosHistoricos < clave.PrestamosHistoricos)
            {
                return true;
            }

            // Criterio 2: En empate de préstamos, ordenar alfabéticamente por Nombre (A-Z)
            if (actual.PrestamosHistoricos == clave.PrestamosHistoricos)
            {
                return string.Compare(actual.Nombre, clave.Nombre, StringComparison.OrdinalIgnoreCase) > 0;
            }

            return false;
        }

        static List<Titulo> CargarCatalogo()
        {
            return new List<Titulo>
            {
                new Titulo("978-0131103627", "C Programming Language", 45, 5),
                new Titulo("978-0132350884", "Clean Code", 120, 3), // Empate (120)
                new Titulo("978-0201633610", "Design Patterns", 88, 1),
                new Titulo("978-0134685991", "Effective Java", 30, 2),
                new Titulo("978-0596007126", "Head First Design Patterns", 120, 5), // Empate (120)
                new Titulo("978-0134494166", "Clean Architecture", 15, 1),
                new Titulo("978-0321356680", "Refactoring", 62, 4),
                new Titulo("978-0132143011", "Introduction to Algorithms", 95, 2)
            };
        }

        static void MostrarCatalogo(List<Titulo> catalogo)
        {
            for (int i = 0; i < catalogo.Count; i++)
            {
                var t = catalogo[i];
                Console.WriteLine($"[{i + 1}] Préstamos: {t.PrestamosHistoricos,3} | Título: {t.Nombre,-28} | ISBN: {t.ISBN}");
            }
        }

        static void VerificarEstabilidad(List<Titulo> catalogo)
        {
            Console.WriteLine("\n--- 🔍 VERIFICACIÓN DE ESTABILIDAD EN EMPATES ---");
            var empatados = catalogo.FindAll(t => t.PrestamosHistoricos == 120);

            if (empatados.Count >= 2)
            {
                Console.WriteLine($"Se detectaron {empatados.Count} títulos con 120 préstamos:");
                foreach (var item in empatados)
                {
                    Console.WriteLine($"   - {item.Nombre}");
                }

                if (string.Compare(empatados[0].Nombre, empatados[1].Nombre, StringComparison.OrdinalIgnoreCase) <= 0)
                {
                    Console.WriteLine("✔ [ÉXITO] El orden alfabético se preservó correctamente en el empate.");
                }
                else
                {
                    Console.WriteLine("❌ [ERROR] Falló la preservación del orden alfabético.");
                }
            }
        }
    }
}