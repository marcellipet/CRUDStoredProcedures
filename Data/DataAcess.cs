using System.Data;
using System.Data.SqlClient;
using CRUDStoredProcedures.Models;

namespace CRUDStoredProcedures.Data
{
    public class DataAcess
    {
        SqlConnection _connection = null;
        SqlCommand _command = null;
        public static IConfiguration Configuration { get; set; }

        private string GetConnectionString()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            return Configuration.GetConnectionString("DefaultConnection");
        }

        public List<Usuario> ListarUsuarios()
        {
            List<Usuario> usuarios = new List<Usuario>();

            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[DBO].[listar_usuarios]";
                _connection.Open();

                SqlDataReader reader = _command.ExecuteReader();

                while (reader.Read())
                {
                    Usuario usuario = new Usuario();
                    usuario.Id = Convert.ToInt32(reader["Id"]);
                    usuario.Nome = reader["Nome"].ToString();
                    usuario.Sobrenome = reader["Sobrenome"].ToString();
                    usuario.Email = reader["Email"].ToString();
                    usuario.Cargo = reader["Cargo"].ToString();

                    usuarios.Add(usuario);

                }

                _connection.Close();
            }

            return usuarios;
        }

    }
}
