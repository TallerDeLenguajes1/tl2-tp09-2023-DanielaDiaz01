namespace tp9Daniela
{
    public interface ITableroRepository
    {
        public Tablero CrearTablero(Tablero tablero);
        public void ModificarTablero(int id, Tablero tablero);
        public Tablero ObtenerTablero(int id);
        public List<Tablero> ListarTableros();
        public List<Tablero> ListarTablerosDeUsuario(int idUsuario);
        public void BorrarTablero(int id);
    }
}