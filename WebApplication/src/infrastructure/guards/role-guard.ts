import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Configuration } from '../configuration/configuration';

@Injectable()
export class RoleGuard implements CanActivate {
  constructor(
    private router: Router,
    private config: Configuration,
    private toastrService: ToastrService
  ) { }

  canActivate(route: ActivatedRouteSnapshot, __: RouterStateSnapshot): boolean {
    const roles = route.data['roles'] as Array<string>;

    let hasAccess: boolean = false;

    const roleAlias = localStorage.getItem(this.config.userRolesProperty);

    roles.forEach(element => {
      if (element === roleAlias) {
        hasAccess = true;
      }
    });

    if (!hasAccess) {
      this.router.navigate(['']);
      this.toastrService.error('Нямате достъп до тази страница!');
    }
    return hasAccess;
  }
}
