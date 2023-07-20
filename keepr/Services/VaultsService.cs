namespace keepr.Services
{
    public class VaultsService
    {
        private readonly VaultsRepository _vaultrepo;
        private readonly ProfileService _profileService;

        public VaultsService(VaultsRepository vaultrepo, ProfileService profileService)
        {
            _vaultrepo = vaultrepo;
            _profileService = profileService;
        }

        internal Vault CreateVault(Vault vaultData)
        {
            Vault vault = _vaultrepo.CreateVault(vaultData);
            return vault;
        }

        internal void DeleteVault(int vaultId, string userId)
        {
            Vault vault = GetById(vaultId, userId);
            if (vault.CreatorId != userId) throw new Exception("I dont think so bud. Mosey on out of here.");
            int rows = _vaultrepo.DeleteVault(vaultId);
            if (rows > 1) throw new Exception("What the in the Sam Hill?");
        }

        internal Vault EditVault(Vault updateData)
        {
            Vault original = GetById(updateData.Id, updateData.CreatorId);
            if (original.CreatorId != updateData.CreatorId)
            {
                throw new Exception("THAT AINT YO VAULT DOG!");
            }
            original.Name = updateData.Name != null ? updateData.Name : original.Name;
            original.Description = updateData.Description != null ? updateData.Description : original.Description;
            original.Img = updateData.Img != null ? updateData.Img : original.Img;
            original.IsPrivate = updateData.IsPrivate != null ? updateData.IsPrivate : original.IsPrivate;
            _vaultrepo.EditVault(original);
            return original;
        }

        internal Vault GetById(int vaultId, string userId)
        {
            Vault vault = _vaultrepo.GetById(vaultId);
            if (vault == null) throw new Exception($"No vault by this Id: {vaultId}");
            if (vault.IsPrivate == true && userId != vault.CreatorId)
            {
                throw new Exception($"SOMETHING WENT REALLY REALLY WRONG");
            }
            return vault;
        }

        internal List<Vault> GetMyVaults(string accountId)
        {
            List<Vault> vaults = _vaultrepo.GetMyVaults(accountId);
            return vaults;
        }

        internal List<Vault> GetVaultsForProfile(string profileId, string userId)
        {

            _profileService.GetProfileById(profileId, userId);
            List<Vault> vaults = _vaultrepo.GetVaultsForProfile(profileId);

            List<Vault> filteredvaults = vaults.FindAll(vault => vault.IsPrivate == false || vault.CreatorId == userId);
            return filteredvaults;




        }
    }




}