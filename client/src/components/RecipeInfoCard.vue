<script setup>
import { AppState } from '@/AppState.js';
import { Account } from '@/models/Account.js';
import { ingredientsService } from '@/services/IngredientsService.js';
import { recipesService } from '@/services/ReciepesService.js';
import { logger } from '@/utils/Logger.js';
import Pop from '@/utils/Pop.js';
import { computed, ref, watch } from 'vue';

const props = defineProps({
    accountProp: { type: Account, required: true }
})

const activeRecipe = computed(() => {
    return (AppState.activeRecipe)
})

const ingredients = computed(() => AppState.ingredients)

const instructions = ref('')

watch(activeRecipe, () => instructions.value = activeRecipe.value.instructions)

async function editRecipe() {
    const newRecipe = await recipesService.editRecipeInstruction(instructions)
    logger.log(newRecipe)
    Pop.success("edited")
}

async function makeIngredient() {
    ingredientsService.createIngredient(ingredientData.value)
}

const ingredientData = ref({
    quantity: 0,
    name: ''
})

let edit = ref(false)
</script>


<template>
    <div class="modal fade" id="recipeModal" tabindex="-1" aria-labelledby="recipeModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-xl">
            <div v-if="edit">
                <div class="modal-content" v-if="activeRecipe">
                    <div v-if="ingredients">
                        <div class="modal-header">
                            <h1 class="modal-title fs-5" id="recipeModalLabel">Recipe Info</h1>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body p-0 m-0">
                            <div class="container-fluid">
                                <div class="row">
                                    <div class="col-6 p-0">
                                        <img class="img-fluid heightset rounded" :src="activeRecipe?.img" alt="">
                                    </div>
                                    <form @submit.prevent="editRecipe()"
                                        class="col-6 d-flex flex-column justify-content-between">
                                        <div class="d-flex align-items-center">
                                            <h2 class="text-primary pe-4">{{ activeRecipe.title }}</h2>
                                            <i @click="edit = false" type="button"
                                                class="mdi mdi-dots-horizontal fs-1 "></i>
                                        </div>
                                        <p>by: {{ activeRecipe.creator.name }}</p>
                                        <div>
                                            <span class="transparent-bg rounded p-1">{{ activeRecipe.category }}</span>
                                        </div>
                                        <h4 class="mt-3">Ingredients:</h4>
                                        <div v-for="ingredient in ingredients" :key="ingredient.id" class="d-flex">
                                            <p class="mb-1"><span type="button" @click="console.log('click')"
                                                    class="text-danger fw-bolder">X</span> {{
                                                        ingredient.quantity }} {{ ingredient.name }}</p>
                                        </div>
                                        <div class="">
                                            <form @submit.prevent="makeIngredient()" class="input-group">
                                                <input v-model="ingredientData.quantity" type="text"
                                                    aria-label="quantity" class="form-control" placeholder="quantity">
                                                <input v-model="ingredientData.name" type="text" aria-label="ingredient"
                                                    class="form-control w-50" placeholder="new ingredient text">
                                                <button class="btn btn-outline-secondary" type="submit">Add</button>
                                            </form>
                                        </div>
                                        <div class="mt-3 d-flex flex-column">
                                            <h5>Instructions:</h5>
                                            <textarea v-model="instructions" class="form-control"
                                                id="instructionsTextArea" rows="6" style="resize: none;"></textarea>
                                        </div>
                                        <button type="submit"
                                            class="btn btn-success w-25 align-self-end mb-2">Save</button>
                                    </form>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div v-else>
                <div class="modal-content" v-if="activeRecipe">
                    <div v-if="ingredients">
                        <div class="modal-header">
                            <h1 class="modal-title fs-5" id="recipeModalLabel">Recipe Info</h1>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body p-0 m-0">
                            <div class="container-fluid">
                                <div class="row">
                                    <div class="col-6 p-0">
                                        <img class="img-fluid heightset rounded" :src="activeRecipe?.img" alt="">
                                    </div>
                                    <div class="col-6 d-flex flex-column justify-content-between mb-5">
                                        <div class="d-flex align-items-center">
                                            <h2 class="text-primary pe-4">{{ activeRecipe.title }}</h2>
                                            <i v-if="accountProp?.id == activeRecipe?.creatorId" @click="edit = true"
                                                type="button" class="mdi mdi-dots-horizontal fs-1 "></i>
                                        </div>
                                        <p>by: {{ activeRecipe.creator.name }}</p>
                                        <div>
                                            <span class="transparent-bg rounded p-1">{{ activeRecipe.category }}</span>
                                        </div>
                                        <h4 class="mt-3">Ingredients:</h4>

                                        <div v-for="ingredient in ingredients" :key="ingredient.id" class="d-flex">
                                            <p class="mb-1">• {{ ingredient.quantity }} {{ ingredient.name }}</p>
                                        </div>

                                        <h4>Instructions:</h4>
                                        <p class="mb-5">{{ activeRecipe.instructions }}</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>


<style lang="scss" scoped>

.heightset {
    height: 75vh;
    object-fit: cover;
    width: 100%;
}
</style>