import { Recipe } from "@/models/Recipe.js"
import { api } from "./AxiosService.js"
import { AppState } from "@/AppState.js"
import { logger } from "@/utils/Logger.js"

class RecipesService {
    setActiveRecipe(recipeProp) {
        AppState.activeRecipe = recipeProp;
    }
    async getRecipes() {
        let response = await api.get('api/recipes')
        let recipes = response.data.map((recipeData) => new Recipe(recipeData))
        AppState.recipes = recipes
        logger.log(recipes)
    }

}
export const recipesService = new RecipesService()