import { Observable } from 'rxjs';
import { CreditSelectFilterDto } from '../models/credit-select-filter.dto';
import { CommitState } from '../enums/commit-state.enum';
import { CreditSearchResultItemDto } from '../../credit/models/credit-search-result-item.dto';

export interface IApplicationResource {
  selectCredit<TDto>(dto?: CreditSelectFilterDto): Observable<TDto>;

  getByState(state: CommitState): Observable<CreditSearchResultItemDto[]>;
}
