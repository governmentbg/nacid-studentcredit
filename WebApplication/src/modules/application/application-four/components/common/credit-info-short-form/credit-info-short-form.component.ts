import { Component } from "@angular/core";
import { CommonFormComponent } from "src/infrastructure/components/common-form.component";
import { CreditTypeEnumLocalization } from "src/infrastructure/constants/enum-localization.const";
import { SharedService } from "src/infrastructure/services/shared-service";
import { CreditType } from "src/modules/application/common/enums/credit-type.enum";
import { ApplicationFourDto } from "../../../models/application-four.dto";

@Component({
  selector: 'app-credit-info-short-form',
  templateUrl: 'credit-info-short-form.component.html'
})

export class CreditInfoShortFormComponent extends CommonFormComponent<ApplicationFourDto> {
  creditType = CreditType;
  creditTypeEnumLocalization = CreditTypeEnumLocalization;

  constructor(public sharedService: SharedService) {
    super()
  }
}