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

        //Autentica o usuário, basicamente serve só pro login checar o usuário no banco
        public UsuarioDTO AuthenticateUserBLL(string user, string password)
        {
            return userDAL.AuthenticateUser(user, password);
        }

        //CRUD
        //Create
        public void CreateUserBLL(UsuarioDTO user)
        {
            userDAL.CreateUser(user);
        }

        //Read
        public List<UsuarioDTO> GetUserBLL()
        {
            return userDAL.GetUser();
        }

        //Update
        public void UpdateUserBLL(UsuarioDTO user)
        {
            userDAL.UpdateUser(user);
        }

        //Delete
        public void DeleteUserBLL(int idUser)
        {
            userDAL.DeleteUser(idUser);
        }

        //SearchById
        public UsuarioDTO SearchByIdBLL(int idUser)
        {
            return userDAL.SearchById(idUser);
        }

        //SearchByName -- o método existe, mas ele está comentado
        //public UsuarioDTO SearchByNameBLL(string nomeUser)
        //{
        //    return userDAL.SearchByName(nomeUser);
        //}

        //DDL/ComboBox - Tipos de usuário
        public List<TipoUsuarioDTO> GetTypeUserBLL()
        {
            return userDAL.GetTypeUser();
        }

        //DDL/ComboBox - Gêneros de usuário
        public List<GeneroUsuarioDTO> GetGenderUserBLL()
        {
            return userDAL.GetGenderUser();
        }
    }
}
