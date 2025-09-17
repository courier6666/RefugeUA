import { Component, Input } from '@angular/core';
import { Address } from '../../../core/models';

@Component({
  selector: 'app-address-detail',
  standalone: true,
  templateUrl: './address-detail.component.html',
  styleUrl: './address-detail.component.css'
})
export class AddressDetailComponent {
  @Input({ required: true }) address!: Address;

}
