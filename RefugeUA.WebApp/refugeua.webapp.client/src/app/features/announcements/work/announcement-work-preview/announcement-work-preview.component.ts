import { Component, Input } from '@angular/core';
import { WorkAnnouncement } from '../../../../core/models';
import { ActivatedRoute, Router } from '@angular/router';
import { shortenContent } from '../../../../shared/util/content-shortening';

@Component({
  selector: 'app-announcement-work-preview',
  standalone: false,
  templateUrl: './announcement-work-preview.component.html',
  styleUrl: './announcement-work-preview.component.css'
})
export class AnnouncementWorkPreviewComponent {
  @Input({ required: true }) workAnnouncement?: WorkAnnouncement;

  constructor(private router: Router, private route: ActivatedRoute) {

  }

  public get shortContent(): string {
    return shortenContent(this.workAnnouncement?.content!, 200);
  }

  onPreviewClick() {
    this.router.navigate([`${this.workAnnouncement?.id}`, "detail"], { relativeTo: this.route });
  }
}
