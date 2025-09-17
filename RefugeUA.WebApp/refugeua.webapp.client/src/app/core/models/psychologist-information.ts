import { Entity } from "./entity"
import { ContactInformation } from "./contact-information"
import { User } from "./user"

export interface PsychologistInformation extends Entity {
    title: string,
    description: string,
    createdAt: Date,
    authorId?: number,
    author?: User,
    contactId: number,
    contact?: ContactInformation
}