import { Component, Input } from '@angular/core';
import { CommitStateEnumLocalization, CreditTypeEnumLocalization, EducationTypeEnumLocalization } from 'src/infrastructure/constants/enum-localization.const';
import { CommitState } from 'src/modules/application/common/enums/commit-state.enum';
import { CreditType } from 'src/modules/application/common/enums/credit-type.enum';
import { CreditSearchResultItemDto } from '../../models/credit-search-result-item.dto';
import { ApplicationOneResource } from 'src/modules/application/application-one/resources/application-one.resource';
import { ApplicationService } from 'src/modules/application/common/services/application.service';
import { ApplicationFourResource } from 'src/modules/application/application-four/resources/application-four.resource';
import { ApplicationType } from 'src/modules/application/common/enums/application-type.enum';
import { ApplicationOneCreditSelectDto } from 'src/modules/application/application-one/models/application-one-credit-select.dto';
import { CreditSelectDto } from 'src/modules/application/common/models/credit-select.dto';

@Component({
  selector: 'app-credit-search-table',
  templateUrl: './credit-search-table.component.html',
  styleUrls: ['./credit-search-table.component.css']
})
export class CreditSearchTableComponent {
  creditTypes = CreditType;
  commitState = CommitState;
  creditTypeEnumLocalization = CreditTypeEnumLocalization;
  educationTypeEnumLocalization = EducationTypeEnumLocalization;
  commitStateEnumLocalization = CommitStateEnumLocalization;

  @Input() isAdmin: boolean = false;
  @Input() hasActions: boolean = false;
  @Input() isApplicationFour: boolean = false;
  @Input() credits: CreditSearchResultItemDto[] = [];

  constructor(
    private applicationOneResourse: ApplicationOneResource,
    private applicationService: ApplicationService,
    private applicationFourResource: ApplicationFourResource
  ) { }

  createApplicationOne(uin: string, creditType: CreditType, creditNumber: string, bankId: number, applicationOneId: number): void {
    this.applicationService.openApplicationNewPage<ApplicationOneCreditSelectDto>(this.applicationOneResourse, ApplicationType.applicationOne, uin, creditType, creditNumber, bankId, applicationOneId);
  }

  createApplicationFour(uin: string, creditType: CreditType, creditNumber: string, bankId: number, applicationOneId: number): void {
    this.applicationService.openApplicationNewPage<CreditSelectDto>(this.applicationFourResource, ApplicationType.applicationFour, uin, creditType, creditNumber, bankId, applicationOneId);
  }
}