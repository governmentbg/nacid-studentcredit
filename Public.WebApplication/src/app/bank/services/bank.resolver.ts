import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve } from "@angular/router";
import { finalize, Observable } from "rxjs";
import { LoadingIndicatorService } from "src/infrastructure/components/loading-indicator/loading-indicator.service";
import { BankDto } from "../models/bank.dto";
import { BankResource } from "../resources/bank.resource";

@Injectable()
export class BankResolver implements Resolve<BankDto> {

  model: Observable<BankDto>;

  constructor(
    private resource: BankResource,
    private loadingIndicator: LoadingIndicatorService,
  ) { }

  resolve(route: ActivatedRouteSnapshot)
    : BankDto | Observable<BankDto> | Promise<BankDto> {
    const bankId = +route.params["id"];

    this.loadingIndicator.show();
    return this.resource.getBankInfo(bankId)
      .pipe(
        finalize(() => this.loadingIndicator.hide())
      );
  }
}
