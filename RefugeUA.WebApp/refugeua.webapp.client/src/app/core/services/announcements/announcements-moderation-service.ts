import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Announcement } from '../../models';
import { Observable } from 'rxjs';
import { Params } from '@angular/router';
import { PagingInfo } from '../../../shared/util/paging-model';
import { BaseAnnouncementService } from './base-announcement-services';

@Injectable({
    providedIn: 'root'
})
export class AnnouncementModerationService extends BaseAnnouncementService {
    constructor(http: HttpClient) {
        super(http);
    }

    public getAnnouncementsForModeration(page: number, pageLength: number, prompt?: string | null): Observable<PagingInfo<Announcement>> {
        let params: Params = {
            page: page,
            pageLength: pageLength
        };

        if (prompt != null && prompt != undefined && prompt.length > 0)
            params['prompt'] = prompt ? prompt : null;

        return this.http.get<PagingInfo<Announcement>>(`api/admin/announcements/moderation`, { params: params });
    }

    public acceptAnnouncement(id: number): Observable<any> {
        return this.http.patch(`api/admin/announcements/${id}/moderation/accept`, {});
    }

    public rejectAnnouncement(id: number): Observable<any> {
        return this.http.patch(`api/admin/announcements/${id}/moderation/reject`, {});
    }

    public setNonAcceptenceReason(id: number, reason: string): Observable<any> {
        return this.http.patch(`api/admin/announcements/${id}/moderation/non-acceptance-reason`, { reason: reason} );
    }
}