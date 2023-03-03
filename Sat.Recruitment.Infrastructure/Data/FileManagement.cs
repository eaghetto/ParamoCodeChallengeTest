using Sat.Recruitment.Domain.Contracts;
using System.IO;
using System.Threading.Tasks;

namespace Sat.Recruitment.Infrastructure.Data
{
    public class FileManagement : IFileManagement
    {
        private readonly string _filePath = Directory.GetCurrentDirectory() + "/Files/Users.txt";

        public FileManagement()
        {
        }

        public async Task<string> readFile()
        {
            string readText = await File.ReadAllTextAsync(_filePath);
            return readText;
        }

        public async Task writeFile(string text)
        {
            await File.WriteAllTextAsync(_filePath, text);
        }
    }
}
