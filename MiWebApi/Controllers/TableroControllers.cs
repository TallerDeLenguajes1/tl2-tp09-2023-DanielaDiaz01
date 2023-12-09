using Microsoft.AspNetCore.Mvc;
using tp9Daniela;
namespace tp9DanielaControllers;

[ApiController]
[Route("[controller]")]

public class TableroController : ControllerBase
{
    private readonly ILogger<TableroController> _logger;
    private ITableroRepository tableroRepository;
    public TableroController(ILogger<TableroController> logger)
    {
        _logger = logger;
        tableroRepository = new TableroRepository();
    }

    //POST /api/Tablero: Permite crear un tablero
    [HttpPost]
    [Route("/api/Tablero")]
    public ActionResult<Tablero> CrearTablero(Tablero tablero)
    {
        tableroRepository.CrearTablero(tablero);
        return Ok(tablero);
    }

    [HttpGet]
    [Route("/api/tableros")]
    public ActionResult<IEnumerable<Tablero>> ListarTableros(){
        List<Tablero> tableros = tableroRepository.ListarTableros();
        if (tableros.Count != 0)
            return Ok(tableros);
        else
            return BadRequest("Todavia no tengo tableros");
    }
}