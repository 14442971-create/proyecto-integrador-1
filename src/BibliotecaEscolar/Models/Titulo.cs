using System;

namespace BibliotecaEscolar.Models
{
    public class Titulo
    {
        public string ISBN { get; set; }
        public string Nombre { get; set; }
        public int PrestamosHistoricos { get; set; }
        
        // Arreglo dimensionado de forma exacta por libro para evitar celdas vacías
        public Ejemplar[] Ejemplares { get; set; }

        public Titulo(string isbn, string nombre, int prestamosHistoricos, int cantidadEjemplares)
        {
            ISBN = isbn;
            Nombre = nombre;
            PrestamosHistoricos = prestamosHistoricos;
            
            // Se dimensiona el arreglo EXACTAMENTE a la cantidad de ejemplares que posee
            Ejemplares = new Ejemplar[cantidadEjemplares];
            for (int i = 0; i < cantidadEjemplares; i++)
            {
                Ejemplares[i] = new Ejemplar(i + 1, true);
            }
        }

        public int ObtenerDisponibles()
        {
            int contador = 0;
            foreach (var ejemplar in Ejemplares)
            {
                if (ejemplar.Disponible)
                {
                    contador++;
                }
            }
            return contador;
        }
    }
}