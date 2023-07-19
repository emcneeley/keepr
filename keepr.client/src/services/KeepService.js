import { AppState } from "../AppState"
import { Keep } from "../models/Keep"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService"

class KeepService {
    async createKeep(keepData) {
        const res = await api.post('api/keeps', keepData)
        console.log(['CREATING KEEP'], res.data)
        AppState.keeps.unshift(new Keep(res.data))
    }

    async getAllKeeps() {
        const res = await api.get('api/keeps')
        // logger.log(['GETTING ALL KEEPS'], res.data)
        AppState.keeps = res.data.map(k => new Keep(k))


    }

    async getActiveKeep(keepId) {
        const res = await api.get(`api/keeps/${keepId}`)
        AppState.activeKeep = res.data
        console.log('IM ACTIVE', res.data)
    }

    async deleteKeep(keepId) {
        const res = await api.delete(`api/keeps/${keepId}`)
        logger.log('[DELETING KEEP]', res.data)
    }

    async getKeepsByVaultId(vaultId) {
        const res = await api.get(`api/vaults/${vaultId}/keeps`)
        logger.log('[GETTING KEEPS IN VAULT', res.data)
        AppState.keeps = res.data.map(k => new Keep(k))
    }
}

export const keepService = new KeepService()