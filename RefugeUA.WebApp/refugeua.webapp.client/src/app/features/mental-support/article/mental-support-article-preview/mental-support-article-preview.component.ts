import { Component, Input } from '@angular/core';
import { MentalSupportArticle } from '../../../../core/models';
import { Router, ActivatedRoute } from '@angular/router';
import { shortenContent } from '../../../../shared/util/content-shortening';

@Component({
  selector: 'app-mental-support-article-preview',
  standalone: false,
  templateUrl: './mental-support-article-preview.component.html',
  styleUrl: './mental-support-article-preview.component.css'
})
export class MentalSupportArticlePreviewComponent {
  @Input({required: true}) mentalSupportArticle?: MentalSupportArticle;

  constructor(private router: Router, private route: ActivatedRoute)
  {
    
  }

  public get shortContent(): string {
    return shortenContent(this.mentalSupportArticle?.content!, 200);
  }

  onPreviewClick() {
    this.router.navigate([`${this.mentalSupportArticle?.id}`, "detail"], {relativeTo: this.route});
  }
}
