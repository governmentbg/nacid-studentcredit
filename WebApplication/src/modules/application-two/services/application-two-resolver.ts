import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { EMPTY, Observable } from 'rxjs';
import { catchError, finalize } from 'rxjs/operators';
import { LoadingIndicatorService } from 'src/infrastructure/components/loading-indicator/loading-indicator.service';
import { ApplicationTwoDto } from '../models/application-two.dto';
import { ApplicationTwoResource } from '../resources/application-two-resource';

@Injectable()
export class ApplicationTwoResolver implements Resolve<ApplicationTwoDto> {

  constructor(
    private resource: ApplicationTwoResource,
    private loadingIndicator: LoadingIndicatorService,
  ) { }

  resolve(
    route: ActivatedRouteSnapshot,
    _: RouterStateSnapshot
  ): ApplicationTwoDto | Observable<ApplicationTwoDto> | Promise<ApplicationTwoDto> {
    const routeId = +route.params.id;

    this.loadingIndicator.show();

    return this.resource.getById(routeId)
      .pipe(
        catchError(() => {
          return EMPTY;
        }),
        finalize(() => this.loadingIndicator.hide())
      );
  }
}
