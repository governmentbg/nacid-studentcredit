import { SheetRowDto } from "./sheet-row.dto";

export class MonthDataDto extends SheetRowDto {
  id: number;
  month: number;
  sheetYearId: number;
}