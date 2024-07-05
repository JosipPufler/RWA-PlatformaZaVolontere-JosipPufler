using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RWA.BL.BLModels;
using RWA.BL.DALModels;
using RWA.BL.Utilities;

namespace RWA.BL.Repositories
{
    public interface IProjectRepo {
        public IEnumerable<BlProject> GetAll();
        public BlProject? Get(int id);
        public IEnumerable<BlProject> SearchByTitle(string searchTerm, int page, int size, int? projectTypeId);
        public BlProject Add(BlProject project);
        public BlProject? Update(BlProject project);
        public BlProject? Delete(int id);
        public IEnumerable<BlUser> GetRelatedUsers(int projectId);
        public void JoinProject(int idUser, int idProject);
        public int SearchByTitleCount(string searchTerm, int? projectTypeId);
    }

    public class ProjectRepo : IProjectRepo
    {
        private readonly RwaContext _context;
        private readonly IMapper _mapper;
        private readonly ILoggerService _logger;
        public ProjectRepo(RwaContext context, IMapper mapper, ILoggerService loggerService)
        {
            _context = context;
            _mapper = mapper;
            _logger = loggerService;
        }

        public IEnumerable<BlProject> GetAll()
        {
            IEnumerable<Project> dalUsers = _context.Projects.Include("ProjectSkillSets").Include("ProjectSkillSets.SkillSet").Include("Type");
            _logger.CreateLog(new BlLog
            {
                Level = 1,
                Message = $"Retrieving all projects",
                Timestamp = DateTime.Now,
            });
            return _mapper.Map<IEnumerable<BlProject>>(dalUsers);
        }

        public BlProject? Get(int id)
        {
            _logger.CreateLog(new BlLog
            {
                Level = 1,
                Message = $"Trying to get project with ID={id}",
                Timestamp = DateTime.Now,
            });
            return _mapper.Map<BlProject>(_context.Projects.Include("ProjectSkillSets")
                    .Include("ProjectSkillSets.SkillSet")
                    .Include("Type")
                    .FirstOrDefault(x => x.Idproject == id));
        }

        public IEnumerable<BlProject> SearchByTitle(string searchTerm, int page, int size, int? projectTypeId)
        {
            var projects = _mapper.Map<IEnumerable<BlProject>>(_context.Projects.Include("ProjectSkillSets").Include("ProjectSkillSets.SkillSet").Include("Type").Where(x => x.Title.ToLower().Contains(searchTerm.ToLower())));
            if (projectTypeId != 0 && projectTypeId != null)
            {
                projects = projects.Where(x => x.ProjectType.IdprojectType == projectTypeId);
            }
            return projects.Skip((page - 1) * size).Take(size);
        }

        public int SearchByTitleCount(string searchTerm, int? projectTypeId) {
            var projects = _mapper.Map<IEnumerable<BlProject>>(_context.Projects.Include("ProjectSkillSets").Include("ProjectSkillSets.SkillSet").Include("Type").Where(x => x.Title.ToLower().Contains(searchTerm.ToLower())));
            if (projectTypeId != 0)
            {
                projects = projects.Where(x => x.ProjectType.IdprojectType == projectTypeId);
            }
            return projects.Count();
        }

        public BlProject Add(BlProject newProject)
        {
            try
            {
                Project dalNewProject = _mapper.Map<Project>(newProject);
                dalNewProject.Idproject = 0;
                dalNewProject.PublishDate = DateTime.Now;
                _context.Projects.Add(dalNewProject);
                _context.SaveChanges();
                foreach (var skill in newProject.SkillSets)
                {
                    _context.ProjectSkillSets.Add(new ProjectSkillSet()
                    {
                        SkillSetId = skill.IdskillSet,
                        ProjectId = dalNewProject.Idproject,
                    });
                }
                newProject.Idproject = dalNewProject.Idproject;
                _context.SaveChanges();

                _logger.CreateLog(new BlLog()
                {
                    Level = 2,
                    Message = $"Created new project with id={newProject.Idproject}",
                    Timestamp = DateTime.Now,
                });
                return newProject;
            }
            catch (Exception)
            {
                _logger.CreateLog(new BlLog
                {
                    Level = 2,
                    Message = $"There was an issue when creating a new project",
                    Timestamp = DateTime.Now,
                });
                throw;
            }
        }

        public BlProject? Update(BlProject project)
        {

            var projectToUpdate = _context.Projects.Include("ProjectSkillSets")
                .Include("ProjectSkillSets.SkillSet")
                .Include("Type").FirstOrDefault(x => x.Idproject == project.Idproject);
            try
            {
                if (projectToUpdate == null)
                {
                    return null;
                }
                var skillSets = _context.ProjectSkillSets.Where(x => x.ProjectId == project.Idproject);
                _context.ProjectSkillSets.RemoveRange(skillSets);

                foreach (var skill in project.SkillSets)
                {
                    _context.ProjectSkillSets.Add(new ProjectSkillSet()
                    {
                        SkillSetId = skill.IdskillSet,
                        ProjectId = project.Idproject,
                    });
                }
                _context.SaveChanges();
                project.PublishDate = projectToUpdate.PublishDate;
                _context.Entry(projectToUpdate).State = EntityState.Detached;
                projectToUpdate = _mapper.Map<Project>(project);
                _context.Update(projectToUpdate);
                _context.SaveChanges();

                _logger.CreateLog(new BlLog()
                {
                    Level = 1,
                    Message = $"Successfully updated project with id={projectToUpdate.Idproject}",
                    Timestamp = DateTime.Now,
                });
                return project;
            }
            catch (Exception)
            {
                _context.Entry(projectToUpdate).State = EntityState.Detached;
                _logger.CreateLog(new BlLog()
                {
                    Level = 1,
                    Message = $"There was a problem when updating project with id={project.Idproject}",
                    Timestamp = DateTime.Now,
                });
                throw;
            }
        }

        public BlProject? Delete(int id)
        {
            try
            {
                Project? project = _context.Projects.Include("ProjectSkillSets")
                    .Include("ProjectSkillSets.SkillSet")
                    .Include("Type").FirstOrDefault(x => x.Idproject == id);
                if (project == null)
                {
                    return null;
                }
                BlProject deletedProject = _mapper.Map<BlProject>(project);
                _context.Projects.Remove(project);
                _context.SaveChanges();
                _logger.CreateLog(new BlLog()
                {
                    Level = 1,
                    Message = $"Successfully delete project with id={id}",
                    Timestamp = DateTime.Now,
                });

                return deletedProject;
            }
            catch (Exception)
            {
                _logger.CreateLog(new BlLog()
                {
                    Level = 1,
                    Message = $"Could not delete project with id={id}",
                    Timestamp = DateTime.Now,
                });
                throw;
            }
        }

        public IEnumerable<BlUser> GetRelatedUsers(int projectId)
        {
            IEnumerable<User> relatedUsers = _context.ProjectUsers.Include("User").Where(x => x.ProjectId == projectId).Select(x => x.User);
            return _mapper.Map<IEnumerable<BlUser>>(relatedUsers);
        }

        public void JoinProject(int idUser, int idProject)
        {
            _context.ProjectUsers.Add(new ProjectUser()
            {
                ProjectId = idProject,
                UserId = idUser,
            });
            _context.SaveChanges();
        }
    }
}
