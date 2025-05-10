using AutoMapper;
using Company.G03.DAL.Model;
using Company.G03.PL.ViewModels;

namespace Company.G03.PL.MappingProfiles
{
    public class UserProfile :Profile
    {
        public UserProfile() 
        {
            CreateMap<ApplicationUser,UserViewModel>().ReverseMap();
        }
    }
}
