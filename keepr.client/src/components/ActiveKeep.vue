<template>
    <div class="modal-content text-black">
        <div class="modal-header">
            <h3 class="modal-title fs-5" id="staticBackdropLabel">{{ activeKeep?.name }}</h3>
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body container">
            <div class="active-keep">
                <p>{{ activeKeep?.description }}</p>
                <img class="img-fluid" :src="activeKeep?.img" alt="">
                <p>VIEWS:{{ activeKeep?.views }}</p>
                <p>KEPT:{{ activeKeep?.kept }}</p>

            </div>
            <div v-if="isKeepCreator">
                <button @click="deleteKeep(activeKeep.id)">DELETE Keep</button>
            </div>

        </div>
        <form class="report-form" @submit.prevent="handleSubmit">

            <div class="form-group mb-3">
                <label for="restaurant">Select Vault</label>
                <select name="vault" class="form-control" v-model="editable.vaultId">
                    <option disabled selected value="">Please Select a Vault</option>
                    <option v-for="v in myVaults" :key="v.id" :value="v.id">{{ v.name }}</option>
                </select>
            </div>
        </form>
        <div class="modal-footer">
            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
            <button @click="handleSubmit()" type="button" class="btn btn-secondary" data-bs-dismiss="modal">Add to
                vault</button>



        </div>
    </div>
</template>


<script>
import { computed, onMounted, ref } from 'vue'
import { AppState } from '../AppState'
import { keepService } from '../services/KeepService'
import { vaultKeepService } from '../services/VaultKeepService'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'

const editable = ref({ vaultId: '' })

export default {
    setup() {



        return {
            editable,
            keep: computed(() => AppState.keeps),
            myVaults: computed(() => AppState.myVaults),
            account: computed(() => AppState.account),
            activeKeep: computed(() => AppState.activeKeep),
            isKeepCreator: computed(() => {
                if (AppState.activeKeep?.creatorId == AppState.account.id)
                    return true;
                else {
                    return false;
                }
            }),
            async deleteKeep(keep) {
                try {
                    logger.log("REMOVING KEEP", keep);
                    await keepService.deleteKeep(keep);
                }
                catch (error) {
                    logger.error(error);
                    Pop.error(error);
                }
            },

            async addKeepToVault(vaultId) {
                try {
                    logger.log("adding to vault", vaultId)
                } catch (error) {
                    logger.error(error)
                    Pop.Toast(error.message, "error")
                }
            },
            async handleSubmit() {
                try {
                    editable.value.keepId = AppState.activeKeep.id
                    await vaultKeepService.createVaultKeep(editable.value)
                    editable.value = { vaultId: '' }
                } catch (error) {
                    Pop.error(error, '[CREATING VAULT KEEP]')
                }
            }
        };
    }
}
</script>


<style lang="scss" scoped></style>