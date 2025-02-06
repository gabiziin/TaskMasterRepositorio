using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMasterDTO;

namespace TaskMasterDAL
{
    public class TarefasDAL : Conexao
    {
        private readonly string connectionString = "Data Source = (LocalDB)\\MSSQLLocalDB; Initial Catalog = TaskMasterDB;Integrated Security = true";

        string msg = "Essa foi mais uma cagada que eu cometi no meu código !!";

        //Create
        public void Create(TarefasDTO tarefas)
        {
            try
            {
                Conectar();
                cmd = new SqlCommand("INSERT INTO Tarefas (IdTarefa, TituloTarefa, DescricaoTarefa, DataCriacao, DataConclusao, StatusTarefa UsuarioId) VALUES (@IdTarefa,@TituloTarefa,@DescricaoTarefa,@DataCriacao,@DataConclusao,@StatusTarefa,@UsuarioId);", conn);
                cmd.Parameters.AddWithValue("@TituloTarefa", tarefas.TituloTarefa);
                cmd.Parameters.AddWithValue("@DescricaoTarefa", tarefas.DescricaoTarefa);
                cmd.Parameters.AddWithValue("@DataCriacao", tarefas.DataCriacao);
                cmd.Parameters.AddWithValue("@DataConclusao", tarefas.DataConclusao);
                cmd.Parameters.AddWithValue("@StatusTarefa", tarefas.DescricaoTarefa);
                cmd.Parameters.AddWithValue("@UsuarioId", tarefas.UsuarioId);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

                throw new Exception($"{msg} - {ex.Message}");
            }
            finally
            {
                Desconectar();
            }

        }

        //Read
        public List<TarefasDTO> GetTarefa()
        {
            try
            {
                Conectar();
                cmd = new SqlCommand("SELECT IdTarefa,TituloTarefa,DescricaoTarefa,StatusTarefa,DataCriacao,DataConclusao FROM Tarefas T INNER JOIN Usuario U ON T.UsuarioId = U.IdUsuario;", conn);
                dr = cmd.ExecuteReader();
                List<TarefasDTO> Lista = new List<TarefasDTO>();//lista vazia
                while (dr.Read())
                {
                    TarefasDTO tarefas = new TarefasDTO();
                    tarefas.IdTarefa = Convert.ToInt32(dr["IdTarefa"]);
                    tarefas.TituloTarefa = dr["TituloTarefa"].ToString();
                    tarefas.DescricaoTarefa = dr["DescricaoTarefa"].ToString();
                    tarefas.StatusTarefa = dr["StatusTarefa"].ToString();
                    tarefas.DataCriacao = Convert.ToDateTime(dr["DataCriacao"]);
                    tarefas.DataConclusao = Convert.ToDateTime(dr["DataConclusao"]);
                    Lista.Add(tarefas);
                }
                return Lista;
            }
            catch (Exception ex)
            {

                throw new Exception($"{msg} - {ex.Message}");
            }
            finally
            {
                Desconectar();
            }
        }

        //Update
        public void Update(TarefasDTO tarefas)
        {
            try
            {
                Conectar();
                cmd = new SqlCommand("UPDATE Tarefas SET TituloTarefa = 'Tarefa Atualizada', StatusTarefa = 'Concluído', DataConclusao = GETDATE() WHERE IdTarefa = 1;", conn);
                cmd.Parameters.AddWithValue("@TituloTarefa", tarefas.TituloTarefa);
                cmd.Parameters.AddWithValue("@StatusTarefa", tarefas.StatusTarefa);
                cmd.Parameters.AddWithValue("@DataConclusao", tarefas.DataConclusao);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

                throw new Exception($"{msg} - {ex.Message}");
            }
            finally
            {
                Desconectar();
            }

        }

        //Delete
        public void Delete(int idTarefa)
        {
            try
            {
                Conectar();
                cmd = new SqlCommand("DELETE FROM Tarefas WHERE IdTarefa = 2;", conn);
                cmd.Parameters.AddWithValue("@IdTarefa", idTarefa);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

                throw new Exception($"{msg} - {ex.Message}");
            }
            finally
            {
                Desconectar();
            }

        }

        //Adicionar tarefa
        public void AdicionarTarefa(TarefasDTO tarefa)
        {
            // Query de inserção considerando as colunas definidas no script SQL.
            string sql = @"INSERT INTO Tarefas 
                           (TituloTarefa, DescricaoTarefa, DataCriacao, StatusTarefa, UsuarioId)
                       VALUES 
                           (@TituloTarefa, @DescricaoTarefa, @DataCriacao, @StatusTarefa, @UsuarioId)";

            // Abre a conexão com o banco de dados utilizando o using para garantir a liberação dos recursos.
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    // Adiciona os parâmetros e seus respectivos valores
                    cmd.Parameters.AddWithValue("@TituloTarefa", tarefa.TituloTarefa);
                    cmd.Parameters.AddWithValue("@DescricaoTarefa", tarefa.DescricaoTarefa);
                    cmd.Parameters.AddWithValue("@DataCriacao", tarefa.DataCriacao);
                    cmd.Parameters.AddWithValue("@StatusTarefa", tarefa.StatusTarefa);
                    cmd.Parameters.AddWithValue("@UsuarioId", tarefa.UsuarioId);

                    // Executa o comando de inserção
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
