import { Injectable, resolveForwardRef } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { catchError, map, Observable, of } from "rxjs";
import { CreateContactInfoCommand } from "../../api-models/create-contact-info-command";
import { Params } from "@angular/router";
import { PagingInfo } from "../../../shared/util/paging-model";
import { AnnouncementGroup, AnnouncementResponse } from "../../models";
import { Announcement } from "../../models";

export class BaseAnnouncementService {
    constructor(protected http: HttpClient) {
    }

    public deleteById(id: number): Observable<any> {
        return this.http.delete(`api/announcements/${id}`);
    }

    public open(id: number): Observable<any> {
        return this.http.patch(`api/announcements/${id}/open`, {});
    }

    public close(id: number): Observable<any> {
        return this.http.patch(`api/announcements/${id}/close`, {});
    }

    public createResponse(id: number, contactInfo: CreateContactInfoCommand): Observable<any> {
        console.log(contactInfo);
        return this.http.post(`api/announcements/${id}/responses`, {
            contactInformation: contactInfo
        });
    }

    public removeMyResponse(id: number) {
        return this.http.delete(`api/announcements/${id}/responses/mine`, {});
    }

    public getResponses(id: number, page: number, pageLength: number, prompt?: string | null): Observable<PagingInfo<AnnouncementResponse>> {
        let params: Params = {
            page: page,
            pageLength: pageLength
        };

        if (prompt != null && prompt != undefined && prompt.length > 0)
            params['prompt'] = prompt ? prompt : null;

        return this.http.get<PagingInfo<AnnouncementResponse>>(`api/announcements/${id}/responses`, { params: params });
    }

    public myResponseExistsForAnnouncement(id: number): Observable<boolean> {
        return this.http.get<boolean>(`api/announcements/${id}/responses/mine/exists`, {});
    }

    public getMyResponses(page: number, pageLength: number, prompt?: string | null): Observable<PagingInfo<AnnouncementResponse>> {
        let params: Params = {
            page: page,
            pageLength: pageLength
        };

        if (prompt != null && prompt != undefined && prompt.length > 0)
            params['prompt'] = prompt ? prompt : null;

        return this.http.get<PagingInfo<AnnouncementResponse>>(`api/announcements/responses/mine`, { params: params });
    }

    public getMyAnnouncements(page: number, pageLength: number, prompt?: string | null): Observable<PagingInfo<Announcement>> {
        let params: Params = {
            page: page,
            pageLength: pageLength
        };

        if (prompt != null && prompt != undefined && prompt.length > 0)
            params['prompt'] = prompt ? prompt : null;

        return this.http.get<PagingInfo<Announcement>>(`api/announcements/mine`, { params: params });
    }

    public createAnnouncementGroup(name: string): Observable<any> {
        return this.http.post(`api/announcements/groups`, { name: name });
    }

    public deleteAnnouncementGroup(name: string): Observable<any> {
        return this.http.delete(`api/announcements/groups`, { params: {
            name: name
        }});
    }

    public addAnnouncementGroup(id: number, groupId: number): Observable<any>
    {
        return this.http.post(`api/announcements/${id}/groups/${groupId}`, {});
    }

    public removeAnnouncementGroup(id: number, groupId: number): Observable<any> {
        return this.http.delete(`api/announcements/${id}/groups/${groupId}`);
    }

    public getGroupsOfAnnouncementById(id: number): Observable<AnnouncementGroup[]> {
        return this.http.get<AnnouncementGroup[]>(`api/announcements/${id}/groups/added`);
    }

    public getGroupsAvailableById(id: number): Observable<AnnouncementGroup[]> {
        return this.http.get<AnnouncementGroup[]>(`api/announcements/${id}/groups/available`);
    }

    public groupByNameExists(name: string): Observable<boolean> {
        return this.http.get<boolean>(`api/announcements/groups/exists`, { params: {
            name: name
        }});
    }

    public getAnnouncementById(id: number): Observable<Announcement> {
        return this.http.get<Announcement>(`api/announcements/${id}`);
    }

    public getAnnouncementGroups(): Observable<AnnouncementGroup[]> {
        return this.http.get<AnnouncementGroup[]>(`api/announcements/groups`);
    }
}