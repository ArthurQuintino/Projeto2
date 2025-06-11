using MySql.Data.MySqlClient;
using System.Data;
using Projeto2.Models;

namespace Projeto2.Repositorio
{
    public class UsuarioRepositorio (IConfiguration configuration)
    {
        private readonly string _conexaoMySQL = configuration.GetConnectionString("ConexaoMySQL");
        public Usuario ObterUsuario(string email)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new("select * from Usuarios where Email = @Email", conexao);
                cmd.Parameters.Add("@Email", MySqlDbType.VarChar).Value = email;

                using (MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {

                    Usuario usuario = null;
                    if (dr.Read())
                    {
                        usuario = new Usuario
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Nome = dr["Nome"].ToString(),
                            Email = dr["Email"].ToString(),
                            Senha = dr["Senha"].ToString(),
                        };
                    }
                    return usuario;
                }
            }
        }

        public void AdicionarUsuario(Usuario usuario) 
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new("INSERT INTO Usuarios (Nome, Email, Senha) VALUES (@Nome, @Email, @Senha)", conexao);

                cmd.Parameters.AddWithValue("@Nome", usuario.Nome);
                cmd.Parameters.AddWithValue("@Email", usuario.Email);
                cmd.Parameters.AddWithValue("@Senha", usuario.Senha);
                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }
    }
}
