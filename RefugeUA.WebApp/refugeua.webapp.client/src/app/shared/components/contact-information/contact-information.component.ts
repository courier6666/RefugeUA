import { Component, Input } from '@angular/core';
import { ContactInformation } from '../../../core/models';

@Component({
  selector: 'app-contact-information',
  standalone: true,
  templateUrl: './contact-information.component.html',
  styleUrl: './contact-information.component.css'
})
export class ContactInformationComponent {
  @Input({required: true}) contactInformation: ContactInformation = {id: 0, phoneNumber: ''};
}
