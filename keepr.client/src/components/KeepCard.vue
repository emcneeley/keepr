<template>
    <div @click="getActiveKeep(keep.id)" class="col-3 card m-2" data-bs-toggle="modal" data-bs-target="#activeKeep">


        <p>{{ keep.name }}</p>
        <img class="img-fluid" :src="keep?.img" :alt="keep?.name">

    </div>
    <!-- <p>{{ recipe.title }}</p> -->
</template>


<script>
import { Keep } from '../models/Keep'
import { keepService } from '../services/KeepService'
import Pop from '../utils/Pop'
export default {
    props: {
        keep: { type: Keep, required: true }
    },
    setup() {
        return {
            async getActiveKeep(keepId) {
                try {
                    await keepService.getActiveKeep(keepId)
                } catch (error) {
                    Pop.error(error)
                }
            }

        }
    }
}
</script>


<style lang="scss" scoped></style>