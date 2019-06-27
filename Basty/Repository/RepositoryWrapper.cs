using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Basty.Interfaces;
using Basty.Models;

namespace Basty.Repository
{
    public class RepositoryWrapper: IRepositoryWrapper
    {
        private BastyDBContext _repoContext;
        private IPlayerRepository _player;
        private ITeamRepository _team;
        private IUserRepository _user;
        private IRoleRepository _role;
 
        public IPlayerRepository Player {
            get {
                if(_player == null)
                {
                    _player = new PlayerRepository(_repoContext);
                }
 
                return _player;
            }
        }
 
        public ITeamRepository Team {
            get {
                if(_team == null)
                {
                    _team = new TeamRepository(_repoContext);
                }
 
                return _team;
            }
        }
         public IUserRepository User {
            get {
                if(_user == null)
                {
                    _user = new UserRepository(_repoContext);
                }
 
                return _user;
            }
        }
         public IRoleRepository Role {
            get {
                if(_role == null)
                {
                    _role = new RoleRepository(_repoContext);
                }
 
                return _role;
            }
        }

        
        public RepositoryWrapper(BastyDBContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }
 
        public void Save()
        {
            _repoContext.SaveChanges();
        }
    }
}