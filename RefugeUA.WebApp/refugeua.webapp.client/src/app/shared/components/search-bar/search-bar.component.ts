import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, ReactiveFormsModule, Validators } from '@angular/forms';
import { IsValueNullNanOrUndefined } from '../../util/value-not-null-undefined-or-nan-checker';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-search-bar',
  standalone: true,
  templateUrl: './search-bar.component.html',
  styleUrl: './search-bar.component.css',
  imports: [ReactiveFormsModule]
})
export class SearchBarComponent implements OnInit {
  @Input() onSearchBarSearch!: (prompt: string | null) => void;
  @Input() placeholder: string = "Пошук...";

  searchForm!: FormGroup;

  constructor(private route: ActivatedRoute) {

  }

  onSubmit() {
    if (this.searchForm.valid) {
      const searchValue = this.searchForm.get('search')?.value;
      console.log(searchValue);
      if (searchValue) {
        this.onSearchBarSearch(searchValue);
      }
    }
  }

  ngOnInit(): void {
    let formBuilder = new FormBuilder();
    this.searchForm = formBuilder.group({
      search: new FormControl<string | null>(null)
    });

    let queryParams = this.route.snapshot.queryParams;

    if (queryParams['prompt'] && queryParams['prompt'].length != 0) {
      this.searchControl?.setValue(queryParams['prompt']);
    }

    this.searchForm.valueChanges.subscribe((value) => {
      console.log(value.search);
      if (IsValueNullNanOrUndefined(value.search) || value.search.length == 0) {
        console.log(1);
        this.onSearchBarSearch(null);
      }
    });
  }

  get searchControl() {
    return this.searchForm.get('search');
  }
}
