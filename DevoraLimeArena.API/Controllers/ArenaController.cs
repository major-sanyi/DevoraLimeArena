using DevoraLimeArena.Shared.Services;
using Microsoft.AspNetCore.Mvc;

namespace DevoraLimeArena.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArenaController : ControllerBase
    {

        private readonly ILogger<ArenaController> _logger;
        private readonly IArenaWarService _arenaWarService;

        public ArenaController(ILogger<ArenaController> logger, IArenaWarService arenaWarService)
        {
            _logger = logger;
            _arenaWarService = arenaWarService;
        }

        [HttpPost(Name = "CreateArena")]
        public Guid CreateArena()
        {
            return _arenaWarService.CreateArena();
        }

        [HttpGet(Name = "GetArenaFights")]
        public async Task<List<Fight>> GetArenaFights(Guid arenaId)
        {
            return await _arenaWarService.GetArenaFights(arenaId);
        }
    }
}
