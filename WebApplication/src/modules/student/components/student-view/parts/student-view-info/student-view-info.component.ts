import { Component, Input } from "@angular/core";
import { SharedService } from "src/infrastructure/services/shared-service";
import { StudentSearchResult } from "src/modules/student/models/student-search-result.dto";

@Component({
  selector: 'app-student-view-info',
  templateUrl: './student-view-info.component.html'
})

export class StudentViewInfoComponent {
  @Input() model: StudentSearchResult = new StudentSearchResult();

  constructor(public sharedService: SharedService) { }
}
