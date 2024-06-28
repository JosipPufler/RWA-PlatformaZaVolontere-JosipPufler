using AutoMapper;
using RWA.BL.BLModels;
using WebApp.Models.ViewModels;

namespace WebApp.Mapping
{
    public class MVCMappings : Profile
    {
        public MVCMappings() {
            CreateMap<BlUser, UserVM>().ForMember(dst => dst.RoleName, opt => opt.MapFrom(src => src.Role.Name)).ForMember(dst => dst.RoleId, opt => opt.MapFrom(src => src.Role.Idrole)).ForMember(dst => dst.UserSkillSets, opt => opt.MapFrom(src => src.UserSkillSets.Select(x => x.IdskillSet)));
            CreateMap<UserVM, BlUser>().ForPath(dst => dst.Role.Idrole, opt => opt.MapFrom(src => src.RoleId)).ForPath(dst => dst.Role.Name, opt => opt.MapFrom(src => src.RoleName)).ForMember(dst => dst.UserSkillSets, opt => opt.MapFrom(src => src.UserSkillSets.Select(x => new BlSkillSet() { IdskillSet = x })));

            CreateMap<RoleVM, BlRole>();
            CreateMap<BlRole, RoleVM>();

            CreateMap<UserLoginVM, BlUser>();

            CreateMap<ProjectTypeVM, BlProjectType>();
            CreateMap<BlProjectType, ProjectTypeVM>();

            CreateMap<ProjectVM, BlProject>().ForMember(dst => dst.SkillSets, opt => opt.MapFrom(src => src.SkillSets.Select(x => new BlSkillSet() { IdskillSet = x }))).ForPath(dst => dst.ProjectType.IdprojectType, opt => opt.MapFrom(src => src.ProjectTypeId));
            CreateMap<BlProject, ProjectVM>().ForMember(dst => dst.ProjectTypeName, opt => opt.MapFrom(src => src.ProjectType.Name)).ForMember(dst => dst.SkillSets, opt => opt.MapFrom(src => src.SkillSets.Select(x => x.IdskillSet))).ForPath(dst => dst.ProjectTypeId, opt => opt.MapFrom(src => src.ProjectType.IdprojectType));

            CreateMap<SkillSetVM, BlSkillSet>();
            CreateMap<BlSkillSet, SkillSetVM>();
        }
    }
}
