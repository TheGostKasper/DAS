using Basty.Interfaces;
using Basty.Models;
 using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;


namespace Basty.Repository
{
    public class UserRepository: RepositoryBase<User>, IUserRepository
    {
        public UserRepository(BastyDBContext repositoryContext)
            :base(repositoryContext)
        {
           
        }

        
    }
}