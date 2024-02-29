import { NomenclatureDto } from "src/infrastructure/models/nomenclature.dto";
import { SheetRowDto } from "./sheet-row.dto";
import { SheetYearDto } from "./sheet-year.dto";

export class SheetDto extends SheetRowDto {
  id: number;
  year: NomenclatureDto;
  bank: NomenclatureDto;

  sheetList: SheetYearDto[] = [];
}