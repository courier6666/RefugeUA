import { BaseAnnouncementService } from "./base-announcement-services";
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";

@Injectable({
    providedIn: 'root'
})
export class AnnouncementService extends BaseAnnouncementService
{
    constructor(http: HttpClient) {
        super(http);
    }
}