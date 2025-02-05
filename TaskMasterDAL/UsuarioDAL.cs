using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMasterDTO;

namespace TaskMasterDAL
{
    public class UsuarioDAL : Conexao
    {
        string msg = "Essa foi mais uma cagada que eu cometi no meu código !!";
        public UsuarioDTO AuthenticateUser(string nomeUser, string senhaUser)
        {
            try
            {
                Conectar();
                cmd = new SqlCommand("SELECT * FROM Usuario WHERE NomeUsuario = @nomeUsuario AND SenhaUsuario=@senhaUsuario;", conn);
                cmd.Parameters.AddWithValue("@nomeUsuario", nomeUser);
                cmd.Parameters.AddWithValue("@senhaUsuario", senhaUser);
                dr = cmd.ExecuteReader();
                UsuarioDTO user = null;//ponteiro
                if (dr.Read())
                {
                    user = new UsuarioDTO();
                    user.IdUsuario = Convert.ToInt32(dr["IdUsuario"]);
                    user.NomeUsuario = dr["NomeUsuario"].ToString();
                    user.EmailUsuario = dr["EmailUsuario"].ToString();
                    user.SenhaUsuario = dr["SenhaUsuario"].ToString();
                    user.UsuarioTp = dr["UsuarioTp"].ToString();
                }
                return user;

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
