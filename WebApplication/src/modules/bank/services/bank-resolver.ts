import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { EMPTY, Observable } from 'rxjs';
import { catchError, finalize } from 'rxjs/operators';
import { BankDto } from '../model/bank.dto';
import { BankResource } from '../resources/bank.resource';
import { LoadingIndicatorService } from 'src/infrastructure/components/loading-indicator/loading-indicator.service';
import { RoleService } from 'src/infrastructure/services/role.service';
import { UserRoleAliases } from 'src/infrastructure/constants/constants';
import { Configuration } from 'src/infrastructure/configuration/configuration';
import { NomenclatureDto } from 'src/infrastructure/models/nomenclature.dto';

@Injectable()
export class BankResolver implements Resolve<BankDto> {

  constructor(
    private resource: BankResource,
    private loadingIndicator: LoadingIndicatorService,
    private roleService: RoleService,
    private configuration: Configuration
  ) { }

  resolve(
    route: ActivatedRouteSnapshot,
    _: RouterStateSnapshot
  ): BankDto | Observable<BankDto> | Promise<BankDto> {
    let bankId = +route.params.id;

    if (isNaN(bankId) && this.roleService.hasRole(UserRoleAliases.BANK_ACTIVE)) {
      const userBank = JSON.parse(localStorage.getItem(this.configuration.bankProperty)) as NomenclatureDto;

      bankId = userBank.id;
    } else if (isNaN(bankId) && !this.roleService.hasRole(UserRoleAliases.BANK_ACTIVE)) {
      return EMPTY;
    }

    this.loadingIndicator.show();

    return this.resource.get(bankId)
      .pipe(
        catchError(() => {
          return EMPTY;
        }),
        finalize(() => this.loadingIndicator.hide())
      );
  }
}
