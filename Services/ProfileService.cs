using EFCoreExample.Data;
using EFCoreExample.DTOs;
using EFCoreExample.Models;

namespace EFCoreExample.Services
{
    public class ProfileService : IProfileService
    {
         private readonly AppDbContext _context;

        public ProfileService(AppDbContext context)
        {
            _context = context;
        }



          public Profile CreateProfile(ProfileDTO ProfileDto)
        {
            if (ValidateNewProfile(ProfileDto))
            {
                var profile = new Profile
                {
                    Bio = ProfileDto.Bio,
                    UserId = ProfileDto.UserId
                };
                _context.Profiles.Add(profile);
                _context.SaveChanges();

                return profile;
            }  else
            {
                throw new Exception("Invalid Profile");
            }
        }


          private bool ValidateNewProfile(ProfileDTO ProfileDto)
        {
            if(ProfileDto.Bio.Trim().Length ==0){
                return false;
            }else return true;
        }


         public void DeleteProfile(int ProfileId)
        {
            var profile = _context.Users.FirstOrDefault(u => u.UserId == ProfileId);
        }

       
         public IEnumerable<ProfileDTO> GetAllProfiles()
        {
           var profiles = _context.Profiles
                 .Select(p => new ProfileDTO
                {
                    UserId = p.ProfileId,
                    Bio = p.Bio,
                }).ToList();
                return profiles;
        }



          public ProfileDTO GetProfileById(int ProfileId)
        {
           var profile = _context.Profiles.Find(ProfileId);
           var profileDto = new ProfileDTO
            {
                Bio = profile.Bio,
                UserId = profile.UserId
            };
            return profileDto;
        }



           public void UpdateProfile(int ProfileId, ProfileDTO UpdatedProfile)
        {
            var profile = _context.Profiles.FirstOrDefault(p => p.ProfileId == ProfileId);

            profile.Bio = UpdatedProfile.Bio;

            _context.Profiles.Update(profile);
            _context.SaveChanges();
        }

       
    }
}