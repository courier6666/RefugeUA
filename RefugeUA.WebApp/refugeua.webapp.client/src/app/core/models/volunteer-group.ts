import { Entity } from "./entity"
import { User } from "./user"
import { VolunteerEvent } from "./volunteer-event"

export interface VolunteerGroup extends Entity {
    title: string
    descriptionContent: string,
    createdAt: Date,
    followers?: User[],
    admins?: User[],
    followersCount?: number,
    administratorsCount?: number,
    volunteerEvents?: VolunteerEvent[],
    volunteerEventsCount?: number
}