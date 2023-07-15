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
        Views=@Views
        WHERE Id=@Id
        ;";
        _db.Execute(sql, original);
    }

    internal List<Keep> GetAllKeep()
    {
        string sql = @"
        SELECT
        k.*,
        creator.*
        FROM keep k
        JOIN accounts creator ON creator.id=k.CreatorId
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
      creator.*
      FROM keep k 
      JOIN accounts creator ON k.CreatorId =creator.id
      WHERE k.id= @keepId     
      ;";

        Keep keep = _db.Query<Keep, Account, Keep>(sql, (keep, creator) =>
        {
            keep.Creator = creator;
            return keep;
        }, new { keepId }).FirstOrDefault();
        return keep;
    }
}
