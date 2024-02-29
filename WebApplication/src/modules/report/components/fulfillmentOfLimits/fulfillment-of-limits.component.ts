import { Component, OnInit } from '@angular/core';
import { ReportResource } from '../../resources/report-resource';
import { FulfillmentOfLimitsReportDto } from '../../models/fulfillment-of-limits-report-dto';
import { LoadingIndicatorService } from 'src/infrastructure/components/loading-indicator/loading-indicator.service';
import { finalize } from 'rxjs';

@Component({
  selector: 'app-fulfillment-of-limits',
  templateUrl: 'fulfillment-of-limits.component.html',
  styleUrls: ['fulfillment-of-limits.component.css']
})
export class FulfillmentOfLimitComponent implements OnInit {
  fulfillmentOfLimits: FulfillmentOfLimitsReportDto[] = [];

  constructor(
    private resource: ReportResource,
    private loadingIndicator: LoadingIndicatorService
  ) {
  }

  ngOnInit(): void {
    this.loadingIndicator.show();

    this.resource.getFulfillmentLimits()
      .pipe(
        finalize(() => this.loadingIndicator.hide())
      )
      .subscribe((result: FulfillmentOfLimitsReportDto[]) => {
        this.fulfillmentOfLimits = result;
        console.log(this.fulfillmentOfLimits)
      });
  }
}
