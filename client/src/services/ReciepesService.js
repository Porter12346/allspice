import { Recipe } from "@/models/Recipe.js"
import { api } from "./AxiosService.js"
import { AppState } from "@/AppState.js"
import { logger } from "@/utils/Logger.js"
import Pop from "@/utils/Pop.js"
import { Favorite } from "@/models/Favorite.js"

class RecipesService {
    async deleteFavorite(index) {
        const favorite = AppState.favorites[index]
        await api.delete(`api/favorites/${favorite.favoriteId}`)
        AppState.favorites.splice(index, 1)
    }
    async addFavorite(id) {
        const response = await api.post('api/favorites', { recipeId: id })
        const favorite = new Favorite(response.data)
        AppState.favorites.unshift(favorite)
    }
    async getFavoriteRecipes() {
        const response = await api.get('account/favorites')
        const recipes = response.data.map((recipeData) => new Favorite(recipeData))
        AppState.favorites = recipes
    }
    setFavorites() {
        AppState.recipes = AppState.favorites
    }
    async getOwnRecipes() {
        await this.getRecipes()
        const recipes = AppState.recipes.filter((recipe) => recipe.creatorId == AppState.account.id)
        AppState.recipes = recipes
        logger.log(recipes)
    }
    async deleteActiveRecipe() {
        const recipe = AppState.activeRecipe
        if (AppState.account.id != recipe.creatorId) throw new Error('you cannot delete this restaurant')
        await api.delete(`api/recipes/${recipe.id}`)
        const index = AppState.recipes.findIndex((recipe) => recipe.id == AppState.activeRecipe.id)
        AppState.recipes.splice(index, 1)
        AppState.activeRecipe = null
    }
    async createRecipe(recipeData) {
        const response = await api.post('api/recipes', recipeData)
        const recipe = new Recipe(response.data)
        AppState.recipes.unshift(recipe)
        return recipe
    }

    async editRecipeInstruction(instructions) {
        const response = await api.put(`api/recipes/${AppState.activeRecipe.id}`, { instructions: instructions.value })
        const newRecipe = new Recipe(response.data)
        AppState.activeRecipe = newRecipe
        const index = AppState.recipes.findIndex((recipe) => recipe.id == AppState.activeRecipe.id)
        AppState.recipes[index] = newRecipe
        return (AppState.activeRecipe)
    }
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