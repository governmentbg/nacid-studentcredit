import { SummaryReportType } from "../enums/summary-report-type.enum";

export class SummaryReportSearchDto {
  summaryReportType: SummaryReportType;
  bankId: number;
}