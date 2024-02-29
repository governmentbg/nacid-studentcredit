import { AutoMap } from "@automapper/classes";
import { NomenclatureDto } from "src/infrastructure/models/nomenclature.dto";
import { CourseType } from "../enums/course-type.enum";
import { YearType } from "../enums/year-type.enum";
import { CreditSelectDto } from "../../common/models/credit-select.dto";
import { ExistingEnum } from "../../common/enums/existing.enum";

export class ApplicationOneCreditSelectDto extends CreditSelectDto {
  @AutoMap()
  nationality: NomenclatureDto;
  @AutoMap()
  secondNationality: NomenclatureDto;
  @AutoMap()
  idNumber: string;
  @AutoMap()
  researchArea: NomenclatureDto;
  @AutoMap()
  educationFormType: NomenclatureDto;
  @AutoMap()
  educationalQualification: NomenclatureDto;
  @AutoMap()
  studentCourse: CourseType | null;
  @AutoMap()
  doctoralYear: YearType | null;

  @AutoMap()
  contactPerson: string;

  @AutoMap()
  expirationDateOfGracePeriod: Date | null;
  @AutoMap()
  schoolRemaining: number | null;
  @AutoMap()
  creditSize: number | null;
  @AutoMap()
  semesterTax: number | null;

  @AutoMap()
  monthlyPayment: number | null;
  @AutoMap()
  paymentDescription: string;
}