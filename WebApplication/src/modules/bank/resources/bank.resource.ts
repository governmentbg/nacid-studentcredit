import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Configuration } from "src/infrastructure/configuration/configuration";
import { IFilterable } from "src/infrastructure/interfaces/filterable.interface";
import { SearchResultItemDto } from "src/infrastructure/models/search-result-item.dto";
import { BaseResource } from "src/infrastructure/services/base.resource";
import { BankDto } from "../model/bank.dto";
import { BankSearchFilter } from "../services/bank-search.filter";

@Injectable()
export class BankResource extends BaseResource 
	implements IFilterable<BankSearchFilter, SearchResultItemDto<BankDto>> {
  constructor(
		protected http: HttpClient,
		protected configuration: Configuration
	) {
		super(http, configuration, 'Bank');
	 }

  getFiltered(filter?: BankSearchFilter): Observable<SearchResultItemDto<BankDto>> {
    return this.http.get<SearchResultItemDto<BankDto>>(`${this.baseUrl}` + this.composeQueryString(filter));
  }

	get(id: number): Observable<BankDto> {
		return this.http.get<BankDto>(`${this.configuration.restUrl}/Bank/${id}`);
	}

	create(model: BankDto): Observable<void> {
		return this.http.post<void>(`${this.configuration.restUrl}/Bank`, model);
	}

	changeStatus(id: number): Observable<boolean> {
		return this.http.post<boolean>(`${this.configuration.restUrl}/Bank/changeStatus/${id}`, null);
	}

	edit(model: BankDto): Observable<void> {
		return this.http.put<void>(`${this.configuration.restUrl}/Bank`, model);
	}
}