<script setup>
import { Recipe } from '@/models/Recipe.js';
import { ingredientsService } from '@/services/IngredientsService.js';

import { recipesService } from '@/services/ReciepesService.js';

const props = defineProps({ recipeProp: { type: Recipe, required: true } })

async function setActive(){
    recipesService.setActiveRecipe(props.recipeProp)
    await ingredientsService.getIngredientsByRecipeId(props.recipeProp.id)
}
</script>


<template>
    <div v-if="recipeProp" @click="setActive()" data-bs-toggle="modal" data-bs-target="#recipeModal"
        class="row flex-column justify-content-between card-bg-img card-height text-light m-2"
        :style="props.recipeProp.backgroundImg"  type="button">
        <div class="col-6 d-flex justify-content-between w-100">
            <p class="bg-transparent-dark rounded py-1 px-3 mt-1">{{ recipeProp.category }}</p>
            <p class="bg-transparent-dark rounded-bottom p-1"><i class="mdi mdi-heart-outline"></i></p>
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