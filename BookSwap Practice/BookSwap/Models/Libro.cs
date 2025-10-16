namespace BookSwap.Models
{
    public class Libro
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string ISBN { get; set; } // Código de identificación
        public bool Disponible { get; set; } = true; // Estado para intercambio
    }
}
