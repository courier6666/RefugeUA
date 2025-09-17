import { CreateAddressCommand } from "./create-address-command";
import { CreateVolunteerEventScheduleItemCommand } from "./create-volunteer-schedule-item-command";

export interface CreateVolunteerEventCommand {
    title: string,
    content: string,
    startTime: Date,
    endTime: Date,
    address?: CreateAddressCommand,
    volunteerGroupId?: number,
    onlineConferenceLink?: string,
    eventType: string,
    donationLink?: string,
    scheduleItems?: CreateVolunteerEventScheduleItemCommand
}