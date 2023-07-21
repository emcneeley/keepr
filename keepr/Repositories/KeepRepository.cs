namespace keepr.Repositories;

public class KeepRepository
{
    private readonly IDbConnection _db;

    public KeepRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Keep CreateKeep(Keep keepData)
    {
        string sql = @"
       INSERT INTO keep
        (Name, Description, Img, CreatorId, Views, Kept)
       VALUES
       (@Name, @Description, @Img, @CreatorId, @Views, @Kept);

       SELECT 
        k.*,
        creator.*
        FROM keep k
        JOIN accounts creator ON creator.id = k.CreatorId
        WHERE k.id =LAST_INSERT_ID()
       ;";

        Keep newKeep = _db.Query<Keep, Account, Keep>(sql, (keep, creator) =>
        {
            keep.Creator = creator;
            return keep;
        }, keepData).FirstOrDefault();
        return newKeep;
    }

    internal int DeleteKeep(int keepId)
    {
        string sql = @"
        DELETE FROM keep 
        WHERE Id= @keepId LIMIT 1
        ;";
        int rows = _db.Execute(sql, new { keepId });
        return rows;
    }

    internal void EditKeep(Keep original)
    {
        string sql = @"
        UPDATE keep SET
        Name=@Name,
        Description=@Description, 
        Img=@Img, 
        Views=@Views,
        Kept=@Kept
        WHERE Id=@Id
        ;";
        _db.Execute(sql, original);
    }

    internal List<Keep> GetAllKeep()
    {
        string sql = @"
    SELECT
    k.*,
    COUNT(vk.Id) AS Kept,
    creator.*
    FROM keep k
    LEFT JOIN vaultKeep vk ON vk.KeepId = k.Id
    JOIN accounts creator ON k.CreatorId = creator.id
    GROUP BY k.id
        ;";
        List<Keep> keep = _db.Query<Keep, Account, Keep>(sql, (keep, profile) =>
        {
            keep.Creator = profile;
            return keep;
        }).ToList();
        return keep;
    }

    internal Keep GetById(int keepId)
    {
        string sql = @"
    SELECT
    k.*,
    COUNT(vk.Id) AS Kept,
    creator.*
    FROM keep k
    LEFT JOIN vaultKeep vk ON vk.KeepId = k.Id
    JOIN accounts creator ON k.CreatorId = creator.id
    WHERE k.Id=@KeepId
    GROUP BY k.id;";

        Keep keep = _db.Query<Keep, Account, Keep>(sql, (keep, creator) =>
        {
            keep.Creator = creator;
            return keep;
        }, new { keepId }).FirstOrDefault();
        return keep;
    }

    internal List<Keep> GetKeepsForProfile(string profileId)
    {
        string sql = @"
       SELECT
       k.*,
       acct.*
       FROM keep k
    JOIN accounts acct ON acct.id = k.CreatorId
    WHERE CreatorId = @profileId
       ; ";

        List<Keep> keeps = _db.Query<Keep, Profile, Keep>
        (sql, (keep, profile) =>
        {
            keep.Creator = profile;
            return keep;
        }, new { profileId }).ToList();

        return keeps;
    }

    // internal List<Keep> GetKeepsInVault(int vaultId)
    // {
    //     string sql=@"
    //     SELECT 
    //     k.*,
    //     act.*
    //     FROM keep k
    //     JOIN accounts act ON act.id =k.CreatorId
    //     WHERE k.VaultId =@VaultId

    //     ;";
    // }
}
