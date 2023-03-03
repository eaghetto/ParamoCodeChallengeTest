using Sat.Recruitment.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.Core.Contracts
{
    public interface IUserService
    {
        Task<bool> AddUser(User user);
        void ValidateErrors(string name, string email, string address, string phone, ref string errors);
    }
}
