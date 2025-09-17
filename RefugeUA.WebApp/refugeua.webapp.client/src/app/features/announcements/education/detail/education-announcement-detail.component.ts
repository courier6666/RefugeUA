import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { EducationAnnouncement } from '../../../../core/models';
import { AnnouncementEducationService } from '../../../../core/services/announcements/education/announcement-education-service';
import { Subscription } from 'rxjs';
import { AuthService } from '../../../../core/services/auth/auth-service';
import { BaseAnnouncementDetail } from '../../base-announcement/detail/base-announcement-detail';
import { Announcement } from '../../../../core/models/announcement';

@Component({
  selector: 'app-education-announcement-detail',
  standalone: false,
  templateUrl: './education-announcement-detail.component.html',
  styleUrl: './education-announcement-detail.component.css'
})
export class EducationAnnouncementDetailComponent extends BaseAnnouncementDetail implements OnInit {
  educationAnnouncement?: EducationAnnouncement;
  @ViewChild('deletionConfirmation') deletionConfirmationRef!: ElementRef<HTMLDivElement>;
  isDeleted: boolean = false;
  isAuthenticated: boolean = false;
  isAuthenticatedSubscription!: Subscription;
  isAllowedToEdit: boolean = false;

  constructor(private router: Router, private route: ActivatedRoute, private announcementEducationService: AnnouncementEducationService, public authService: AuthService) {
    super(announcementEducationService);
  }

  override getAnnouncement(): Announcement
  {
    return this.educationAnnouncement!;
  }

  onDelete() {
    this.deletionConfirmationRef.nativeElement.style.display = 'flex';
  }

  confirmDeletion() {
    let id = this.educationAnnouncement?.id!;
    this.educationAnnouncement = undefined;
    this.announcementEducationService.deleteById(id).subscribe({
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

    this.announcementEducationService.getEducationAnnouncement(id).subscribe({
      next: (result) => {
        console.log(result);
        this.educationAnnouncement = result;
        console.log(this.educationAnnouncement);
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
