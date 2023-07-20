namespace keepr.Services;

public class KeepService
{
    private readonly KeepRepository _keepRepository;
    private readonly VaultsService _vaultsService;

    private readonly VaultKeepsRepository _vaultKeepRepo;
    private readonly ProfileService _profileService;

    public KeepService(KeepRepository keepRepository, VaultsService vaultsService, VaultKeepsRepository vaultKeepsRepo, ProfileService profileService)
    {
        _keepRepository = keepRepository;
        _vaultsService = vaultsService;
        _vaultKeepRepo = vaultKeepsRepo;
        _profileService = profileService;
    }

    internal Keep CreateKeep(Keep keepData)
    {
        Keep newKeep = _keepRepository.CreateKeep(keepData);
        return newKeep;
    }

    public List<Keep> GetAllKeep()
    {
        List<Keep> keeps = _keepRepository.GetAllKeep();
        return keeps;
    }

    internal Keep GetById(int keepId, string userId)
    {
        Keep keep = _keepRepository.GetById(keepId);
        if (keep == null) throw new Exception($"no keep at id:{keepId}");
        keep.Views++;
        _keepRepository.EditKeep(keep);
        return keep;
    }

    internal Keep EditKeep(Keep updateData)
    {

        Keep original = GetById(updateData.Id, updateData.CreatorId);
        if (original.CreatorId != updateData.CreatorId)
        {
            throw new Exception("THAT AINT YO KEEP DOG!");
        }

        original.Name = updateData.Name != null ? updateData.Name : original.Name;
        original.Description = updateData.Description != null ? updateData.Description : original.Description;
        original.Img = updateData.Img != null ? updateData.Img : original.Img;
        _keepRepository.EditKeep(original);
        return original;
    }

    internal void DeleteKeep(int keepId, string userId)
    {

        Keep keep = GetById(keepId, userId);
        if (keep.CreatorId != userId) throw new Exception("I dont think so buddy. Move along with yo bad self");

        int rows = _keepRepository.DeleteKeep(keepId);
        if (rows > 1) throw new Exception("What in tarnation?");
    }

    internal List<Keep> GetKeepsForProfile(string profileId, string userId)
    {
        _profileService.GetProfileById(profileId, userId);
        List<Keep> keeps = _keepRepository.GetKeepsForProfile(profileId);
        return keeps;
    }

    // internal void RemoveKeepFromVault(int vaultKeepId, string id)
    // {
    //     Keep keep = GetById(keepId)
    // }

    // internal List<Keep> GetKeepsInVault(int vaultId, string userId)
    // {
    //     _vaultsService.GetById(vaultId, userId);
    //     List<Keep> keeps = _vaultKeepRepo.GetKeepsInVault(vaultId);
    //     return keeps;

    // }
}