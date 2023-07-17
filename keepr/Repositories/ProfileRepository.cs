namespace keepr.Repositories;

public class ProfileRepository
{
    private readonly IDbConnection _db;

    public ProfileRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Profile GetProfileById(string id)
    {
        string sql = @"
           SELECT 
           *
           FROM accounts WHERE accounts.id=@id;";

        return _db.QueryFirstOrDefault<Profile>(sql, new { id });
    }
}
