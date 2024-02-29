import { NomenclatureDto } from "src/infrastructure/models/nomenclature.dto";
import { EducationType } from "../../application-one/enums/education-type.enum";
import { CreditType } from "../../common/enums/credit-type.enum";
import { CreditApplicationDto } from "./credit-application.dto";
import { RecordEntryDto } from "src/modules/application-two/models/record-entry.dto";
import { CommitState } from "../../common/enums/commit-state.enum";
import { ExistingEnum } from "../../common/enums/existing.enum";

export class CreditInfo {
  id: number;
  commitState: CommitState;
  paidByApplicationFour: boolean;

  studentFullName: string;
  uin: string;
  institution: NomenclatureDto;
  researchArea: NomenclatureDto;
  speciality: NomenclatureDto;
  educationalQualification: NomenclatureDto;
  uan: string;
  educationType: EducationType;
  birthDate: Date;
  specialityEnum: ExistingEnum;
  migrationSpecialityName: string;
  researchAreaEnum: ExistingEnum;
  migrationResearchAreaName: string;

  bank: NomenclatureDto;

  contractDate: Date | null;
  creditNumber: string;
  creditType: CreditType;
  expirationDateOfGracePeriod: Date | null;
  paymentPeriod: number;
  creditSize: number | null;

  applicationsOne: CreditApplicationDto[] = [];
  applicationsTwo: RecordEntryDto[] = [];
  applicationsFour: CreditApplicationDto[] = [];
}