import { NomenclatureDto } from "src/infrastructure/models/nomenclature.dto";
import { EducationType } from "../../application-one/enums/education-type.enum";
import { CreditType } from "../../common/enums/credit-type.enum";
import { CommitState } from "../../common/enums/commit-state.enum";

export class CreditSearchResultItemDto {
  id: number;
  contractDate: Date;
  creditNumber: string;
  creditType: CreditType;
  bank: NomenclatureDto;
  studentFullName: string;
  uin: string;
  uan: string;
  educationType: EducationType;
  educationalQualification: NomenclatureDto;
  institution: string;
  birthDate: Date;
  commitState: CommitState;
  rootId: number;
  paidByApplicationFour: boolean;
  applicationType: NomenclatureDto;
  applicationOneId: number;
}
