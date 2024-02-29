import { NomenclatureDto } from "src/infrastructure/models/nomenclature.dto";
import { ApplicationFiveType } from "../enums/application-five-type.enum";
import { CommitState } from "../../common/enums/commit-state.enum";

export class ApplicationFiveSearchResultItemDto {
  id: number;
  blankDate: Date;
  from: Date;
  to: Date;
  bank: NomenclatureDto;
  applicationFiveType: ApplicationFiveType;
  commitState: CommitState;
}