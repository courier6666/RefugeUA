import { Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { PsychologistInformation } from '../../../../core/models';
import { mockPsychologists } from '../../../../shared/util/mock-up-data/psychologist-information-ua';
import { Router, ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { AuthService } from '../../../../core/services/auth/auth-service';
import { PsychologistInformationService } from '../../../../core/services/mental-support/psychologist-information-service';
import { Roles } from '../../../../core/constants/user-roles-constants';

@Component({
  selector: 'app-specialists-info-detail',
  standalone: false,
  templateUrl: './specialists-info-detail.component.html',
  styleUrl: './specialists-info-detail.component.css'
})
export class SpecialistsInfoDetailComponent implements OnInit, OnDestroy {
  Roles = Roles;
  psychologistInformation?: PsychologistInformation;
  @ViewChild('deletionConfirmation') deletionConfirmationRef!: ElementRef<HTMLDivElement>;
  isDeleted: boolean = false;
  isAuthenticated: boolean = false;
  isAuthenticatedSubscription!: Subscription;
  isAllowedToEdit: boolean = false;

  constructor(private router: Router,
    private route: ActivatedRoute,
    private authService: AuthService,
    private psychInfoService: PsychologistInformationService) {

  }

  onDelete() {
    this.deletionConfirmationRef.nativeElement.style.display = 'flex';
  }

  confirmDeletion() {
    let id = this.psychologistInformation?.id!;
    this.psychologistInformation = undefined;
    this.psychInfoService.deletePsychologistInformation(id).subscribe({
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

    this.psychInfoService.getPsychologistInformationById(id).subscribe({
      next: (res) => {
        this.psychologistInformation = res;
        this.isAuthenticatedSubscription = this.authService.authSubject.subscribe({
          next: (val) => {
            this.isAuthenticated = val;
            this.isAllowedToEdit = this.authService.userClaims.id === res.authorId?.toString() || this.authService.userClaims.role == Roles.Admin;
          }
        });
      },
      error: (err) => {
        console.log(err);
      }
    });
  }

  ngOnDestroy(): void {
    if (this.isAuthenticatedSubscription) {
      this.isAuthenticatedSubscription.unsubscribe();
    }
  }
}
