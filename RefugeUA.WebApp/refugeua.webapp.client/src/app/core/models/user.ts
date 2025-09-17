import { Entity } from "./entity"

export interface User extends Entity {
    firstName: string,
    lastName: string,
    email: string,
    phoneNumber: string,
    dateOfBirth: Date,
    createdAt: Date,
    profileImageUrl?: string,
    role?: string,
    isAccepted?: boolean,
    isEmailConfirmed?: boolean,
    isPhoneNumberConfirmed?: boolean,
    district?: string,
}