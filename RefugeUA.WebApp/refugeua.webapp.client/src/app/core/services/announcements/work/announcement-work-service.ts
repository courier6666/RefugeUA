import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { WorkCategory } from "../../../models";
import { Observable, of, delay, map, catchError, throwError } from "rxjs";
import { WorkAnnouncementQuery } from "../../../queries/work-announcement-query";
import { WorkAnnouncement } from "../../../models";
import { IsValueNullNanOrUndefined } from "../../../../shared/util/value-not-null-undefined-or-nan-checker";
import { PagingInfo } from "../../../../shared/util/paging-model";
import { CreateWorkAnnouncementCommand } from "../../../api-models/create-work-announcement-command";
import { BaseAnnouncementService } from "../base-announcement-services";
import { catchErrorForFormMethod } from "../../../../shared/util/form-error-capture-method";

@Injectable({
    providedIn: 'root'
})
export class AnnouncementWorkService extends BaseAnnouncementService {
    constructor(http: HttpClient) {
        super(http);
    }

    getCategories(): Observable<WorkCategory[]> {
        return this.http.get<WorkCategory[]>("api/announcements/work/categories");
    }

    createWorkAnnouncement(createWorkAnnouncementCommand: CreateWorkAnnouncementCommand) {
        return this.http.post('api/announcements/work', createWorkAnnouncementCommand).pipe(map(res => {
            return res;
        }),
            catchError(catchErrorForFormMethod));
    }

    editWorkAnnouncement(id: number, editWorkAnnouncementCommand: CreateWorkAnnouncementCommand) {
        return this.http.put(`api/announcements/work/${id}`, editWorkAnnouncementCommand).pipe(map(res => {
            return res;
        }),
            catchError(catchErrorForFormMethod));
    }

    getWorkAnnouncements(query: WorkAnnouncementQuery): Observable<PagingInfo<WorkAnnouncement>> {

        const params: any = {};
        for (const key of Object.keys(query)) {
            const value = (query as any)[key];
            if (value !== undefined && value !== null) {
                params[key] = value;
            }
        }

        return this.http.get<PagingInfo<WorkAnnouncement>>("api/announcements/work/paged", { params: params });
    }

    getWorkAnnouncement(id: number): Observable<WorkAnnouncement> {
        return this.http.get<WorkAnnouncement>(`api/announcements/work/${id}`);
    }
}