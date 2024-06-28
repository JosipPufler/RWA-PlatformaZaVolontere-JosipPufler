using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RWA.BL.BLModels;
using RWA.BL.DALModels;

namespace RWA.BL.Repositories
{
    public interface IProjectTypeRepo {
        public IEnumerable<BlProjectType> GetAll();
        public BlProjectType? Get(int id);
        public BlProjectType Add(BlProjectType projectType);
        public BlProjectType? Update(BlProjectType projectType);
        public BlProjectType? Delete(int id);
    }

    public class ProjectTypeRepo : IProjectTypeRepo
    {
        private readonly IMapper _mapper;
        private readonly RwaContext _context;
        public ProjectTypeRepo(IMapper mapper, RwaContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public BlProjectType Add(BlProjectType projectType)
        {
            var newProjectType = _mapper.Map<ProjectType>(projectType);
            _context.ProjectTypes.Add(newProjectType);
            _context.SaveChanges();
            projectType.IdprojectType = newProjectType.IdprojectType;
            return projectType;
        }

        public BlProjectType? Delete(int id)
        {
            var projectType = _context.ProjectTypes.FirstOrDefault(x => x.IdprojectType == id);
            if (projectType == null)
            {
                return null;
            }
            _context.ProjectTypes.Remove(projectType);
            _context.SaveChanges();
            return _mapper.Map<BlProjectType>(projectType);
        }

        public BlProjectType? Get(int id)
        {
            var projectType = _context.ProjectTypes.FirstOrDefault(x => x.IdprojectType == id);
            if (projectType == null)
            {
                return null;
            }
            return _mapper.Map<BlProjectType>(projectType);
        }

        public IEnumerable<BlProjectType> GetAll()
        {
            return _mapper.Map<IEnumerable<BlProjectType>>(_context.ProjectTypes);
        }

        public BlProjectType? Update(BlProjectType projectType)
        {
            var projectTypeToUpdate = _context.ProjectTypes.FirstOrDefault(x => x.IdprojectType == projectType.IdprojectType);
            if (projectTypeToUpdate == null)
            {
                return null;
            }
            _context.Entry(projectTypeToUpdate).State = EntityState.Detached;
            _context.ProjectTypes.Update(_mapper.Map<ProjectType>(projectType));
            _context.SaveChanges();
            return projectType;
        }
    }
}
