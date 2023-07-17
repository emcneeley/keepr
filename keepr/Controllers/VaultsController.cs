namespace keepr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VaultsController : ControllerBase
{
    private readonly VaultKeepsServices _vaultKeepsServices;
    private readonly VaultsService _vaultService;
    private readonly Auth0Provider _auth;
    private readonly KeepService _keepsService;

    public VaultsController(VaultsService vaultService, Auth0Provider auth, KeepService keepService, VaultKeepsServices vaultsKeepServices)
    {
        _vaultService = vaultService;
        _auth = auth;
        _keepsService = keepService;
        _vaultKeepsServices = vaultsKeepServices;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Vault>> CreateVault([FromBody] Vault vaultData)
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            vaultData.CreatorId = userInfo.Id;

            Vault vault = _vaultService.CreateVault(vaultData);
            return Ok(vault);
        }
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }
    }

    [HttpGet("{vaultId}")]
    public async Task<ActionResult<Vault>> GetById(int vaultId)
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            Vault vault = _vaultService.GetById(vaultId);
            return Ok(vault);
        }
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }
    }

    [HttpPut("{vaultId}")]
    [Authorize]

    public ActionResult<Vault> EditVault(int vaultId, [FromBody] Vault updateData)
    {
        try
        {
            updateData.Id = vaultId;
            Vault updatedVault = _vaultService.EditVault(updateData);
            return Ok(updatedVault);
        }
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{vaultId}")]
    [Authorize]
    public async Task<ActionResult<string>> DeleteVault(int vaultId)
    {
        Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
        _vaultService.DeleteVault(vaultId, userInfo.Id);
        return Ok("This Vault has been DeLorTed");
    }

    [HttpGet("{vaultId}/keeps")]

    public async Task<ActionResult<List<KeepInVault>>> GetKeepsInVault(int vaultId)
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            List<KeepInVault> keeps = _vaultKeepsServices.GetKeepsInVault(vaultId);
            return Ok(keeps);
        }
        catch (Exception e)
        {

            return (BadRequest(e.Message));
        }
    }












}
