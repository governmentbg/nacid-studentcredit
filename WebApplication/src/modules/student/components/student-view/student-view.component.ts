import { Component, Input } from '@angular/core';
import { StudentSearchResult } from '../../models/student-search-result.dto';
import { SharedService } from 'src/infrastructure/services/shared-service';

@Component({
  selector: 'app-student-view',
  templateUrl: './student-view.component.html'
})

export class StudentViewComponent {
  @Input() model: StudentSearchResult = new StudentSearchResult();

  constructor(
    public sharedService: SharedService
  ) { }
}