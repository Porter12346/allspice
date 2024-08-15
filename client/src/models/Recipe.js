import { Account } from "./Account.js"

export class Recipe{
    constructor(data){
        this.id = data.id || data._id
        this.createdAt = new Date(data.createdAt)
        this.updatedAt = new Date(data.updatedAt)
        this.title = data.title
        this.instructions = data.instructions
        this.img = data.img
        this.category = data.category
        this.creatorId = data.creatorId
        this.creator = new Account(data.creator)
        this.subTitle = data.subTitle
    }
}