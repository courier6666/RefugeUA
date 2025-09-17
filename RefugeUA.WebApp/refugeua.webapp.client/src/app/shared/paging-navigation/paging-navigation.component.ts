import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-paging-navigation',
  standalone: true,
  templateUrl: './paging-navigation.component.html',
  styleUrl: './paging-navigation.component.css'
})
export class PagingNavigationComponent {
  @Input({ required: true }) pagesCount: number = 0;
  @Input({ required: true }) onNavigationCallback: (page: number) => void = () => { };
  @Input({ required: true }) currentPage: number = 1;
  @Input() currentPageNeighboursDisplayed: number = 1;

  get pages() {
    let pages: number[] = [];

    if (this.currentPage - this.currentPageNeighboursDisplayed > 1) {
      pages.push(1);
    }

    for (let i = Math.max(1, this.currentPage - this.currentPageNeighboursDisplayed); i < this.currentPage; ++i) {
      pages.push(i);
    }

    pages.push(this.currentPage);

    for (let i = this.currentPage + 1; i <= Math.min(this.currentPage + this.currentPageNeighboursDisplayed, this.pagesCount); ++i) {
      pages.push(i);
    }

    if (!pages.includes(this.pagesCount) && this.pagesCount != 0 && this.pagesCount) {
      pages.push(this.pagesCount);
    }

    return pages;
  }
}
