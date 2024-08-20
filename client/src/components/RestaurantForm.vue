<script setup>
import { recipesService } from '@/services/ReciepesService.js';
import { Modal } from 'bootstrap';
import { ref } from 'vue';

async function createRecipe() {
    const newRecipe = await recipesService.createRecipe(recipeData.value)
    recipesService.setActiveRecipe(newRecipe)
    Modal.getOrCreateInstance('#createRecipeModal').hide()
    Modal.getOrCreateInstance('#recipeModal').show()
}

const recipeData = ref({
    title: '',
    category: '',
    img: ''
})

</script>


<template>
    <div class="modal fade" id="createRecipeModal" tabindex="-1" aria-labelledby="createRecipeModalLabel"
        aria-hidden="true">
        <form @submit.prevent="createRecipe()" class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-success-subtle">
                    <h1 class="modal-title fs-5" id="createRecipeModalLabel">Create Recipe</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="container">
                        <div class="row">
                            <div class="col-6">
                                <div class="mb-3">
                                    <label for="createRecipeTitle" class="form-label">Title</label>
                                    <input v-model="recipeData.title" type="string" class="form-control"
                                        id="createRecipeTitle">
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="mb-3">
                                    <label for="createRecipeCategory" class="form-label">Category</label>
                                    <select v-model="recipeData.category" id="createRecipeCategory" class="form-select"
                                        aria-label="Category Select">
                                        <option selected disabled>Category</option>
                                        <option value="breakfast">Breakfast</option>
                                        <option value="lunch">Lunch</option>
                                        <option value="dinner">Dinner</option>
                                        <option value="snack">Snack</option>
                                        <option value="dessert">Dessert</option>
                                    </select>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="mb-3 px-4">
                    <label for="createRecipeImg" class="form-label">Img URL</label>
                    <input v-model="recipeData.img" type="string" class="form-control" id="createRecipeImg">
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                    <button type="submit" class="btn btn-primary">Create</button>
                </div>
            </div>
        </form>
    </div>
</template>


<style lang="scss" scoped></style>