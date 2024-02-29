import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { Observable, finalize } from "rxjs";
import { LoadingIndicatorService } from "src/infrastructure/components/loading-indicator/loading-indicator.service";
import { CreditType } from "../enums/credit-type.enum";
import { CreditSelectFilterDto } from "../models/credit-select-filter.dto";
import { IApplicationResource } from "../interfaces/application.resource.interface";
import { ApplicationType } from "../enums/application-type.enum";
import { CommitState } from "../enums/commit-state.enum";
import { CreditSearchResultItemDto } from "../../credit/models/credit-search-result-item.dto";

@Injectable()
export class ApplicationService {

  constructor(
    private loadingIndicator: LoadingIndicatorService,
    private router: Router
  ) { }

  openApplicationNewPage<TReturnDto>(resource: IApplicationResource, applicationType: ApplicationType, uin: string, creditType: CreditType, creditNumber: string, bankId: number, applicationOneId: number) {
    this.loadingIndicator.show();

    const creditSelectFilterDto = new CreditSelectFilterDto(uin, creditType, creditNumber, bankId, applicationOneId);

    resource.selectCredit<TReturnDto>(creditSelectFilterDto)
      .pipe(
        finalize(() => this.loadingIndicator.hide())
      )
      .subscribe((result: TReturnDto) => {
        if (applicationType == ApplicationType.applicationOne) {
          this.router.navigate(
            ['/credit', 'one', 'new'],
            { state: { credit: result } }
          );
        } else if (applicationType == ApplicationType.applicationFour) {
          this.router.navigate(
            ['/credit', 'four', 'new'],
            { state: { credit: result, isEditMode: true } }
          );
        }
      });
  }

  getByState(resource: IApplicationResource, state: CommitState): Observable<CreditSearchResultItemDto[]> {
    return resource.getByState(state);
  }
}