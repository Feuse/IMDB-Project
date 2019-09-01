using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ILogic
    {
        Task<List<Actor>> GetAllActorsAsync();
        Task RemoveActorAsync(string name);
        Task ResetAsync();
    }
}