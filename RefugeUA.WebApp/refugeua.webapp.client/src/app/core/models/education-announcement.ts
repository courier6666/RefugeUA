import { TimeSpan } from "../../shared/util/time-span";
import { Announcement } from "./announcement";

export interface EducationAnnouncement extends Announcement {
    educationType: string,
    institutionName: string,
    targetGroups: string[],
    isFree: boolean,
    fee?: number,
    duration: number,
    language: string,
}