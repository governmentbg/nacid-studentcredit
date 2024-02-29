import { Component, Input } from '@angular/core';
import { CommonFormComponent } from 'src/infrastructure/components/common-form.component';
import { CreditTypeEnumLocalization } from 'src/infrastructure/constants/enum-localization.const';
import { SharedService } from 'src/infrastructure/services/shared-service';
import { CreditType } from 'src/modules/application/common/enums/credit-type.enum';
import { ApplicationOneDto } from '../../../models/application-one.dto';
import { RegExps } from 'src/infrastructure/constants/constants';

@Component({
  selector: 'app-contract-form',
  templateUrl: './contract-form.component.html'
})
export class ContractFormComponent extends CommonFormComponent<ApplicationOneDto> {
  @Input() isNewApplicationOne: boolean = false;

  creditType = CreditType;
  creditTypeEnumLocalization = CreditTypeEnumLocalization;

  cyrillicNumberRegExp = RegExps.CYRILLIC_AND_NUMBER_REGEX;
  lettersAndNumbersRegExp = RegExps.LETTERS_AND_NUMBERS_REGEX;

  constructor(public sharedService: SharedService) {
    super()
  }
}
