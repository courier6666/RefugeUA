import { Component, Input } from '@angular/core';
import { User } from '../../../core/models';
import { Router, ActivatedRoute, RouterModule } from '@angular/router';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-user-preview',
  standalone: true,
  templateUrl: './user-preview.component.html',
  styleUrl: './user-preview.component.css',
  imports: [RouterModule, DatePipe]
})
export class UserPreviewComponent {
  @Input({ required: true }) user!: User;
  @Input({ required: true }) isAllowedToRemove!: boolean;
  @Input({ required: true}) onUserRemoved!: (id: number) => void;

  acceptUserStateSuccess?: boolean;
  isAcceptLoading: boolean = false;
  confirmEmailUserStateSuccess?: boolean;
  isConfirmEmailLoading: boolean = false;

  constructor(private router: Router, private route: ActivatedRoute) {

  }

  ngOnInit(): void {

  }

  onRemoveUser() {
    this.onUserRemoved(this.user.id!);
  }
}
