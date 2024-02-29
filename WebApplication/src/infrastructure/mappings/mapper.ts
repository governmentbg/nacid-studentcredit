import { createMap, createMapper, forMember, mapFrom } from '@automapper/core';
import { classes } from '@automapper/classes';
import { StudentSelectDto } from 'src/modules/student/models/student-select.dto';
import { ApplicationOneDto } from 'src/modules/application/application-one/models/application-one.dto';
import { ApplicationOneCreditSelectDto } from 'src/modules/application/application-one/models/application-one-credit-select.dto';
import { CreditSelectDto } from 'src/modules/application/common/models/credit-select.dto';
import { ApplicationFourDto } from 'src/modules/application/application-four/models/application-four.dto';

export const mapper = createMapper({
  strategyInitializer: classes(),
});

createMap(
  mapper,
  StudentSelectDto,
  ApplicationOneDto,
  forMember((m) => m.institution, mapFrom((src) => src.specialities[0].institution)),
  forMember((m) => m.researchArea, mapFrom((src) => src.specialities[0].researchArea)),
  forMember((m) => m.speciality, mapFrom((src) => src.specialities[0].speciality)),
  forMember((m) => m.educationalQualification, mapFrom((src) => src.specialities[0].educationalQualification)),
  forMember((m) => m.educationFormType, mapFrom((src) => src.specialities[0].educationFormType)),
  forMember((m) => m.studentCourse, mapFrom((src) => src.specialities[0].studentCourse)),
  forMember((m) => m.doctoralYear, mapFrom((src) => src.specialities[0].doctoralYear)),
  forMember((m) => m.facultyNumber, mapFrom((src) => src.specialities[0].facultyNumber)),
  forMember((m) => m.personStudentDoctoralId, mapFrom((src) => src.specialities[0].personStudentDoctoralId)),
  forMember((m) => m.nationality, mapFrom((src) => src.specialities[0].nationality)),
);

createMap(mapper, ApplicationOneCreditSelectDto, ApplicationOneDto);
createMap(mapper, CreditSelectDto, ApplicationFourDto);