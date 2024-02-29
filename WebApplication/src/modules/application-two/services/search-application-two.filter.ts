import { Injectable } from '@angular/core';
import { NomenclatureDto } from 'src/infrastructure/models/nomenclature.dto';
import { BaseSearchFilter } from 'src/infrastructure/services/base-search.filter';

@Injectable()
export class SearchApplicationTwoFilter extends BaseSearchFilter {
  bank: NomenclatureDto;
  bankId: number | null;

  fromDate: Date;
  toDate: Date;

  constructor() {
    super(10);
  }
}
