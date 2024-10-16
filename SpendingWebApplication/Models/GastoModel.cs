namespace SpendingWebApplication.Models
{
    public class GastoModel
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public DateTime Data { get; set;}
        public int Valor { get; set; }
        public CategoriaModel Categoria { get; set; }
    }
}