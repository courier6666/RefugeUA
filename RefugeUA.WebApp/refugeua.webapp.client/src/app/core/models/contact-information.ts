import { Entity } from "./entity"

export interface ContactInformation extends Entity{
    phoneNumber: string,
    email?: string,
    telegram?: string,
    viber?: string,
    facebook?: string
}