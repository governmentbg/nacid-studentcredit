import { NomenclatureDto } from "src/infrastructure/models/nomenclature.dto";
import { SheetRowDto } from "./sheet-row.dto";
import { MonthDataDto } from "./month-data.dto";

export class SheetYearDto extends SheetRowDto {
  id: number;
  year: NomenclatureDto;

  sheetList: MonthDataDto[] = [];

  isExpand: boolean;
}