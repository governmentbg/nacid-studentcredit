import { Component, OnInit } from "@angular/core";
import { UserResource } from "../../resources/user.resource";
import { UserSearchFilter } from "../../services/user-search-filter.service";
import { BaseSearchComponent } from "src/infrastructure/components/search-component/base-search.component";
import { UserSearchResultDto } from "../../models/user-search-result.dto";
import { LoadingIndicatorService } from "src/infrastructure/components/loading-indicator/loading-indicator.service";
import { SharedService } from "src/infrastructure/services/shared-service";
import { UserStatus } from "../../enums/user-status.enum";
import { UserStatusEnumLocalization } from "src/infrastructure/constants/enum-localization.const";
import { UserRoleAliases } from "src/infrastructure/constants/constants";
import { Router } from "@angular/router";

@Component({
  selector: 'user-search',
  templateUrl: 'user-search.component.html'
})
export class UserSearchComponent extends BaseSearchComponent<UserSearchResultDto> {
  userRoleAliases = UserRoleAliases;
  userStatusLocalization = UserStatusEnumLocalization;
  userStatus = UserStatus;

  constructor(public filter: UserSearchFilter,
    public sharedService: SharedService,
    protected resource: UserResource,
    protected loadingIndicator: LoadingIndicatorService,
    private router: Router) {
    super(filter, resource, loadingIndicator);
  }

  ngOnInit(): void {
    this.search();
  }

  clearBank(event: any) {
    if (event.alias === this.userRoleAliases.ADMINISTRATOR) {
      this.filter.bank = null;
      this.filter.bankId = null;
    }
  }
}