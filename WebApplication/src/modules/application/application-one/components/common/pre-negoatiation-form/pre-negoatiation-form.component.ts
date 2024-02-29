import { Component } from '@angular/core';
import { CommonFormComponent } from 'src/infrastructure/components/common-form.component';
import { SharedService } from 'src/infrastructure/services/shared-service';
import { ApplicationOneDto } from '../../../models/application-one.dto';
import { RecontractionType } from '../../../enums/recontraction-type.enum';
import { RegExps } from 'src/infrastructure/constants/constants';

@Component({
  selector: 'app-pre-negoatiation-form',
  templateUrl: './pre-negoatiation-form.component.html'
})
export class PreNegoatiationFormComponent extends CommonFormComponent<ApplicationOneDto> {
  recontractionTypes = RecontractionType;

  cyrillicNumberRegExp = RegExps.CYRILLIC_AND_NUMBER_REGEX;

  constructor(public sharedService: SharedService) {
    super()
  }

  onChange(): void {
    this.model.adjournType = null;
    this.model.adjournDate = null;
    this.model.adjournPeriod = null;
    this.model.extensionOfGracePeriod = null;
    this.model.newExpirationDateOfGracePeriod = null;
    this.model.principalAbsorbedInOldBank = null;
    this.model.interestDueInOldBank = null;
    this.model.overallSizeInOldBank = null;
  }
}
