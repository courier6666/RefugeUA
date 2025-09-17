import { Component, Input } from '@angular/core';
import { VolunteerGroup } from '../../../../core/models';
import { ActivatedRoute, Router } from '@angular/router';
import { shortenContent } from '../../../../shared/util/content-shortening';

@Component({
  selector: 'app-volunteer-group-preview',
  standalone: false,
  templateUrl: './volunteer-group-preview.component.html',
  styleUrl: './volunteer-group-preview.component.css'
})
export class VolunteerGroupPreviewComponent {
  @Input({ required: true }) volunteerGroup?: VolunteerGroup;

  constructor(private router: Router, private route: ActivatedRoute){

  }

  onPreviewClick() {
    this.router.navigate([`${this.volunteerGroup?.id}`, "detail"], { relativeTo: this.route });
  }

  get shortContent(): string {
    return shortenContent(this.volunteerGroup?.descriptionContent!, 400);
  }
}
