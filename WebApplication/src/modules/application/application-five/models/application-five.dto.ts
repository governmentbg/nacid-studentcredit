import { NomenclatureDto } from "src/infrastructure/models/nomenclature.dto";
import { CommitDto } from "../../common/models/commit.dto";
import { ApplicationFiveType } from "../enums/application-five-type.enum";
import { AttachedFile } from "src/infrastructure/models/attached-file.model";
import { YearPeriod } from "../enums/year-period.enum";

export class ApplicationFiveDto extends CommitDto {
  applicationFiveType: ApplicationFiveType;
  blankDate: Date;
  bank: NomenclatureDto;
  amountRequested: number;
  amountRequestedCorrection: number;
  creditCount: number;
  from: Date;
  to: Date;
  applicationFiveAttachedFile: AttachedFile;
  period: YearPeriod;
  year: NomenclatureDto;
  amountRequestedAfterCorrection: number;
}