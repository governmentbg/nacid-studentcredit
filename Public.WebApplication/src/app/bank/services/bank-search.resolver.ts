import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve } from "@angular/router";
import { finalize, map, Observable } from "rxjs";
import { LoadingIndicatorService } from "src/infrastructure/components/loading-indicator/loading-indicator.service";
import { BankDto } from "../models/bank.dto";
import { BankResource } from "../resources/bank.resource";
import { BankSearchFilter } from "./bank-search.filter";

@Injectable()
export class BankSearchResolver implements Resolve<BankDto[]> {

  model: Observable<BankDto[]>;

  constructor(
    private resource: BankResource,
    private loadingIndicator: LoadingIndicatorService,
  ) { }

  resolve(route: ActivatedRouteSnapshot)
    : BankDto[] | Observable<BankDto[]> | Promise<BankDto[]> {

    this.loadingIndicator.show();
    return this.resource.getFiltered(new BankSearchFilter())
      .pipe(
        map(result => {
          return result.items
        }),
        finalize(() => this.loadingIndicator.hide())
      );
  }
}
