<script setup>
import { AppState } from '@/AppState.js';
import { ingredientsService } from '@/services/IngredientsService.js';
import { recipesService } from '@/services/ReciepesService.js';
import { computed, onMounted, ref } from 'vue';


const recipes = computed(() => AppState.recipes)
const activeRecipe = computed(() => { instructions = AppState.activeRecipe?.instructions; return (AppState.activeRecipe) })
const ingredients = computed(() => AppState.ingredients)

let edit = true

onMounted(() => {
  getRecipes()
})

let instructions = ""

// async function setActive(recipe){
//     recipesService.setActiveRecipe(recipe)
//     await ingredientsService.getIngredientsByRecipeId(recipe.id)
// }

async function getRecipes() {
  await recipesService.getRecipes()
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
                  <div class="col-6 d-flex flex-column justify-content-between">
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
                      <div class="input-group">
                        <input type="text" aria-label="quantity" class="form-control" placeholder="quantity">
                        <input type="text" aria-label="ingredient" class="form-control w-50" placeholder="new ingredient text">
                        <button class="btn btn-outline-secondary" type="button">Add</button>
                      </div>
                    </div>
                    <div class="my-3 d-flex flex-column">
                      <h5>Instructions:</h5>
                      <textarea v-model="instructions" class="form-control" id="instructionsTextArea" rows="6"
                        style="resize: none;"></textarea>
                      <button class="btn btn-success w-25 align-self-end mt-2">Save</button>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
      <form v-else>
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
                  <div class="col-6">
                    <div class="d-flex align-items-center">
                      <h2 class="text-primary pe-4">{{ activeRecipe.title }}</h2>
                      <i type="button" class="mdi mdi-dots-horizontal fs-1 "></i>
                    </div>
                    <p>by: {{ activeRecipe.creator.name }}</p>
                    <span class="transparent-bg rounded p-1">{{ activeRecipe.category }}</span>
                    <h4 class="mt-3">Ingredients:</h4>
                    <div v-for="ingredient in ingredients" :key="ingredient.id" class="d-flex">
                      <p class="mb-1">• {{ ingredient.quantity }} {{ ingredient.name }}</p>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </form>
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
</style>
