import { HttpClient } from "@angular/common/http";
import { Observable, of, map, throwError, catchError } from "rxjs";
import { AccomodationAnnouncement } from "../../../models";
import { AccomodationAnnouncementQuery } from "../../../queries/accomodation-announcement-query";
import { PagingInfo } from "../../../../shared/util/paging-model";
import { BuildingType, buildingTypes } from "../../../constants/building-type";
import { Injectable } from "@angular/core";
import { BaseAnnouncementService } from "../base-announcement-services";
import { CreateAccomodationAnnouncementCommand } from "../../../api-models/create-accomodation-announcement";
import { catchErrorForFormMethod } from "../../../../shared/util/form-error-capture-method";

@Injectable({
    providedIn: 'root'
})
export class AnnouncementAccomodationService extends BaseAnnouncementService {
    constructor(http: HttpClient) {
        super(http);
    }

    getBuildingTypes(): Observable<BuildingType[]> {
        return of(buildingTypes);
    }

    createAccomodationAnnouncement(createAccomodationAnnouncementCommand: CreateAccomodationAnnouncementCommand) {
        let formData: FormData = new FormData();

        formData.append('Title', createAccomodationAnnouncementCommand.title);
        formData.append('Content', createAccomodationAnnouncementCommand.content);
        formData.append('BuildingType', createAccomodationAnnouncementCommand.buildingType);
        formData.append('PetsAllowed', String(createAccomodationAnnouncementCommand.petsAllowed));
        formData.append('NumberOfRooms', String(createAccomodationAnnouncementCommand.numberOfRooms));
        formData.append('Floors', String(createAccomodationAnnouncementCommand.floors));
        if (createAccomodationAnnouncementCommand.areaSqMeters !== undefined && createAccomodationAnnouncementCommand.areaSqMeters !== null) {
            formData.append('AreaSqMeters', String(createAccomodationAnnouncementCommand.areaSqMeters));
        }
        if (createAccomodationAnnouncementCommand.price !== undefined && createAccomodationAnnouncementCommand.price !== null) {
            formData.append('Price', String(createAccomodationAnnouncementCommand.price));
        }
        formData.append('Capacity', String(createAccomodationAnnouncementCommand.capacity));

        formData.append('ContactInformation.PhoneNumber', createAccomodationAnnouncementCommand.contactInformation.phoneNumber);
        if (createAccomodationAnnouncementCommand.contactInformation.email) {
            formData.append('ContactInformation.Email', createAccomodationAnnouncementCommand.contactInformation.email);
        }
        if (createAccomodationAnnouncementCommand.contactInformation.telegram) {
            formData.append('ContactInformation.Telegram', createAccomodationAnnouncementCommand.contactInformation.telegram);
        }
        if (createAccomodationAnnouncementCommand.contactInformation.viber) {
            formData.append('ContactInformation.Viber', createAccomodationAnnouncementCommand.contactInformation.viber);
        }
        if (createAccomodationAnnouncementCommand.contactInformation.facebook) {
            formData.append('ContactInformation.Facebook', createAccomodationAnnouncementCommand.contactInformation.facebook);
        }

        formData.append('Address.Country', createAccomodationAnnouncementCommand.address.country);
        formData.append('Address.Region', createAccomodationAnnouncementCommand.address.region);
        formData.append('Address.District', createAccomodationAnnouncementCommand.address.district);
        formData.append('Address.Settlement', createAccomodationAnnouncementCommand.address.settlement);
        formData.append('Address.Street', createAccomodationAnnouncementCommand.address.street);
        formData.append('Address.PostalCode', createAccomodationAnnouncementCommand.address.postalCode);

        if (createAccomodationAnnouncementCommand.images && createAccomodationAnnouncementCommand.images.length > 0) {
            for (let i = 0; i < createAccomodationAnnouncementCommand.images.length; ++i) {
                let image = createAccomodationAnnouncementCommand.images[i];
                formData.append('Images', image, image.name);
            }
        }

        return this.http.post('api/announcements/accomodation', formData).pipe(map(res => {
                    return res;
                }),
                    catchError(catchErrorForFormMethod));
    }

    editAccomodationAnnouncement(id: number, editAccomodationAnnouncementCommand: CreateAccomodationAnnouncementCommand) {

        let formData: FormData = new FormData();

        formData.append('Title', editAccomodationAnnouncementCommand.title);
        formData.append('Content', editAccomodationAnnouncementCommand.content);
        formData.append('BuildingType', editAccomodationAnnouncementCommand.buildingType);
        formData.append('PetsAllowed', String(editAccomodationAnnouncementCommand.petsAllowed));
        formData.append('NumberOfRooms', String(editAccomodationAnnouncementCommand.numberOfRooms));
        formData.append('Floors', String(editAccomodationAnnouncementCommand.floors));
        if (editAccomodationAnnouncementCommand.areaSqMeters !== undefined && editAccomodationAnnouncementCommand.areaSqMeters !== null) {
            formData.append('AreaSqMeters', String(editAccomodationAnnouncementCommand.areaSqMeters));
        }
        if (editAccomodationAnnouncementCommand.price !== undefined && editAccomodationAnnouncementCommand.price !== null) {
            formData.append('Price', String(editAccomodationAnnouncementCommand.price));
        }
        formData.append('Capacity', String(editAccomodationAnnouncementCommand.capacity));

        formData.append('ContactInformation.PhoneNumber', editAccomodationAnnouncementCommand.contactInformation.phoneNumber);
        if (editAccomodationAnnouncementCommand.contactInformation.email) {
            formData.append('ContactInformation.Email', editAccomodationAnnouncementCommand.contactInformation.email);
        }
        if (editAccomodationAnnouncementCommand.contactInformation.telegram) {
            formData.append('ContactInformation.Telegram', editAccomodationAnnouncementCommand.contactInformation.telegram);
        }
        if (editAccomodationAnnouncementCommand.contactInformation.viber) {
            formData.append('ContactInformation.Viber', editAccomodationAnnouncementCommand.contactInformation.viber);
        }
        if (editAccomodationAnnouncementCommand.contactInformation.facebook) {
            formData.append('ContactInformation.Facebook', editAccomodationAnnouncementCommand.contactInformation.facebook);
        }

        formData.append('Address.Country', editAccomodationAnnouncementCommand.address.country);
        formData.append('Address.Region', editAccomodationAnnouncementCommand.address.region);
        formData.append('Address.District', editAccomodationAnnouncementCommand.address.district);
        formData.append('Address.Settlement', editAccomodationAnnouncementCommand.address.settlement);
        formData.append('Address.Street', editAccomodationAnnouncementCommand.address.street);
        formData.append('Address.PostalCode', editAccomodationAnnouncementCommand.address.postalCode);

        if (editAccomodationAnnouncementCommand.images && editAccomodationAnnouncementCommand.images.length > 0) {
            for (let i = 0; i < editAccomodationAnnouncementCommand.images.length; ++i) {
                let image = editAccomodationAnnouncementCommand.images[i];
                formData.append('Images', image, image.name);
            }
        }

        return this.http.put(`api/announcements/accomodation/${id}`, formData).pipe(map(res => {
                    return res;
                }),
                    catchError(catchErrorForFormMethod));
    }

    getAccomodationAnnouncements(query: AccomodationAnnouncementQuery): Observable<PagingInfo<AccomodationAnnouncement>> {

        const params: any = {};
        for (const key of Object.keys(query)) {
            const value = (query as any)[key];
            if (value !== undefined && value !== null) {
                params[key] = value;
            }
        }

        return this.http.get<PagingInfo<AccomodationAnnouncement>>("api/announcements/accomodation/paged", { params: params });
    }

    getAccomodationAnnouncement(id: number): Observable<AccomodationAnnouncement> {
        return this.http.get<any>(`api/announcements/accomodation/${id}`);
    }
}