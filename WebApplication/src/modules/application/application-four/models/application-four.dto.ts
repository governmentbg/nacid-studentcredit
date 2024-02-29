import { NomenclatureDto } from "src/infrastructure/models/nomenclature.dto"
import { CreditType } from "../../common/enums/credit-type.enum";
import { ApplicationFourAttachedFileType } from "../enums/application-four-attached-file-type";
import { ApplicationFourAttachedFileDto } from "./application-four-attached-file.dto";
import { CommitDto } from "../../common/models/commit.dto";
import { AutoMap } from "@automapper/classes";
import { EducationType } from "../../application-one/enums/education-type.enum";
import { ExistingEnum } from "../../common/enums/existing.enum";

export class ApplicationFourDto extends CommitDto {
  type: NomenclatureDto;
  blankDate: Date;
  bankExpenses: number;

  @AutoMap()
  applicationOneId: number;

  @AutoMap()
  bank: NomenclatureDto;
  @AutoMap()
  bulstat: string;
  bankAccount: string;

  @AutoMap()
  personStudentDoctoralId: number;
  @AutoMap()
  studentFullName: string;
  @AutoMap()
  uin: string;
  @AutoMap()
  institution: NomenclatureDto;
  @AutoMap()
  speciality: NomenclatureDto;
  @AutoMap()
  uan: string;
  @AutoMap()
  facultyNumber: string;
  @AutoMap()
  birthDate: Date;
  @AutoMap()
  educationType: EducationType;

  @AutoMap()
  creditNumber: string;
  @AutoMap()
  creditType: CreditType;
  @AutoMap()
  contractDate: Date;
  schoolRemaining: number | null;
  @AutoMap()
  paymentPeriod: number;
  @AutoMap()
  interest: number;
  @AutoMap()
  description: string;
  outstandingDebtAmount: number;
  files: ApplicationFourAttachedFileDto[] = [];

  @AutoMap()
  specialityEnum: ExistingEnum;
  @AutoMap()
  migrationSpecialityName: string;

  constructor() {
    super()
    this.files.push(new ApplicationFourAttachedFileDto(ApplicationFourAttachedFileType.debtStatementDocument));
    this.files.push(new ApplicationFourAttachedFileDto(ApplicationFourAttachedFileType.creditContractAndAnnexes));
    this.files.push(new ApplicationFourAttachedFileDto(ApplicationFourAttachedFileType.documentInFavorOfTheBank));
    this.files.push(new ApplicationFourAttachedFileDto(ApplicationFourAttachedFileType.documentPartialRepayment));
    this.files.push(new ApplicationFourAttachedFileDto(ApplicationFourAttachedFileType.deathCertificate));
    this.files.push(new ApplicationFourAttachedFileDto(ApplicationFourAttachedFileType.permanentIncapacity));
    this.files.push(new ApplicationFourAttachedFileDto(ApplicationFourAttachedFileType.birthCertificatesOrCourtDecisionsForAdoptions));
  }
}