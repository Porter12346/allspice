import { Ingredient } from "@/models/Ingredient.js"
import { api } from "./AxiosService.js"
import { AppState } from "@/AppState.js"

class IngredientsService{
    async getIngredientsByRecipeId(id) {
        let response = await api.get(`api/recipes/${id}/ingredients`)
        let ingredients = response.data.map((data)=>new Ingredient(data))
        AppState.ingredients = ingredients;
    }
}
export const ingredientsService = new IngredientsService()