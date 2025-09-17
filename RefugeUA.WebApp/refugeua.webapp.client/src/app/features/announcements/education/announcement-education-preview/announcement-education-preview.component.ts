import { Component, Input } from '@angular/core';
import { EducationAnnouncement } from '../../../../core/models';
import { Router, ActivatedRoute } from '@angular/router';
import { shortenContent } from '../../../../shared/util/content-shortening';

@Component({
  selector: 'app-announcement-education-preview',
  standalone: false,
  templateUrl: './announcement-education-preview.component.html',
  styleUrl: './announcement-education-preview.component.css'
})
export class AnnouncementEducationPreviewComponent {
  @Input({required: true}) educationAnnouncement?: EducationAnnouncement;

  constructor(private router: Router, private route: ActivatedRoute)
  {
    
  }

    public get shortContent(): string {
      return shortenContent(this.educationAnnouncement?.content!, 200);
    }

  onPreviewClick() {
    this.router.navigate([`${this.educationAnnouncement?.id}`, "detail"], {relativeTo: this.route});
  }
}
