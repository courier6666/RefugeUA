import { Entity } from "./entity"
import { Address } from "./address"
import { User } from "./user"
import { VolunteerGroup } from "./volunteer-group"
import { VolunteerEventScheduleItem } from "./volunteer-event-schedule-item"
import { VolunteerEventType } from "../enums/volunteer-event-type-enum"

export interface VolunteerEvent extends Entity {
    title: string,
    content: string,
    startTime: Date,
    endTime: Date,
    createdAt: Date,
    addressId?: number,
    address?: Address,
    scheduleItems?: VolunteerEventScheduleItem[],
    volunteerGroupId?: number,
    volunteerGroup?: VolunteerGroup,
    volunteerGroupTitle?: string,
    onlineConferenceLink?: string,
    isClosed: boolean,
    donationLink?: string,
    volunteerEventType: VolunteerEventType,
    organizers: User[],
    participants?: User[],
    participantsCount?: number
}