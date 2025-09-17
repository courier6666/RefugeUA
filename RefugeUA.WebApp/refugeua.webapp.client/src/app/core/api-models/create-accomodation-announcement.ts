import { BaseCreateAnnouncementCommand } from "./base-create-announcement-command";

export interface CreateAccomodationAnnouncementCommand extends BaseCreateAnnouncementCommand {
    buildingType: string,
    petsAllowed: boolean,
    numberOfRooms: number,
    floors: number,
    areaSqMeters?: number,
    price?: number,
    capacity: number,
    images?: File[]
}