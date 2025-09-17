import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { Announcement, AnnouncementGroup } from '../../../core/models';
import { AnnouncementService } from '../../../core/services/announcements/announcement-service';
import { groupShouldNotExist } from '../../../shared/validators/group-should-not-exist-validator';
import { groupShouldExist } from '../../../shared/validators/group-shoud-exist-validator';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-announcement-groups',
  standalone: false,
  templateUrl: './announcement-groups.component.html',
  styleUrl: './announcement-groups.component.css'
})
export class AnnouncementGroupsComponent implements OnInit {
  announcement!: Announcement;
  announcementLoadedSuccess?: boolean;
  createAnnouncementGroupFrom!: FormGroup;
  deleteAnnouncementGroupFrom!: FormGroup;

  currentAddedGroups?: AnnouncementGroup[] = [];
  currentToAddGroups?: AnnouncementGroup[] = [{ id: 1, name: 'Від представників громади' }, { id: 2, name: 'Від міської ради' }];

  addedGroupsLoadedStatus?: boolean = undefined;
  toAddGroupsLoadedStatus?: boolean = undefined;

  addGroupStatusSuccess?: boolean;
  removeGroupStatusSuccess?: boolean;

  createGroupStatusSuccess?: boolean | null = null;
  deleteGroupStatusSuccess?: boolean | null = null;

  isCreateFormSubmitted: boolean = false;
  isDeleteFormSubmitted: boolean = false;

  constructor(private route: ActivatedRoute, private fb: FormBuilder, private announcementService: AnnouncementService) {

  }

  ngOnInit(): void {

    let id = +this.route.snapshot.paramMap.get('id')!;

    this.announcementService.getAnnouncementById(id).subscribe({
      next: (res) => {
        this.announcement = res;
        this.loadAddedGroups();
        this.loadAvailableGroups();
        this.announcementLoadedSuccess = true;
      },
      error: (err) => {
        this.announcementLoadedSuccess = false;
      }
    });

    this.createAnnouncementGroupFrom = this.fb.group({
      name: new FormControl<string | null>(null, {
        validators: [Validators.required, Validators.maxLength(100)],
        asyncValidators: [groupShouldNotExist(this.announcementService)]
      }),
    });

    this.deleteAnnouncementGroupFrom = this.fb.group({
      name: new FormControl<string | null>(null, {
        validators: [Validators.required, Validators.maxLength(100)],
        asyncValidators: [groupShouldExist(this.announcementService)]
      }),
    });

    this.createAnnouncementGroupFrom.valueChanges.subscribe({
      next: () => this.isCreateFormSubmitted = false
    });

    this.deleteAnnouncementGroupFrom.valueChanges.subscribe({
      next: () => this.isDeleteFormSubmitted = false
    });
  }

  loadAddedGroups() {
    this.addedGroupsLoadedStatus = undefined;
    this.announcementService.getGroupsOfAnnouncementById(this.announcement.id!).subscribe({
      next: (res) => {
        this.currentAddedGroups = res;
        this.addedGroupsLoadedStatus = true;
      },
      error: (err) => {
        this.addedGroupsLoadedStatus = false;
      }
    });
  }

  loadAvailableGroups() {
    this.toAddGroupsLoadedStatus = undefined;
    this.announcementService.getGroupsAvailableById(this.announcement.id!).subscribe({
      next: (res) => {
        this.currentToAddGroups = res;
        this.toAddGroupsLoadedStatus = true;
      },
      error: (err) => {
        this.toAddGroupsLoadedStatus = false;
      }
    });
  }

  onGroupAdd(id: number) {
    this.announcementService.addAnnouncementGroup(this.announcement.id!, id).subscribe({
      next: (res) => {
        this.loadAddedGroups();
        this.loadAvailableGroups();
        this.addGroupStatusSuccess = true;
      },
      error: (err) => {
        this.addGroupStatusSuccess = false;
      }
    });
  }

  onGroupRemove(id: number) {
    this.announcementService.removeAnnouncementGroup(this.announcement.id!, id).subscribe({
      next: (res) => {
        this.loadAddedGroups();
        this.loadAvailableGroups();
        this.removeGroupStatusSuccess = true;
      },
      error: (err) => {
        this.removeGroupStatusSuccess = false;
      }
    });
  }

  onGroupCreate() {
    this.isCreateFormSubmitted = true;
    let formValue = this.createAnnouncementGroupFrom.value;
    this.createGroupStatusSuccess = null;
    this.announcementService.createAnnouncementGroup(formValue.name).subscribe({
      next: (res) => {
        this.loadAddedGroups();
        this.loadAvailableGroups();
        this.createGroupStatusSuccess = true;
      },
      error: (err) => {
        this.createGroupStatusSuccess = false;
      }
    });
  }

  onGroupDelete() {
    this.isDeleteFormSubmitted = true;
    let formValue = this.deleteAnnouncementGroupFrom.value;
    this.deleteGroupStatusSuccess = null;
    this.announcementService.deleteAnnouncementGroup(formValue.name).subscribe({
      next: (res) => {
        this.loadAddedGroups();
        this.loadAvailableGroups();
        this.deleteGroupStatusSuccess = true;
      },
      error: (err) => {
        this.deleteGroupStatusSuccess = false;
      }
    });
  }

  get createName() {
    return this.createAnnouncementGroupFrom.get('name');
  }

  get deleteName () {
    return this.deleteAnnouncementGroupFrom.get('name');
  }
}
