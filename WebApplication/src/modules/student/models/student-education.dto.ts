import { CourseType } from "src/modules/application/application-one/enums/course-type.enum";
import { EducationBasicDto } from "./education-basic.dto";

export class StudentEducationDto extends EducationBasicDto {
  course: CourseType;
  startPeriod: string;
  finishPeriod: string;
}