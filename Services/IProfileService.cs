using EFCoreExample.DTOs;
using EFCoreExample.Models;

namespace EFCoreExample.Services
{
public interface IProfileService
    {
        IEnumerable<ProfileDTO> GetAllProfiles();

        ProfileDTO GetProfileById(int ProfileId);

        Profile CreateProfile(ProfileDTO ProfileDto);

        void UpdateProfile(int ProfileId, ProfileDTO UpdatedProfile);

        void DeleteProfile(int ProfileId);
    }
}


