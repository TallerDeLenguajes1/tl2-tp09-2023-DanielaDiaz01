using System.Data.SQLite;

namespace tp9Daniela{
    public class UsuarioRepository : IUsuarioRepository
    {
        private string cadenaConexion = "Data Source=DB/kanban.db;Cache=Shared";
        
        //Crear un nuevo usuario (recibe un objeto Usuario)
        public void CrearUsuario(Usuario usuario)
        {
            var query = $"INSERT INTO Usuario (nombre_de_usuario) VALUES (@nombre)";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@nombre", usuario.NombreUsuario));
                command.ExecuteNonQuery();
                connection.Close();   
            }
        }
            
        //Modificar un usuario existente (recibe un Id y un objeto Usuario)
        public void ModificarUsuario(int id, Usuario usuario){
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                var queryString = @"UPDATE Usuario SET nombre_de_usuario = @nombre WHERE id = @idUsuario;";
                connection.Open();
                var command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter("@nombre", usuario.NombreUsuario));
                command.Parameters.Add(new SQLiteParameter("@idUsuario", id));
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        //Listar todos los usuarios registrados (devuelve un List de Usuarios)
        public List<Usuario> ListarUsuarios()
        {
            var queryString = @"SELECT * FROM Usuario;";
            List<Usuario> usuarios = new List<Usuario>();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                connection.Open();
            
                using(SQLiteDataReader reader = command.ExecuteReader()){
                    while (reader.Read())
                    {
                        var usuario = new Usuario();
                        usuario.Id = Convert.ToInt32(reader["id"]);
                        usuario.NombreUsuario = reader["nombre_de_usuario"].ToString();
                        usuarios.Add(usuario);
                    }
                }
                connection.Close();
            }
            return usuarios;
        }

        //Obtener detalles de un usuario por su ID (recibe un Id y devuelve un Usuario)
        public Usuario ObtenerUsuario(int id)
         {
            Usuario usuario = new Usuario();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                string queryString = @"SELECT * FROM Usuario WHERE id = @idUsuario;";
                var command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter("@idUsuario", id));
                connection.Open();
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        usuario.Id = Convert.ToInt32(reader["id"]);
                        usuario.NombreUsuario = reader["nombre_de_usuario"].ToString();
                    }
                }
                connection.Close();
            }
            return usuario;
        }

        //Eliminar un usuario por ID
        public void BorrarUsuario(int id)
        {
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                string query = @"DELETE FROM Usuario WHERE id = @id;";
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@id", id));
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}