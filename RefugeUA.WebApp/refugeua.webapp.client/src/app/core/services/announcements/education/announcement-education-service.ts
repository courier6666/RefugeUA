import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, of, delay, map, catchError, throwError } from "rxjs";
import { EducationAnnouncementQuery } from "../../../queries/education-announcement-query";
import { EducationAnnouncement } from "../../../models";
import { IsValueNullNanOrUndefined } from "../../../../shared/util/value-not-null-undefined-or-nan-checker";
import { PagingInfo } from "../../../../shared/util/paging-model";
import { CreateEducationAnnouncementCommand } from "../../../api-models/create-education-announcement-command";
import { BaseAnnouncementService } from "../base-announcement-services";
import { educationTargetGroups } from "../../../constants/target-groups";
import { educationTypes } from "../../../constants/education-types";
import { TargetGroup } from "../../../frontend-only-models/target-group";
import { EducationType } from "../../../frontend-only-models/education-type";
import { TimeSpan } from "../../../../shared/util/time-span";
import { catchErrorForFormMethod } from "../../../../shared/util/form-error-capture-method";

@Injectable({
    providedIn: 'root'
})
export class AnnouncementEducationService extends BaseAnnouncementService {
    constructor(http: HttpClient) {
        super(http);
    }

    getTargetGroups(): Observable<TargetGroup[]> {
        return of(educationTargetGroups);
    }

    getEducationTypes(): Observable<EducationType[]> {
        return of(educationTypes);
    }

    createEducationAnnouncement(createEducationAnnouncementCommand: CreateEducationAnnouncementCommand) {
        return this.http.post('api/announcements/education', createEducationAnnouncementCommand).pipe(map(res => {
            return res;
        }),
            catchError(catchErrorForFormMethod));
    }

    editEducationAnnouncement(id: number, editEducationAnnouncementCommand: CreateEducationAnnouncementCommand) {
        return this.http.put(`api/announcements/education/${id}`, editEducationAnnouncementCommand).pipe(map(res => {
            return res;
        }),
            catchError(catchErrorForFormMethod));
    }

    educationAnnouncementWithTargetGroupsMapping(announcement: EducationAnnouncement) {
        if (announcement.targetGroups && announcement.targetGroups.length > 0) {
            announcement.targetGroups = announcement.targetGroups[0]
                .split(';')
                .map(group => group.trim())
                .filter(group => group.length > 0);
        } else {
            announcement.targetGroups = [];
        }
    }


    getEducationAnnouncements(query: EducationAnnouncementQuery): Observable<PagingInfo<EducationAnnouncement>> {

        const params: any = {};
        for (const key of Object.keys(query)) {
            const value = (query as any)[key];
            if (value !== undefined && value !== null) {
                params[key] = value;
            }
        }

        return this.http.get<PagingInfo<EducationAnnouncement>>("api/announcements/education/paged", { params: params }).
            pipe(map(res => {
                res.items.forEach(item => {
                    this.educationAnnouncementWithTargetGroupsMapping(item);
                })

                return res;
            }));
    }

    getEducationAnnouncement(id: number): Observable<EducationAnnouncement> {
        return this.http.get<EducationAnnouncement>(`api/announcements/education/${id}`).
            pipe(map(res => {
                this.educationAnnouncementWithTargetGroupsMapping(res);
                return res;
            }));;
    }
}