import { Component, EventEmitter, Output } from '@angular/core';
import { Subscription } from 'rxjs';
import { finalize } from 'rxjs/operators';
import { LoadingIndicatorService } from 'src/infrastructure/components/loading-indicator/loading-indicator.service';
import { EducationType } from '../../enums/education-type.enum';
import { ApplicationOneDto } from '../../models/application-one.dto';
import { StudentSelectDto } from '../../../../student/models/student-select.dto';
import { ApplicationOneResource } from '../../resources/application-one.resource';
import { StudentSelectSearchFilter } from '../../services/student-select-search.filter';
import { StudentResource } from 'src/modules/student/resources/student.resource';
import { EducationSelectDto } from 'src/modules/student/models/education-select.dto';

@Component({
  selector: 'app-application-one-student-select',
  templateUrl: './application-one-student-select.component.html'
})
export class ApplicationOneStudentSelectComponent {
  @Output() studentSelected: EventEmitter<StudentSelectDto> = new EventEmitter();
  @Output() applicationOneSelected: EventEmitter<ApplicationOneDto> = new EventEmitter();
  @Output() isFromXml: EventEmitter<boolean> = new EventEmitter();

  model: StudentSelectDto = new StudentSelectDto();
  showTable: boolean = false;
  educationType = EducationType;

  isStudentType: boolean = false;

  constructor(
    public filter: StudentSelectSearchFilter,
    private resource: ApplicationOneResource,
    private loadingIndicator: LoadingIndicatorService,
    private studentResource: StudentResource
  ) {
    this.filter = new StudentSelectSearchFilter();
  }

  search(): Subscription {
    this.model = new StudentSelectDto();

    this.loadingIndicator.show();

    return this.studentResource.selectStudent(this.filter)
      .pipe(
        finalize(() => this.loadingIndicator.hide())
      )
      .subscribe((result: StudentSelectDto) => {
        this.model = result;
        if (result != null) {
          this.model.educationType = this.filter.educationType;
        }
        this.showTable = true;
        this.isStudentType = this.filter.educationType == this.educationType.student ? true : false
      });
  }

  selectSpeciality(speciality: EducationSelectDto): void {
    this.isFromXml.emit(false);

    this.model.specialities = [];
    this.model.doctorals = [];
    this.model.specialities.push(speciality);

    this.studentSelected.emit(this.model);
  }

  readFile = (e) => {
    const file = e.target.files[0];
    if (!file) {
      return;
    }
    const reader = new FileReader();

    reader.onload = (evt) => {
      const xmlData: string = (evt as any).target.result;
      this.resource.importFromXml(xmlData).subscribe((result: ApplicationOneDto) => {
        result.isNewCredit = true;
        this.isFromXml.emit(true);
        this.applicationOneSelected.emit(result);
      })
    };
    reader.readAsText(file);
  }
}