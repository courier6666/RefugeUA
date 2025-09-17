import { Component, OnInit, ViewChild, ElementRef, AfterViewInit } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { filter } from 'rxjs';

@Component({
  selector: 'app-announcement',
  standalone: false,
  templateUrl: './announcement.component.html'
})
export class AnnouncementComponent implements OnInit, AfterViewInit {
  @ViewChild('accomodationNav', { static: false }) accomodationNavButton!: ElementRef;
  @ViewChild('educationNav', { static: false }) educationNavButton!: ElementRef;
  @ViewChild('workNav', { static: false }) workNavButton!: ElementRef;

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
    if (!this.accomodationNavButton || !this.workNavButton || !this.educationNavButton) return;

    this.resetStylesNavButtons();
    if (url.includes('/announcements/education'))
    {
      this.educationNavButton.nativeElement.classList.replace('btn-custom', 'btn-custom-active');
      return;
    }

    if (url.includes('/announcements/work'))
    {
      this.workNavButton.nativeElement.classList.replace('btn-custom', 'btn-custom-active');
      return;
    }

    if (url.includes('/announcements/accomodation'))
    {
      this.accomodationNavButton.nativeElement.classList.replace('btn-custom', 'btn-custom-active');
      return;
    }
  }

  resetStylesNavButtons(): void {
    [this.accomodationNavButton, this.workNavButton, this.educationNavButton].forEach(btn => {
      btn.nativeElement.classList.add('btn-custom');
      btn.nativeElement.classList.remove('btn-custom-active');
    });
  }

  onAnnouncementNavButtonToggle(event: Event): void {
    const navButton = event.target as HTMLElement;
    this.resetStylesNavButtons();
    navButton.classList.replace('btn-custom', 'btn-custom-active');
  }
}
