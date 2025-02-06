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
        string msg = "Essa foi mais uma cagada que eu cometi no meu código !!";

        // CREATE - Adicionar nova tarefa
        public void Create(TarefasDTO tarefa)
        {
            try
            {
                Conectar();
                cmd = new SqlCommand("INSERT INTO Tarefas (TituloTarefa, DescricaoTarefa, UsuarioId) VALUES (@TituloTarefa, @DescricaoTarefa, @UsuarioId);", conn);
                cmd.Parameters.AddWithValue("@TituloTarefa", tarefa.TituloTarefa);
                cmd.Parameters.AddWithValue("@DescricaoTarefa", tarefa.DescricaoTarefa);
                cmd.Parameters.AddWithValue("@UsuarioId", tarefa.UsuarioId);
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

        // READ - Buscar todas as tarefas de um usuário específico
        public List<TarefasDTO> GetTarefasByUsuario(int usuarioId)
        {
            try
            {
                Conectar();
                cmd = new SqlCommand("SELECT * FROM Tarefas WHERE UsuarioId = @UsuarioId;", conn);
                cmd.Parameters.AddWithValue("@UsuarioId", usuarioId);
                dr = cmd.ExecuteReader();

                List<TarefasDTO> lista = new List<TarefasDTO>();
                while (dr.Read())
                {
                    TarefasDTO tarefa = new TarefasDTO();
                    tarefa.IdTarefa = Convert.ToInt32(dr["IdTarefa"]);
                    tarefa.TituloTarefa = dr["TituloTarefa"].ToString();
                    tarefa.DescricaoTarefa = dr["DescricaoTarefa"].ToString();
                    tarefa.DataCriacao = Convert.ToDateTime(dr["DataCriacao"]);
                    tarefa.DataConclusao = dr["DataConclusao"] != DBNull.Value ? Convert.ToDateTime(dr["DataConclusao"]) : (DateTime?)null;
                    tarefa.StatusTarefa = dr["StatusTarefa"].ToString();
                    tarefa.UsuarioId = Convert.ToInt32(dr["UsuarioId"]);

                    lista.Add(tarefa);
                }
                return lista;
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

        // UPDATE - Atualizar tarefa específica
        public void Update(TarefasDTO tarefa)
        {
            try
            {
                Conectar();
                cmd = new SqlCommand("UPDATE Tarefas SET TituloTarefa = @TituloTarefa, DescricaoTarefa = @DescricaoTarefa, StatusTarefa = @StatusTarefa, DataConclusao = @DataConclusao WHERE IdTarefa = @IdTarefa AND UsuarioId = @UsuarioId;", conn);
                cmd.Parameters.AddWithValue("@TituloTarefa", tarefa.TituloTarefa);
                cmd.Parameters.AddWithValue("@DescricaoTarefa", tarefa.DescricaoTarefa);
                cmd.Parameters.AddWithValue("@StatusTarefa", tarefa.StatusTarefa);
                cmd.Parameters.AddWithValue("@DataConclusao", tarefa.DataConclusao.HasValue ? (object)tarefa.DataConclusao.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@IdTarefa", tarefa.IdTarefa);
                cmd.Parameters.AddWithValue("@UsuarioId", tarefa.UsuarioId);
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

        // DELETE - Remover tarefa (apenas se pertencer ao usuário)
        public void Delete(int idTarefa, int usuarioId)
        {
            try
            {
                Conectar();
                cmd = new SqlCommand("DELETE FROM Tarefas WHERE IdTarefa = @IdTarefa AND UsuarioId = @UsuarioId;", conn);
                cmd.Parameters.AddWithValue("@IdTarefa", idTarefa);
                cmd.Parameters.AddWithValue("@UsuarioId", usuarioId);
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
    }
}
