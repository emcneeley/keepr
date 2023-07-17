namespace keepr.Repositories;

public class VaultsRepository
{
    private readonly IDbConnection _db;

    public VaultsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Vault CreateVault(Vault vaultData)
    {
        string sql = @"
     INSERT INTO vault 
     (Name, Description, Img, CreatorId,IsPrivate)
     VALUES
     (@Name, @Description, @Img,@CreatorId, @IsPrivate);
     SELECT
     v.*,
     creator.*
     FROM vault v 
     JOIN accounts creator ON v.CreatorId =creator.id
     WHERE v.Id= LAST_INSERT_ID()
     ;";

        Vault vault = _db.Query<Vault, Account, Vault>(sql, (vault, creator) =>
        {
            vault.Creator = creator;
            return vault;
        }, vaultData).FirstOrDefault();
        return vault;

    }

    internal int DeleteVault(int vaultId)
    {
        string sql = @"
        DELETE FROM vault
        WHERE Id= @vaultId LIMIT 1
        ;";
        int rows = _db.Execute(sql, new { vaultId });
        return rows;
    }

    internal void EditVault(Vault original)
    {
        string sql = @"
        UPDATE vault SET
        Name=@Name,
        Description=@Description, 
        Img=@Img,
        IsPrivate=@IsPrivate
        WHERE Id=@Id
        ;";
        _db.Execute(sql, original);
    }

    internal Vault GetById(int vaultId)
    {
        string sql = @"
       SELECT 
       v.*,
        creator.*
        FROM vault v 
        JOIN accounts creator ON v.CreatorId =creator.id
        WHERE v.Id=@vaultID

       ;";

        Vault vault = _db.Query<Vault, Account, Vault>(sql, (vault, creator) =>
        {
            vault.Creator = creator;
            return vault;
        }, new { vaultId }).FirstOrDefault();
        return vault;
    }

    internal List<Vault> GetMyVaults(string accountId)
    {
        string sql = @"
       SELECT 
       v.*,
       acct.*
       FROM vault v
       JOIN accounts acct ON acct.id =v.CreatorId
       WHERE v.CreatorId=@accountId
       ;";

        List<Vault> vaults = _db.Query<Vault, Account, Vault>
        (sql, (vault, account) =>
        {
            vault.Creator = account;
            return vault;
        }, new { accountId }).ToList();

        return vaults;
    }

    internal List<Vault> GetVaultsForProfile(string profileId)
    {

        string sql = @"
       SELECT 
       v.*,
       acct.*
       FROM vault v
       JOIN accounts acct ON acct.id =v.CreatorId
       WHERE v.CreatorId=@profileId
       ;";

        List<Vault> vaults = _db.Query<Vault, Profile, Vault>
        (sql, (vault, profile) =>
        {
            vault.Creator = profile;
            return vault;
        }, new { profileId }).ToList();

        return vaults;
    }
}
