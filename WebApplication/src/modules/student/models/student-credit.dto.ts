import { NomenclatureDto } from "src/infrastructure/models/nomenclature.dto";
import { CreditType } from "src/modules/application/common/enums/credit-type.enum";

export class StudentCreditDto {
  creditType: CreditType;
  contractDate: Date;
  bankName: string;
  educationalQualificationName: string;
  specialityName: string;
  applicationOneType: NomenclatureDto;
}