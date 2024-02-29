import { Component } from '@angular/core';
import { LoadingIndicatorService } from 'src/infrastructure/components/loading-indicator/loading-indicator.service';
import { BaseSearchComponent } from 'src/infrastructure/components/search-component/base-search.component';
import { ContentTypes } from 'src/infrastructure/constants/constants';
import { CreditType } from 'src/modules/application/common/enums/credit-type.enum';
import { ReportSearchResultItemDto } from '../../models/report-search-result-item-dto.model';
import { SearchReportFilter } from '../../models/search-report.filter';
import { ReportResource } from '../../resources/report-resource';
import { ReportType } from '../../enums/report-type.enum';
import { NewContractFilterType } from '../../enums/new-contract-filter-type.enum';
import { SharedService } from 'src/infrastructure/services/shared-service';
import { LearnerType } from '../../enums/learner-type.enum';
import { InstitutionOwnershipType } from 'src/modules/nomenclature/enums/institution-ownership-type.enum';
import { ReportExportDto } from '../../models/report-export-dto.model';
import { SearchResultItemReportDto } from '../../models/search-result-item-report.dto';
import { finalize } from 'rxjs';

@Component({
  selector: 'app-report',
  templateUrl: 'report.component.html',
  styleUrls: ['report.component.css']
})
export class ReportComponent extends BaseSearchComponent<ReportSearchResultItemDto> {

  result: SearchResultItemReportDto<ReportSearchResultItemDto>;
  model: ReportSearchResultItemDto[] = [];
  totalCounts = 0;

  reportTypes = ReportType;
  newContractFilterTypes = NewContractFilterType;
  learnerTypes = LearnerType;
  institutionOwnershipTypes = InstitutionOwnershipType;

  currentReportType: ReportType;
  currentNewContractFilterType: NewContractFilterType;
  currentLearnerType: LearnerType;
  currentInstitutionOwnershipType: InstitutionOwnershipType;

  years: number[] = [2000];
  creditTypes = CreditType;
  currentYear: number;

  contentTypes = ContentTypes;
  bulgarianCitizens = 'Само български граждани';

  reportExportDto: ReportExportDto = new ReportExportDto();

  constructor(public filter: SearchReportFilter,
    public sharedService: SharedService,
    protected resource: ReportResource,
    protected loadingIndicator: LoadingIndicatorService,
  ) {
    super(filter, resource, loadingIndicator);
  }

  ngOnInit(): void {
    this.clearFilter();
    this.search();
    this.calculateYears();

    this.resource.getFulfillmentLimits().subscribe((res: any) => {
      console.log(res);
    })
  }

  clearDates(): void {
    this.filter.startDate = null;
    this.filter.endDate = null;
  }

  clearBasicFilter(): void {
    this.filter.clear();
    this.modelCounts = 0;
    this.search();
  }

  clearFilter(): void {
    this.currentReportType = null;
    this.currentNewContractFilterType = null;
    this.currentLearnerType = null;
    this.currentInstitutionOwnershipType = null;
    this.clearBasicFilter();
  }

  clearSmallFilters(newType: any): void {
    if (newType != this.reportTypes.newContract) {
      this.currentNewContractFilterType = null;
      this.currentLearnerType = null;
      this.currentInstitutionOwnershipType = null;
    }
  }

  clearSmallestFilter(newType: any): void {
    this.currentLearnerType = null;
    this.currentInstitutionOwnershipType = null;
  }

  calculateYears() {
    this.currentYear = new Date().getFullYear();
    while (this.years.at(-1) < this.currentYear) {
      this.years.push(Number(this.years.at(-1) + 1));
    }
  }

  beginSearch() {
    this.filter.reportType = this.currentReportType;

    this.filter.newContractFilterType = this.currentNewContractFilterType;
    this.filter.learnerType = this.currentLearnerType;
    this.filter.institutionOwnershipType = this.currentInstitutionOwnershipType;

    this.search();
  }

  search() {
    this.loadingIndicator.show();
    return this.resource.getFiltered(this.filter)
      .pipe(
        finalize(() => this.loadingIndicator.hide())
      )
      .subscribe((result: SearchResultItemReportDto<ReportSearchResultItemDto>) => {
        this.result = result;
        this.totalCounts = result.totalCount;
        this.model = result.items;
      });
  }
}
