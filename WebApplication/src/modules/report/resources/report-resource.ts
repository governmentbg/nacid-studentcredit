import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Configuration } from 'src/infrastructure/configuration/configuration';
import { IFilterable } from 'src/infrastructure/interfaces/filterable.interface';
import { SearchResultItemDto } from 'src/infrastructure/models/search-result-item.dto';
import { BaseResource } from 'src/infrastructure/services/base.resource';
import { ReportSearchResultItemDto } from '../models/report-search-result-item-dto.model';
import { SearchReportFilter } from '../models/search-report.filter';

@Injectable()
export class ReportResource extends BaseResource
  implements IFilterable<SearchReportFilter, SearchResultItemDto<ReportSearchResultItemDto>>{

  constructor(protected http: HttpClient,
    protected configuration: Configuration) {
    super(http, configuration, 'Report');
  }

  getFiltered(filter?: SearchReportFilter): Observable<SearchResultItemDto<ReportSearchResultItemDto>> {
    return this.http.get<SearchResultItemDto<ReportSearchResultItemDto>>(`${this.baseUrl}` + this.composeQueryString(filter));
  }

  getFulfillmentLimits(): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/fulfillmentOfLimits`);
  }
}
