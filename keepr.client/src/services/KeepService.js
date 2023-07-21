import { AppState } from "../AppState"
import { Keep, VaultKeep } from "../models/Keep"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService"
import { vaultKeepService } from "./VaultKeepService"

class KeepService {
    async createKeep(keepData) {
        const res = await api.post('api/keeps', keepData)
        console.log(['CREATING KEEP'], res.data)
        AppState.keeps.unshift(new Keep(res.data))
    }

    async getAllKeeps() {
        const res = await api.get('api/keeps')
        logger.log(['GETTING ALL KEEPS'], res.data)
        AppState.keeps = res.data.map(k => new Keep(k))


    }

    async getActiveKeep(keep) {

        if (keep.vaultKeepId) {
            AppState.activeKeep = keep;
        }
        else {
            const res = await api.get(`api/keeps/${keep.id}`)
            AppState.activeKeep = res.data
            console.log('IM ACTIVE', res.data)

        }
        // TODO is this a vault keep? check if the keep has a vaultKeepId
        // TODO if it is a vault keep..... just save the keep in the AppState.activeKeep instead of going to the server
    }

    async deleteKeep(keepId) {
        const res = await api.delete(`api/keeps/${keepId}`)
        logger.log('[DELETING KEEP]', res.data)
    }

    async getKeepsByVaultId(vaultId) {
        const res = await api.get(`api/vaults/${vaultId}/keeps`)
        logger.log('[GETTING KEEPS IN VAULT', res.data)
        // TODO map into vault keep view model, not regular keep
        AppState.keeps = res.data.map(k => new VaultKeep(k))
    }



    async getKeepsByProfileId(profileId) {
        const res = await api.get(`api/profiles/${profileId}/keeps`)
        logger.log('[GETTING KEEPS BY PROFILE]', res.data)
        AppState.keeps = res.data.map(k => new Keep(k))
    }

    async getAccountKeeps(accountId) {

        const res = await api.get(`api/profiles/${accountId}/keeps`)
        logger.log(`[GETTING KEEPS FOR ACCOUNT]`, res.data)
        AppState.keeps = res.data.map(k => new Keep(k))
    }

}
export const keepService = new KeepService()