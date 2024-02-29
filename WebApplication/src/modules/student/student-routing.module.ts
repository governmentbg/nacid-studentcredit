import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StudentSearchComponent } from './components/student-search/student-search.component';
import { AuthGuard } from 'src/infrastructure/guards/auth-guard';

const routes: Routes = [
  {
    path: 'student/search',
    component: StudentSearchComponent,
    canActivate: [AuthGuard],
    data: { breadcrumb: 'Справка за студент' }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class StudentRoutingModule { }
