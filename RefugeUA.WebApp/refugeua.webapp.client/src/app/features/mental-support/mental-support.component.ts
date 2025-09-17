import { Component, OnInit, ViewChild, ElementRef, AfterViewInit } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { filter } from 'rxjs';

@Component({
  selector: 'app-mental-support',
  standalone: false,
  templateUrl: './mental-support.component.html',
  styleUrl: './mental-support.component.css'
})
export class MentalSupportComponent {
  @ViewChild('articlesNav', { static: false }) articlesNavButton!: ElementRef;
  @ViewChild('psychologistContactsNav', { static: false }) psychologistContactsNavButton!: ElementRef;

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
    if (!this.articlesNavButton || !this.psychologistContactsNavButton) return;

    this.resetStylesNavButtons();
    if (url.includes('/mental-support/articles'))
    {
      this.articlesNavButton.nativeElement.classList.replace('btn-custom', 'btn-custom-active');
      return;
    }

    if (url.includes('/mental-support/specialists-info'))
    {
      this.psychologistContactsNavButton.nativeElement.classList.replace('btn-custom', 'btn-custom-active');
      return;
    }
  }

  resetStylesNavButtons(): void {
    [this.articlesNavButton, this.psychologistContactsNavButton].forEach(btn => {
      btn.nativeElement.classList.add('btn-custom');
      btn.nativeElement.classList.remove('btn-custom-active');
    });
  }

  onMentalSupportNavButtonToggle(event: Event): void {
    const navButton = event.target as HTMLElement;
    this.resetStylesNavButtons();
    navButton.classList.replace('btn-custom', 'btn-custom-active');
  }
}
