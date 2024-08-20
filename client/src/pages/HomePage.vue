<script setup>
import { AppState } from '@/AppState.js';
import { ingredientsService } from '@/services/IngredientsService.js';
import { recipesService } from '@/services/ReciepesService.js';
import { logger } from '@/utils/Logger.js';
import Pop from '@/utils/Pop.js';
import { computed, onMounted, ref, watch } from 'vue';

const instructions = ref('')

const recipes = computed(() => AppState.recipes)
const activeRecipe = computed(() => {
  return (AppState.activeRecipe)
})
const ingredients = computed(() => AppState.ingredients)

watch(activeRecipe, () => instructions.value = activeRecipe.value.instructions)

let edit = true

onMounted(() => {
  getRecipes()
})


const ingredientData = ref({
  quantity: 0,
  name: ''
})

async function makeIngredient() {
  ingredientsService.createIngredient(ingredientData.value)
}


// async function setActive(recipe){
//     recipesService.setActiveRecipe(recipe)
//     await ingredientsService.getIngredientsByRecipeId(recipe.id)
// }

async function getRecipes() {
  await recipesService.getRecipes()
}

async function editRecipe() {
  const newRecipe = await recipesService.editRecipeInstruction(instructions)
  logger.log(newRecipe)
  Pop.success("edited")
}
</script>

<template>
  <div class="m-2 the-font">
    <div class="container-fluid bg-img pb-5">
      <div class="row">
        <div class="col-12 d-flex justify-content-end align-items-center">
          <h5 class="px-3">
            <input type="search" placeholder="search">
          </h5>
          <Login />
        </div>
        <div class="col-12 text-center text-light">
          <h1>All-Spice</h1>
          <p>Cherish Your Family</p>
          <p>and Their Cooking</p>
        </div>
      </div>
    </div>
    <div class="container">
      <div class="row">
        <div class="col-2"></div>
        <div class="col-8 justify-content-center align-items-center shadow neg-mar">
          <div class="row text-center">
            <div class="col-4 p-2 border">
              <a href="">Home</a>
            </div>
            <div class="col-4 p-2 border">
              <a href="">Your recipes</a>
            </div>
            <div class="col-4 p-2 border">
              <a href="">favorites</a>
            </div>
          </div>
        </div>
        <div class="container">
          <div v-if="recipes.length > 0" class="row">
            <div v-for="recipe in recipes" :key="recipe.id" class="col-4">
              <RecipeCard :recipeProp="recipe" />
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>



  <!-- Modal -->
  <div class="modal fade" id="recipeModal" tabindex="-1" aria-labelledby="recipeModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-xl">\
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
                  <form @submit.prevent="editRecipe()" class="col-6 d-flex flex-column justify-content-between">
                    <div class="d-flex align-items-center">
                      <h2 class="text-primary pe-4">{{ activeRecipe.title }}</h2>
                      <i type="button" class="mdi mdi-dots-horizontal fs-1 "></i>
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
                        <input v-model="ingredientData.quantity" type="text" aria-label="quantity" class="form-control"
                          placeholder="quantity">
                        <input v-model="ingredientData.name" type="text" aria-label="ingredient"
                          class="form-control w-50" placeholder="new ingredient text">
                        <button class="btn btn-outline-secondary" type="submit">Add</button>
                      </form>
                    </div>
                    <div class="mt-3 d-flex flex-column">
                      <h5>Instructions:</h5>
                      <textarea v-model="instructions" class="form-control" id="instructionsTextArea" rows="6"
                        style="resize: none;"></textarea>
                    </div>
                    <button type="submit" class="btn btn-success w-25 align-self-end mb-2">Save</button>
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
                      <i type="button" class="mdi mdi-dots-horizontal fs-1 "></i>
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
  <div class="sticky-bottom text-end pb-3 pe-3">
    <button data-bs-toggle="modal" data-bs-target="#createRecipeModal"
      class="btn btn-success fs-1 rounded-circle custom-padding">
      <i class="mdi mdi-plus"></i>
    </button>
  </div>

  <!-- Modal 2 -->
  <div class="modal fade" id="createRecipeModal" tabindex="-1" aria-labelledby="createRecipeModalLabel"
    aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header bg-success-subtle">
          <h1 class="modal-title fs-5" id="createRecipeModalLabel">Create Recipe</h1>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
          <div class="container">
            <form class="row">
              <div class="col-6">
                <div class="mb-3">
                  <label for="createRecipeTitle" class="form-label">Title</label>
                  <input type="string" class="form-control" id="createRecipeTitle">
                </div>
              </div>
              <div class="col-6">
                <div class="mb-3">
                  <label for="createRecipeCategory" class="form-label">Category</label>
                  <input type="string" class="form-control" id="createRecipeCategory">
                </div>
              </div>
            </form>
          </div>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
          <button type="button" class="btn btn-primary">Save changes</button>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped lang="scss">
.home {
  display: grid;
  height: 80vh;
  place-content: center;
  text-align: center;
  user-select: none;

  .home-card {
    width: clamp(500px, 50vw, 100%);

    >img {
      height: 200px;
      max-width: 200px;
      width: 100%;
      object-fit: contain;
      object-position: center;
    }
  }
}

.bg-img {
  background-image: url("https://s3-alpha-sig.figma.com/img/137d/d14c/aa93e0df14a805e8e10c9f1221ba5131?Expires=1724630400&Key-Pair-Id=APKAQ4GOSFWCVNEHN3O4&Signature=mtHJiwb4sSS7RJjSOZYwFMhZtycXiJa7fAzoL1cpqsW59EO~aNSRUD-bupz9dGeRbN~5G5-fsg76VTP6fWlIjdbpTK54v1RXJSzZMGeBFG-FsIj888cIT6MGfA5CrLUDXRrhx1W2t399duCslrPGtYzHgy~Gs6nLuZoIhSnKccpC3VALhORZ3fjTnbb45yzNkjZLfyK7dyVbGw7aeDU4IY-4YS3xM8eIraeQRlfwG15hPVFmjyunFn19vZSefazk6zJzegwWH55zp9POj~-2DIkUORPo-N7YW4iz~ytI7pmL~iW1zpbWM4v1mgbvhVI58XlY5enLfttjY1ScDi2oTw__");
  background-position: center;
  background-size: cover;
}

.neg-mar {
  background-color: white;
  margin-top: -2%;
}

.the-font {
  font-family: "Sahitya", serif;
  font-weight: 400;
  font-style: normal;
}

.transparent-bg {
  background-color: rgba(0, 0, 0, 0.259);
}

.heightset {
  height: 75vh;
  object-fit: cover;
  width: 100%;

}

.custom-padding {
  padding-left: 0.25em;
  padding-right: 0.25em;
  padding-top: 0em;
  padding-bottom: 0em;
}
</style>
