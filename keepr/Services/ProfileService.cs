namespace keepr.Services
{
    public class ProfileService
    {
        private readonly ProfileRepository _profileRepo;

        public ProfileService(ProfileRepository profileRepo)
        {
            _profileRepo = profileRepo;
        }


        internal Profile GetProfileById(string profileId, string userId)
        {
            Profile profile = _profileRepo.GetProfileById(profileId);
            if (profile == null) throw new Exception($"No PROFILE AT ID:{profileId}");
            return profile;
        }
    }
}