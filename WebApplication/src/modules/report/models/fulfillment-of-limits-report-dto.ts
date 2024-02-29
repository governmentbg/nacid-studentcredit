import { NomenclatureDto } from "src/infrastructure/models/nomenclature.dto";
import { FulfillmentOfLimitsInfoDto } from "./fulfillment-of-limits-info-dto";

export class FulfillmentOfLimitsReportDto {
  year: NomenclatureDto;
  yearLimit: number;
  fulfillmentOfLimits: FulfillmentOfLimitsInfoDto[] = [];
}