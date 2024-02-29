import { AutoMap } from "@automapper/classes";
import { NomenclatureDto } from "src/infrastructure/models/nomenclature.dto";
import { CourseType } from "src/modules/application/application-one/enums/course-type.enum";
import { YearType } from "src/modules/application/application-one/enums/year-type.enum";

export class EducationSelectDto {
  @AutoMap()
  personStudentDoctoralId: number;
  @AutoMap()
  institution: NomenclatureDto;
  @AutoMap()
  researchArea: NomenclatureDto;
  @AutoMap()
  speciality: NomenclatureDto;
  @AutoMap()
  educationFormType: NomenclatureDto;
  @AutoMap()
  educationalQualification: NomenclatureDto;
  @AutoMap()
  studentCourse: CourseType | null;
  @AutoMap()
  doctoralYear: YearType | null;
  @AutoMap()
  facultyNumber: string;
  @AutoMap()
  nationality: NomenclatureDto;
}