import { Component } from '@angular/core';
import { LoadingIndicatorService } from 'src/infrastructure/components/loading-indicator/loading-indicator.service';
import { BaseSearchComponent } from 'src/infrastructure/components/search-component/base-search.component';
import { ContractType } from '../enums/contract-type.enum';
import { BankDto } from '../model/bank.dto';
import { BankResource } from '../resources/bank.resource';
import { BankSearchFilter } from '../services/bank-search.filter';

@Component({
  selector: 'app-bank',
  templateUrl: 'bank.component.html'
})

export class BankComponent extends BaseSearchComponent<BankDto> {
  contractTypes = ContractType;

  constructor(
    public filter: BankSearchFilter,
    protected resource: BankResource,
    protected loadingIndicator: LoadingIndicatorService
  ) {
    super(filter, resource, loadingIndicator);
  }

  ngOnInit() {
    super.search();
  }
}