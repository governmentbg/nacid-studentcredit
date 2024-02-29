import { NomenclatureDto } from "src/infrastructure/models/nomenclature.dto";
import { SearchIdentificationType } from "../enums/search-identification.enum";

export class StudentFilterDto {
  identificator: string;
  identificationType: SearchIdentificationType;

  educationalQualification: NomenclatureDto;
  educationalQualificationId: number | null;

  educationalForm: NomenclatureDto;
  educationalFormId: number | null;
}