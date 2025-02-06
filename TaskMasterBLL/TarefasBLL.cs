using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMasterDAL;
using TaskMasterDTO;

namespace TaskMasterBLL
{
    public class TarefasBLL
    {
        // Objeto global
        TarefasDTO tarefaDTO = new TarefasDTO();
        TarefasDAL tarefaDAL = new TarefasDAL();

        // Create - Cria uma nova tarefa
        public void CreateTarefaBLL(TarefasDTO tarefa)
        {
            tarefaDAL.Create(tarefa);
        }

        // Read - Retorna todas as tarefas
        public List<TarefasDTO> GetTarefasBLL()
        {
            return tarefaDAL.GetTarefa();
        }

        // Update - Atualiza uma tarefa existente
        public void UpdateTarefaBLL(TarefasDTO tarefa)
        {
            tarefaDAL.Update(tarefa);
        }

        // Delete - Exclui uma tarefa pelo ID
        public void DeleteTarefaBLL(int idTarefa)
        {
            tarefaDAL.Delete(idTarefa);
        }

        // Exemplo adicional: Método para criar uma tarefa inicial após o login do usuário
        public void CriarTarefaInicialBLL(UsuarioDTO usuario)
        {
            TarefasDTO tarefaInicial = new TarefasDTO
            {
                TituloTarefa = "Tarefa Inicial",
                DescricaoTarefa = "Tarefa criada automaticamente após o login.",
                DataCriacao = DateTime.Now,
                StatusTarefa = "Pendente",
                UsuarioId = usuario.IdUsuario // Certifique-se de que a propriedade se chama IdUsuario em UsuarioDTO
            };

            tarefaDAL.Create(tarefaInicial);
        }
    }
}
