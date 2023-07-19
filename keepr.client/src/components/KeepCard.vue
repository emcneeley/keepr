<template>
    <div @click="getActiveKeep(keep.id)" class="col-3 card m-2" data-bs-toggle="modal" data-bs-target="#activeKeep">
        <p>{{ keep.name }}</p>
        <img class="img-fluid" :src="keep?.img" :alt="keep?.name">
        <button @click="addToVault()">Add 2 Vault</button>
        <button>delete from vault</button>
        <i midi mdi-eye> {{ keep.views }}</i>
        <router-link :to="{ name: 'Profile', params: { profileId: keep?.creatorId } }">
            <div>
                <img class="rounded-circle" :src="keep.creator.picture" alt="">
            </div>

        </router-link>
        {{ keep.creator.name }}
    </div>

    <!-- <p>{{ recipe.title }}</p> -->
</template>

 
<script>
import { AppState } from '../AppState'
import { Profile } from '../models/Account'
import { Keep } from '../models/Keep'
import { keepService } from '../services/KeepService'
import Pop from '../utils/Pop'
export default {
    props: {
        keep: { type: Keep, required: true },
        profile: { type: Profile, required: true }
    },
    setup() {
        return {
            async getActiveKeep(keepId) {
                try {
                    await keepService.getActiveKeep(keepId)
                } catch (error) {
                    Pop.error(error)
                }
            },
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


<style lang="scss" scoped></style>