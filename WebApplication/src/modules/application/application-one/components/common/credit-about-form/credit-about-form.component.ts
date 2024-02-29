import { Component, Input } from '@angular/core';
import { CommonFormComponent } from 'src/infrastructure/components/common-form.component';
import { SharedService } from 'src/infrastructure/services/shared-service';
import { ApplicationOneDto } from '../../../models/application-one.dto';

@Component({
  selector: 'app-credit-about-form',
  templateUrl: './credit-about-form.component.html'
})
export class CreditAboutFormComponent extends CommonFormComponent<ApplicationOneDto> {
  @Input() isNewApplicationOne: boolean = false;
  @Input() isNewCredit: boolean = false;

  constructor(public sharedService: SharedService) {
    super()
  }
}
