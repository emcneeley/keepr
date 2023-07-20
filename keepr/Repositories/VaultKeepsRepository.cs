namespace keepr.Repositories;

public class VaultKeepsRepository
{
    private readonly IDbConnection _db;

    public VaultKeepsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
    {
        string sql = @"
       INSERT INTO vaultKeep
       (CreatorId, VaultId,KeepId)
       VALUES
       (@CreatorId, @VaultId,@KeepId);
       SELECT
       vk.*,
       creator.*
       FROM vaultKeep vk
       JOIN accounts creator ON vk.CreatorId = creator.id
       WHERE vk.Id=LAST_INSERT_ID()
       ;";

        VaultKeep vaultKeep = _db.Query<VaultKeep, Account, VaultKeep>(sql, (vaultKeep, creator) =>
        {
            vaultKeep.Creator = creator;
            return vaultKeep;
        }, vaultKeepData).FirstOrDefault();
        return vaultKeep;
    }

    internal VaultKeep GetById(int vaultKeepId)
    {
        string sql = @" SELECT * FROM vaultKeep WHERE id =@vaultKeepId;";
        VaultKeep vaultkeep = _db.Query<VaultKeep>(sql, new { vaultKeepId }).FirstOrDefault();
        return vaultkeep;
    }


    internal List<KeepInVault> GetKeepsInVault(int vaultId)
    {
        string sql = @"
    SELECT 
    k.*,
    vk.*,
    act.*
    FROM keep k
    JOIN vaultKeep vk ON k.id = vk.keepId
    JOIN vault v ON vk.vaultId = v.id
    JOIN accounts act ON act.id =k.CreatorId
    WHERE vk.VaultId=@vaultId;";


        List<KeepInVault> keeps = _db.Query<KeepInVault, VaultKeep, Profile, KeepInVault>(sql, (keep, vaultkeep, profile) =>
        {
            keep.Creator = profile;
            keep.VaultKeepId = vaultkeep.Id;
            return keep;
        }, new { vaultId }).ToList();
        return keeps;
    }

    internal void RemoveKeepFromVault(int vaultKeepId)
    {
        string sql = @" DELETE FROM vaultKeep WHERE id=@vaultKeepId ;";
        _db.Execute(sql, new { vaultKeepId });
    }
}

