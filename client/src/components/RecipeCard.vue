<script setup>
import { AppState } from '@/AppState.js';
import { Account } from '@/models/Account.js';
import { Favorite } from '@/models/Favorite.js';
import { Recipe } from '@/models/Recipe.js';
import { ingredientsService } from '@/services/IngredientsService.js';

import { recipesService } from '@/services/ReciepesService.js';
import { logger } from '@/utils/Logger.js';
import Pop from '@/utils/Pop.js';
import { Modal } from 'bootstrap';
import { computed, onMounted, ref } from 'vue';

const props = defineProps({ recipeProp: { type: Recipe, required: true }, accountProp: { type: Account, required: true } })

const favorites = computed(() => AppState.favorites)

const isFavorite = computed(() => favorites.value.findIndex(favorite => props.recipeProp.id == favorite.id))


// const cultists = computed(() => AppState.cultists)

// const isCultist = computed(() => cultists.value.some(cultist => account.value?.id == cultist.id))


async function setActive() {
    try {
        recipesService.setActiveRecipe(props.recipeProp)
        await ingredientsService.getIngredientsByRecipeId(props.recipeProp.id)
        Modal.getOrCreateInstance('#recipeModal').show()
    }
    catch (error) {
        Pop.error(error);
    }

}

async function addFavorite() {
    try {
        recipesService.addFavorite(props.recipeProp.id)
    }
    catch (error) {
        Pop.error(error);
    }
}

async function deleteFavorite() {
    try {
        recipesService.deleteFavorite(isFavorite.value)
    }
    catch (error) {
        Pop.error(error);
    }
}
</script>


<template>
    <!-- ask how to make button not double press -->
    <div v-if="recipeProp" @click="setActive()"
        class="row flex-column justify-content-between card-bg-img card-height text-light m-2"
        :style="props.recipeProp.backgroundImg" type="button">
        <div class="col-6 d-flex justify-content-between w-100">
            <p class="bg-transparent-dark rounded py-1 px-3 mt-1">{{ recipeProp.category }}</p>
            <div v-if="accountProp">
            <p v-if="isFavorite == -1" @click.stop="addFavorite()" class="bg-transparent-dark rounded-bottom p-1"><i
                    class="mdi mdi-heart-outline"></i></p>
            <p v-else-if="isFavorite != -1" @click.stop="deleteFavorite()" class="bg-transparent-dark rounded-bottom p-1"><i
                    class="mdi mdi-heart text-danger"></i></p>
            </div>
        </div>
        <div class="col-6 d-flex justify-content-end w-100">
            <p class="bg-transparent-dark rounded p-1 w-100">{{ recipeProp.title }}</p>
        </div>
    </div>
</template>


<style lang="scss" scoped>
.card-height {
    aspect-ratio: 1/1;
    background-position: center;
    background-size: cover;
}

.bg-transparent-dark {
    background-color: rgba(0, 0, 0, 0.454);
}
</style>