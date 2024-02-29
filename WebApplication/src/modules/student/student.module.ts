import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { CommonBootstrapModule } from 'src/infrastructure/common-bootstrap.module';
import { StudentSearchComponent } from './components/student-search/student-search.component';
import { StudentViewCreditComponent } from './components/student-view/parts/student-view-credit/student-view-credit.component';
import { StudentViewInfoComponent } from './components/student-view/parts/student-view-info/student-view-info.component';
import { StudentViewPersonDoctoralComponent } from './components/student-view/parts/student-view-person-doctoral/student-view-person-doctoral.component';
import { StudentViewPersonStudentComponent } from './components/student-view/parts/student-view-person-student/student-view-person-student.component';
import { StudentViewComponent } from './components/student-view/student-view.component';
import { StudentResource } from './resources/student.resource';
import { StudentRoutingModule } from './student-routing.module';

@NgModule({
  declarations: [
    StudentSearchComponent,
    StudentViewComponent,
    StudentViewInfoComponent,
    StudentViewPersonStudentComponent,
    StudentViewPersonDoctoralComponent,
    StudentViewCreditComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    CommonBootstrapModule,
    StudentRoutingModule,
    TranslateModule
  ],
  providers: [
    StudentResource
  ]
})
export class StudentModule { }
