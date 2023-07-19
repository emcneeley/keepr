import { AppState } from "../AppState"
import { Vault } from "../models/Vault"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultsService {

    async createVault(vaultData) {
        const res = await api.post('api/vaults', vaultData)
        logger.log('[CREATING VAULT]', res.data)
        AppState.vaults.push(new Vault(res.data))
    }

    async getMyVaults() {
        const res = await api.get('account/vaults')
        console.log(res.data, '[THESE ARE MY VAULTS]')
        AppState.myVaults = res.data
    }

    async getById(vaultId) {
        const res = await api.get(`api/vaults/${vaultId}`)
        logger.log('[GET BY ID]', res.data)
        AppState.activeVault = new Vault(res.data)
    }

    async getvaultsByProfileId(profileId) {
        AppState.vaults = []
        const res = await api.get(`api/profiles/${profileId}/vaults`)
        console.log('[THESE ARE THE PROFILES VAULTS]', res.data)
        AppState.vaults = res.data.map(v => new Vault(v))
    }

}

export const vaultsService = new VaultsService()