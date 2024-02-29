import { Injectable } from '@angular/core';
import { UserRoleAliases } from 'src/infrastructure/constants/constants';
import { IMenuItem } from 'src/infrastructure/interfaces/IMenuItem';
import { RoleService } from 'src/infrastructure/services/role.service';

@Injectable({
  providedIn: 'root'
})
export class MenuItemsService {
  isBankActive: boolean = false;
  isBankPassive: boolean = false;
  isAdmin: boolean = false;

  constructor(private roleService: RoleService
  ) { }

  getMainMenuItems(isLoggedInUser: boolean): IMenuItem[] {
    if (!isLoggedInUser) {
      return [];
    }

    this.roleService.hasRole(UserRoleAliases.BANK_ACTIVE) == true ? this.isBankActive = true : this.isBankActive = false;
    this.roleService.hasRole(UserRoleAliases.BANK_PASSIVE) == true ? this.isBankPassive = true : this.isBankPassive = false;
    this.roleService.hasRole(UserRoleAliases.ADMINISTRATOR) == true ? this.isAdmin = true : this.isAdmin = false;

    return [
      {
        title: 'Кредити', icon: 'file-earmark-fill', routerLink: '/credit', isVisible: isLoggedInUser && !this.isBankPassive
      },
      {
        title: "Отчети", icon: 'book', routerLink: '/application-two', isVisible: isLoggedInUser && !this.isBankPassive
      },
      this.isAdmin == true
        ? {
          title: "Премии", icon: 'card-list', isVisible: isLoggedInUser && !this.isBankPassive, children: [
            { title: 'Преглед', icon: 'file-text-fill', routerLink: '/application-five', isVisible: isLoggedInUser },
            { title: 'Справкa приложение 5', icon: 'bar-chart-fill', routerLink: '/report-dividend', isVisible: isLoggedInUser }
          ]
        }
        : {
          title: "Премии", icon: 'card-list', routerLink: '/application-five', isVisible: isLoggedInUser && !this.isBankPassive
        },
      {
        title: this.isBankActive ? "Банка" : "Банки", icon: 'bank', isVisible: isLoggedInUser && !this.isBankPassive, children: [
          { title: 'Преглед', icon: 'file-text-fill', routerLink: this.isBankActive ? '/bank-info' : '/bank', isVisible: isLoggedInUser },
          { title: 'Справкa за МФ', icon: 'bar-chart-fill', routerLink: '/summary-report', isVisible: isLoggedInUser }
        ]
      },
      {
        title: "Справки", icon: 'bar-chart-fill', isVisible: isLoggedInUser && this.isAdmin, children: [
          { title: 'Кредити', icon: 'file-text-fill', routerLink: '/reports', isVisible: isLoggedInUser },
          { title: 'Изпълнение на лимита', icon: 'clipboard-data-fill', routerLink: '/fulfillmentOfLimits', isVisible: isLoggedInUser }
        ]
      },
      {
        title: "Справки", icon: 'mortarboard-fill', routerLink: '/student/search', isVisible: isLoggedInUser && (this.isBankActive || this.isBankPassive)
      }
    ];
  }
}
