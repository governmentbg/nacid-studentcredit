import { NomenclatureDto } from "src/infrastructure/models/nomenclature.dto";
import { ApplicationFiveType } from "../enums/application-five-type.enum";
import { YearPeriod } from "../enums/year-period.enum";

export class DividendReportSearchDto {
  bank: NomenclatureDto;
  applicationFiveType: ApplicationFiveType;
  year: NomenclatureDto;
  from: Date;
  to: Date;
  period: YearPeriod;
}