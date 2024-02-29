import { Component, Input } from "@angular/core";
import { CommonFormComponent } from "src/infrastructure/components/common-form.component";
import { SharedService } from "src/infrastructure/services/shared-service";
import { ApplicationFourDto } from "../../../models/application-four.dto";

@Component({
  selector: 'app-application-four-about-form',
  templateUrl: 'application-four-about-form.component.html'
})

export class ApplicationFourAboutFormComponent extends CommonFormComponent<ApplicationFourDto> {
  @Input() isViewMode: boolean;

  constructor(public sharedService: SharedService) {
    super()
  }
}