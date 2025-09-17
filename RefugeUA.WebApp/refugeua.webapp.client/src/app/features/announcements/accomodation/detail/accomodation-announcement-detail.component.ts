import { Component, ViewChild, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AccomodationAnnouncement, Announcement } from '../../../../core/models';
import { ElementRef } from '@angular/core';
import { Subscription } from 'rxjs';
import { AnnouncementAccomodationService } from '../../../../core/services/announcements/accomodation/announcement-accomodation-service';
import { AuthService } from '../../../../core/services/auth/auth-service';
import { BaseAnnouncementDetail } from '../../base-announcement/detail/base-announcement-detail';
import { FileService } from '../../../../core/services/util/file-service';

@Component({
  selector: 'app-accomodation-announcement-detail',
  standalone: false,
  templateUrl: './accomodation-announcement-detail.component.html',
  styleUrl: './accomodation-announcement-detail.component.css'
})
export class AccomodationAnnouncementDetailComponent extends BaseAnnouncementDetail implements OnInit {
  accomodationAnnouncement?: AccomodationAnnouncement;
  @ViewChild('deletionConfirmation') deletionConfirmationRef!: ElementRef<HTMLDivElement>;
  isDeleted: boolean = false;
  isAuthenticated: boolean = false;
  isAuthenticatedSubscription!: Subscription;
  isAllowedToEdit: boolean = false;


  constructor(private router: Router,
    private route: ActivatedRoute,
    private announcementAccomodationService: AnnouncementAccomodationService,
    public authService: AuthService,
    public fileService: FileService) {
    super(announcementAccomodationService);
  }

  override getAnnouncement(): Announcement
  {
    return this.accomodationAnnouncement!;
  }

  onDelete() {
    this.deletionConfirmationRef.nativeElement.style.display = 'flex';
  }
  confirmDeletion() {
    let id = this.accomodationAnnouncement?.id!;
    this.accomodationAnnouncement = undefined;
    this.announcementAccomodationService.deleteById(id).subscribe({
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
      next: (val) => {
        this.isAuthenticated = val;
        this.authService.isAllowedToEditAnnouncement(id).subscribe({
          next: (res) => this.isAllowedToEdit = res
        });
      }
    });

    this.announcementAccomodationService.getAccomodationAnnouncement(id).subscribe({
      next: (result) => {
        console.log(result);
        this.accomodationAnnouncement = result;
        console.log(this.accomodationAnnouncement);
      },
      error: (err) => console.log(err)
    });
  }

  ngOnDestroy(): void {
    if (this.isAuthenticatedSubscription) {
      this.isAuthenticatedSubscription.unsubscribe();
    }
  }

}
