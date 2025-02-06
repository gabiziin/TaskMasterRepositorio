using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMasterDAL;
using TaskMasterDTO;

namespace TaskMasterBLL
{
    public class UsuarioBLL
    {
        UsuarioDTO userDTO = new UsuarioDTO();
        UsuarioDAL userDAL = new UsuarioDAL();

        // Create - Cria uma nova tarefa
        public void CreateUser(UsuarioDTO tarefa)
        {
            userDAL.CreateUser(tarefa);
        }

        // Read - Retorna todas as tarefas
        public List<UsuarioDTO> GetUser()
        {
            return UsuarioDAL.GetUser();
        }

        // Update - Atualiza uma tarefa existente
        public void UpdateUsuario(UsuarioDTO tarefa)
        {
            userDAL.UpdateUser(tarefa);
        }

        // Delete - Exclui uma tarefa pelo ID
        public void DeleteUsuario(int idTarefa)
        {
            userDAL.DeleteUser(idTarefa);
        }
    }
}
