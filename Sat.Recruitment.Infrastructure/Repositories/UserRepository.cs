using AutoMapper;
using Sat.Recruitment.Domain.Contracts;
using Sat.Recruitment.Domain.Models;
using Sat.Recruitment.Infrastructure.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Infrastructure.Repositories
{
    public class UserRepository:IUserRepository
    {
        private readonly IMapper _mapper;
        private readonly IFileManagement _fileManagement;

        public UserRepository(IFileManagement fileManagement, IMapper mapper)
        {
            _mapper = mapper;
            _fileManagement = fileManagement;
        }

        public async Task<bool> AddUser(User userDto)
        {
            bool isDuplicated = false;
            List<UserModel> users = await GetListUser();
            var newUser = _mapper.Map<UserModel>(userDto);
            
            isDuplicated = ValidateDuplicatedUser(users, newUser);
            
            if (!isDuplicated)
            {
                users.Add(newUser);
                await SaveUsersFile(users);
            }
            
            return isDuplicated;
        }

        public Task<bool> DeleteUser(User user)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUser(string nombre)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetUsers()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateUser(User user)
        {
            throw new NotImplementedException();
        }

        private bool ValidateDuplicatedUser(List<UserModel> lstUsers, UserModel newUser)
        {
            bool isDuplicated = false;

            foreach (var user in lstUsers)
            {
                if (user.Email == newUser.Email
                    ||
                    user.Phone == newUser.Phone)
                {
                    isDuplicated = true;
                }
                else if (user.Name == newUser.Name)
                {
                    if (user.Address == newUser.Address)
                    {
                        isDuplicated = true;
                    }

                }
            }

            return isDuplicated;
        }

        private async Task<List<UserModel>> GetListUser()
        {
            string readFile = await _fileManagement.readFile();
            List<string> lstUsers = readFile.Split(Environment.NewLine).AsEnumerable().ToList();
            List<UserModel> users = new List<UserModel>();

            foreach (var item in lstUsers)
            {
                string[] userColumns = item.Split(",");
                users.Add(new UserModel()
                {
                    Name = userColumns[0],
                    Email = userColumns[1],
                    Phone = userColumns[2],
                    Address = userColumns[3],
                    UserType = userColumns[4],
                    Money = Convert.ToDecimal(userColumns[5])
                });
            }

            return users;
        }

        private async Task<bool> SaveUsersFile(List<UserModel> users)
        {
            string record = string.Join(Environment.NewLine, users.Select(x => $"{x.Name},{x.Email},{x.Phone},{x.Address},{x.UserType},{x.Money}").ToArray());
            await _fileManagement.writeFile(record);
            return true;
        }
    }
}
