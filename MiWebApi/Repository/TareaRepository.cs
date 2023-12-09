using System.Data.SQLite;

namespace tp9Daniela
{
    public class TareaRepository : ITareaRepository
    {
        private string cadenaConexion = "Data Source=DB/kanban.db;Cache=Shared";

        //Crear una nueva tarea en un tablero (recibe un idTablero, devuelve un objeto Tarea)
        public Tarea CrearTarea(Tarea tarea)
        {
            var query = @"
            INSERT INTO Tarea (id_tablero, nombre, estado, descripcion, color, id_usuario_asignado)
            VALUES (@idTablero, @nombre , @estado, @descripcion, @color, @idUsuarioAsignado);";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@idTablero", tarea.IdTablero));
                command.Parameters.Add(new SQLiteParameter("@nombre", tarea.Nombre));
                command.Parameters.Add(new SQLiteParameter("@estado", tarea.Estado));
                command.Parameters.Add(new SQLiteParameter("@descripcion", tarea.Descripcion));
                command.Parameters.Add(new SQLiteParameter("@color", tarea.Color));
                command.Parameters.Add(new SQLiteParameter("@idUsuarioAsignado", tarea.IdUsuarioAsignado));
                command.ExecuteNonQuery();
                connection.Close();
            }
            return tarea;
        }

        //Modificar una tarea existente (recibe un id y un objeto Tarea)
        public void ModificarTarea(int idTarea, Tarea tarea){
            var query = "UPDATE tarea SET id_tablero = @idtab, nombre = @nombre, estado = @estado, descripcion = @descripcion, color = @color, id_usuario_asignado = @idusu WHERE id = @idtar;";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                var command= new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@idtar", idTarea));
                command.Parameters.Add(new SQLiteParameter("@idtab", tarea.IdTablero));
                command.Parameters.Add(new SQLiteParameter("@nombre", tarea.Nombre));
                command.Parameters.Add(new SQLiteParameter("@estado", tarea.Estado));
                command.Parameters.Add(new SQLiteParameter("@descripcion", tarea.Descripcion));
                command.Parameters.Add(new SQLiteParameter("@color", tarea.Color));
                command.Parameters.Add(new SQLiteParameter("@idusu", tarea.IdUsuarioAsignado));
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
       
        //Obtener detalles de una tarea por su ID (devuelve un objeto Tarea)
        public Tarea ObtenerTarea(int id)
        {
            Tarea tarea = new Tarea();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                string queryString = @"SELECT * FROM Tarea WHERE id = @idTarea;";
                var command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter("@idTarea", id));
                connection.Open();
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tarea.Id = Convert.ToInt32(reader["id"]);
                        tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                        tarea.Nombre = reader["nombre"].ToString();
                        tarea.Estado = (EstadoTarea) Convert.ToInt32(reader["estado"]);
                        tarea.Descripcion = reader["descripcion"].ToString();
                        tarea.Color = reader["color"].ToString();
                        tarea.IdUsuarioAsignado = Convert.ToInt32(reader["id_usuario_asignado"]);
                    }
                }
                connection.Close();
            }
            return tarea;
        }

        //Listar todas las tareas asignadas a un usuario específico (recibe un idUsuario, devuelve un list de tareas)
        public List<Tarea> ListarTareasPorUsuario(int idUsuario)
        {
            string query = @"SELECT * FROM Tarea WHERE id_usuario_asignado = @idUsuario;";
            List<Tarea> tareas = new List<Tarea>();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@idUsuario", idUsuario));
                connection.Open();

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Tarea tarea = new Tarea();
                        tarea.Id = Convert.ToInt32(reader["id"]);
                        tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                        tarea.Nombre = reader["nombre"].ToString();
                        tarea.Estado = (EstadoTarea) Convert.ToInt32(reader["estado"]);
                        tarea.Descripcion = reader["descripcion"].ToString();
                        tarea.Color = reader["color"].ToString();
                        tarea.IdUsuarioAsignado = Convert.ToInt32(reader["id_usuario_asignado"]);
                        tareas.Add(tarea);
                    }
                }

                connection.Close();
            }
            return tareas;
        }

        //Listar todas las tareas de un tablero específico (recibe un idTablero, devuelve un list de tareas)
        public List<Tarea> ListarTareasPorTablero(int idTablero)
        {
            string query = @"SELECT * FROM Tarea WHERE id_tablero = @idTablero;";
            List<Tarea> tareas = new List<Tarea>();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@idTablero", idTablero));
                connection.Open();

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Tarea tarea = new Tarea();
                        tarea.Id = Convert.ToInt32(reader["id"]);
                        tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                        tarea.Nombre = reader["nombre"].ToString();
                        tarea.Estado = (EstadoTarea) Convert.ToInt32(reader["estado"]);
                        tarea.Descripcion = reader["descripcion"].ToString();
                        tarea.Color = reader["color"].ToString();
                        tarea.IdUsuarioAsignado = Convert.ToInt32(reader["id_usuario_asignado"]);
                        tareas.Add(tarea);
                    }
                }

                connection.Close();
            }
            return tareas;
        }

        //Eliminar una tarea (recibe un IdTarea)
        public void BorrarTarea(int id)
        {
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                string query = @"DELETE FROM Tarea WHERE id = @id;";
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@id", id));
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        //Asignar Usuario a Tarea (recibe idUsuario y un idTarea)
        public void AsignarUsuarioATarea(int idUsuario, int idTarea)
        {
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                var queryString = @"
                UPDATE Tarea SET id_usuario_asignado = @idUsuarioAsignado
                WHERE id = @idTarea;";
                connection.Open();
                var command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter("@isUsuarioAsignado", idUsuario));
                command.Parameters.Add(new SQLiteParameter("@idTarea", idTarea));
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public List<Tarea> ListarTodasLasTareas(){
            var query="SELECT * FROM tarea;";
            List<Tarea> listaTareasPorUsuario = new List<Tarea>();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                var command= new SQLiteCommand(query, connection);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while(reader.Read()){
                        var tarea = new Tarea();
                        tarea.Id = Convert.ToInt32(reader["id"]);
                        tarea.IdTablero=Convert.ToInt32(reader["id_tablero"]);
                        tarea.Nombre=reader["nombre"].ToString();
                        tarea.Descripcion=reader["descripcion"].ToString();
                        tarea.Color =reader["color"].ToString();
                        tarea.Estado=(EstadoTarea)Enum.Parse(typeof(EstadoTarea), reader["estado"].ToString());
                        tarea.IdUsuarioAsignado=Convert.ToInt32(reader["id_usuario_asignado"]);
                        listaTareasPorUsuario.Add(tarea);
                    }
                }
                connection.Close();
            }
            return (listaTareasPorUsuario);
        }
    }
}
