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

    [HttpPut("{keepId}")]
    [Authorize]
    public ActionResult<Keep> EditKeep(int keepId, [FromBody] Keep updateData)
    {
        try
        {
            // Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            // updateData.CreatorId = userInfo.Id;
            updateData.Id = keepId;
            Keep updatedKeep = _keepService.EditKeep(updateData);
            return Ok(updatedKeep);
        }
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{keepId}")]
    [Authorize]

    public async Task<ActionResult<string>> DeleteKeep(int keepId)
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            _keepService.DeleteKeep(keepId, userInfo.Id);
            return Ok("This Keep has been DelOrTed");
        }
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }
    }

}
