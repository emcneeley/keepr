namespace keepr.Services
{
    public class VaultKeepsServices
    {
        private readonly VaultKeepsRepository _vaultKeepRepo;
        private readonly VaultsService _vaultsService;

        public VaultKeepsServices(VaultKeepsRepository vaultKeepRepo, VaultsService vaults)
        {
            _vaultKeepRepo = vaultKeepRepo;
        }

        internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
        {
            VaultKeep vaultKeep = _vaultKeepRepo.CreateVaultKeep(vaultKeepData);
            return vaultKeep;
        }

        internal List<KeepInVault> GetKeepsInVault(int vaultId)
        {
            List<KeepInVault> keeps = _vaultKeepRepo.GetKeepsInVault(vaultId);
            return keeps;

        }

    }

}