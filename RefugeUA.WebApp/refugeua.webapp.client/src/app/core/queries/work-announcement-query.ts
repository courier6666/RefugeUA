import { WorkCategory } from "../models";
import { PageItemsQuery } from "./page-items-query";

export interface WorkAnnouncementQuery extends PageItemsQuery
{
    prompt?: string | null,
    salaryLower?: number | null,
    salaryUpper?: number | null,
    jobCategories?: number[],
    salaryNotSet?: boolean,
    isClosed?: boolean
    district?: string,
    announcementGroup?: string
}