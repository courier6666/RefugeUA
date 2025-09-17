import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-navigation-menu',
  standalone: false,
  templateUrl: './navigation-menu.component.html',
  styleUrl: './navigation-menu.component.css'
})
export class NavigationMenuComponent implements OnInit {

  previousScroll = 0;
  htmlHeaderElement = document.getElementById('main-nav-header')!;
  constructor() {
    
  }

  ngOnInit(): void {
    window.onscroll = () => this.onScroll();
  }

  onScroll() {

    this.previousScroll = pageYOffset;
  }
}
