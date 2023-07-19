namespace keepr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfilesController : ControllerBase
{
    private readonly AccountService _accountService;
    private readonly ProfileService _profileService;

    private readonly Auth0Provider _auth;
    private readonly KeepService _keepService;

    private readonly VaultsService _vaultsService;

    public ProfilesController(AccountService accountService, ProfileService profileService, Auth0Provider auth, KeepService keepService, VaultsService vaultsService)
    {
        _accountService = accountService;
        _profileService = profileService;
        _auth = auth;
        _keepService = keepService;
        _vaultsService = vaultsService;
    }



    [HttpGet("{profileId}")]

    public async Task<ActionResult<Profile>> GetProfileById(string profileId)
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            Profile profile = _profileService.GetProfileById(profileId, userInfo?.Id);
            return profile;
        }
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }
    }


    [HttpGet("{profileId}/keeps")]
    public async Task<ActionResult<List<Keep>>> GetKeepsForProfile(string profileId)
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            List<Keep> keeps = _keepService.GetKeepsForProfile(profileId, userInfo?.Id);
            return Ok(keeps);
        }
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }
    }


    [HttpGet("{profileId}/vaults")]
    public async Task<ActionResult<List<Vault>>> GetVaultsForProfile(string profileId)
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            List<Vault> vaults = _vaultsService.GetVaultsForProfile(profileId, userInfo?.Id);
            return Ok(vaults);
        }
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }
    }

}
