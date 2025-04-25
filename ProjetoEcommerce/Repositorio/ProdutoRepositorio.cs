using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using MySql.Data.MySqlClient;
using ProjetoEcommerce.Models;
using System.Data;


namespace ProjetoEcommerce.Repositorio
{
    public class ProdutoRepositorio(IConfiguration configuration)
    {
        private readonly string _conexaoMySQL = configuration.GetConnectionString("ConexaoMySQL");


        public void Cadastrar(Produto produto)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("insert into produto (CodProd, Nome, Descricao, quantidade, preco) values (@codprod, @nome, @descricao, @quantidade, @preco )", conexao);
                cmd.Parameters.Add("@codprod", MySqlDbType.Int32).Value = produto.CodProd;
                cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = produto.Nome;
                cmd.Parameters.Add("@descricao", MySqlDbType.VarChar).Value = produto.Descricao;
                cmd.Parameters.Add("@quantidade", MySqlDbType.Int32).Value = produto.quantidade;
                cmd.Parameters.Add("@preco", MySqlDbType.Decimal).Value = produto.preco;
                cmd.ExecuteNonQuery();
                conexao.Close();
            }

        }

        public bool Atualizar(Produto produto)
        {

            try
            {
                using (var conexao = new MySqlConnection(_conexaoMySQL))
                {
                    conexao.Open();
                    MySqlCommand cmd = new MySqlCommand("Update produto set Nome=@nome, Descricao=@descricao, quantidade=@quantidade, preco=@preco" + "where CodProd=@codprod", conexao);
                    cmd.Parameters.Add("@codprod", MySqlDbType.Int32).Value = produto.CodProd;
                    cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = produto.Nome;
                    cmd.Parameters.Add("@descricao", MySqlDbType.VarChar).Value = produto.Descricao;
                    cmd.Parameters.Add("@quantidade", MySqlDbType.Int32).Value = produto.quantidade;
                    cmd.Parameters.Add("@preco", MySqlDbType.Decimal).Value = produto.preco;
                    int linhasAfetadas = cmd.ExecuteNonQuery();
                    return linhasAfetadas > 0;
                }
            }
            catch (MySqlException ex)
            {

                Console.WriteLine($"Erro ao atualizar produto: {ex.Message}");
                return false;
            }
        }

        public IEnumerable<Produto> TodosProduto()
        {
            List<Produto> ProdutoList = new List<Produto>();

            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("select * from produto", conexao);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                conexao.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    ProdutoList.Add(
                        new Produto

                        {
                            CodProd = Convert.ToInt32(dr["CodProd"]),
                            Nome = ((string)dr["Nome"]),
                            Descricao = ((string)dr["Descricao"]),
                            quantidade = Convert.ToInt32(dr["quantidade"]),
                            preco = ((decimal)dr["preco"]),
                        });
                }
                return ProdutoList;
            }
        }

        public Produto ObterProduto(int Codigo)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * from produto where CodProd=@codprod ", conexao);

                cmd.Parameters.AddWithValue("@codprod", Codigo);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;
                Produto produto = new Produto();

                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    produto.CodProd = Convert.ToInt32(dr["CodProd"]);
                    produto.Nome = (string)(dr["Nome"]);
                    produto.Descricao = (string)(dr["Descricao"]);
                    produto.quantidade = Convert.ToInt32(dr["quantidade"]);
                    produto.preco = (decimal)(dr["preco"]);
                }
                return produto;
            }

        }

        public void Excluir(int id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("delete from produto where CodProd=@codprod", conexao);
                cmd.Parameters.AddWithValue("@codprod", id);

                int i = cmd.ExecuteNonQuery();
                conexao.Close();
            }

        }
    }

}