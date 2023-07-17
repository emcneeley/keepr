<template>
    <div class="modal-content">
        <div class="modal-header">
            <h1 class="modal-title fs-5" id="staticBackdropLabel">Create Vault</h1>
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
            <form @submit.prevent="createVault()">
                <div class="modal-body">
                    <div class="mb-3">
                        <label for="Name" class="form-label">Name</label>
                        <input v-model="editable.name" type="text" class="form-control" id="name" placeholder="Name">
                    </div>
                    <div class="mb-3">
                        <label for="img" class="form-label">Img</label>
                        <input v-model="editable.img" type="text" class="form-control" id="img" placeholder=" Image">
                    </div>
                    <div class="mb-3">
                        <label for="img" class="form-label">Is Private?</label>
                        <input v-model="editable.isPrivate" type="checkbox" class="form-control" id="img"
                            placeholder=" Image">
                    </div>
                    <textarea v-model="editable.description" class="form-control w-100" name="description" id="desscription"
                        cols="30" rows="10"></textarea>
                </div>

                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                    <button class="btn btn-primary">Create Vault</button>
                </div>
            </form>
        </div>
    </div>
</template>


<script>
import { ref } from 'vue'
import Pop from '../utils/Pop'
import { vaultsService } from '../services/VaultsService'
export default {
    setup() {
        const editable = ref({})
        return {
            editable,

            async createVault() {
                try {
                    console.log('hello im a create')
                    const vaultData = editable.value
                    await vaultsService.createVault(vaultData)
                    editable.value = {}
                } catch (error) {
                    Pop.toast(error.message, 'error')
                }
            }


        }
    }
}
</script>


<style lang="scss" scoped></style>