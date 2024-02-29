import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Configuration } from 'src/infrastructure/configuration/configuration';
import { IFilterable } from 'src/infrastructure/interfaces/filterable.interface';
import { SearchResultItemDto } from 'src/infrastructure/models/search-result-item.dto';
import { BaseResource } from 'src/infrastructure/services/base.resource';
import { CreditInfo } from '../models/credit-info.dto';
import { CreditSearchResultItemDto } from '../models/credit-search-result-item.dto';
import { SearchCreditFilter } from '../services/search-credit.filter';
import { CreditSelectFilterDto } from '../../common/models/credit-select-filter.dto';

@Injectable()
export class CreditResource extends BaseResource
  implements IFilterable<SearchCreditFilter, SearchResultItemDto<CreditSearchResultItemDto>>{
  constructor(
    protected http: HttpClient,
    protected configuration: Configuration
  ) {
    super(http, configuration, 'Credit');
  }

  getFiltered(filter?: SearchCreditFilter): Observable<SearchResultItemDto<CreditSearchResultItemDto>> {
    return this.http.get<SearchResultItemDto<CreditSearchResultItemDto>>(`${this.baseUrl}` + this.composeQueryString(filter));
  }

  getCreditInfo(id: number): Observable<CreditInfo> {
    return this.http.get<CreditInfo>(`${this.baseUrl}/${id}`);
  }

  payCreditByApplicationFour(applicationOneId: number, dto: CreditSelectFilterDto): Observable<CreditInfo> {
    return this.http.post<CreditInfo>(`${this.baseUrl}/payCredit/${applicationOneId}`, dto);
  }
}