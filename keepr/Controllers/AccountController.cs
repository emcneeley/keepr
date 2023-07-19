namespace keepr.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly AccountService _accountService;
    private readonly Auth0Provider _auth0Provider;

    private readonly VaultsService _vaultsServices;

    public AccountController(AccountService accountService, Auth0Provider auth0Provider, VaultsService vaultsServices)
    {
        _accountService = accountService;
        _auth0Provider = auth0Provider;
        _vaultsServices = vaultsServices;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<Account>> Get()
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            return Ok(_accountService.GetOrCreateProfile(userInfo));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


    [HttpGet("vaults")]
    public async Task<ActionResult<List<Vault>>> GetMyVaults()
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            List<Vault> vaults = _vaultsServices.GetMyVaults(userInfo?.Id);
            return Ok(vaults);
        }
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    [Authorize]

    public async Task<ActionResult<Account>> EditAccount([FromBody] Account editData)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            editData.Id = userInfo?.Id;
            Account updatedAccount = _accountService.Edit(editData, userInfo.Email);
            return Ok(updatedAccount);


        }
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }



    }

}
