using Challenge_Backend___API.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace Challenge_Backend___API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Despesas_Controller : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public Despesas_Controller(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        //Cadastro de Despesa

        [HttpPost]
        public JsonResult Post(Despesas desp)
        {
            string query = @"INSERT INTO [dbo].[Despesas] ([Descrição] ,[Valor] ,[Data])
                           VALUES (@Descricao, @Valor, @Data)";
            
            DataTable tabela = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("APICon");
            SqlDataReader reader;

            using (SqlConnection conexao = new SqlConnection(sqlDataSource))
            {
                conexao.Open();

                using (SqlCommand postcommand = new SqlCommand(query, conexao))
                {
                    postcommand.Parameters.AddWithValue("@Descricao", desp.Descricao);
                    postcommand.Parameters.AddWithValue("@Valor", desp.Valor);
                    postcommand.Parameters.AddWithValue("@Data", desp.Data);

                    reader = postcommand.ExecuteReader();
                    tabela.Load(reader);
                    reader.Close();
                    conexao.Close();
                }
            }

            return new JsonResult("Adicionado com sucesso.");
        }

        //Fim - Cadastro de Despesa



        //Listagem das receitas

        [HttpGet]
        public JsonResult get()
        {
            string query = @"SELECT ID_Despesas,Descrição,Valor,Data FROM dbo.Despesas";

            string sqldatasource = _configuration.GetConnectionString("apicon");
            var resultado = new List<Despesas>();
            SqlDataReader reader;

            using (SqlConnection conexao = new SqlConnection(sqldatasource))
            {
                conexao.Open();

                using (SqlCommand getcommand = new SqlCommand(query, conexao))
                {
                    reader = getcommand.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            resultado.Add(new Despesas
                            {
                                ID_Despesa = reader.GetInt32(0),
                                Descricao = reader.GetString(1),
                                Valor = reader.GetDecimal(2),
                                Data = reader.GetDateTime(3)
                            });
                        }
                    }

                    else
                    {
                        return new JsonResult("Sem dados.");
                    }

                    reader.Close();
                    conexao.Close();
                }
            }

            return new JsonResult(resultado);

        }

        //Fim - Listagem das receitas



        //Detalhamento de receita

        [HttpGet("{id}")]
        public JsonResult getid(int id)
        {
            string query = @"SELECT ID_Despesas,Descrição,Valor,Data FROM dbo.Despesas WHERE ID_Despesas = @ID_Despesa";

            string sqldatasource = _configuration.GetConnectionString("apicon");
            var resultado = new List<Despesas>();
            SqlDataReader reader;

            using (SqlConnection conexao = new SqlConnection(sqldatasource))
            {
                conexao.Open();

                using (SqlCommand getidcommand = new SqlCommand(query, conexao))
                {
                    getidcommand.Parameters.AddWithValue("@ID_Despesa", id);
                    reader = getidcommand.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            resultado.Add(new Despesas
                            {
                                ID_Despesa = reader.GetInt32(0),
                                Descricao = reader.GetString(1),
                                Valor = reader.GetDecimal(2),
                                Data = reader.GetDateTime(3)
                            });
                        }
                    }

                    else
                    {
                        return new JsonResult("Sem dados.");
                    }

                    reader.Close();
                    conexao.Close();
                }
            }

            return new JsonResult(resultado);

        }

        //Fim - Detalhamento de receita



        //Exclusão de receita

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"DELETE FROM [dbo].[Despesas] WHERE ID_Despesas = @ID_Despesa";

            DataTable tabela = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("apicon");
            SqlDataReader reader;

            using (SqlConnection conexao = new SqlConnection(sqlDatasource))
            {
                conexao.Open();
                using (SqlCommand delcommand = new SqlCommand(query, conexao))
                {
                    delcommand.Parameters.AddWithValue("@ID_Despesa", id);

                    reader = delcommand.ExecuteReader();
                    tabela.Load(reader);
                    reader.Close();
                    conexao.Close();
                }
            }

            return new JsonResult("Excluído com sucesso.");

        }

        //Fim - Exclusão de receita



        //Atualização de receita

        [HttpPut]
        public JsonResult Put(Despesas update)
        {
            string query = @"UPDATE [dbo].[Despesas]
                           SET Descrição = @Descricao, Valor = @Valor, Data = @Data
                           WHERE ID_Despesas = @ID_Despesa";

            DataTable tabela = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("apicon");
            SqlDataReader reader;

            using (SqlConnection conexao = new SqlConnection(sqlDatasource))
            {
                conexao.Open();

                using (SqlCommand updatecommand = new SqlCommand(query, conexao))
                {
                    updatecommand.Parameters.AddWithValue("@ID_Despesa", update.ID_Despesa);
                    updatecommand.Parameters.AddWithValue("@Descricao", update.Descricao);
                    updatecommand.Parameters.AddWithValue("@Valor", update.Valor);
                    updatecommand.Parameters.AddWithValue("@Data", update.Data);

                    reader = updatecommand.ExecuteReader();
                    tabela.Load(reader);
                    reader.Close();
                    conexao.Close();
                }
            }

            return new JsonResult("Alterado com sucesso.");
        }

        //Fim - Atualização de receita


    }
}
