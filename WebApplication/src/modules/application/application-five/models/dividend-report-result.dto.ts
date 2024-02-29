import { NomenclatureDto } from "src/infrastructure/models/nomenclature.dto";
import { ApplicationFiveType } from "../enums/application-five-type.enum";

export class DividendReportResultDto {
  totalSum: number;
  amountRequestedCorrection: number;
  applicationFiveType: ApplicationFiveType;
  bank: NomenclatureDto;
  year: NomenclatureDto;
}