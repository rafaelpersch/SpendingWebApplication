namespace SpendingWebApplication.Models
{
    public class CategoriaModel
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; } = string.Empty;

        public static List<CategoriaModel> GerarListaPadrao()
        {
            var lista = new List<CategoriaModel>();

            lista.Add(new CategoriaModel { Id = Guid.Parse("233be0ac-f646-4e46-b5d0-cf8d81ac17dc"), Descricao = "Farmácia" });
            lista.Add(new CategoriaModel { Id = Guid.Parse("f4cc790f-ed47-4c77-ad5e-386ce95f725f"), Descricao = "Mercado" });
            lista.Add(new CategoriaModel { Id = Guid.Parse("b8af0a3b-0ac1-4221-a00f-f78c6951f3ca"), Descricao = "Refeições/Lanches" });
            lista.Add(new CategoriaModel { Id = Guid.Parse("6fb8267d-a8ae-42aa-98bc-fd8ab7c68811"), Descricao = "Carro" });
            lista.Add(new CategoriaModel { Id = Guid.Parse("2d7a065f-50e9-4448-8d29-b2e8e2c1607d"), Descricao = "Gasolina" });
            lista.Add(new CategoriaModel { Id = Guid.Parse("4843a8f1-c8ab-486c-9f32-10751c78c490"), Descricao = "Contas Fixas" });
            lista.Add(new CategoriaModel { Id = Guid.Parse("8a41ba69-bd1e-422e-9dc0-ad5bb239aefd"), Descricao = "Outros" });

            return lista.OrderBy(x => x.Descricao).ToList();
        }
    }
}