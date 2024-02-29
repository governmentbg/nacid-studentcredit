import { BankNomenclatureDto } from "src/modules/nomenclature/common/models/bank-nomenclature.dto";

export class ApplicationTwoSearchResultItemDto {
  id: number;
  bank: BankNomenclatureDto;
  totalSum: number;
  creditCount: number;
  creationDate: Date;
  recordDate: Date;
}
