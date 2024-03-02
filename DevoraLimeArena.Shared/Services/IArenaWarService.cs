
namespace DevoraLimeArena.Shared.Services
{
    public interface IArenaWarService
    {
        Guid CreateArena();
        Task<List<Fight>> GetArenaFights(Guid id);
    }
}