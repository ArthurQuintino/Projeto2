using MySqlX.XDevAPI;

namespace Projeto2.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco {  get; set; }
        public decimal Qtd { get; set; }
        public string? Descricao { get; set; }
        public List<Produto>? ListaCliente { get; set; }
    }
}
