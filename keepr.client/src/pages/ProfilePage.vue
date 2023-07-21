<template>
  <div class="profile">
    <h1>{{ profile?.name }}</h1>
    <p>THIS MANY VAULTS: {{ vaults.length }}</p>
  </div>

  <div v-for="v in vaults" :key="v.id" class="col-12 col-md-6 p-5">
    <VaultCard :vault="v" />

  </div>
</template>

<script>
import { computed, onMounted } from 'vue'
import { useRoute } from 'vue-router'
import { profileService } from '../services/ProfileService'
import { AppState } from '../AppState'
import Pop from '../utils/Pop'
import { vaultsService } from '../services/VaultsService'
export default {
  setup() {
    const route = useRoute()

    async function getProfileById() {
      try {
        await profileService.getProfileById(route.params.profileId)
      } catch (error) {
        Pop.error(error, ['Getting Profile'])
      }
    }

    async function getVaultsByProfile() {
      try {
        await vaultsService.getvaultsByProfileId(route.params.profileId)
      } catch (error) {
        Pop.error(error)
      }
    }

    // NOTE GO GET YOUR KEEPS BY PROFILE - THEN SAVE IT IN YOUR APPSTATE, COMPUTE THEM, THEN PUT THEM IN YOUR HTML
    onMounted(() =>
      getProfileById())
    getVaultsByProfile()
    return {
      profile: computed(() => AppState.profile),
      vaults: computed(() => AppState.vaults)

    }
  }
}
</script>
