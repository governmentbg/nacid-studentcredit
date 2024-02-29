import { Component, Input } from '@angular/core';
import { CommonFormComponent } from 'src/infrastructure/components/common-form.component';
import { CourseTypeEnumLocalization, YearTypeEnumLocalization } from 'src/infrastructure/constants/enum-localization.const';
import { SpecialityInformationDto } from 'src/modules/nomenclature/common/models/speciality-information.dto';
import { CourseType } from '../../../enums/course-type.enum';
import { EducationType } from '../../../enums/education-type.enum';
import { YearType } from '../../../enums/year-type.enum';
import { ApplicationOneDto } from '../../../models/application-one.dto';
import { SharedService } from 'src/infrastructure/services/shared-service';
import { ExistingEnum } from 'src/modules/application/common/enums/existing.enum';

@Component({
  selector: 'app-student-info-form',
  templateUrl: './student-info-form.component.html'
})
export class StudentInfoFormComponent extends CommonFormComponent<ApplicationOneDto> {
  existingEnum = ExistingEnum;

  educationType = EducationType;

  courseType = CourseType;
  courseTypeEnumLocalization = CourseTypeEnumLocalization;

  yearType = YearType;
  yearTypeEnumLocalization = YearTypeEnumLocalization;

  @Input() importFromXml: boolean = false;

  specialityMissing = 'Специалността липсва в регистъра и е въведена чрез свободен текст';
  researchAreaMissing = 'Направлението липсва в регистъра и е въведено чрез свободен текст';

  constructor(public sharedService: SharedService) {
    super()
  }

  setSpecialityInformation(speciality: SpecialityInformationDto): void {
    this.model.researchArea = speciality.researchArea;
    this.model.educationFormType = speciality.form;
    this.model.educationalQualification = speciality.qualification;
  }

  onInstitutionChange(): void {
    this.model.speciality = null;
    this.model.researchArea = null;
    this.model.educationalQualification = null;
    this.model.educationFormType = null;
  }
}
