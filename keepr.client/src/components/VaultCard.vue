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
                </div>
            </div>


        </router-link>
    </div>
</template>


<script>
import { Vault } from '../models/Vault'
export default {
    props: {
        vault: { type: Vault, required: true }
    },

    setup() {
        return {}
    }
}
</script>


<style lang="scss" scoped></style>