<template>
    <div>
        <router-link :to="{ name: 'Vault', params: { vaultId: vault.id } }">

            <div class="row selectable elevation-5 p-3">
                <div class="col-4">
                    <img :src="vault?.img" class="img-fluid" alt="">
                </div>
                <div class="col-8 text-dark">
                    <div class="d-flex justify-content-between mb-5 align-items-center">
                        <h2>{{ vault?.name }}</h2>
                        <!-- <img :src="vault.img" alt=""> -->
                    </div>
                    <p>
                        {{ vault.description }}
                    </p>

                    <button v-if="vault.creatorId == account.id" @click="deleteVault(vaultId)">Delete</button>
                </div>
            </div>


        </router-link>
    </div>
</template>


<script>
import { computed } from 'vue'
import { AppState } from '../AppState'
import { Account } from '../models/Account'
import { Vault } from '../models/Vault'
import { vaultsService } from '../services/VaultsService'
import Pop from '../utils/Pop'
export default {
    props: {
        vault: { type: Vault, required: true }
        // account: { type: Account, required: true }
    },

    setup(props) {
        return {
            account: computed(() => AppState.account),
            async deleteVault() {
                try {
                    if (await Pop.confirm) {
                        await vaultsService.deleteVault(props.vault?.id)
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