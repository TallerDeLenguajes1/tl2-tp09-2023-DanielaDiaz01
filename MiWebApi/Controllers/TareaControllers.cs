using Microsoft.AspNetCore.Mvc;
using tp9Daniela;
namespace tp9DanielaControllersControllers;

[ApiController]
[Route("[controller]")]

public class TareaController : ControllerBase
{
    private readonly ILogger<TareaController> _logger;
    private ITareaRepository tareaRepository;
    public TareaController(ILogger<TareaController> logger)
    {
        _logger = logger;
        tareaRepository = new TareaRepository();
    }

    //POST /api/tarea: Permite crear una Tarea
    [HttpPost]
    [Route("/api/Tarea")]
    public ActionResult<Tarea> CrearTarea(Tarea tarea)
    {
        if (tarea==null)
        {
            return BadRequest("El objeto usuario es nulo");
        }
        tareaRepository.CrearTarea(tarea);
        return Ok(tarea);
    }

    //PUT /api/Tarea/{id}/Nombre/{Nombre}: Permite modificar una Tarea
    [HttpPut]
    [Route("/api/Tarea/{id}/Nombre/{Nombre}")]
    public ActionResult<Tarea> CambiarNombre(string nombre, int id)
    {
        var tarea = new Tarea();
        tarea = tareaRepository.ObtenerTarea(id);
        tarea.Nombre=nombre;
        tareaRepository.ModificarTarea(id, tarea);
        return Ok(tarea);
    }
  
    //PUT /api/Tarea/{id}/Estado/{estado}: Permite modificar el estado de una Tarea
    [HttpPut]
    [Route("/api/Tarea/{id}/Estado/{Estado}")]
     public ActionResult<Tarea> CambiarEstado(EstadoTarea Estado, int id)
    {
        var tarea = new Tarea();
        tarea = tareaRepository.ObtenerTarea(id);
        tarea.Estado=Estado;
        tareaRepository.ModificarTarea(id, tarea);
        return Ok(tarea);
    }
    
    //DELETE /api/Tarea/{id}: Elimina una tarea por su ID
    [HttpDelete]
    [Route("/api/Tarea/{id}")]
    public ActionResult<Tarea> EliminarTarea(int id)
    {
        tareaRepository.BorrarTarea(id);
        return Ok("Tarea eliminada");
    }

    //GET /api/Tarea/{Estado}: Cantidad de tareas en un estado
    [HttpGet]
    [Route("/api/Tarea/{Estado}")]
    public ActionResult<int> CantidadTareasPorEstado(EstadoTarea Estado)
    {
        int cantidad=0;
        var lista = new List<Tarea>();
        lista=tareaRepository.ListarTodasLasTareas();
        foreach (var item in lista)
        {
            if (item.Estado== Estado)
            {
                cantidad++;
            }
        }
        return Ok(cantidad);
    }

    //GET /api/Tarea/Usuario/{Id}: Listar tareas asignada a un usuario
    [HttpGet]
    [Route("/api/Tarea/Usuario/{id}")]
    public ActionResult<IEnumerable<Tarea>> ListarTareasPorUsuario(int id){
        List<Tarea> tareas = tareaRepository.ListarTareasPorUsuario(id);
        if (tareas.Count != 0)
            return Ok(tareas);
        else
            return BadRequest("No hay tareas para este usuario");
    }

    //GET /api/Tarea/Tablero/{Id}: Listar tareas asignada de un tablero
    [HttpGet]
    [Route("/api/Tarea/Tablero/{id}")]
    public ActionResult<IEnumerable<Tarea>> ListarTareasPorTablero(int id){
        List<Tarea> tareas = tareaRepository.ListarTareasPorTablero(id);
        if (tareas.Count != 0)
            return Ok(tareas);
        else
            return BadRequest("No hay tareas para este tablero");
    }
}
