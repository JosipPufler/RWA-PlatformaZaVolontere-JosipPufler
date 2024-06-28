using AutoMapper;
using Bl.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RWA.BL.BLModels;
using RWA.BL.DALModels;

namespace RWA.BL.Repositories
{
    public interface IUserRepo
    {
        public IEnumerable<BlUser> GetAll();
        public BlUser? Get(int id);
        public BlUser Add(BlUser user);
        public BlUser? Update(BlUser user);
        public BlUser? Delete(int id);
        public BlUser? Login(BlUser user);
        public bool ChangePassword(string username, string oldPassword, string newPassword);
    }

    public class UserRepo : IUserRepo
    {
        private readonly RwaContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public UserRepo(RwaContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _config = configuration;
        }

        public BlUser Add(BlUser user)
        {
            var skillSets = user.UserSkillSets;
            user.UserSkillSets = null;
            User newUser = _mapper.Map<User>(user);
            newUser.PasswordSalt = AuthUtilities.GetSalt();
            newUser.PasswordHash = AuthUtilities.GetStringSha256Hash(user.Password, newUser.PasswordSalt);
            newUser.JoinDate = DateTime.Now;
            if (user.Role == null || user.Role.Idrole == 0)
            {
                newUser.RoleId = 1;
            }
            else
            {
                newUser.RoleId = user.Role.Idrole;
            }
            newUser.Role = null;
            _context.Users.Add(newUser);
            _context.SaveChanges();

            foreach (var skillSet in skillSets)
            {
                _context.UserSkillSets.Add(new UserSkillSet()
                {
                    UserId = newUser.Iduser,
                    SkillSetId = skillSet.IdskillSet
                });
                _context.SaveChanges();
            }

            return Get(newUser.Iduser);
        }

        public BlUser? Login(BlUser userLogin) {
            User? user = _context.Users.Include("Role").FirstOrDefault(x => x.Username == userLogin.Username);
            if (user == null)
            {
                return null;
            }
            string v = AuthUtilities.GetStringSha256Hash(userLogin.Password, user.PasswordSalt);
            if (v != user.PasswordHash)
            {
                return null;
            }

            return _mapper.Map<BlUser>(user);
        }

        public bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            var existingUser = _context.Users.FirstOrDefault(x => x.Username == username);
            if (existingUser == null || AuthUtilities.GetStringSha256Hash(oldPassword, existingUser.PasswordSalt) != existingUser.PasswordHash)
            {
                return false;
            }

            existingUser.PasswordSalt = AuthUtilities.GetSalt();
            existingUser.PasswordHash = AuthUtilities.GetStringSha256Hash(newPassword, existingUser.PasswordSalt);

            _context.Users.Update(existingUser);
            _context.SaveChanges();
            return true;
        }

        public BlUser? Delete(int id)
            {
            var user = _context.Users.Include("UserSkillSets").Include("UserSkillSets.SkillSet").FirstOrDefault(x => x.Iduser == id);
            if (user == null)
            {
                return null;
            }
            var deletedUser = _mapper.Map<BlUser>(user);
            _context.Users.Remove(user);
            _context.SaveChanges();
            return deletedUser;
        }

        public BlUser? Get(int id)
        {
            var user = _context.Users.Include("Role").Include("UserSkillSets").Include("UserSkillSets.SkillSet")
                    .FirstOrDefault(x => x.Iduser == id);
            if (user == null)
            {
                return null;
            }
            return _mapper.Map<BlUser>(user);
        }

        public IEnumerable<BlUser> GetAll()
        {
            var x = _context.Users.Include("Role")
                    .Include("UserSkillSets")
                    .Include("UserSkillSets.SkillSet");
            return _mapper.Map<IEnumerable<BlUser>>(x);
        }

        public BlUser? Update(BlUser user)
        {
            var userToUpdate = _context.Users.Include("Role").Include("UserSkillSets")
                    .Include("UserSkillSets.SkillSet").FirstOrDefault(x => x.Iduser == user.Iduser);
            if (userToUpdate == null)
            {
                return null;
            }
            var skillSets = _context.UserSkillSets.Where(x => x.UserId == user.Iduser);
            _context.UserSkillSets.RemoveRange(skillSets);

            foreach (var skill in user.UserSkillSets)
            {
                _context.UserSkillSets.Add(
                        new UserSkillSet() { 
                            SkillSetId = skill.IdskillSet,
                            UserId = user.Iduser,
                        }
                    );
            }
            _context.SaveChanges();
            userToUpdate.Username = user.Username;
            userToUpdate.FirstName = user.FirstName;
            userToUpdate.LastName = user.LastName;
            userToUpdate.PhoneNumber = user.PhoneNumber;
            userToUpdate.Email = user.Email;
            if (user.Role != null && user.Role.Idrole != 0)
            {
                userToUpdate.RoleId = user.Role.Idrole;
            }
            _context.Entry(userToUpdate).State = EntityState.Detached;
            _context.Users.Update(userToUpdate);
            _context.SaveChanges();
            return user;
        }
    }
}
