import { Directive, HostListener, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { finalize } from 'rxjs/operators';
import { IFilterable } from 'src/infrastructure/interfaces/filterable.interface';
import { SearchResultItemDto } from 'src/infrastructure/models/search-result-item.dto';
import { BaseSearchFilter } from 'src/infrastructure/services/base-search.filter';
import { LoadingIndicatorService } from '../loading-indicator/loading-indicator.service';

@Directive({})
export abstract class BaseSearchComponent<TDto> implements OnInit {

  @HostListener('document:keypress', ['$event'])
  handleKeyboardEvent(event: KeyboardEvent) {
    if (event.key === 'Enter') {
      this.search();
    }
  }

  model: TDto[] = [];
  canLoadMore: boolean;
  modelCounts = 0;
  totalCounts = 0;

  constructor(
    public filter: BaseSearchFilter,
    protected resource: IFilterable<BaseSearchFilter, SearchResultItemDto<TDto>>,
    protected loadingIndicator: LoadingIndicatorService
  ) { }

  ngOnInit(): void {
    this.search();
  }

  search(loadMore?: boolean): Subscription {
    if (!loadMore) {
      this.filter.offset = 0;
    }

    this.loadingIndicator.show();
    return this.resource.getFiltered(this.filter)
      .pipe(
        finalize(() => this.loadingIndicator.hide())
      )
      .subscribe((result: SearchResultItemDto<TDto>) => {
        this.totalCounts = result.totalCount;

        if (!this.filter.offset) {
          this.modelCounts = result.items.length
          this.model = result.items;
        } else {
          this.modelCounts = this.modelCounts + result.items.length
          this.model = [...this.model, ...result.items];
        }

        this.canLoadMore = result.items.length === this.filter.limit;
        this.filter.offset = this.model.length;
      });
  }

  loadMore(): void {
    this.filter.offset = this.model.length;
    this.search(true);
  }

  clearFilter(): void {
    this.filter.clear();
    this.modelCounts = 0;
    this.search();
  }
}
