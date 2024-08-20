<script setup>
import { AppState } from '@/AppState.js';
import RecipeInfoCard from '@/components/RecipeInfoCard.vue';
import { ingredientsService } from '@/services/IngredientsService.js';
import { recipesService } from '@/services/ReciepesService.js';
import { logger } from '@/utils/Logger.js';
import Pop from '@/utils/Pop.js';
import { Modal } from 'bootstrap';
import { computed, onMounted, ref, watch } from 'vue';

const recipes = computed(() => AppState.recipes)
const account = computed(() => AppState.account)

onMounted(() => {
  getRecipes()
})

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

  <div v-if="account" class="sticky-bottom text-end pb-3 pe-3">
    <button data-bs-toggle="modal" data-bs-target="#createRecipeModal"
      class="btn btn-success fs-1 rounded-circle custom-padding">
      <i class="mdi mdi-plus"></i>
    </button>
  </div>
  <RestaurantForm />
  <RecipeInfoCard :accountProp="account"/>
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



.custom-padding {
  padding-left: 0.25em;
  padding-right: 0.25em;
  padding-top: 0em;
  padding-bottom: 0em;
}
</style>
