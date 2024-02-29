import { Component } from '@angular/core';
import { finalize } from 'rxjs';
import { LoadingIndicatorService } from 'src/infrastructure/components/loading-indicator/loading-indicator.service';
import { SearchIdentificationEnumLocalization } from 'src/infrastructure/constants/enum-localization.const';
import { SearchIdentificationType } from '../../enums/search-identification.enum';
import { StudentFilterDto } from '../../models/student-filter.dto';
import { StudentResource } from '../../resources/student.resource';
import { StudentSearchResult } from '../../models/student-search-result.dto';

@Component({
  selector: 'app-student-search',
  templateUrl: './student-search.component.html'
})

export class StudentSearchComponent {
  filter: StudentFilterDto = new StudentFilterDto();
  identificationTypes = SearchIdentificationType;

  identificationTypeEnumLocalization = SearchIdentificationEnumLocalization;

  model: StudentSearchResult = new StudentSearchResult();
  result: boolean = false;

  constructor(
    private studentResource: StudentResource,
    private loadingIndicator: LoadingIndicatorService
  ) { }

  search() {
    this.loadingIndicator.show();

    this.studentResource.getFiltered(this.filter)
      .pipe(
        finalize(() => this.loadingIndicator.hide())
      )
      .subscribe(model => {
        this.model = model;
        this.result = true;
      });
  }

  clearFilter() {
    this.filter.identificationType = null;
    this.filter.identificator = '';
    this.filter.educationalQualification = null;
    this.filter.educationalQualificationId = null;
    this.filter.educationalForm = null;
    this.filter.educationalFormId = null;
    this.result = false;
  }
}