import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { Configuration } from "src/infrastructure/configuration/configuration";
import { BaseResource } from "src/infrastructure/services/base.resource";
import { ApplicationHistoryDto } from "../models/application-history.dto";
import { CreditSelectFilterDto } from "../models/credit-select-filter.dto";
import { CommitState } from "../enums/commit-state.enum";
import { CreditSearchResultItemDto } from "../../credit/models/credit-search-result-item.dto";

export class ApplicationResource<TDto> extends BaseResource {

  constructor(
    protected http: HttpClient,
    protected configuration: Configuration,
    protected suffix: string
  ) {
    super(http, configuration, suffix);
  }

  getById(id: number): Observable<TDto> {
    return this.http.get<TDto>(`${this.baseUrl}/${id}`);
  }

  getHistory(rootId: number): Observable<ApplicationHistoryDto[]> {
    return this.http.get<ApplicationHistoryDto[]>(`${this.baseUrl}/history/${rootId}`);
  }

  selectCredit<TReturnDto>(dto: CreditSelectFilterDto): Observable<TReturnDto> {
    return this.http.get<TReturnDto>(`${this.baseUrl}/selectCredit` + this.composeQueryString(dto));
  }

  getByState(state: CommitState): Observable<CreditSearchResultItemDto[]> {
    return this.http.get<CreditSearchResultItemDto[]>(`${this.baseUrl}/getByState?state=${state}`);
  }

  create(dto: TDto): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}`, dto);
  }

  returnForModification(dto: ApplicationHistoryDto, state: CommitState): Observable<TDto> {
    return this.http.post<TDto>(`${this.baseUrl}/returnForModification?state=${state}`, dto);
  }

  finishModification(dto: TDto, state: CommitState) {
    return this.http.post<void>(`${this.baseUrl}/finishModification?state=${state}`, dto);
  }

  approve(applicationId: number): Observable<TDto> {
    return this.http.post<TDto>(`${this.baseUrl}/${applicationId}/approve`, null)
  }
}