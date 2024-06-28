using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RWA.BL.BLModels;
using RWA.BL.DALModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RWA.BL.Repositories
{
    public interface ISkillSetRepo
    {
        public IEnumerable<BlSkillSet> GetAll();
        public BlSkillSet? Get(int id);
        public BlSkillSet Add(BlSkillSet skillSet);
        public BlSkillSet? Update(BlSkillSet skillSet);
        public BlSkillSet? Delete(int id);
    }

    public class SkillSetRepo : ISkillSetRepo
    {
        private readonly IMapper _mapper;
        private readonly RwaContext _context;
        public SkillSetRepo(IMapper mapper, RwaContext rwaContext)
        {
            _mapper = mapper;
            _context = rwaContext;
        }

        public BlSkillSet Add(BlSkillSet skillSet)
        {
            var newSkillSet = _mapper.Map<SkillSet>(skillSet);
            _context.SkillSets.Add(newSkillSet);
            skillSet.IdskillSet = newSkillSet.IdskillSet;
            _context.SaveChanges();
            return skillSet;
        }

        public BlSkillSet? Delete(int id)
        {
            var skillSet = _context.SkillSets.FirstOrDefault(x => x.IdskillSet == id);
            if (skillSet == null)
            {
                return null;
            }
            _context.SkillSets.Remove(skillSet);
            _context.SaveChanges();
            return _mapper.Map<BlSkillSet>(skillSet);
        }

        public BlSkillSet? Get(int id)
        {
            var skillSet = _context.SkillSets.FirstOrDefault(x => x.IdskillSet == id);
            if (skillSet == null)
            {
                return null;
            }
            return _mapper.Map<BlSkillSet>(skillSet);
        }

        public IEnumerable<BlSkillSet> GetAll()
        {
            return _mapper.Map<IEnumerable<BlSkillSet>>(_context.SkillSets);
        }

        public BlSkillSet? Update(BlSkillSet skillSet)
        {
            var skillSetToUpdate = _context.SkillSets.FirstOrDefault(x => x.IdskillSet == skillSet.IdskillSet);
            if (skillSetToUpdate == null)
            {
                return null;
            }
            _context.Entry(skillSetToUpdate).State = EntityState.Detached;
            _context.SkillSets.Update(_mapper.Map<SkillSet>(skillSet));
            _context.SaveChanges();
            return skillSet;
        }
    }
}
