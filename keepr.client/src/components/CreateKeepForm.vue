<template>
    <div class="modal-content">
        <div class="modal-header text-dark">
            <h1 class="modal-title fs-5" id="staticBackdropLabel">Create Keep</h1>
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
            <form @submit.prevent="createKeep()">
                <div class="modal-body">
                    <div class="mb-3">
                        <label for="Name" class="form-label">Name</label>
                        <input v-model="editable.name" type="text" class="form-control" id="name" placeholder="Name">
                    </div>
                    <div class="mb-3">
                        <label for="img" class="form-label">Img</label>
                        <input v-model="editable.img" type="text" class="form-control" id="img" placeholder=" Image">
                    </div>
                    <textarea v-model="editable.description" class="form-control w-100" name="description" id="desscription"
                        cols="30" rows="10"></textarea>
                </div>

                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                    <button class="btn btn-primary">Create Keep</button>
                </div>
            </form>
        </div>
    </div>
</template>


<script>
import { ref } from 'vue'
import Pop from '../utils/Pop'
import { keepService } from '../services/KeepService'
import { Modal } from 'bootstrap'
export default {
    setup() {
        const editable = ref({})

        return {

            editable,

            async createKeep() {
                try {
                    console.log('hello')
                    const keepData = editable.value
                    await keepService.createKeep(keepData)
                    editable.value = {}
                    Modal.getOrCreateInstance('#createKeep').hide()
                } catch (error) {
                    Pop.toast(error.message, 'error')

                }

            }
        }
    }
}
</script>


<style lang="scss" scoped></style>