import { Entity } from "./entity"

export interface Address extends Entity{
    country: string,
    region: string,
    district: string,
    settlement: string,
    street: string,
    postalCode: string
}