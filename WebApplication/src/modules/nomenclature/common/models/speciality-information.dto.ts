import { NomenclatureDto } from "src/infrastructure/models/nomenclature.dto";

export class SpecialityInformationDto {
  id: number;
  duration: number;
  name: string;
  form: NomenclatureDto;
  qualification: NomenclatureDto;
  instituton: NomenclatureDto;
  researchArea: NomenclatureDto;
}
