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
                UsuarioDTO user = null; //ponteiro
                if (dr.Read())
                {
                    user = new UsuarioDTO();
                    user.IdUsuario = Convert.ToInt32(dr["IdUsuario"]);
                    user.NomeUsuario = dr["NomeUsuario"].ToString();
                    user.UsuarioGenero = dr["UsuarioGenero"].ToString();
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

                string query = @"
                    SELECT 
                        U.IdUsuario, 
                        U.NomeUsuario, 
                        G.DescricaoGeneroUsuario, 
                        U.EmailUsuario, 
                        U.SenhaUsuario, 
                        T.DescricaoTipoUsuario 
                    FROM Usuario U
                    INNER JOIN TipoUsuario T ON U.UsuarioTp = T.IdTipoUsuario
                    INNER JOIN GeneroUsuario G ON U.UsuarioGenero = G.IdGeneroUsuario;";

                cmd = new SqlCommand(query, conn);
                dr = cmd.ExecuteReader();
                List<UsuarioDTO> listUser = new List<UsuarioDTO>();//ponteiro
                while (dr.Read())
                {
                    UsuarioDTO user = new UsuarioDTO();
                    user.IdUsuario = Convert.ToInt32(dr["IdUsuario"]);
                    user.NomeUsuario = dr["NomeUsuario"].ToString();
                    user.UsuarioGenero = dr["DescricaoGeneroUsuario"].ToString();
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

        //SearchById -- Procura o usuário pelo ID
        public UsuarioDTO SearchById(int idUser)
        {
            try
            {
                Conectar();
                cmd = new SqlCommand("SELECT * FROM Usuario WHERE IdUsuario = @IdUsuario;", conn);
                cmd.Parameters.AddWithValue("@IdUsuario", idUser);
                dr = cmd.ExecuteReader();
                UsuarioDTO user = null;//ponteiro
                if (dr.Read())
                {
                    user = new UsuarioDTO();
                    user.IdUsuario = Convert.ToInt32(dr["IdUsuario"]);
                    user.NomeUsuario = dr["NomeUsuario"].ToString();
                    user.UsuarioGenero = dr["UsuarioGenero"].ToString();
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

        //SearchByName -- Procura os usuários pelo nome, mas não sei se vou implementá-lo ainda...
        //public UsuarioDTO SearchByName(string nomeUser)
        //{
        //    try
        //    {
        //        Conectar();
        //        cmd = new SqlCommand("SELECT * FROM Usuario WHERE NomeUsuario = @NomeUsuario;", conn);
        //        cmd.Parameters.AddWithValue("@NomeUsuario", nomeUser);
        //        dr = cmd.ExecuteReader();
        //        UsuarioDTO user = null;//ponteiro
        //        if (dr.Read())
        //        {
        //            user = new UsuarioDTO();
        //            user.IdUsuario = Convert.ToInt32(dr["IdUsuario"]);
        //            user.NomeUsuario = dr["NomeUsuario"].ToString();
        //            user.UsuarioGenero = dr["UsuarioGenero"].ToString();
        //            user.EmailUsuario = dr["EmailUsuario"].ToString();
        //            user.SenhaUsuario = dr["SenhaUsuario"].ToString();
        //            user.UsuarioTp = dr["UsuarioTp"].ToString();
        //        }
        //        return user;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception($"{msg} - {ex.Message}");
        //    }
        //    finally
        //    {
        //        Desconectar();
        //    }

        //}

        //Preenche a DDL com os tipos
        public List<TipoUsuarioDTO> GetTypeUser()
        {
            try
            {
                Conectar();
                cmd = new SqlCommand("SELECT * FROM TipoUsuario;", conn);
                dr = cmd.ExecuteReader();
                List<TipoUsuarioDTO> listUser = new List<TipoUsuarioDTO>();//ponteiro
                while (dr.Read())
                {
                    TipoUsuarioDTO userTp = new TipoUsuarioDTO();
                    userTp.IdTipoUsuario = Convert.ToInt32(dr["IdTipoUsuario"]);
                    userTp.DescricaoTipoUsuario = dr["DescricaoTipoUsuario"].ToString();

                    listUser.Add(userTp);
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

        //Preenche a DDL com os gêneros
        public List<GeneroUsuarioDTO> GetGenderUser()
        {
            try
            {
                Conectar();
                cmd = new SqlCommand("SELECT * FROM GeneroUsuario;", conn);
                dr = cmd.ExecuteReader();
                List<GeneroUsuarioDTO> listUser = new List<GeneroUsuarioDTO>();//ponteiro
                while (dr.Read())
                {
                    GeneroUsuarioDTO userTp = new GeneroUsuarioDTO();
                    userTp.IdGeneroUsuario = Convert.ToInt32(dr["IdGeneroUsuario"]);
                    userTp.DescricaoGeneroUsuario = dr["DescricaoGeneroUsuario"].ToString();

                    listUser.Add(userTp);
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
    }
}
