import { Component, Input } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { PsychologistInformation } from '../../../../core/models';

@Component({
  selector: 'app-specialists-info-preview',
  standalone: false,
  templateUrl: './specialists-info-preview.component.html',
  styleUrl: './specialists-info-preview.component.css'
})
export class SpecialistsInfoPreviewComponent {
  @Input({required: true}) psychologistInformation?: PsychologistInformation;

  constructor(private router: Router, private route: ActivatedRoute)
  {
    
  }

  public get shortContent(): string {
    if(this.psychologistInformation?.description.length! > 200)
    {
      return this.psychologistInformation?.description.substring(0, 200) + '...';
    }

    return this.psychologistInformation?.description!;
  }

  onPreviewClick() {
    this.router.navigate([`${this.psychologistInformation?.id}`, "detail"], {relativeTo: this.route});
  }
}
