namespace keepr.Services
{
    public class VaultKeepsServices
    {
        private readonly KeepService _keepService;
        private readonly VaultKeepsRepository _vaultKeepRepo;
        private readonly VaultsService _vaultsService;
        private readonly KeepRepository _keepRepository;

        public VaultKeepsServices(VaultKeepsRepository vaultKeepRepo, VaultsService vaults, VaultsService vaultsService, KeepRepository keepRepository)
        {
            _vaultKeepRepo = vaultKeepRepo;
            _vaultsService = vaultsService;
            _keepRepository = keepRepository;
        }

        internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
        {

            Vault vault = _vaultsService.GetById(vaultKeepData.VaultId, vaultKeepData.CreatorId);
            if (vault.CreatorId != vaultKeepData.CreatorId)
            {
                throw new Exception("NO WAY JOSE");
            }

            VaultKeep vaultKeep = _vaultKeepRepo.CreateVaultKeep(vaultKeepData);
            if (vaultKeep.CreatorId != vaultKeepData.CreatorId)
            {
                throw new Exception("THAT AINT YO DOG!");
            }

            return vaultKeep;
            // Keep keep = _keepService.GetById(vaultKeepData.KeepId, vaultKeepData.CreatorId);
            // keep.Kept++;
            // _keepRepository.EditKeep(keep);
            // return keep;



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
            _vaultsService.GetById(vaultId, userId);
            List<KeepInVault> keeps = _vaultKeepRepo.GetKeepsInVault(vaultId);
            return keeps;

        }

        internal string RemoveKeepFromVault(int vaultKeepId, string userId)
        {
            VaultKeep vaultkeep = GetById(vaultKeepId);
            if (vaultkeep.CreatorId != userId)
            {
                throw new Exception("THIS AINT YOURS! SCRAM!");
            }

            _vaultKeepRepo.RemoveKeepFromVault(vaultKeepId);
            return $"KEEP HAS BEEN SUCCESSFULLY DELETED";
        }
    }

}