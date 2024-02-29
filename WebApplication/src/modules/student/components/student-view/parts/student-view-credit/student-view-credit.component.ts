import { Component, Input } from "@angular/core";
import { ApplicationOneTypeAliases } from "src/infrastructure/constants/constants";
import { CreditTypeEnumLocalization } from "src/infrastructure/constants/enum-localization.const";
import { StudentSearchResult } from "src/modules/student/models/student-search-result.dto";

@Component({
  selector: 'app-student-view-credit',
  templateUrl: './student-view-credit.component.html'
})

export class StudentViewCreditComponent {
  @Input() model: StudentSearchResult = new StudentSearchResult();

  applicationOneTypeAliases = ApplicationOneTypeAliases;
  creditTypeEnumLocalization = CreditTypeEnumLocalization;
}
