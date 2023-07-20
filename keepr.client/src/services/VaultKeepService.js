import { AppState } from "../AppState"
import { api } from "./AxiosService"

class VaultKeepService {

    async createVaultKeep(vaultData) {
        const res = await api.post('api/vaultkeeps', vaultData);
        console.log('ADDING TO VAULT', res.data)
        AppState.vaultKeeps.push(res.data)

    }

    async deleteKeepFromVault(vaultKeepId) {
        const res = await api.delete(`api/vaultkeeps/${vaultKeepId}`)
        console.log(['I JUST DELETED THE KEEP'])
    }



}

export const vaultKeepService = new VaultKeepService()