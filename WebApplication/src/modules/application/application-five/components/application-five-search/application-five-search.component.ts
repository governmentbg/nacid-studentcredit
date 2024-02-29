import { Component } from '@angular/core';
import { LoadingIndicatorService } from 'src/infrastructure/components/loading-indicator/loading-indicator.service';
import { BaseSearchComponent } from 'src/infrastructure/components/search-component/base-search.component';
import { ApplicationFiveTypeEnumLocalization, CommitStateEnumLocalization } from 'src/infrastructure/constants/enum-localization.const';
import { RoleService } from 'src/infrastructure/services/role.service';
import { UserRoleAliases } from 'src/infrastructure/constants/constants';
import { Configuration } from 'src/infrastructure/configuration/configuration';
import { NomenclatureDto } from 'src/infrastructure/models/nomenclature.dto';
import { SearchApplicationFiveFilter } from '../../services/search-application-five.filter';
import { CommitState } from 'src/modules/application/common/enums/commit-state.enum';
import { ApplicationFiveSearchResultItemDto } from '../../models/application-five-search-result-item.dto';
import { ApplicationFiveResource } from '../../resources/application-five.resource';
import { ApplicationFiveType } from '../../enums/application-five-type.enum';

@Component({
  selector: 'app-application-five-search',
  templateUrl: './application-five-search.component.html'
})
export class ApplicationFiveSearchComponent extends BaseSearchComponent<ApplicationFiveSearchResultItemDto> {
  commitState = CommitState;
  applicationFiveType = ApplicationFiveType;

  commitStateEnumLocalization = CommitStateEnumLocalization;
  applicationFiveTypeEnumLocalization = ApplicationFiveTypeEnumLocalization;

  isBankUser: boolean = false;
  isAdmin: boolean = false;

  constructor(
    public filter: SearchApplicationFiveFilter,
    protected loadingIndicator: LoadingIndicatorService,
    private roleService: RoleService,
    private configuration: Configuration,
    protected resource: ApplicationFiveResource
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

    this.search();
  }

  clearFilter(): void {
    this.filter.clear();
    this.modelCounts = 0;

    this.setUserBank();

    this.search();
  }

  setUserBank() {
    const userBank = JSON.parse(localStorage.getItem(this.configuration.bankProperty)) as NomenclatureDto;

    if (userBank) {
      this.filter.bank = userBank;
      this.filter.bankId = userBank.id;
    }
  }
}