<template>
  <div>
    <button data-bs-toggle="modal" data-bs-target="#createKeep"> Create Keep</button>
    <button data-bs-toggle="modal" data-bs-target="#createVault"> Create Vault</button>

  </div>

  <div class="row">

    <KeepCard v-for="k in keep" :keep="k" />

  </div>
</template>

<script>
import { Modal } from 'bootstrap'
import Pop from '../utils/Pop'
import { keepService } from '../services/KeepService'
import { computed, onMounted, ref } from 'vue'
import { AppState } from '../AppState'
export default {
  setup() {
    async function getAllKeeps() {
      try {
        await keepService.getAllKeeps()
      } catch (error) {
        Pop.error(error)

      }
    }

    onMounted(() => {

      getAllKeeps()

    }

    )
    return {
      keep: computed(() => AppState.keeps)
    }


  }
}

</script>

<style scoped lang="scss">
.home {
  display: grid;
  height: 80vh;
  place-content: center;
  text-align: center;
  user-select: none;

  .home-card {
    width: 50vw;

    >img {
      height: 200px;
      max-width: 200px;
      width: 100%;
      object-fit: contain;
      object-position: center;
    }
  }
}
</style>
