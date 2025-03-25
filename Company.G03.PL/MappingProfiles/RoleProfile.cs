using AutoMapper;
using Company.G03.PL.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Company.G03.PL.MappingProfiles
{
    public class RoleProfile:Profile
    {
        public RoleProfile()
        {
            CreateMap<IdentityRole,RoleViewModel>  ().ForMember(d=>d.RoleName,r=>r.MapFrom(o=>o.Name)).ReverseMap();
        }
    }
}
