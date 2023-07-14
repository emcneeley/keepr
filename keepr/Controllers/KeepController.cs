namespace keepr.Controllers;

[ApiController]
[Route("api/keeps")]
public class KeepController : ControllerBase
{
    private readonly KeepService _keepService;
    private readonly Auth0Provider _auth;

    public KeepController(KeepService keepService, Auth0Provider auth)
    {
        _keepService = keepService;
        _auth = auth;
    }


    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Keep>> CreateKeep([FromBody] Keep keepData)
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            keepData.CreatorId = userInfo.Id;
            Keep newKeep = _keepService.CreateKeep(keepData);
            return Ok(newKeep);
        }
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    public ActionResult<List<Keep>> GetAllKeep()
    {
        try
        {
            List<Keep> keeps = _keepService.GetAllKeep();
            return Ok(keeps);

        }
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }
    }

    [HttpGet("{keepId}")]
    public ActionResult<Keep> GetKeepById(int keepId)
    {
        try
        {
            Keep keep = _keepService.GetById(keepId);
            return Ok(keep);
        }
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }
    }

}
