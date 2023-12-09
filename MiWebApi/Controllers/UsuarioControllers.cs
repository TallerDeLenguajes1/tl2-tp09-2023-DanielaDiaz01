using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using tp9Daniela;
namespace tp9DanielaControllers;

[ApiController]
[Route("[controller]")]
public class UsuarioController : ControllerBase
{

    private readonly ILogger<UsuarioController> _logger;
    private IUsuarioRepository usuarioRepository;
    public UsuarioController(ILogger<UsuarioController> logger)
    {
        _logger = logger;
        usuarioRepository = new UsuarioRepository();
    }

    //POST /api/usuario: Permite crear un nuevo usuario
    [HttpPost]
    [Route("/api/usuario")]
    public ActionResult<Usuario> CrearUsuario(Usuario usuario)
    {
        usuarioRepository.CrearUsuario(usuario);
        return Ok(usuario);
    }


    //GET /api/usuario: Permite listar los usuarios existentes
    [HttpGet]
    [Route("/api/usuarios")]
    public ActionResult<IEnumerable<Usuario>> ListarUsuarios()
    {
        return Ok(usuarioRepository.ListarUsuarios());
    }


    //GET /api/usuario/{Id}: Permite buscar un usuarios por id
    [HttpGet]
    [Route("/api/usuario/{id}")]
    public ActionResult<Usuario> ObtenerUsuario(int id)
    {
        Usuario usuario = usuarioRepository.ObtenerUsuario(id);
        if(String.IsNullOrEmpty(usuario.NombreUsuario))
            return NotFound("No se encontro");
        else 
            return Ok(usuario);
    }


    //PUT /api/tarea/{Id}/Nombre : Permite modificar un nombre de un Usuario
    [HttpPut]
    [Route("/api/usuario/{id}/Nombre")]
    public ActionResult<string> ModificarNombreUsuario(int id, Usuario usuario)
    {
        if (usuario==null){
            return BadRequest("El objeto usuario es nulo");
        }
        usuarioRepository.ModificarUsuario(id, usuario);
        var user = new Usuario();
        user = usuarioRepository.ObtenerUsuario(id);
        return Ok(user);
    }
}