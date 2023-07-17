namespace keepr.Services
{
    public class VaultsService
    {
        private readonly VaultsRepository _vaultrepo;

        public VaultsService(VaultsRepository vaultrepo)
        {
            _vaultrepo = vaultrepo;
        }

        internal Vault CreateVault(Vault vaultData)
        {
            Vault vault = _vaultrepo.CreateVault(vaultData);
            return vault;
        }

        internal void DeleteVault(int vaultId, string userId)
        {
            Vault vault = GetById(vaultId);
            if (vault.CreatorId != userId) new Exception("I dont think so bud. Mosey on out of here.");
            int rows = _vaultrepo.DeleteVault(vaultId);
            if (rows > 1) new Exception("What the in the Sam Hill?");
        }

        internal Vault EditVault(Vault updateData)
        {
            Vault original = GetById(updateData.Id);
            original.Name = updateData.Name != null ? updateData.Name : original.Name;
            original.Description = updateData.Description != null ? updateData.Description : original.Description;
            original.Img = updateData.Img != null ? updateData.Img : original.Img;
            original.IsPrivate = updateData.IsPrivate != null ? updateData.IsPrivate : original.IsPrivate;
            _vaultrepo.EditVault(original);
            return original;
        }

        internal Vault GetById(int vaultId)
        {
            Vault vault = _vaultrepo.GetById(vaultId);
            if (vault == null) throw new Exception($"No vault by this Id: {vaultId}");
            return vault;
        }

        internal List<Vault> GetMyVaults(string accountId)
        {
            List<Vault> vaults = _vaultrepo.GetMyVaults(accountId);
            return vaults;
        }

        internal List<Vault> GetVaultsForProfile(string profileId, string id)
        {
            List<Vault> vaults = _vaultrepo.GetVaultsForProfile(profileId);
            return vaults;
        }
    }




}