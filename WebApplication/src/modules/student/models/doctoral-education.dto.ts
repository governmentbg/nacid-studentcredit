import { YearType } from "src/modules/application/application-one/enums/year-type.enum";
import { EducationBasicDto } from "./education-basic.dto";

export class DoctoralEducationDto extends EducationBasicDto {
  yearType: YearType;
  startDate: Date;
  finishDate: Date;
}