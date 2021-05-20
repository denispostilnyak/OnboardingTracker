import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CandidatesComponent } from './candidates/candidates.component';
import { AuthGuard } from './guards/auth.guard';
import { HomeComponent } from './home/home.component';
import { RecruitersComponent } from './recruiters/recruiters.component';
import { VacanciesComponent } from './vacancies/vacancies.component';

const routes: Routes = [
  { path: '', component: HomeComponent, data:{breadcrumb:"Home"}, pathMatch: 'full' },
  {
    path:'', data:{breadcrumb:"Home"}, children:[
      {
        path: 'vacancies',
        component: VacanciesComponent,
        pathMatch: 'full',
        canActivate: [AuthGuard],
        data:{breadcrumb:"Vacancies", createButtonCaption: "Create Vacancy"}
      },
      {
        path: 'recruiters',
        component: RecruitersComponent,
        pathMatch: 'full',
        canActivate: [AuthGuard],
        data:{breadcrumb:"Recruiters", createButtonCaption: "Create Recruiter"}
      },
      {
        path: 'candidates',
        component: CandidatesComponent,
        pathMatch: 'full',
        canActivate: [AuthGuard],
        data:{breadcrumb:"Candidates", createButtonCaption: "Create Candidate"}

      },
    ]
  }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
