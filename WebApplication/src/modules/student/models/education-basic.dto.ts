import { InstitutionDto } from "src/infrastructure/models/institution.dto";
import { NomenclatureDto } from "src/infrastructure/models/nomenclature.dto";

export class EducationBasicDto {
  id: number;
  institution: InstitutionDto;
  speciality: NomenclatureDto;
  educationalQualification: NomenclatureDto;
  educationalForm: NomenclatureDto;
  status: NomenclatureDto;
  researchArea: NomenclatureDto;
  duration: number;

  graduationDate: Date;
  interruptionReason: string;
}