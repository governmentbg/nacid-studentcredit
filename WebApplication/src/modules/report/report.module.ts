import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ReportRoutingModule } from './report-routing.module';
import { ReportComponent } from './components/report/report.component';
import { FormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { SearchReportFilter } from './models/search-report.filter';
import { ReportResource } from './resources/report-resource';
import { CommonBootstrapModule } from "../../infrastructure/common-bootstrap.module";
import { SummaryReportComponent } from './components/summary-report/summary-report.component';
import { MonthDataModalComponent } from './components/summary-report/month-data-modal/month-data-modal.component';
import { LimitModalComponent } from './components/summary-report/limit-modal/limit-modal.component';
import { SummaryReportResource } from './resources/summary-report-resource';
import { FulfillmentOfLimitComponent } from './components/fulfillmentOfLimits/fulfillment-of-limits.component';


@NgModule({
  declarations: [
    ReportComponent,
    SummaryReportComponent,
    MonthDataModalComponent,
    LimitModalComponent,
    FulfillmentOfLimitComponent
  ],
  providers: [
    SearchReportFilter,
    ReportResource,
    SummaryReportResource
  ],
  imports: [
    CommonModule,
    ReportRoutingModule,
    FormsModule,
    TranslateModule,
    CommonBootstrapModule
  ]
})
export class ReportModule { }
