import { Component, Input } from "@angular/core";
import { YearTypeEnumLocalization } from "src/infrastructure/constants/enum-localization.const";
import { StudentSearchResult } from "src/modules/student/models/student-search-result.dto";

@Component({
  selector: 'app-student-view-person-doctoral',
  templateUrl: './student-view-person-doctoral.component.html',
  styleUrls: ['./student-view-person-doctoral.component.css']
})

export class StudentViewPersonDoctoralComponent {
  @Input() model: StudentSearchResult = new StudentSearchResult();

  yearTypeEnumLocalization = YearTypeEnumLocalization;
}
