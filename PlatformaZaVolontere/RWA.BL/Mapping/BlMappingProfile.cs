using AutoMapper;
using RWA.BL.BLModels;
using RWA.BL.DALModels;

namespace RWA.BL.Mapping
{
    public class BlMappingProfile : Profile
    {
        public BlMappingProfile()
        {
            CreateMap<Project, BlProject>().ForMember(dst => dst.SkillSets, opt => opt.MapFrom(src => src.ProjectSkillSets.Select(x => x.SkillSet)))
                .ForMember(dst => dst.ProjectType, opt => opt.MapFrom(src => src.Type));
            CreateMap<BlProject, Project>().ForMember(dst => dst.TypeId, opt => opt.MapFrom(src => src.ProjectType.IdprojectType));
            
            CreateMap<User, BlUser>().ForMember(dst => dst.UserSkillSets, opt => opt.MapFrom(src => src.UserSkillSets.Select(x => x.SkillSet)))
                .ForMember(dst => dst.Password, opt => opt.MapFrom(src => src.PasswordHash));
            CreateMap<BlUser, User>().ForMember(dst => dst.PasswordHash, opt => opt.MapFrom(src => src.Password));

            CreateMap<SkillSet, BlSkillSet>();
            CreateMap<BlSkillSet, SkillSet>();

            CreateMap<BlSkillSet, UserSkillSet>();

            CreateMap<Role, BlRole>();
            CreateMap<BlRole, Role>();

            CreateMap<ProjectType, BlProjectType>();
            CreateMap<BlProjectType, ProjectType>();
            
            CreateMap<BlUserSkillSet, UserSkillSet>();
            CreateMap<UserSkillSet, BlUserSkillSet>();

            CreateMap<BlProjectSkillSet, ProjectSkillSet>();
            CreateMap<ProjectSkillSet, BlProjectSkillSet>();

            CreateMap<BlProjectUser, ProjectUser>();
            CreateMap<ProjectUser, BlProjectUser>();

            CreateMap<Log, BlLog>();
            CreateMap<BlLog, Log>();
        }
    }
}
