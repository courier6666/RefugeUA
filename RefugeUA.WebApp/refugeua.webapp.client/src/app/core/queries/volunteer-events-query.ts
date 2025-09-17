import { PageItemsQuery } from "./page-items-query";

export interface VolunteerEventsQuery extends PageItemsQuery {
    startDate?: Date,
    endDate?: Date,
    prompt?: string,
    eventType?: string,
    volunteerGroupId?: number,
    district?: string,
    isClosed?: boolean
}