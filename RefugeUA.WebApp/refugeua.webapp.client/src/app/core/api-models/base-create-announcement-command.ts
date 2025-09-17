import { CreateAddressCommand } from "./create-address-command";
import { CreateContactInfoCommand } from "./create-contact-info-command";

export interface BaseCreateAnnouncementCommand {
    title: string,
    content: string,
    contactInformation: CreateContactInfoCommand,
    address: CreateAddressCommand,
}