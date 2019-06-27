using Basty.Interfaces;
using Basty.Models;
 
namespace Basty.Repository
{
    public class PlayerRepository: RepositoryBase<Player>, IPlayerRepository
    {
        public PlayerRepository(BastyDBContext repositoryContext)
            :base(repositoryContext)
        {
        }
    }
}