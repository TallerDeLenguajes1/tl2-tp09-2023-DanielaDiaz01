namespace tp9Daniela
{
    public interface ITareaRepository
    {
        public Tarea CrearTarea(Tarea tarea);
        public void ModificarTarea(int id, Tarea tarea);
  
        public Tarea ObtenerTarea(int id);
        public List<Tarea> ListarTareasPorUsuario(int idUsuario);
        public List<Tarea> ListarTareasPorTablero(int idTablero);
        public void BorrarTarea(int id);
        public void AsignarUsuarioATarea(int idUsuario, int idTarea);
        public List<Tarea> ListarTodasLasTareas();
    }
}
