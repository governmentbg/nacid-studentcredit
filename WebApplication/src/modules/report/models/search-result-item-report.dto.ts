import { SearchResultItemDto } from "src/infrastructure/models/search-result-item.dto";

export class SearchResultItemReportDto<TDto> extends SearchResultItemDto<TDto> {
  totalCreditsCount: number;
  totalCreditsSize: number;
  totalCreditsRefusedCount: number;
}