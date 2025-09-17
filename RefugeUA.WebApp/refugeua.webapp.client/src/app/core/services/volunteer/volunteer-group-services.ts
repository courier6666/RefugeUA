import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, pipe, map, catchError } from "rxjs";
import { VolunteerGroup } from "../../models";
import { CreateVolunteerGroupCommand } from "../../api-models/create-volunteer-group-command";
import { VolunteerGroupsQuery } from "../../queries/volunteer-groups-query";
import { Params } from "@angular/router";
import { PagingInfo } from "../../../shared/util/paging-model";
import { catchErrorForFormMethod } from "../../../shared/util/form-error-capture-method";
import { User } from "../../models";

@Injectable({
    providedIn: 'root'
})
export class VolunteerGroupService {
    constructor(private http: HttpClient) {
    }

    getVolunteerGroupPreviews(): Observable<VolunteerGroup[]> {
        return this.http.get<VolunteerGroup[]>("api/volunteer/groups/previews");
    }

    getAvailableVolunteerGroupPreviews(): Observable<VolunteerGroup[]> {
        return this.http.get<VolunteerGroup[]>(`api/volunteer/groups/previews/available`);
    }

    getVolunteerGroupById(id: number) {
        return this.http.get<VolunteerGroup>(`api/volunteer/groups/${id}`);
    }

    getVolunteerGroups(query: VolunteerGroupsQuery): Observable<PagingInfo<VolunteerGroup>> {
        const params: any = {};
        for (const key of Object.keys(query)) {
            const value = (query as any)[key];
            if (value !== undefined && value !== null) {
                params[key] = value;
            }
        }

        return this.http.get<PagingInfo<VolunteerGroup>>("api/volunteer/groups/paged", { params: params });
    }

    getMyVolunteerGroups(page: number, pageLength: number, prompt?: string | null): Observable<PagingInfo<VolunteerGroup>> {
        let params: Params = {
            page: page,
            pageLength: pageLength
        };

        if (prompt != null && prompt != undefined && prompt.length > 0)
            params['prompt'] = prompt ? prompt : null;

        return this.http.get<PagingInfo<VolunteerGroup>>(`api/volunteer/groups/mine/paged`, { params: params });
    }

    getFollowedVolunteerGroups(page: number, pageLength: number, prompt?: string | null): Observable<PagingInfo<VolunteerGroup>> {
        let params: Params = {
            page: page,
            pageLength: pageLength
        };

        if (prompt != null && prompt != undefined && prompt.length > 0)
            params['prompt'] = prompt ? prompt : null;

        return this.http.get<PagingInfo<VolunteerGroup>>(`api/volunteer/groups/followed/paged`, { params: params });
    }

    createVolunteerGroup(command: CreateVolunteerGroupCommand): Observable<any> {
        return this.http.post("api/volunteer/groups", command).pipe(map(res => {
            return res;
        }),
            catchError(catchErrorForFormMethod));
    }

    editVolunteerGroup(id: number, command: CreateVolunteerGroupCommand): Observable<any> {
        return this.http.put(`api/volunteer/groups/${id}`, command).pipe(map(res => {
            return res;
        }),
            catchError(catchErrorForFormMethod));
    }

    deleteById(id: number): Observable<any> {
        return this.http.delete(`api/volunteer/groups/${id}`);
    }

    meFollowingGroup(id: number): Observable<boolean> {
        return this.http.get<boolean>(`api/volunteer/groups/${id}/followers/me/exists`);
    }

    followVolunteerGroup(id: number): Observable<any> {
        return this.http.post(`api/volunteer/groups/${id}/follow`, {});
    }

    leaveVolunteerGroup(id: number): Observable<any> {
        return this.http.delete(`api/volunteer/groups/${id}/leave`, {});
    }

    getVolunteerGroupFollowers(id: number, page: number, pageLength: number, prompt?: string | null): Observable<PagingInfo<User>> {
        let params: Params = {
            page: page,
            pageLength: pageLength
        };

        if (prompt != null && prompt != undefined && prompt.length > 0)
            params['prompt'] = prompt ? prompt : null;

        return this.http.get<PagingInfo<User>>(`api/volunteer/groups/${id}/followers`, { params: params });
    }

    getVolunteerGroupAdmins(id: number, page: number, pageLength: number, prompt?: string | null): Observable<PagingInfo<User>> {
        let params: Params = {
            page: page,
            pageLength: pageLength
        };

        if (prompt != null && prompt != undefined && prompt.length > 0)
            params['prompt'] = prompt ? prompt : null;

        return this.http.get<PagingInfo<User>>(`api/volunteer/groups/${id}/admins`, { params: params });
    }

    removeFollowerFromVolunteerGroup(id: number, userId: number): Observable<any> {
        return this.http.delete(`api/volunteer/groups/${id}/followers/${userId}`);
    }
}