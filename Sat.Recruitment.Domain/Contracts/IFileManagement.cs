using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.Domain.Contracts
{
    public interface IFileManagement
    {
        Task<string> readFile();
        Task writeFile(string text);
    }
}
