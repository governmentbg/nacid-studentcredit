import { ApplicationTwoSearchResultItemDto } from "./application-two-search-result-item.dto";
import { RecordEntryDto } from "./record-entry.dto";

export class ApplicationTwoDto extends ApplicationTwoSearchResultItemDto {
  recordEntries: RecordEntryDto[];
}