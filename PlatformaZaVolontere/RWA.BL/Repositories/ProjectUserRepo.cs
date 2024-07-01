using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RWA.BL.BLModels;
using RWA.BL.DALModels;
using RWA.BL.Utilities;

namespace RWA.BL.Repositories
{
    public interface IProjectUserRepo
    { 
        public IEnumerable<BlProjectUser> GetAll();
    }
    public class ProjectUserRepo : IProjectUserRepo
    {
        private readonly RwaContext _context;
        private readonly IMapper _mapper;
        public ProjectUserRepo(RwaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public IEnumerable<BlProjectUser> GetAll()
        {
            return _mapper.Map<IEnumerable<BlProjectUser>>(_context.ProjectUsers.Include("User").Include("Project").Include("Project.Type"));
        }
    }
}
