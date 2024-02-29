import { Injectable } from '@angular/core';
import { BaseSearchFilter } from 'src/infrastructure/services/base-search.filter';
import { UserStatus } from '../enums/user-status.enum';
import { Nomenclature } from 'src/modules/nomenclature/common/models/nomenclature.model';
import { NomenclatureDto } from 'src/infrastructure/models/nomenclature.dto';

@Injectable({
  providedIn: 'root'
})
export class UserSearchFilter extends BaseSearchFilter {
  firstName: string;
  middleName: string;
  lastName: string;
  username: string;
  email: string;

  roleId: number | null;
  role: NomenclatureDto;
  status: UserStatus | null;

  bank: NomenclatureDto;
  bankId: number;

  constructor() {
    super(10);
    this.status = null;
  }
}
