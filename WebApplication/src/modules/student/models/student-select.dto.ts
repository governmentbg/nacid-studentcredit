import { AutoMap } from "@automapper/classes";
import { NomenclatureDto } from "src/infrastructure/models/nomenclature.dto";
import { EducationType } from "src/modules/application/application-one/enums/education-type.enum";
import { EducationSelectDto } from "src/modules/student/models/education-select.dto";

export class StudentSelectDto {
  @AutoMap()
  studentFullName: string;
  @AutoMap()
  secondNationality: NomenclatureDto;
  @AutoMap()
  uin: string;
  @AutoMap()
  idNumber: string;
  @AutoMap()
  uan: string;
  @AutoMap()
  educationType: EducationType;
  @AutoMap()
  birthDate: Date;
  @AutoMap()
  specialities: EducationSelectDto[] = [];
  @AutoMap()
  doctorals: EducationSelectDto[] = [];
}