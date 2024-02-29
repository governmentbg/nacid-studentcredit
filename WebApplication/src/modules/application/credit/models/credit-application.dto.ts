import { NomenclatureDto } from "src/infrastructure/models/nomenclature.dto";
import { CommitState } from "../../common/enums/commit-state.enum";
import { BankNomenclatureDto } from "src/modules/nomenclature/common/models/bank-nomenclature.dto";

export class CreditApplicationDto {
  id: number;
  applicationType: NomenclatureDto;
  blankDate: Date;
  bank: BankNomenclatureDto;
  commitState: CommitState;
}