import { TimeSpan } from "../../shared/util/time-span";
import { PageItemsQuery } from "./page-items-query";

export interface AccomodationAnnouncementQuery extends PageItemsQuery {
    prompt?: string | null,
    capacity?: number,
    numberOfRooms?: number,
    floors?: number,
    areaSqMetersLower?: number,
    areaSqMetersUpper?: number,
    priceLower?: number,
    priceUpper?: number,
    isFree?: boolean,
    buildingTypes?: string[],
    petsAllowed?: boolean,
    district?: string,
    announcementGroup?: string
}