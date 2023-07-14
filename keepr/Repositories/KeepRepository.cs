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
