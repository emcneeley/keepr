<template>
    <div class="modal-content">
        <div class="modal-header">
            <h1 class="modal-title fs-5" id="staticBackdropLabel">KEEP NAME</h1>
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
            <div class="active-keep">
                <p>{{ activeKeep?.name }}</p>
                <p>{{ activeKeep?.description }}</p>
                <img class="img-fluid" :src="activeKeep?.img" alt="">
            </div>
            <div v-if="isKeepCreator">
                <!-- ADD LOGIC TO MAKE SURE YOU DONT GET TO DELTE UNLESS YOURE THE OWNER -->
                <button @click="deleteKeep(activeKeep?.id)">DELETE KEEP</button>
            </div>
        </div>
        <div class="modal-footer">
            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
            <!-- <div v-if="isRecipeOwner">
                    <button @click="openCreateRecipeModal()" class="btn btn-primary">Edit
                        Recipe</button>
                    <button @click="deleteRecipe(activeRecipe.id)" class="btn btn-danger">delete
                        Recipe</button>
                </div> -->
        </div>
    </div>
</template>


<script>
import { computed } from 'vue'
import { AppState } from '../AppState'
import { keepService } from '../services/KeepService'
import { logger } from '../utils/Logger'
export default {
    setup() {
        return {
            // keep: computed(() => AppState.activeKeep),
            activeKeep: computed(() => AppState.activeKeep),
            isKeepCreator: computed(() => {
                if (AppState.activeKeep?.creatorId == AppState.account.id)
                    return true
                else { return false }
            }),

            async deleteKeep(keepId) {
                try {
                    logger.log('REMOVING KEEP', keepId)
                    await keepService.deleteKeep(keepId)
                } catch (error) {
                    logger.error(error)
                    Pop.Toast(error.message, 'error')
                }
            }


        }
    }
}
</script>


<style lang="scss" scoped></style>