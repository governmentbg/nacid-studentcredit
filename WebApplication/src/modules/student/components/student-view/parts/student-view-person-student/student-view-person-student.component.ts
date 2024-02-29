import { Component, Input, OnInit } from "@angular/core";
import { CourseTypeEnumLocalization } from "src/infrastructure/constants/enum-localization.const";
import { StudentSearchResult } from "src/modules/student/models/student-search-result.dto";

@Component({
  selector: 'app-student-view-person-student',
  templateUrl: './student-view-person-student.component.html',
  styleUrls: ['./student-view-person-student.component.css']
})

export class StudentViewPersonStudentComponent {
  @Input() model: StudentSearchResult = new StudentSearchResult();

  courseTypeEnumLocalization = CourseTypeEnumLocalization;
}
