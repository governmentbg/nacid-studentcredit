import { Component } from '@angular/core';
import { CommonFormComponent } from 'src/infrastructure/components/common-form.component';
import { SharedService } from 'src/infrastructure/services/shared-service';
import { ApplicationOneDto } from '../../../models/application-one.dto';

@Component({
  selector: 'app-repayment-form',
  templateUrl: './repayment-form.component.html'
})
export class RepaymentFormComponent extends CommonFormComponent<ApplicationOneDto> {
  constructor(public sharedService: SharedService) {
    super()
  }
}
