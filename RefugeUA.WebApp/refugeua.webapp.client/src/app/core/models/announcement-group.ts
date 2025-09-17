import { Entity } from "./entity"
import { Announcement } from "./announcement"

export interface AnnouncementGroup extends Entity {
    name: string,
    announcements?: Announcement[]
}