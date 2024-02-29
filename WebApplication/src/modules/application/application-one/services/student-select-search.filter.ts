import { Injectable } from '@angular/core';
import { EducationType } from '../enums/education-type.enum';

@Injectable()
export class StudentSelectSearchFilter {
  uan: string;
  educationType: EducationType
}
