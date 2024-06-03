using EFCoreExample.Data;
using EFCoreExample.DTOs;
using EFCoreExample.Models;
using EFCoreExample.Services;
using Microsoft.AspNetCore.Mvc;



namespace EFCoreExample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProfilesController : ControllerBase
    {
        //This appdbcontext is used to interact with our database


        //private readonly AppDbContext _context;


        /*public ProfilesController(AppDbContext context)
        {
            _context = context;
        }  */


        private readonly IProfileService _profileService;
      public ProfilesController(IProfileService profileService)
      {
        _profileService = profileService;
      }



        [HttpGet]
        public ActionResult<IEnumerable<ProfileDTO>> GetProfiles()
        {
           var profiles = _profileService.GetAllProfiles(); 
           return Ok(profiles);
        }


        //added
         [HttpGet("{ProfileId}")]
        public ActionResult<ProfileDTO> GetProfileById(int ProfileId)
        {

             var profile = _profileService.GetProfileById(ProfileId);
            return profile;

        }








         [HttpPost]
        public ActionResult<ProfileDTO> PostProfile(ProfileDTO profileDto)
        {

            var profile = _profileService.CreateProfile(profileDto);
            return CreatedAtAction(nameof(GetProfileById), new { ProfileId = profile.ProfileId }, profileDto);

            /*
            var user = _context.Users.Find(profileDto.UserId);
            var NewProfile = new Profile
            {
                Bio = profileDto.Bio,
                UserId = profileDto.UserId,
                User = user
            };

            _context.Profiles.Add(NewProfile);
            _context.SaveChanges();  */

            //return CreatedAtAction(nameof(GetProfileById), new { ProfileId = NewProfile.ProfileId }, profileDto);
        }



        
        //added
        [HttpPut("{ProfileId}")]
        public ActionResult<ProfileDTO> UpdateProfile(int ProfileId, ProfileDTO UpdatedProfile)
        {

            _profileService.UpdateProfile(ProfileId, UpdatedProfile);
            return Ok(UpdatedProfile);


            /*
            var profile = _context.Profiles.FirstOrDefault(p => p.ProfileId == ProfileId);

            profile.Bio = UpdatedProfile.Bio;

            _context.Profiles.Update(profile);
            _context.SaveChanges();

            return Ok(UpdatedProfile); */
        }
        

        
            //added
            [HttpDelete("{ProfileId}")]
        public IActionResult DeleteProfile(int ProfileId)
        {

             _profileService.DeleteProfile(ProfileId);
            return Ok();
            /*
            var profile = _context.Profiles.FirstOrDefault(p => p.ProfileId == ProfileId);
            _context.Profiles.Remove(profile);
            _context.SaveChanges();

            return Ok(); */
        }

    }
}