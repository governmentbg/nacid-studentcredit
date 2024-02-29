import { Injectable } from "@angular/core";
import { NomenclatureDto } from "src/infrastructure/models/nomenclature.dto";
import { BaseSearchFilter } from "src/infrastructure/services/base-search.filter";
import { ApplicationFiveType } from "../enums/application-five-type.enum";
import { CommitState } from "../../common/enums/commit-state.enum";

@Injectable()
export class SearchApplicationFiveFilter extends BaseSearchFilter {
  bank: NomenclatureDto;
  bankId: number | null;
  from: Date;
  to: Date;
  applicationFiveType: ApplicationFiveType;
  commitState: CommitState;

  constructor() {
    super(10);
  }
}