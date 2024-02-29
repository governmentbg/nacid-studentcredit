import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Configuration } from "src/infrastructure/configuration/configuration";
import { IFilterable } from "src/infrastructure/interfaces/filterable.interface";
import { SearchResultItemDto } from "src/infrastructure/models/search-result-item.dto";
import { BaseResource } from "src/infrastructure/services/base.resource";
import { BankDto } from "../models/bank.dto";
import { BankSearchFilter } from "../services/bank-search.filter";

@Injectable({
  providedIn: 'root'
})
export class BankResource extends BaseResource
  implements IFilterable<BankSearchFilter, SearchResultItemDto<BankDto>>{

  constructor(
    protected override http: HttpClient,
    protected override configuration: Configuration
  ) {
    super(http, configuration, 'Bank');
  }
  getFiltered(filter?: BankSearchFilter): Observable<SearchResultItemDto<BankDto>> {
    return this.http.get<SearchResultItemDto<BankDto>>(`${this.baseUrl}` + this.composeQueryString(filter));
  }

  getBankInfo(id: number): Observable<BankDto> {
    return this.http.get<BankDto>(`${this.baseUrl}/${id}`)
  }
}