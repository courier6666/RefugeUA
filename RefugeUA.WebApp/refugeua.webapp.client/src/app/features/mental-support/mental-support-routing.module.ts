import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MentalSupportArticleComponent } from './article/mental-support-article.component';
import { SpecialistsInfoComponent } from './specialists-info/specialists-info.component';
import { MentalSupportArticleDetailComponent } from './article/detail/mental-support-article-detail.component';
import { SpecialistsInfoDetailComponent } from './specialists-info/detail/specialists-info-detail.component';
import { MentalSuppportArticleCreateEditComponent } from './article/create-edit/mental-suppport-article-create-edit.component';
import { SpecialistsInfoCreateEditComponent } from './specialists-info/create-edit/specialists-info-create-edit.component';

const routes: Routes = [
  { path: 'articles', component: MentalSupportArticleComponent },
  { path: 'specialists-infos', component: SpecialistsInfoComponent},
  { path: 'articles/:id/detail', component: MentalSupportArticleDetailComponent},
  { path: 'articles/:id/edit', component: MentalSuppportArticleCreateEditComponent },
  { path: 'specialists-infos/:id/detail', component: SpecialistsInfoDetailComponent},
  { path: 'articles/create', component: MentalSuppportArticleCreateEditComponent },
  { path: 'specialists-infos/create', component: SpecialistsInfoCreateEditComponent},
  { path: 'specialists-infos/:id/edit', component: SpecialistsInfoCreateEditComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MentalSupportRoutingModule { }
