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



}

export const vaultsService = new VaultsService()