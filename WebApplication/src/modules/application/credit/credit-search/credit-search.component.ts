import { Component } from '@angular/core';
import { finalize } from 'rxjs';
import { LoadingIndicatorService } from 'src/infrastructure/components/loading-indicator/loading-indicator.service';
import { BaseSearchComponent } from 'src/infrastructure/components/search-component/base-search.component';
import { CommitStateEnumLocalization, CreditTypeEnumLocalization, EducationTypeEnumLocalization } from 'src/infrastructure/constants/enum-localization.const';
import { CreditType } from 'src/modules/application/common/enums/credit-type.enum';
import { CreditSearchResultItemDto } from '../models/credit-search-result-item.dto';
import { CreditResource } from '../resources/credit-resource';
import { SearchCreditFilter } from '../services/search-credit.filter';
import { RoleService } from 'src/infrastructure/services/role.service';
import { UserRoleAliases } from 'src/infrastructure/constants/constants';
import { Configuration } from 'src/infrastructure/configuration/configuration';
import { NomenclatureDto } from 'src/infrastructure/models/nomenclature.dto';
import { CommitState } from '../../common/enums/commit-state.enum';
import { ApplicationOneResource } from '../../application-one/resources/application-one.resource';
import { ApplicationService } from '../../common/services/application.service';
import { IApplicationResource } from '../../common/interfaces/application.resource.interface';
import { ApplicationFourResource } from '../../application-four/resources/application-four.resource';
import { ApplicationType } from '../../common/enums/application-type.enum';
import { BankNomenclatureDto } from 'src/modules/nomenclature/common/models/bank-nomenclature.dto';

@Component({
  selector: 'app-credit-search',
  templateUrl: './credit-search.component.html',
  styleUrls: ['./credit-search.component.css']
})
export class CreditSearchComponent extends BaseSearchComponent<CreditSearchResultItemDto> {
  creditTypes = CreditType;
  commitState = CommitState;
  creditTypeEnumLocalization = CreditTypeEnumLocalization;
  educationTypeEnumLocalization = EducationTypeEnumLocalization;
  commitStateEnumLocalization = CommitStateEnumLocalization;

  isBankUser: boolean = false;
  isAdmin: boolean = false;
  isBankActive: boolean = false;

  pendingCredits: CreditSearchResultItemDto[] = [];
  modificationCredits: CreditSearchResultItemDto[] = [];
  applicationsFour: CreditSearchResultItemDto[] = [];
  modificationApplicationFours: CreditSearchResultItemDto[] = [];

  constructor(
    public filter: SearchCreditFilter,
    protected resource: CreditResource,
    protected loadingIndicator: LoadingIndicatorService,
    private roleService: RoleService,
    private configuration: Configuration,
    private applicationOneResource: ApplicationOneResource,
    private applicationFourResource: ApplicationFourResource,
    private applicationService: ApplicationService
  ) {
    super(filter, resource, loadingIndicator);
    this.isBankUser = this.roleService.hasRole(UserRoleAliases.BANK_ACTIVE);
    this.isAdmin = this.roleService.hasRole(UserRoleAliases.ADMINISTRATOR);
  }

  ngOnInit(): void {
    if (this.isBankUser == true) {
      this.setUserBank();
    } else {
      this.filter.bank = null;
      this.filter.bankId = null;
    }

    if (this.isAdmin) {
      this.getApplicationsByState(ApplicationType.applicationOne, this.applicationOneResource, CommitState.pending);
      this.getApplicationsByState(ApplicationType.applicationOne, this.applicationOneResource, CommitState.approvedModification);
      this.getApplicationsByState(ApplicationType.applicationFour, this.applicationFourResource, CommitState.pending);
    } else {
      this.getApplicationsByState(ApplicationType.applicationOne, this.applicationOneResource, CommitState.modification);
      this.getApplicationsByState(ApplicationType.applicationFour, this.applicationFourResource, CommitState.modification);
      this.getApplicationsByState(ApplicationType.applicationOne, this.applicationOneResource, CommitState.pending);
      this.getApplicationsByState(ApplicationType.applicationFour, this.applicationFourResource, CommitState.pending);

    }

    this.search();
  }

  clearFilter(): void {
    this.filter.clear();
    this.modelCounts = 0;
    this.setUserBank();
    this.filter.commitState = CommitState.approved;
    this.search();
  }

  getApplicationsByState(type: ApplicationType, resource: IApplicationResource, state: CommitState): void {
    this.loadingIndicator.show();

    this.applicationService.getByState(resource, state)
      .pipe(
        finalize(() => this.loadingIndicator.hide())
      )
      .subscribe((result: CreditSearchResultItemDto[]) => {
        if (type == ApplicationType.applicationFour) {
          state == CommitState.pending ? this.applicationsFour = result : this.modificationApplicationFours = result;
        } else {
          state == CommitState.pending ? this.pendingCredits = result : this.modificationCredits = result;
        }
      });
  }

  setUserBank() {
    const userBank = JSON.parse(localStorage.getItem(this.configuration.bankProperty)) as BankNomenclatureDto;

    if (userBank) {
      this.filter.bank = userBank;
      this.filter.bankId = userBank.id;
      this.isBankActive = userBank.isActive;
    }
  }
}