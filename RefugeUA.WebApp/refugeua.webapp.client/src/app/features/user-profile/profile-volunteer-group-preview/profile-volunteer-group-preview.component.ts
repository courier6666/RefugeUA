import { Component, Input } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { shortenContent } from '../../../shared/util/content-shortening';
import { VolunteerGroup } from '../../../core/models';

@Component({
  selector: 'app-profile-volunteer-group-preview',
  standalone: false,
  templateUrl: './profile-volunteer-group-preview.component.html',
  styleUrl: './profile-volunteer-group-preview.component.css'
})
export class ProfileVolunteerGroupPreviewComponent {
  @Input({ required: true }) volunteerGroup?: VolunteerGroup;

  constructor(private router: Router, private route: ActivatedRoute){

  }

  get shortContent(): string {
    return shortenContent(this.volunteerGroup?.descriptionContent!, 400);
  }
}
