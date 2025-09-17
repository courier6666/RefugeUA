import { VolunteerEventType } from "../../core/enums/volunteer-event-type-enum";

export const getLabelForVolunteerEventType = (type: VolunteerEventType) =>
{
    return type == VolunteerEventType.Donation ? "Пожертвування" : "Участь в заході";
}