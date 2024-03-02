using DevoraLimeArena.Shared.Services;
using Microsoft.AspNetCore.Mvc;

namespace DevoraLimeArena.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArenaController(IArenaWarService arenaWarService) : ControllerBase
    {
        [HttpPost]
        [Route("CreateArena")]
        public Guid CreateArena(int N)
        {
            return arenaWarService.CreateArena(N);
        }

        [HttpGet]
        [Route("GetArenaFights")]
        public async Task<List<Fight>> GetArenaFights(Guid arenaId)
        {
            return await arenaWarService.GetArenaFights(arenaId);
        }
    }
}
