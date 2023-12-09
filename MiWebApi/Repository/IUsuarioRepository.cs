namespace tp9Daniela{
    public interface IUsuarioRepository
    {
        public void CrearUsuario(Usuario usuario);
        public void ModificarUsuario(int id, Usuario usuario);
        public List<Usuario> ListarUsuarios();
        public Usuario ObtenerUsuario(int id);
        public void BorrarUsuario(int id);
    }
}
