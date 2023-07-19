<template>
  <div class="about text-center">
    <h1>Welcome {{ account.name }}</h1>
    <img class="rounded" :src="account.picture" alt="" />
    <p>{{ account.email }}</p>
  </div>


  <div>
    <EditAccountForm />
  </div>


  <div>
    <h2>
      YOUR VAULTS
    </h2>

    <div v-for="v in vaults" :key="v.id" class="col-12 col-md-6 p-5">
      <VaultCard :vault="v" />
    </div>

  </div>
</template>

<script>
import { computed, onMounted } from 'vue';
import { AppState } from '../AppState';
import Pop from '../utils/Pop';
import { vaultsService } from '../services/VaultsService';
export default {

  setup() {

    async function getMyVaults() {
      try {
        await vaultsService.getMyVaults()
      } catch (error) {
        Pop.error(error)
      }
    }
    onMounted(() => getMyVaults())
    return {
      account: computed(() => AppState.account),
      vaults: computed(() => AppState.myVaults)
    }
  }
}
</script>

<style scoped>
img {
  max-width: 100px;
}
</style>
