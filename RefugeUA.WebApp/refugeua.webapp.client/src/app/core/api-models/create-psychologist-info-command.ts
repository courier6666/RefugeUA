import { CreateContactInfoCommand } from "./create-contact-info-command";

export interface CreatePsychologistInfoCommand {
    title: string,
    description: string,
    contactInformation: CreateContactInfoCommand
}