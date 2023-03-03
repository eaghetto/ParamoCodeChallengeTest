using AutoMapper;
using Sat.Recruitment.Domain.Models;
using Sat.Recruitment.Infrastructure.DataModels;

namespace Sat.Recruitment.Infrastructure.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserModel, User>().ReverseMap();
        }
    }
}
