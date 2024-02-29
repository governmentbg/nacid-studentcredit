import { ReportSearchResultItemDto } from "./report-search-result-item-dto.model";
import { SearchReportFilter } from "./search-report.filter";

export class ReportExportDto {
  reportResult: ReportSearchResultItemDto[];
  reportFilter: SearchReportFilter;

  constructor() {
  }
}
