namespace keepr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VaultKeepsController : ControllerBase
{
    private readonly VaultKeepsServices _vaultKeepsServices;
    private readonly Auth0Provider _auth;

    public VaultKeepsController(VaultKeepsServices vaultKeepsServices, Auth0Provider auth)
    {
        _vaultKeepsServices = vaultKeepsServices;
        _auth = auth;
    }


    [HttpPost]
    [Authorize]

    public async Task<ActionResult<Vault>> CreateVaultKeep([FromBody] VaultKeep vaultKeepData)
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            vaultKeepData.CreatorId = userInfo.Id;
            VaultKeep newVaultKeep = _vaultKeepsServices.CreateVaultKeep(vaultKeepData);
            return Ok(newVaultKeep);
        }
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }
    }

    // [HttpDelete("{vaultKeepId}")]
    // [Authorize]
    // public async Task<ActionResult<string>> RemoveKeepFromVault(int vaultKeepId)
    // {
    //     Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
    //     _vaultKeepsServices.RemoveKeepFromVault(vaultKeepId, userInfo?.Id);
    //     return Ok("Keep Successfully Removed");

    // }

}
