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
                    user.UsuarioGenero = Convert.ToInt32(dr["UsuarioGenero"]);
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

        //CRUD
        //Create
        public void CreateUser(UsuarioDTO user)
        {
            try
            {
                Conectar();
                cmd = new SqlCommand("INSERT INTO Usuario (NomeUsuario,UsuarioGenero,EmailUsuario,SenhaUsuario,UsuarioTp) VALUES (@NomeUsuario,@UsuarioGenero,@EmailUsuario,@SenhaUsuario,@UsuarioTp)", conn);
                cmd.Parameters.AddWithValue("@NomeUsuario", user.NomeUsuario);
                cmd.Parameters.AddWithValue("@UsuarioGenero", user.UsuarioGenero);
                cmd.Parameters.AddWithValue("@EmailUsuario", user.EmailUsuario);
                cmd.Parameters.AddWithValue("@SenhaUsuario", user.SenhaUsuario);
                cmd.Parameters.AddWithValue("@UsuarioTp", user.UsuarioTp);
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
        public List<UsuarioDTO> GetUser()
        {
            try
            {
                Conectar();
                cmd = new SqlCommand("SELECT IdUsuario,NomeUsuario, UsuarioGenero, EmailUsuario,SenhaUsuario,DescricaoTipoUsuario FROM Usuario INNER JOIN TipoUsuario ON UsuarioTp = IdTipoUsuario;", conn);
                dr = cmd.ExecuteReader();
                List<UsuarioDTO> listUser = new List<UsuarioDTO>();//ponteiro
                while (dr.Read())
                {
                    UsuarioDTO user = new UsuarioDTO();
                    user.IdUsuario = Convert.ToInt32(dr["IdUsuario"]);
                    user.NomeUsuario = dr["NomeUsuario"].ToString();
                    user.UsuarioGenero = Convert.ToInt32(dr["UsuarioGenero"]);
                    user.EmailUsuario = dr["EmailUsuario"].ToString();
                    user.SenhaUsuario = dr["SenhaUsuario"].ToString();
                    user.UsuarioTp = dr["DescricaoTipoUsuario"].ToString();
                    listUser.Add(user);
                }
                return listUser;

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
        public void UpdateUser(UsuarioDTO user)
        {
            try
            {
                Conectar();
                cmd = new SqlCommand("UPDATE Usuario SET NomeUsuario = @NomeUsuario, UsuarioGenero = @UsuarioGenero, EmailUsuario = @EmailUsuario,SenhaUsuario = @SenhaUsuario, UsuarioTp = @UsuarioTp WHERE IdUsuario = @IdUsuario;", conn);
                cmd.Parameters.AddWithValue("@NomeUsuario", user.NomeUsuario);
                cmd.Parameters.AddWithValue("@UsuarioGenero", user.UsuarioGenero);
                cmd.Parameters.AddWithValue("@EmailUsuario", user.EmailUsuario);
                cmd.Parameters.AddWithValue("@SenhaUsuario", user.SenhaUsuario);
                cmd.Parameters.AddWithValue("@UsuarioTp", user.UsuarioTp);
                //passando o id para condicao WHERE do comando sql
                cmd.Parameters.AddWithValue("@IdUsuario", user.IdUsuario);
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
        public void DeleteUser(int idUser)
        {
            try
            {
                Conectar();
                cmd = new SqlCommand("DELETE FROM Usuario WHERE IdUsuario = @IdUsuario;", conn);
                cmd.Parameters.AddWithValue("@IdUsuario", idUser);
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
