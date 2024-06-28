using AutoMapper;
using RestApi.DTOs;
using RWA.BL.BLModels;

namespace RestApi.AutoMapper
{
    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile() {
            CreateMap<ProjectDto, BlProject>();
            CreateMap<BlProject, ProjectDto>();

            CreateMap<UserDto, BlUser>();
            CreateMap<BlUser, UserDto>();
            
            CreateMap<SkillSetDto, BlSkillSet>();
            CreateMap<BlSkillSet, SkillSetDto>();

            CreateMap<RoleDto, BlRole>();
            CreateMap<BlRole, RoleDto>();

            CreateMap<ProjectTypeDto, BlProjectType>();
            CreateMap<BlProjectType, ProjectTypeDto>();

            CreateMap<UserSkillSetDto, BlUserSkillSet>();
            CreateMap<BlUserSkillSet, UserSkillSetDto>();

            CreateMap<BlProjectSkillSet, ProjectSkillSetDto>();
            CreateMap<ProjectSkillSetDto, BlProjectSkillSet>();

            CreateMap<BlProjectUser, ProjectUserDto>();
            CreateMap<ProjectUserDto, BlProjectUser>();

            CreateMap<UserRegistrationDto, BlUser>();
            CreateMap<BlUser, UserRegistrationDto>();
            
            CreateMap<UserLoginDto, BlUser>();
            CreateMap<BlUser, UserLoginDto>();

            CreateMap<LogDto, BlLog>();
            CreateMap<BlLog, LogDto>();
        }
    }
}
