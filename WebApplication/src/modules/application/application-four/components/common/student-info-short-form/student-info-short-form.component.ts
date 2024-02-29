import { Component } from "@angular/core";
import { CommonFormComponent } from "src/infrastructure/components/common-form.component";
import { ApplicationFourDto } from "../../../models/application-four.dto";
import { SharedService } from "src/infrastructure/services/shared-service";
import { ExistingEnum } from "src/modules/application/common/enums/existing.enum";

@Component({
  selector: 'app-student-info-short-form',
  templateUrl: 'student-info-short-form.component.html'
})

export class StudentInfoShortFormComponent extends CommonFormComponent<ApplicationFourDto> {
  specialityMissing = 'Специалността липсва в регистъра и е въведена чрез свободен текст';

  existingEnum = ExistingEnum;

  constructor(public sharedService: SharedService) {
    super()
  }
}