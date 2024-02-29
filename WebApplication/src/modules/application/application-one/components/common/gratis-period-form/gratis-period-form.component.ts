import { Component } from '@angular/core';
import { CommonFormComponent } from 'src/infrastructure/components/common-form.component';
import { SharedService } from 'src/infrastructure/services/shared-service';
import { ApplicationOneDto } from '../../../models/application-one.dto';
import { RegExps } from 'src/infrastructure/constants/constants';

@Component({
  selector: 'app-gratis-period-form',
  templateUrl: './gratis-period-form.component.html'
})
export class GratisPeriodFormComponent extends CommonFormComponent<ApplicationOneDto> {
  cyrillicNumberRegExp = RegExps.CYRILLIC_AND_NUMBER_REGEX;

  constructor(public sharedService: SharedService) {
    super()
  }
}
