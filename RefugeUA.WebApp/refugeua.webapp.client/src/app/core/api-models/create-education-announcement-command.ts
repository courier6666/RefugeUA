import { BaseCreateAnnouncementCommand } from "./base-create-announcement-command";

export interface CreateEducationAnnouncementCommand extends BaseCreateAnnouncementCommand {
    educationType: string,
    institutionName: string,
    targetGroups: string[],
    fee?: number | null,
    duration: number,
    language: string
}