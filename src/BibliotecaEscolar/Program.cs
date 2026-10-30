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
            Console.WriteLine("    BIBLIOTECA ESCOLAR - DÍA 2: CARGA DE DATOS Y VERIFICACIÓN    ");
            Console.WriteLine("=================================================================\n");

            // Carga de 8 títulos con cantidades irregulares de ejemplares (de 1 a 5 copias)
            List<Titulo> catalogo = new List<Titulo>
            {
                new Titulo("978-0131103627", "C Programming Language", 45, 5),
                new Titulo("978-0132350884", "Clean Code", 120, 3),
                new Titulo("978-0201633610", "Design Patterns", 88, 1),
                new Titulo("978-0134685991", "Effective Java", 30, 2),
                new Titulo("978-0596007126", "Head First Design Patterns", 120, 5), // Empate en préstamos con Clean Code
                new Titulo("978-0134494166", "Clean Architecture", 15, 1),
                new Titulo("978-0321356680", "Refactoring", 62, 4),
                new Titulo("978-0132143011", "Introduction to Algorithms", 95, 2)
            };

            Console.WriteLine($"Catálogo cargado exitosamente. Total de títulos: {catalogo.Count}\n");

            bool hayCeldasVacias = false;

            foreach (var titulo in catalogo)
            {
                Console.WriteLine($"ISBN: {titulo.ISBN} | Título: {titulo.Nombre}");
                Console.WriteLine($"   - Préstamos Históricos: {titulo.PrestamosHistoricos}");
                Console.WriteLine($"   - Capacidad del Arreglo: {titulo.Ejemplares.Length} ejemplares");
                Console.WriteLine($"   - Disponibles actuales: {titulo.ObtenerDisponibles()}");

                // Verificación de celdas nulas o desaprovechadas en memoria
                for (int i = 0; i < titulo.Ejemplares.Length; i++)
                {
                    if (titulo.Ejemplares[i] == null)
                    {
                        hayCeldasVacias = true;
                        Console.WriteLine($"   [ALERTA] Se detectó celda nula en el índice {i}");
                    }
                }
                Console.WriteLine(new string('-', 60));
            }

            if (!hayCeldasVacias)
            {
                Console.WriteLine("\n[VERIFICACIÓN EXITOSA] Ningún título contiene celdas vacías ni desaprovechadas.");
            }
            else
            {
                Console.WriteLine("\n[ERROR DE ESTRUCTURA] Se encontraron celdas vacías en los arreglos.");
            }
        }
    }
}