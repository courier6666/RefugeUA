import { Component, OnInit, ViewChild, ElementRef, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Announcement, WorkAnnouncement } from '../../../../core/models';
import { AnnouncementWorkService } from '../../../../core/services/announcements/work/announcement-work-service';
import { AuthService } from '../../../../core/services/auth/auth-service';
import { Observable, Subscription } from 'rxjs';
import { BaseAnnouncementDetail } from '../../base-announcement/detail/base-announcement-detail';

@Component({
  selector: 'app-work-announcement-detail',
  standalone: false,
  templateUrl: './work-announcement-detail.component.html',
  styleUrl: './work-announcement-detail.component.css'
})
export class WorkAnnouncementDetailComponent extends BaseAnnouncementDetail implements OnInit, OnDestroy {
  workAnnouncement?: WorkAnnouncement;
  @ViewChild('deletionConfirmation') deletionConfirmationRef!: ElementRef<HTMLDivElement>;
  isDeleted: boolean = false;
  isAuthenticated: boolean = false;
  isAuthenticatedSubscription!: Subscription;
  isAllowedToEdit: boolean = false;

  constructor(private router: Router, private route: ActivatedRoute, private announcementWorkService: AnnouncementWorkService, public authService: AuthService) {
    super(announcementWorkService);
  }

  override getAnnouncement(): Announcement
  {
    return this.workAnnouncement!;
  }

  onDelete() {
    this.deletionConfirmationRef.nativeElement.style.display = 'flex';
  }

  confirmDeletion() {
    let id = this.workAnnouncement?.id!;
    this.workAnnouncement = undefined;
    this.announcementWorkService.deleteById(id).subscribe({
      next: (res) => {
        this.isDeleted = true;
      },
      error: (err) => console.error(err)
    });
  }

  cancelDeletion() {
    this.deletionConfirmationRef.nativeElement.style.display = 'none';
  }

  ngOnInit(): void {

    let routeParams = this.route.snapshot.paramMap;
    let id = +routeParams.get('id')!;

    this.isAuthenticatedSubscription = this.authService.authSubject.subscribe({
      next:(val) => {
        this.isAuthenticated = val;
        this.authService.isAllowedToEditAnnouncement(id).subscribe({
          next: (res) => this.isAllowedToEdit = res
        });
      }
    });

    this.announcementWorkService.getWorkAnnouncement(id).subscribe({
      next: (result) => {
        console.log(result);
        this.workAnnouncement = result;
        console.log(this.workAnnouncement);
      },
      error: (err) => console.log(err)
    });

    console.log(this.workAnnouncement);
  }
  ngOnDestroy(): void {
    if(this.isAuthenticatedSubscription)
    {
      this.isAuthenticatedSubscription.unsubscribe();
    }
  }
}
