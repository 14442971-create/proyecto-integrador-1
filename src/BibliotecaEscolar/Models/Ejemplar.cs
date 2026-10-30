namespace BibliotecaEscolar.Models
{
    public class Ejemplar
    {
        public int IdCopia { get; set; }
        public bool Disponible { get; set; }

        public Ejemplar(int idCopia, bool disponible = true)
        {
            IdCopia = idCopia;
            Disponible = disponible;
        }
    }
}