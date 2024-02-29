import { Component, Input, OnInit } from "@angular/core";
import { UserDto } from "../../models/user.dto";
import { UserRoleAliases, RegExps } from "src/infrastructure/constants/constants";
import { UserStatus } from "../../enums/user-status.enum";
import { SharedService } from "src/infrastructure/services/shared-service";
import { CommonFormComponent } from "src/infrastructure/components/common-form.component";

@Component({
  selector: 'user-common',
  templateUrl: 'user-common.component.html'
})
export class UserCommonComponent extends CommonFormComponent<UserDto> {
  @Input() showInstitutions: boolean;
  monName = 'МОН';

  userStatus = UserStatus;
  userRoleAliases = UserRoleAliases;
  cyrillicRegExp = RegExps.CYRILLIC_NAMES_REGEX;
  emailRegex = RegExps.EMAIL_REGEX;

  constructor(public sharedService: SharedService) {
    super()
  }

  selectRole(role: any) {
    if (role.alias === this.userRoleAliases.ADMINISTRATOR) {
      this.model.bank = null;
      this.showInstitutions = false;
    }

    if (role.alias === this.userRoleAliases.BANK_ACTIVE) {
      this.showInstitutions = true;
    }
  }
}