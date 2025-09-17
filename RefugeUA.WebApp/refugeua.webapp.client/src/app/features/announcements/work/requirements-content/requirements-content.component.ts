import { Component, Input, OnInit } from '@angular/core';
import { Renderer2, ElementRef } from '@angular/core';

interface RequirementsSubSection{
  heading: string,
  requirements: string[]
}

@Component({
  selector: 'app-requirements-content',
  standalone: false,
  templateUrl: './requirements-content.component.html',
  styleUrl: './requirements-content.component.css'
})
export class RequirementsContentComponent implements OnInit {
  @Input({ required: true }) requirementsContent: string = '';
  @Input() listClass: string = '';
  @Input() listItemClass: string = '';

  requirements: RequirementsSubSection[] = [];


  constructor(private renderer: Renderer2, private el: ElementRef) {}

  ngOnInit(): void {
    let lines = this.requirementsContent.
    split('\n').filter(s => s != '').
    map(s => s.trim());

    let currentSubSection: RequirementsSubSection = {heading: '', requirements: []};

    if(lines[0].endsWith(':')) {
      currentSubSection.heading = lines[0];
    }
    else {
      currentSubSection.requirements.push(lines[0]);
    }

    for(let i = 1; i < lines.length; ++i)
    {
      if(lines[i].endsWith(':')) {
        this.requirements.push(currentSubSection);
        currentSubSection = {
          heading: lines[i],
          requirements: []
        };
      }
      else {
        currentSubSection.requirements.push(lines[i]);
      }
    }

    this.requirements.push(currentSubSection);
  }
}
