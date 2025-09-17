import { BaseCreateAnnouncementCommand } from "./base-create-announcement-command";

export interface CreateWorkAnnouncementCommand extends BaseCreateAnnouncementCommand {
    jobPosition: string,
    companyName: string,
    salaryLower?: number,
    salaryUpper?: number,
    requirementsContent: string,
    workCategoryId: number,
}