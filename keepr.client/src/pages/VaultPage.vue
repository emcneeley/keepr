<template>
    <div class="container-fluid" v-if="vault" :style="{ backgroundImage: `url(${vault.img})` }">
        <section class="row img-row align-content-center">
            <div class="col-5 text-danger">
                <h1>{{ vault?.name }}</h1>
            </div>
        </section>
        <section class="row">
            <div class="col-md-6">

                <!-- <h2 class="text-danger">Popularity: {{ cult.popularity }}</h2> -->

                <p>{{ vault.description }}</p>
            </div>
            <div class="col-md-6">
                <div>
                    <h2>{{ vault.creator }}</h2>
                    <!-- <img class="leader-img" :src="vault?.Creator.picture" alt=""> -->
                </div>
            </div>
        </section>
    </div>
    <section class="row">
        <KeepCard v-for="k in keep" :keep="k" />

    </section>
</template>


<script>
import { computed, onMounted } from 'vue'
import { useRoute } from 'vue-router'
import { vaultsService } from '../services/VaultsService'
import Pop from '../utils/Pop'
import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { keepService } from '../services/KeepService'
export default {
    setup() {
        const route = useRoute()

        async function getById() {
            try {
                const vaultId = route.params.vaultId
                await vaultsService.getById(vaultId)
            } catch (error) {
                Pop.error(error)
                Pop.toast(error.message, 'error')
            }
        }

        async function getKeepsByVaultId() {

            try {
                const vaultId = route.params.vaultId
                await keepService.getKeepsByVaultId(vaultId)
            } catch (error) {
                logger.error(error)
                Pop.toast(error.message, 'error')
            }
        }

        onMounted(() => {
            getById()
            getKeepsByVaultId()
        })
        return {
            route,
            vault: computed(() => AppState.activeVault),
            keep: computed(() => AppState.keeps),

            async deleteKeepsFromVault() {
                try {
                    if (await Pop.confirm) {

                        await keepService.deleteKeeps()
                    }
                } catch (error) {
                    Pop.error(error)
                }
            }
        }
    }
}
</script>


<style lang="scss" scoped></style>