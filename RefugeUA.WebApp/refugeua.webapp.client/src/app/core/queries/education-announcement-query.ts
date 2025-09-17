import { TimeSpan } from "../../shared/util/time-span";
import { PageItemsQuery } from "./page-items-query";

export interface EducationAnnouncementQuery extends PageItemsQuery {
    prompt?: string | null,
    educationTypes?: string[],
    targetGroups?: string[],
    isFreeOnly?: boolean,
    feeLower?: number,
    feeUpper?: number,
    durationLower?: number,
    durationUpper?: number,
    district?: string,
    announcementGroup?: string
}