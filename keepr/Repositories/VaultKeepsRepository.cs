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
    // TODO I NEED TO GET THIS INSANE THING DONE!!!!!!!!!!!!!!!!
    internal List<KeepInVault> GetKeepsInVault(int vaultId)
    {
        string sql = @"
        SELECT 
        vk.*,
        act.*,
        k.*
        FROM vaultKeep vk
        JOIN accounts act ON act.Id=vk.Id
        JOIN keep k ON k.id = vk.keepId
        WHERE vk.KeepId =@VaultId
        ;";

        List<KeepInVault> keeps = _db.Query<VaultKeep, KeepInVault, KeepInVault>(sql, (vaultkeeps, keep) =>
        {
            keep.VaultKeepId = vaultkeeps.Id;
            return keep;
        }, new { vaultId }).ToList();
        return keeps;

    }

}

