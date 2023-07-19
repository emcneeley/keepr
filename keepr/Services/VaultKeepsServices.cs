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

        internal VaultKeep GetById(int vaultKeepId)
        {
            VaultKeep vaultkeep = _vaultKeepRepo.GetById(vaultKeepId);
            if (vaultkeep == null)
            {
                throw new Exception($"This is a bad ID holms, try again: {vaultKeepId}");
            }
            return vaultkeep;
        }

        internal List<KeepInVault> GetKeepsInVault(int vaultId, string userId)
        {
            List<KeepInVault> keeps = _vaultKeepRepo.GetKeepsInVault(vaultId);
            return keeps;

        }

        internal string RemoveKeepFromVault(int vaultKeepId, string userId)
        {
            VaultKeep vaultkeep = GetById(vaultKeepId);
            if (vaultkeep?.CreatorId != userId)
            {
                throw new Exception("THIS AINT YOURS! SCRAM!");
            }

            _vaultKeepRepo.RemoveKeepFromVault(vaultKeepId);
            return $"KEEP HAS BEEN SUCCESSFULLY DELETED";
        }
    }

}