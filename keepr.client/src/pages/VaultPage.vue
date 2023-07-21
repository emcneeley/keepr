<template>
    <div class="container-fluid" v-if="vault" :style="{ backgroundImage: `url(${vault.img})` }">
        <section class="row img-row align-content-center">
            <div class="col-5 text-danger">
                <h1>{{ vault?.name }}</h1>
                <p>Kepts:{{ keep?.kept }}</p>
                <p></p>
            </div>
        </section>
        <section class="row">
            <div class="col-md-6">

                <p>{{ vault.description }}</p>
            </div>
            <div class="col-md-6">
                <div>
                    <h2>{{ vault.creator.name }}</h2>
                    <img class="creator-img" :src="vault.creator.picture" alt="">
                </div>
            </div>
        </section>
    </div>
    <section class="row">
        <KeepCard v-for="vk in keep" :key="vk.id" :keep="vk" />

    </section>
</template>


<script>
import { computed, onMounted, watchEffect } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { vaultsService } from '../services/VaultsService'
import Pop from '../utils/Pop'
import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { keepService } from '../services/KeepService'
export default {
    setup() {
        const route = useRoute()
        const router = useRouter()

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
                router.push({ name: 'Home' })
            }
        }

        watchEffect(() => {
            route.params.vaultId;
            getById();
            getKeepsByVaultId();
        });

        // onMounted(() => {
        //     getById()
        //     getKeepsByVaultId()
        // })
        return {
            route,
            vault: computed(() => AppState.activeVault),
            keep: computed(() => AppState.keeps),
            vaultkeep: computed(() => AppState.vaultKeeps),

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