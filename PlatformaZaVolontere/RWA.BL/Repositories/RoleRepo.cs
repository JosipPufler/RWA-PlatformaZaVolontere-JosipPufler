using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RWA.BL.BLModels;
using RWA.BL.DALModels;

namespace RWA.BL.Repositories
{
    public interface IRoleRepo
    {
        public IEnumerable<BlRole> GetAll();
        public BlRole? Get(int id);
        public BlRole Add(BlRole role);
        public BlRole? Update(BlRole role);
        public BlRole? Delete(int id);
    }

    public class RoleRepo : IRoleRepo
    {
        private readonly IMapper _mapper;
        private readonly RwaContext _context;
        public RoleRepo(RwaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public BlRole Add(BlRole role)
        {
            var newRole = _mapper.Map<Role>(role);
            _context.Roles.Add(newRole);
            _context.SaveChanges();
            role.Idrole = newRole.Idrole;
            return role;
        }

        public BlRole? Delete(int id)
        {
            var role = _context.Roles.FirstOrDefault(x => x.Idrole == id);
            if (role == null)
            {
                return null;
            }
            _context.Roles.Remove(role);
            _context.SaveChanges();
            return _mapper.Map<BlRole>(role);
        }

        public BlRole? Get(int id)
        {
            var role = _context.Roles.FirstOrDefault(x => x.Idrole == id);
            if (role == null)
            {
                return null;
            }
            return _mapper.Map<BlRole>(role);
        }

        public IEnumerable<BlRole> GetAll()
        {
            return _mapper.Map<IEnumerable<BlRole>>(_context.Roles);
        }

        public BlRole? Update(BlRole role)
        {
            var roleToUpdate = _context.Roles.FirstOrDefault(x=> x.Idrole == role.Idrole);
            if (roleToUpdate == null)
            {
                return null;
            }
            _context.Entry(roleToUpdate).State = EntityState.Detached;
            _context.Roles.Update(_mapper.Map<Role>(role));
            _context.SaveChanges();
            return role;
        }
    }
}
