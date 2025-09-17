import { Component, Input } from '@angular/core';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { AccomodationAnnouncement } from '../../../../core/models';
import { shortenContent } from '../../../../shared/util/content-shortening';

@Component({
  selector: 'app-announcement-accomodation-preview',
  standalone: false,
  templateUrl: './announcement-accomodation-preview.component.html',
  styleUrl: './announcement-accomodation-preview.component.css'
})
export class AnnouncementAccomodationPreviewComponent {
  @Input({ required: true }) accomodationAnnouncement?: AccomodationAnnouncement;

  constructor(private router: Router, private route: ActivatedRoute) {

  }

  public get shortContent(): string {
    return shortenContent(this.accomodationAnnouncement?.content!, 200);
  }

  onPreviewClick() {
    this.router.navigate([`${this.accomodationAnnouncement?.id}`, "detail"], { relativeTo: this.route });
  }
}
