import { WorkCategory } from "./work-category"
import { Announcement } from "./announcement"
import { AnnouncementGroup } from "./announcement-group"

export interface WorkAnnouncement extends Announcement {
    jobPosition: string,
    companyName?: string,
    salaryLower?: number,
    salaryUpper?: number,
    requirementsContent: string,
    workCategoryId: number,
    workCategory: WorkCategory,
    announcementGroups?: AnnouncementGroup[]
}