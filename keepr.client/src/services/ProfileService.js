import { AppState } from "../AppState"
import { Profile } from "../models/Account"
import { api } from "./AxiosService"

class ProfileService {

    async getProfileById(profileId) {
        const res = await api.get(`/api/profiles/${profileId}`)
        console.log(['HEY LOOK OVER HERE'], res.data)

        AppState.profile = new Profile(res.data)
    }

}

export const profileService = new ProfileService()