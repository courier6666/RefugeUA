import { Component, Input } from '@angular/core';
import { Address } from '../../../core/models';

@Component({
  selector: 'app-address',
  standalone: true,
  templateUrl: './address.component.html',
  styleUrl: './address.component.css'
})
export class AddressComponent {
  @Input({required: true}) address?: Address;
}
