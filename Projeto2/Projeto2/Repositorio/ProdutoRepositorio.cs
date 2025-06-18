using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using Projeto2.Models;
using System.Data;

namespace Projeto2.Repositorio
{
    public class ProdutoRepositorio (IConfiguration configuration)
    {
        private readonly string _conexaoMySQL = configuration.GetConnectionString("ConexaoMySQL");
        public Produto ObterProduto(int Id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new("select * from Produtos where Id = @Id", conexao);
                cmd.Parameters.Add("@Id", MySqlDbType.Int64).Value = Id;

                using (MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {

                   Produto produto = null;
                    if (dr.Read())
                    {
                        produto = new Produto
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Nome = dr["Nome"].ToString(),
                            Preco = Convert.ToDecimal(dr["Preco"]),
                            Qtd = Convert.ToInt32(dr["Qtd"]),
                            Descricao = dr["Descricao"].ToString(),

                        };
                    }
                    return produto;
                }
            }
        }

        public void AdicionarProduto(Produto produto)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new("INSERT INTO produtos (Nome, Preco, Qtd, Descricao) VALUES (@Nome, @Preco, @Qtd, @Descricao)", conexao);

                cmd.Parameters.AddWithValue("@Nome", produto.Nome);
                cmd.Parameters.AddWithValue("@Preco", produto.Preco);
                cmd.Parameters.AddWithValue("@Qtd", produto.Qtd);
                cmd.Parameters.AddWithValue("@Descricao", produto.Descricao);
                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }
        // Método para listar todos os produtos do banco de dados
        public IEnumerable<Produto> TodosProdutos()
        {
            // Cria uma nova lista para armazenar os objetos Produto
            List<Produto> Productlist = new List<Produto>();

            // Bloco using para garantir que a conexão seja fechada e os recursos liberados após o uso
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                // Abre a conexão com o banco de dados MySQL
                conexao.Open();
                // Cria um novo comando SQL para selecionar todos os registros da tabela 'produto'
                MySqlCommand cmd = new MySqlCommand("SELECT * from produtos", conexao);

                // Cria um adaptador de dados para preencher um DataTable com os resultados da consulta
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                // Cria um novo DataTable
                DataTable dt = new DataTable();
                // metodo fill- Preenche o DataTable com os dados retornados pela consulta
                da.Fill(dt);
                // Fecha explicitamente a conexão com o banco de dados 
                conexao.Close();

                // interage sobre cada linha (DataRow) do DataTable
                foreach (DataRow dr in dt.Rows)
                {
                    // Cria um novo objeto Produto e preenche suas propriedades com os valores da linha atual
                    Productlist.Add(
                                new Produto
                                {
                                    Id = Convert.ToInt32(dr["Id"]),
                                    Nome = dr["Nome"].ToString(),
                                    Preco = Convert.ToDecimal(dr["Preco"]),
                                    Qtd = Convert.ToInt32(dr["Qtd"]),
                                    Descricao = dr["Descricao"].ToString(),
                                });
                }
                // Retorna a lista de todos os produtos
                return Productlist;
            }
        }

        // Método para buscar um produto específico pelo seu código (Codigo)
    }
}
