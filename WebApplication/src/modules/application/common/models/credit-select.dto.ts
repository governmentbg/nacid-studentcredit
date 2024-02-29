import { NomenclatureDto } from "src/infrastructure/models/nomenclature.dto";
import { CreditType } from "../enums/credit-type.enum";
import { AutoMap } from "@automapper/classes";
import { EducationType } from "../../application-one/enums/education-type.enum";
import { ExistingEnum } from "../enums/existing.enum";

export class CreditSelectDto {
  @AutoMap()
  applicationOneId: number;

  @AutoMap()
  bank: NomenclatureDto;
  @AutoMap()
  bulstat: string;

  @AutoMap()
  personStudentDoctoralId: number;
  @AutoMap()
  studentFullName: string;
  @AutoMap()
  birthDate: Date;
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
  educationType: EducationType;

  @AutoMap()
  creditNumber: string;
  @AutoMap()
  creditType: CreditType;
  @AutoMap()
  contractDate: Date;
  @AutoMap()
  paymentPeriod: number | null;
  @AutoMap()
  interest: number | null;
  @AutoMap()
  description: string;

  @AutoMap()
  specialityEnum: ExistingEnum;
  @AutoMap()
  migrationSpecialityName: string;

  @AutoMap()
  researchAreaEnum: ExistingEnum;
  @AutoMap()
  migrationResearchAreaName: string;
}