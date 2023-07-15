namespace keepr.Services;

public class KeepService
{
    private readonly KeepRepository _keepRepository;

    public KeepService(KeepRepository keepRepository)
    {
        _keepRepository = keepRepository;
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

    internal Keep GetById(int keepId)
    {
        Keep keep = _keepRepository.GetById(keepId);
        if (keep == null) throw new Exception($"no keep at id:{keepId}");
        keep.Views++;
        _keepRepository.EditKeep(keep);
        return keep;
    }

    internal Keep EditKeep(Keep updateData)
    {
        Keep original = GetById(updateData.Id);
        original.Name = updateData.Name != null ? updateData.Name : original.Name;
        original.Description = updateData.Description != null ? updateData.Description : original.Description;
        original.Img = updateData.Img != null ? updateData.Img : original.Img;
        _keepRepository.EditKeep(original);
        return original;
    }

    internal void DeleteKeep(int keepId, string userId)
    {
        Keep keep = GetById(keepId);
        if (keep.CreatorId != userId) new Exception("I dont think so buddy. Move along with yo bad self");
        int rows = _keepRepository.DeleteKeep(keepId);
        if (rows > 1) new Exception("What in tarnation?");
    }
}