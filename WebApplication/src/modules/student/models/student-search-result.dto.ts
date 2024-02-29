import { DoctoralEducationDto } from "./doctoral-education.dto";
import { StudentCreditDto } from "./student-credit.dto";
import { StudentEducationDto } from "./student-education.dto";

export class StudentSearchResult {
  fullName: string;
  uan: string;

  uin: string;
  foreignerNumber: number;
  idnNumber: string;
  birthDate: Date;

  universities: StudentEducationDto[] = [];
  doctorals: DoctoralEducationDto[] = [];
  credits: StudentCreditDto[] = [];
}