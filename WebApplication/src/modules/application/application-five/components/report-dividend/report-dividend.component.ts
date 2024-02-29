import { Component } from '@angular/core';
import { ApplicationFiveType } from '../../enums/application-five-type.enum';
import { ApplicationFiveTypeEnumLocalization, YearPeriodEnumLocalization } from 'src/infrastructure/constants/enum-localization.const';
import { DividendReportSearchDto } from '../../models/dividend-report-search.dto';
import { ApplicationFiveResource } from '../../resources/application-five.resource';
import { LoadingIndicatorService } from 'src/infrastructure/components/loading-indicator/loading-indicator.service';
import { YearPeriod } from '../../enums/year-period.enum';
import { finalize } from 'rxjs';
import { DividendReportResultDto } from '../../models/dividend-report-result.dto';

@Component({
  selector: 'app-report-dividend',
  templateUrl: './report-dividend.component.html'
})
export class ReportDividendComponent {
  period = YearPeriod;
  applicationFiveType = ApplicationFiveType;

  applicationFiveTypeEnumLocalization = ApplicationFiveTypeEnumLocalization;
  periodEnumLocalization = YearPeriodEnumLocalization;

  searchDto: DividendReportSearchDto = new DividendReportSearchDto();
  resultDto: DividendReportResultDto = new DividendReportResultDto();

  result: boolean = false;

  constructor(
    private resource: ApplicationFiveResource,
    private loadingIndicator: LoadingIndicatorService
  ) {
  }

  generateReport() {
    this.loadingIndicator.show();

    this.resource.generateReport(this.searchDto)
      .pipe(
        finalize(() => this.loadingIndicator.hide())
      )
      .subscribe((dto: DividendReportResultDto) => {
        this.resultDto = dto;
        this.result = true;
      })
  }

  clear() {
    this.searchDto = new DividendReportSearchDto();
    this.result = false;
  }

  changeApplicationFiveType() {
    this.searchDto.bank = null;
    this.searchDto.from = null;
    this.searchDto.to = null;
    this.searchDto.year = null;
    this.searchDto.period = null;
    this.result = false;
  }
}
