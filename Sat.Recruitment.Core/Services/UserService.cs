using Sat.Recruitment.Core.Contracts;
using Sat.Recruitment.Domain.Contracts;
using Sat.Recruitment.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Sat.Recruitment.Core.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> AddUser(User user)
        {
            GetGif(user);
            NormalizeEmail(user);
            return await _userRepository.AddUser(user);
        }

        public void ValidateErrors(string name, string email, string address, string phone, ref string errors)
        {
            if (name == null)
                //Validate if Name is null
                errors = "The name is required";
            if (email == null)
                //Validate if Email is null
                errors = errors + " The email is required";
            if (address == null)
                //Validate if Address is null
                errors = errors + " The address is required";
            if (phone == null)
                //Validate if Phone is null
                errors = errors + " The phone is required";
        }

        private void GetGif(User user)
        {
            switch (user.UserType.ToLower())
            {
                case "normal":
                    {
                        if (user.Money > 100)
                        {
                            var percentage = Convert.ToDecimal(0.12);
                            //If new user is normal and has more than USD100
                            var gif = user.Money * percentage;
                            user.Money = user.Money + gif;
                        }
                        if (user.Money < 100)
                        {
                            if (user.Money > 10)
                            {
                                var percentage = Convert.ToDecimal(0.8);
                                var gif = user.Money * percentage;
                                user.Money = Math.Round((user.Money + gif), 0);
                            }
                        }
                    }
                    break;
                case "superuser":
                    {
                        if (user.Money > 100)
                        {
                            var percentage = Convert.ToDecimal(0.20);
                            var gif = user.Money * percentage;
                            user.Money = Math.Round((user.Money + gif), 0);
                        }
                    }
                    break;
                case "premium":
                    {
                        if (user.Money > 100)
                        {
                            var gif = user.Money * 2;
                            user.Money = Math.Round((user.Money + gif), 0);
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        private void NormalizeEmail(User user)
        {
            var aux = user.Email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

            user.Email = string.Join("@", new string[] { aux[0], aux[1] });
        }
    }
}
