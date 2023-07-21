<template>
  <div class="card elevation-4 profile-design m-5">

    <div class="card-body d-flex justify-content-around">
      <img :src="account?.coverImg" alt="">
      <img class="img-shit" :src="account?.picture" alt="">
      <div>
        <div class="container-fluid ">
          <p><b>NAME:</b> {{ account?.name }}</p>

        </div>
      </div>

    </div>
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

    <div>

      THESE ARE KEEPS
      <div v-for="k in keeps" :key="k.id" class="col-12 col-md-6 p-5">
        <KeepCard :keep="k" />
      </div>
    </div>




  </div>
</template>

<script>
import { computed, onMounted } from 'vue';
import { AppState } from '../AppState';
import Pop from '../utils/Pop';
import { vaultsService } from '../services/VaultsService';
import { keepService } from '../services/KeepService';
import { logger } from '../utils/Logger';
export default {

  setup() {

    async function getMyVaults() {
      try {
        await vaultsService.getMyVaults()
      } catch (error) {
        Pop.error(error)
      }
    }

    async function getAccountKeeps() {
      try {
        const accountId = AppState.user.id
        await keepService.getAccountKeeps(accountId)
      } catch (error) {
        Pop.error(error)
      }
    }

    // NOTE USE YOUR GET KEEPS BY PROFILE
    onMounted(() => getMyVaults())
    onMounted(() => getAccountKeeps())

    return {
      account: computed(() => AppState.account),
      vaults: computed(() => AppState.myVaults),
      keeps: computed(() => AppState.keeps)
    }
  }
}
</script>

<style scoped></style>
