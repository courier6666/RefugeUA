import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable, pipe, map, catchError, throwError } from "rxjs";
import { CreateVolunteerEventCommand } from "../../api-models/create-volunteer-event-command";
import { VolunteerEvent } from "../../models";
import { PagingInfo } from "../../../shared/util/paging-model";
import { VolunteerEventsQuery } from "../../queries/volunteer-events-query";
import { VolunteerEventType } from "../../enums/volunteer-event-type-enum";
import { Params } from "@angular/router";
import { User } from "../../models";
import { catchErrorForFormMethod } from "../../../shared/util/form-error-capture-method";

@Injectable({
    providedIn: 'root'
})
export class VolunteerEventService {
    constructor(private http: HttpClient) {
    }

    deleteById(id: number): Observable<any> {
        return this.http.delete(`api/volunteer/events/${id}`);
    }

    createVolunteerEvent(createVolunteerEvent: CreateVolunteerEventCommand): Observable<any> {
        return this.http.post("api/volunteer/events", createVolunteerEvent).pipe(map(res => {
            return res;
        }),
            catchError(catchErrorForFormMethod));
    }

    editVolunteerEvent(id: number, createVolunteerEvent: CreateVolunteerEventCommand): Observable<any> {
        return this.http.put(`api/volunteer/events/${id}`, createVolunteerEvent).pipe(map(res => {
            return res;
        }),
            catchError(catchErrorForFormMethod));
    }

    participateInVolunteerEvent(id: number): Observable<any> {
        return this.http.post(`api/volunteer/events/${id}/register`, {});
    }

    leaveFromVolunteerEvent(id: number): Observable<any> {
        return this.http.delete(`api/volunteer/events/${id}/leave`);
    }

    meParticipatingInEvent(id: number): Observable<boolean> {
        return this.http.get<boolean>(`api/volunteer/events/${id}/participants/me/exists`);
    }

    getVolunteerEvent(id: number): Observable<VolunteerEvent> {
        return this.http.get<any>(`api/volunteer/events/${id}`).
            pipe(map((result) => {
                console.log(result);
                let eventType: VolunteerEventType;

                if (result.eventType == "Participation") {
                    eventType = VolunteerEventType.Participation;
                }
                else {
                    eventType = VolunteerEventType.Donation;
                }

                let event: VolunteerEvent = {
                    ...result,
                    volunteerEventType: eventType
                }

                return event;
            }), catchError((error) => {
                return throwError(() => error);
            }));
    }

    mapPagingInfoVolunteerEvent(result: PagingInfo<any>): PagingInfo<VolunteerEvent> {
        let items: VolunteerEvent[] = result.items.map(i => {

            let eventType: VolunteerEventType;

            if (i.eventType == "Participation") {
                eventType = VolunteerEventType.Participation;
            }
            else {
                eventType = VolunteerEventType.Donation;
            }

            let event: VolunteerEvent = {
                ...i,
                volunteerEventType: eventType
            }

            return event;
        });

        let pagingInfo: PagingInfo<VolunteerEvent> = {
            items: items,
            totalCount: result.totalCount,
            page: result.page,
            pageLength: result.pageLength,
            pagesCount: result.pagesCount
        };

        return pagingInfo;
    }

    getVolunteerEvents(query: VolunteerEventsQuery): Observable<PagingInfo<VolunteerEvent>> {
        const params: any = {};
        for (const key of Object.keys(query)) {
            const value = (query as any)[key];
            if (value !== undefined && value !== null) {
                params[key] = value;
            }
        }

        return this.http.get<PagingInfo<any>>(`api/volunteer/events/paged`, { params: params }).
            pipe(map(this.mapPagingInfoVolunteerEvent), catchError((error) => {
                return throwError(() => error);
            }));
    }

    getMyVolunteerEvents(page: number, pageLength: number, prompt?: string | null): Observable<PagingInfo<VolunteerEvent>> {
        let params: Params = {
            page: page,
            pageLength: pageLength
        };

        if (prompt != null && prompt != undefined && prompt.length > 0)
            params['prompt'] = prompt ? prompt : null;


        return this.http.get<PagingInfo<any>>(`api/volunteer/events/mine/paged`, { params: params }).
            pipe(map(this.mapPagingInfoVolunteerEvent), catchError((error) => {
                return throwError(() => error);
            }));
    }


    getParticipatedVolunteerEvents(page: number, pageLength: number, prompt?: string | null): Observable<PagingInfo<VolunteerEvent>> {
        let params: Params = {
            page: page,
            pageLength: pageLength
        };

        if (prompt != null && prompt != undefined && prompt.length > 0)
            params['prompt'] = prompt ? prompt : null;


        return this.http.get<PagingInfo<any>>(`api/volunteer/events/participated/paged`, { params: params }).
            pipe(map(this.mapPagingInfoVolunteerEvent), catchError((error) => {
                return throwError(() => error);
            }));
    }

    getVolunteerEventParticipants(id: number, page: number, pageLength: number, prompt?: string | null): Observable<PagingInfo<User>> {
        let params: Params = {
            page: page,
            pageLength: pageLength
        };

        if (prompt != null && prompt != undefined && prompt.length > 0)
            params['prompt'] = prompt ? prompt : null;

        return this.http.get<PagingInfo<User>>(`api/volunteer/events/${id}/participants`, { params: params });
    }

    openVolunteerEvent(id: number): Observable<any> {
        return this.http.patch(`api/volunteer/events/${id}/open`, {});
    }

    closeVolunteerEvent(id: number): Observable<any> {
        return this.http.patch(`api/volunteer/events/${id}/close`, {});
    }

    removeParticipantFromVolunteerEvent(id: number, userId: number): Observable<any> {
        return this.http.delete(`api/volunteer/events/${id}/participants/${userId}`);
    }
}