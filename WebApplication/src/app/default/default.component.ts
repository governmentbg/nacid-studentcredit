import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { UserRoleAliases } from "src/infrastructure/constants/constants";
import { RoleService } from "src/infrastructure/services/role.service";

@Component({
  selector: 'app-default',
  templateUrl: './default.component.html'
})
export class DefaultComponent implements OnInit {

  constructor(private router: Router, private roleService: RoleService) {
  }

  ngOnInit(): void {
    if (this.roleService.hasRole(UserRoleAliases.ADMINISTRATOR) || this.roleService.hasRole(UserRoleAliases.BANK_ACTIVE)) {
      this.router.navigate(['credit']);
    }
    else if (this.roleService.hasRole(UserRoleAliases.BANK_PASSIVE)) {
      this.router.navigate(['student/search']);
    }
    else {
      this.router.navigate(['login']);
    }
  }
}