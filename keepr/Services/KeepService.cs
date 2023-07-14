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
        return keep;
    }
}