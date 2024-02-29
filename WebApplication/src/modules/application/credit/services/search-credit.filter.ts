import { Injectable } from '@angular/core';
import { NomenclatureDto } from 'src/infrastructure/models/nomenclature.dto';
import { BaseSearchFilter } from 'src/infrastructure/services/base-search.filter';
import { CreditType } from '../../common/enums/credit-type.enum';
import { CommitState } from '../../common/enums/commit-state.enum';

@Injectable()
export class SearchCreditFilter extends BaseSearchFilter {
  bank: NomenclatureDto;
  bankId: number | null;
  uin: string;
  creditNumber: string;
  uan: string;
  contractDate: Date | null;
  type: CreditType | null = null;
  institution: NomenclatureDto;
  institutionId: number | null;
  studentFullName: string;
  commitState: CommitState = CommitState.approved;

  constructor() {
    super(10);
  }
}
