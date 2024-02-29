import { FilterDto } from "src/infrastructure/models/filter.dto";

export class BaseNomenclatureFilterDto extends FilterDto {
  entityId: number | null;
}
