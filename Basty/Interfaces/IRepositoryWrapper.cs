using System;
using Basty.Models;
using Basty.Repository;

namespace Basty.Interfaces
{
    public interface IRepositoryWrapper
    {
        IPlayerRepository Player { get; }
        ITeamRepository Team { get; }
        IUserRepository User { get; }
        IRoleRepository Role { get; }
        void Save();
    }
}