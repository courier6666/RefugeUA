import { Entity } from "./entity"
import { User } from "./user"

export interface MentalSupportArticle extends Entity{
    title: string,
    content: string,
    createdAt: Date,
    authorId?: number,
    author?: User
}