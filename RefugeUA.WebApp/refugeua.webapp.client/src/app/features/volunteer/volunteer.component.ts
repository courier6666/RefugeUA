import { Component, OnInit, ViewChild, ElementRef, AfterViewInit } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { filter } from 'rxjs';

@Component({
  selector: 'app-volunteer',
  standalone: false,
  templateUrl: './volunteer.component.html',
  styleUrl: './volunteer.component.css'
})
export class VolunteerComponent {
  @ViewChild('volunteerEventsNav', { static: false }) volunteerEventsNavButton!: ElementRef;
  @ViewChild('volunteerGroupNav', { static: false }) volunteerGroupNavButton!: ElementRef;
  @ViewChild('volunteerCalendarNav', { static: false }) volunteerCalendarNavButton!: ElementRef;

  constructor(private router: Router) {

  }

  ngOnInit(): void {
    this.router.events
      .pipe(filter(event => event instanceof NavigationEnd))
      .subscribe(() => {
        this.highlightActiveNavButton(this.router.url.split('?')[0]);
      });
  }

  ngAfterViewInit(): void {
    this.highlightActiveNavButton(this.router.url.split('?')[0]);
  }

  highlightActiveNavButton(url: string): void {
    if (!this.volunteerEventsNavButton || !this.volunteerGroupNavButton) return;
    console.log(url);
    this.resetStylesNavButtons();

    if (url.includes('/volunteer/events'))
    {
      this.volunteerEventsNavButton.nativeElement.classList.replace('btn-custom', 'btn-custom-active');
      return;
    }

    if (url.includes('/volunteer/groups'))
    {
      this.volunteerGroupNavButton.nativeElement.classList.replace('btn-custom', 'btn-custom-active');
      return;
    }
  }

  resetStylesNavButtons(): void {
    [this.volunteerEventsNavButton, this.volunteerGroupNavButton].forEach(btn => {
      btn.nativeElement.classList.add('btn-custom');
      btn.nativeElement.classList.remove('btn-custom-active');
    });
  }

  onVolunteerNavButtonToggle(event: Event): void {
    const navButton = event.target as HTMLElement;
    this.resetStylesNavButtons();
    navButton.classList.replace('btn-custom', 'btn-custom-active');
  }
}
