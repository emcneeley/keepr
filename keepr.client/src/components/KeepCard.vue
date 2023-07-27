<template>
    <div @click="getActiveKeep(keep)" class="keep-card col-md-3 card m-2 " data-bs-toggle="modal"
        data-bs-target="#activeKeep">
        <p class="text-dark"> {{ keep.name }}</p>
        <img class="img-fluid" :src="keep?.img" :alt="keep?.name">

        <i mdi mdi-eye-plus> {{ keep.views }}</i>
        <!-- <i mdi mdi-file>{{ keep.kept }}</i> -->
        <router-link :to="{ name: 'Profile', params: { profileId: keep?.creatorId } }">
            <div>
                <img class="rounded-circle img-fluid" :src="keep.creator.picture" alt="">
            </div>
        </router-link>
        {{ keep.creator.name }}
        <div>
            <button v-if="keep.vaultKeepId" @click="deleteKeepFromVault(keep.vaultKeepId)">delete from vault</button>
        </div>
    </div>
</template>

 
<script>
import { AppState } from '../AppState'
import { Profile } from '../models/Account'
import { Keep } from '../models/Keep'
import { keepService } from '../services/KeepService'
import { vaultKeepService } from '../services/VaultKeepService'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
export default {
    props: {
        keep: { type: Keep, required: true }
        // profile: { type: Profile, required: true }
    },
    setup() {
        return {
            async getActiveKeep(keep) {
                try {

                    await keepService.getActiveKeep(keep)
                } catch (error) {
                    Pop.error(error)
                }
            },
            async deleteKeepFromVault(vaultKeepId) {
                try {
                    await vaultKeepService.deleteKeepFromVault(vaultKeepId)
                    logger.log("REMOVING KEEP FROM VAULT", vaultKeepId)
                } catch (error) {
                    logger.error(error)
                    Pop.error(error)
                }
            }
            // ANCHOR YOU NEED TO FINSIH THIS LOGIC
            // async addToVault(){
            //     try {
            //         const vaultId = AppState.vaults.id
            //         await 
            //     } catch (error) {
            //         Pop.error(error)
            //     }
            // }

        }
    }
}
</script>


<style lang="scss" scoped>
.keep-card {
    background: black;
    color: white;


}

.img {}
</style>