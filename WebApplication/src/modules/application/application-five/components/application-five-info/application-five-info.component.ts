import { Component, Input } from '@angular/core';
import { CommonFormComponent } from 'src/infrastructure/components/common-form.component';
import { SharedService } from 'src/infrastructure/services/shared-service';
import { ApplicationFiveDto } from '../../models/application-five.dto';
import { ApplicationFiveType } from '../../enums/application-five-type.enum';
import { ApplicationFiveTypeEnumLocalization, YearPeriodEnumLocalization } from 'src/infrastructure/constants/enum-localization.const';
import { YearPeriod } from '../../enums/year-period.enum';

@Component({
  selector: 'app-application-five-info',
  templateUrl: './application-five-info.component.html'
})
export class ApplicationFiveInfoComponent extends CommonFormComponent<ApplicationFiveDto> {
  applicationFiveType = ApplicationFiveType;
  applicationFiveTypeEnumLocalization = ApplicationFiveTypeEnumLocalization;

  period = YearPeriod;
  periodEnumLocalization = YearPeriodEnumLocalization;

  @Input() isBankUser: boolean = false;

  constructor(public sharedService: SharedService) {
    super()
  }

  clear() {
    this.model.amountRequested = null;
    this.model.creditCount = null;
    this.model.from = null;
    this.model.to = null;
    this.model.period = null;
    this.model.applicationFiveAttachedFile = null;
  }

  calculateAmountRequestedAfterCorrection() {
    if (this.model.applicationFiveType == ApplicationFiveType.totalDebtExposure && this.model.amountRequestedCorrection != null) {
      this.model.amountRequestedAfterCorrection = this.model.amountRequested - this.model.amountRequestedCorrection;
    }
  }
}
