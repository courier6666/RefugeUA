import { Entity } from "./entity"
import { VolunteerEvent } from "./volunteer-event"

export interface VolunteerEventScheduleItem extends Entity {
    startTime: Date,
    description: string,
    volunteerEventId: number,
    volunteerEvent?: VolunteerEvent
}