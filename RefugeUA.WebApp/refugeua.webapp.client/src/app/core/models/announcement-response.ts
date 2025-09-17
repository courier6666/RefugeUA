import { Entity } from "./entity"
import { Announcement } from "./announcement"
import { ContactInformation } from "./contact-information"
import { User } from "./user"

export interface AnnouncementResponse extends Entity{
    userId?: number,
    user?: User,
    announcementId: number,
    announcement?: Announcement,
    contactInformationId: number,
    contactInformation?: ContactInformation,
    createdAt: Date 
}