import { Announcement } from "../../../../core/models";
import { BaseAnnouncementService } from "../../../../core/services/announcements/base-announcement-services";
import { Roles } from "../../../../core/constants/user-roles-constants";
import { Subscription } from "rxjs";

export abstract class BaseAnnouncementDetail {

    openSubscription!: Subscription;
    closeSubscription!: Subscription;

    public Roles = Roles;
    constructor(public baseAnnouncementService: BaseAnnouncementService) {
    }

    abstract getAnnouncement(): Announcement;

    onOpen(): void {

        if (this.openSubscription) {
            this.openSubscription.unsubscribe();
        }

        this.openSubscription = this.baseAnnouncementService.open(this.getAnnouncement().id!).subscribe({
            next: () => {
                this.getAnnouncement().isClosed = false;
            },
            error: (error) => {
                console.error("Error opening announcement:", error);
            }
        });
    }

    onClosed(): void {
        if (this.closeSubscription) {
            this.closeSubscription.unsubscribe();
        }

        this.closeSubscription = this.baseAnnouncementService.close(this.getAnnouncement().id!).subscribe({
            next: () => {
                this.getAnnouncement().isClosed = true;
            },
            error: (error) => {
                console.error("Error closing announcement:", error);
            }
        });
    }
}