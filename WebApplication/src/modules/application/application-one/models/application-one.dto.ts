import { AutoMap } from "@automapper/classes";
import { NomenclatureDto } from "src/infrastructure/models/nomenclature.dto";
import { CreditType } from "../../common/enums/credit-type.enum";
import { CourseType } from "../enums/course-type.enum";
import { EducationType } from "../enums/education-type.enum";
import { YearType } from "../enums/year-type.enum";
import { ApplicationOneStatus } from "../enums/application-one-status.enum";
import { RecontractionType } from "../enums/recontraction-type.enum";
import { CommitDto } from "../../common/models/commit.dto";
import { ExistingEnum } from "../../common/enums/existing.enum";

export class ApplicationOneDto extends CommitDto {
  applicationOneType: NomenclatureDto;
  blankDate: Date;
  applicationOneStatus: ApplicationOneStatus;
  isNewCredit: boolean = false;

  // #region ДАННИ ЗА БАНКАТА
  @AutoMap()
  bank: NomenclatureDto;
  @AutoMap()
  bulstat: string;
  @AutoMap()
  contactPerson: string;
  // #endregion

  // #region ДАННИ ЗА КРЕДИТОПОЛУЧАТЕЛЯ
  @AutoMap()
  personStudentDoctoralId: number;
  @AutoMap()
  educationType: EducationType;
  @AutoMap()
  studentFullName: string;
  @AutoMap()
  nationality: NomenclatureDto;
  @AutoMap()
  secondNationality: NomenclatureDto;
  @AutoMap()
  uin: string;
  @AutoMap()
  idNumber: string;
  @AutoMap()
  institution: NomenclatureDto;
  @AutoMap()
  researchArea: NomenclatureDto;
  @AutoMap()
  speciality: NomenclatureDto;
  @AutoMap()
  educationalQualification: NomenclatureDto;
  @AutoMap()
  educationFormType: NomenclatureDto;
  @AutoMap()
  studentCourse: CourseType | null;
  @AutoMap()
  doctoralYear: YearType | null;
  @AutoMap()
  facultyNumber: string;
  @AutoMap()
  uan: string;
  @AutoMap()
  birthDate: Date;

  // #endregion 

  // #region ДАННИ ЗА СКЛЮЧВАНЕ ИЛИ ОТКАЗ ЗА СКЛЮЧВАНЕ НА ДОГОВОР ЗА КРЕДИТ
  @AutoMap()
  creditType: CreditType;
  @AutoMap()
  contractDate: Date | null;
  @AutoMap()
  creditNumber: string;
  @AutoMap()
  schoolRemaining: number | null;
  cancelCondition: string;
  @AutoMap()
  paymentPeriod: number;
  @AutoMap()
  creditSize: number | null;
  @AutoMap()
  semesterTax: number | null;
  @AutoMap()
  interest: number | null;
  @AutoMap()
  description: string;
  // #endregion

  // #region ДАННИ ЗА ГРАТИСНИЯ ПЕРИОД
  @AutoMap()
  expirationDateOfGracePeriod: Date;
  @AutoMap()
  monthlyPayment: number;
  @AutoMap()
  paymentDescription: string;
  principalAbsorbed: number;
  interestDue: number;
  overallSize: number;
  // #endregion

  // #region ДАННИ ЗА ПОГАСЯВАНЕ НА КРЕДИТА
  paymentDate: Date | null;
  earlyPaymentDate: Date | null;
  // #endregion

  // #region ДАННИ ЗА ПРЕДОГОВАРЯНЕ НА КРЕДИТА
  recontractionType: RecontractionType;
  recontractionInfo: string;
  recontractionDate: Date | null;
  adjournType: NomenclatureDto;
  adjournDate: Date | null;
  adjournPeriod: number | null;
  extensionOfGracePeriod: NomenclatureDto;
  newExpirationDateOfGracePeriod: Date | null;
  principalAbsorbedInOldBank: number | null;
  interestDueInOldBank: number | null;
  overallSizeInOldBank: number | null;
  // #endregion

  // #region НАСТЪПВАНЕ НА ПРЕДСРОЧНА ИЗИСКУЕМОСТ И ПРЕДПРИЕМАНЕ НА ДЕЙСТВИЯ ПО ПРИНУДИТЕЛНО ИЗПЪЛНЕНИЕ ПО КРЕДИТА
  earlyDemandOfTheLoan: Date | null;
  forcePaymentDate: Date | null;
  forcePaymentInfo: string;
  // #endregion

  // #region ПОЛЕТА ОТ МИГРАЦИЯ
  @AutoMap()
  specialityEnum: ExistingEnum | null;
  @AutoMap()
  migrationSpecialityName: string;

  @AutoMap()
  researchAreaEnum: ExistingEnum | null;
  @AutoMap()
  migrationResearchAreaName: string;
  // #endregion
}