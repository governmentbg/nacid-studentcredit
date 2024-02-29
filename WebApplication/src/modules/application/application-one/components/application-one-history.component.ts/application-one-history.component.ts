import { Component } from '@angular/core';
import { ApplicationOneResource } from '../../resources/application-one.resource';
import { ActivatedRoute } from '@angular/router';
import { LoadingIndicatorService } from 'src/infrastructure/components/loading-indicator/loading-indicator.service';
import { finalize } from 'rxjs';
import { ApplicationHistoryDto } from 'src/modules/application/common/models/application-history.dto';
import { ApplicationHistoryEnumLocalization } from 'src/infrastructure/constants/enum-localization.const';

@Component({
  selector: 'app-application-one-history',
  templateUrl: './application-one-history.component.html'
})
export class ApplicationOneHistoryComponent {
  applications: ApplicationHistoryDto[] = [];
  parentRouteId: number;

  applicationHistoryEnumLocalization = ApplicationHistoryEnumLocalization;

  constructor(
    private resource: ApplicationOneResource,
    private activatedRoute: ActivatedRoute,
    private loadingIndicator: LoadingIndicatorService
  ) { }

  ngOnInit(): void {
    const rootId = +this.activatedRoute.snapshot.params.rootId;
    this.parentRouteId = this.activatedRoute.parent.snapshot.params['parentId'];

    this.loadingIndicator.show();

    this.resource.getHistory(rootId)
      .pipe(finalize(() => this.loadingIndicator.hide()))
      .subscribe((result: ApplicationHistoryDto[]) => {
        this.applications = result;
      });
  }
}