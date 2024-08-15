import { Account } from "./Account.js"

export class Recipe{
    constructor(data){
        this.id = data.id
        this.createdAt = new Date(data.createdAt)
        this.updatedAt = new Date(data.updatedAt)
        this.title = data.title
        this.instructions = data.instructions
        this.img = data.img
        this.category = data.category
        this.creatorId = data.creatorId
        this.creator = new Account(data.creator)
    }

    get backgroundImg(){
        return `background-image: url(${this.img})`
    }
}

