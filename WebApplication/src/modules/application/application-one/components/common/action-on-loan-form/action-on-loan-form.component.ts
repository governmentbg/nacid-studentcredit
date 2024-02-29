import { Component } from '@angular/core';
import { CommonFormComponent } from 'src/infrastructure/components/common-form.component';
import { SharedService } from 'src/infrastructure/services/shared-service';
import { ApplicationOneDto } from '../../../models/application-one.dto';

@Component({
  selector: 'app-action-on-loan-form',
  templateUrl: './action-on-loan-form.component.html'
})
export class ActionOnLoanFormComponent extends CommonFormComponent<ApplicationOneDto> {

  constructor(public sharedService: SharedService) {
    super()
  }
}
