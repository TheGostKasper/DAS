using Basty.Interfaces;
using Basty.Models;
 
namespace Basty.Repository
{
    public class RoleRepository: RepositoryBase<Role>, IRoleRepository
    {
        public RoleRepository(BastyDBContext repositoryContext)
            :base(repositoryContext)
        {
        }
    }
}