import { Component, Input } from '@angular/core';
import { VolunteerEvent } from '../../../../core/models';
import { VolunteerEventTypeToStringPipe } from '../../../../shared/pipes/volunteer-event-type-to-string.pipe';
import { Router, ActivatedRoute } from '@angular/router';
import { shortenContent } from '../../../../shared/util/content-shortening';

@Component({
  selector: 'app-volunteer-event-preview',
  standalone: false,
  templateUrl: './volunteer-event-preview.component.html',
  styleUrl: './volunteer-event-preview.component.css'
})
export class VolunteerEventPreviewComponent {
  @Input({ required: true }) volunteerEvent?: VolunteerEvent;


  constructor(private router: Router, private route: ActivatedRoute) {

  }

  public get shortContent(): string {
    return shortenContent(this.volunteerEvent?.content!, 200);
  }

  onPreviewClick() {
    this.router.navigate([`${this.volunteerEvent?.id}`, "detail"], { relativeTo: this.route });
  }
}
