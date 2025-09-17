import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-content',
  standalone: true,
  templateUrl: './content.component.html',
  styleUrl: './content.component.css'
})
export class ContentComponent implements OnInit {
  @Input({required: true}) content: string = '';
  @Input() paragraphClass: string = '';

  paragraphs: string[] = [];

  ngOnInit(): void {
    this.paragraphs = this.content.split('\n').filter(s => s != '') ?? [];
  }
}
