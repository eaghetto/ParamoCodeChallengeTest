using Sat.Recruitment.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.Domain.Contracts
{
    public interface IUserRepository
    {
        Task<bool> AddUser(User user);
    }
}
