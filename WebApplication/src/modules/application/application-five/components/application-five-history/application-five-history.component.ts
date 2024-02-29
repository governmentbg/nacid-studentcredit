import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { LoadingIndicatorService } from 'src/infrastructure/components/loading-indicator/loading-indicator.service';
import { finalize } from 'rxjs';
import { ApplicationHistoryDto } from 'src/modules/application/common/models/application-history.dto';
import { ApplicationHistoryEnumLocalization } from 'src/infrastructure/constants/enum-localization.const';
import { ApplicationFiveResource } from '../../resources/application-five.resource';

@Component({
  selector: 'app-application-five-history',
  templateUrl: './application-five-history.component.html'
})
export class ApplicationFiveHistoryComponent {
  applications: ApplicationHistoryDto[] = [];

  applicationHistoryEnumLocalization = ApplicationHistoryEnumLocalization;

  constructor(
    private resource: ApplicationFiveResource,
    private activatedRoute: ActivatedRoute,
    private loadingIndicator: LoadingIndicatorService
  ) { }

  ngOnInit(): void {
    const rootId = +this.activatedRoute.snapshot.params.rootId;

    this.loadingIndicator.show();

    this.resource.getHistory(rootId)
      .pipe(
        finalize(() => this.loadingIndicator.hide())
      )
      .subscribe((result: ApplicationHistoryDto[]) => {
        this.applications = result;
      });
  }
}