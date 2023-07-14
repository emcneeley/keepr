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
        logger.log(['GETTING ALL KEEPS'], res.data)
    }

}

export const keepService = new KeepService()