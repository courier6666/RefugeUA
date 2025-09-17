import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MentalSupportComponent } from './mental-support.component';
import { MentalSupportArticleComponent } from './article/mental-support-article.component';
import { SpecialistsInfoComponent } from './specialists-info/specialists-info.component';
import { SearchBarComponent } from '../../shared/components/search-bar/search-bar.component';
import { PagingNavigationComponent } from '../../shared/paging-navigation/paging-navigation.component';
import { MentalSupportArticlePreviewComponent } from './article/mental-support-article-preview/mental-support-article-preview.component';
import { MentalSupportRoutingModule } from './mental-support-routing.module';
import { MentalSupportArticleDetailComponent } from './article/detail/mental-support-article-detail.component';
import { ContentComponent } from '../../shared/components/content/content.component';
import { SpecialistsInfoDetailComponent } from './specialists-info/detail/specialists-info-detail.component';
import { SpecialistsInfoPreviewComponent } from './specialists-info/specialists-info-preview/specialists-info-preview.component';
import { ContactInformationComponent } from "../../shared/components/contact-information/contact-information.component";
import { MentalSuppportArticleCreateEditComponent } from './article/create-edit/mental-suppport-article-create-edit.component';
import { ReactiveFormsModule } from '@angular/forms';
import { SpecialistsInfoCreateEditComponent } from './specialists-info/create-edit/specialists-info-create-edit.component';


@NgModule({
  declarations: [
    MentalSupportComponent,
    MentalSupportArticleComponent,
    SpecialistsInfoComponent,
    MentalSupportArticlePreviewComponent,
    MentalSupportArticleDetailComponent,
    SpecialistsInfoPreviewComponent,
    SpecialistsInfoDetailComponent,
    MentalSuppportArticleCreateEditComponent,
    SpecialistsInfoCreateEditComponent
  ],
  imports: [
    MentalSupportRoutingModule,
    CommonModule,
    SearchBarComponent,
    PagingNavigationComponent,
    ContentComponent,
    ContactInformationComponent,
    ReactiveFormsModule
]
})
export class MentalSupportModule { }
