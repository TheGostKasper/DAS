using Basty.Interfaces;
using Basty.Models;
 
namespace Basty.Repository
{
    public class TeamRepository: RepositoryBase<Team>, ITeamRepository
    {
        public TeamRepository(BastyDBContext repositoryContext)
            :base(repositoryContext)
        {
        }
    }
}