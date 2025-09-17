import { Announcement } from "./announcement"
import { Image } from "./image"

export interface AccomodationAnnouncement extends Announcement{
    images?: Image[],
    buildingType: string,
    petsAllowed: boolean,
    floors: number,
    numberOfRooms: number,
    capacity: number,
    areaSqMeters?: number,
    price?: number,
    isFree: boolean
}