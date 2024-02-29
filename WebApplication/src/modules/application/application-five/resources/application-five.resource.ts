import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Configuration } from 'src/infrastructure/configuration/configuration';
import { IApplicationResource } from '../../common/interfaces/application.resource.interface';
import { ApplicationResource } from '../../common/services/application.resource';
import { ApplicationFiveDto } from '../models/application-five.dto';
import { SearchApplicationFiveFilter } from '../services/search-application-five.filter';
import { IFilterable } from 'src/infrastructure/interfaces/filterable.interface';
import { ApplicationFiveSearchResultItemDto } from '../models/application-five-search-result-item.dto';
import { SearchResultItemDto } from 'src/infrastructure/models/search-result-item.dto';
import { DividendReportSearchDto } from '../models/dividend-report-search.dto';
import { DividendReportResultDto } from '../models/dividend-report-result.dto';

@Injectable()
export class ApplicationFiveResource extends ApplicationResource<ApplicationFiveDto>
  implements IApplicationResource, IFilterable<SearchApplicationFiveFilter, SearchResultItemDto<ApplicationFiveSearchResultItemDto>> {
  constructor(
    protected http: HttpClient,
    protected configuration: Configuration
  ) {
    super(http, configuration, 'ApplicationFive');
  }

  getFiltered(filter?: SearchApplicationFiveFilter): Observable<SearchResultItemDto<ApplicationFiveSearchResultItemDto>> {
    return this.http.get<SearchResultItemDto<ApplicationFiveSearchResultItemDto>>(`${this.baseUrl}` + this.composeQueryString(filter));
  }

  generateReport(dto: DividendReportSearchDto): Observable<DividendReportResultDto> {
    return this.http.post<DividendReportResultDto>(`${this.baseUrl}/report`, dto);
  }
}
