namespace CheckPoint.Dto
{
    public class EventoDto
    {

        public required string Titulo { get; set; }


        public required string Descricao { get; set; }


        public required DateTime Data { get; set; }


        public string? Local { get; set; }


        public string? Categoria { get; set; }


        public int? CapacidadeMaxima { get; set; }

        public required DateTime DataCriacao { get; set; }
    }
}
