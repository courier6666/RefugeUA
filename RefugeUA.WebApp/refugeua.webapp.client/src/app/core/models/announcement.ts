import { Entity } from "./entity"
import { Address } from "./address"
import { ContactInformation } from "./contact-information"
import { AnnouncementResponse } from "./announcement-response"
import { User } from "./user"
import { AnnouncementGroup } from "./announcement-group"

export interface Announcement extends Entity {
    title: string,
    content: string,
    contactInformationId: number,
    contactInformation?: ContactInformation
    addressId: number,
    address?: Address,
    responses?: AnnouncementResponse[],
    createdAt: Date,
    authorId?: number,
    author?: User,
    isAccepted: boolean,
    nonAcceptenceReason?: string,
    isClosed: boolean,
    announcementGroups?: AnnouncementGroup[]
    type?: string,
}