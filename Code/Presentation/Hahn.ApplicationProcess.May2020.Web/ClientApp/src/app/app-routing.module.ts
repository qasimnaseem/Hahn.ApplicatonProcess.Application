import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ApplicantAddEditComponent } from './components/applicant/applicant-add-edit/applicant-add-edit.component';
import { Shell } from './components/common/shell/shell/shell.service';
import { ApplicantListComponent } from './components/applicant/applicant-list/applicant-list.component';


const routes: Routes = [
  Shell.childRoutes([{ path: '', component: ApplicantListComponent }]),
  Shell.childRoutes([{ path: 'applicant', component: ApplicantAddEditComponent }]),
  Shell.childRoutes([{ path: 'applicant/:id', component: ApplicantAddEditComponent }]),
  Shell.childRoutes([{ path: 'applicants/view', component: ApplicantListComponent }]),
  
  { path: '', redirectTo: '/applicants/view', pathMatch: 'full' },
  { path: '**', redirectTo: '', pathMatch: 'full', component: ApplicantListComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
