import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Configuration } from 'src/infrastructure/configuration/configuration';
import { IFilterable } from 'src/infrastructure/interfaces/filterable.interface';
import { SearchResultItemDto } from 'src/infrastructure/models/search-result-item.dto';
import { BaseResource } from 'src/infrastructure/services/base.resource';
import { SearchApplicationTwoFilter } from '../services/search-application-two.filter';
import { ApplicationTwoSearchResultItemDto } from '../models/application-two-search-result-item.dto';
import { ApplicationTwoDto } from '../models/application-two.dto';
import { RecordEntryDto } from '../models/record-entry.dto';

@Injectable()
export class ApplicationTwoResource extends BaseResource
  implements IFilterable<SearchApplicationTwoFilter, SearchResultItemDto<ApplicationTwoSearchResultItemDto>>{
  constructor(
    protected http: HttpClient,
    protected configuration: Configuration
  ) {
    super(http, configuration, 'applicationTwo');
  }

  getFiltered(filter?: SearchApplicationTwoFilter): Observable<SearchResultItemDto<ApplicationTwoSearchResultItemDto>> {
    return this.http.get<SearchResultItemDto<ApplicationTwoSearchResultItemDto>>(`${this.baseUrl}` + this.composeQueryString(filter));
  }

  getById(id: number): Observable<ApplicationTwoDto> {
    return this.http.get<ApplicationTwoDto>(`${this.baseUrl}/${id}`);
  }

  createApplicationTwo(model: ApplicationTwoDto): Observable<number> {
    return this.http.post<number>(`${this.baseUrl}`, model);
  }

  addRecordEntry(model: RecordEntryDto): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}/recordEntry`, model);
  }

  importExcel(file: FormData): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}/importExcel`, file);
  }

  edit(model: ApplicationTwoDto): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}`, model);
  }
}