import { ApplicationFourAttachedFileType } from "src/modules/application/application-four/enums/application-four-attached-file-type";
import { CourseType } from "src/modules/application/application-one/enums/course-type.enum";
import { EducationType } from "src/modules/application/application-one/enums/education-type.enum";
import { YearType } from "src/modules/application/application-one/enums/year-type.enum";
import { CreditType } from "src/modules/application/common/enums/credit-type.enum";
import { SearchIdentificationType } from "src/modules/student/enums/search-identification.enum";
import { CommitState } from "../../modules/application/common/enums/commit-state.enum";
import { ApplicationHistoryType } from "src/modules/application/common/enums/application-history-type.enum";
import { UserStatus } from "src/modules/user/enums/user-status.enum";
import { ApplicationFiveType } from "src/modules/application/application-five/enums/application-five-type.enum";
import { YearPeriod } from "src/modules/application/application-five/enums/year-period.enum";

export const CourseTypeEnumLocalization = {
  [CourseType.first]: 'Първи',
  [CourseType.second]: 'Втори',
  [CourseType.third]: 'Трети',
  [CourseType.fourth]: 'Четвърти',
  [CourseType.fifth]: 'Пети',
  [CourseType.sixth]: 'Шести',
  [CourseType.seventh]: 'Седми',
};

export const YearTypeEnumLocalization = {
  [YearType.firstYear]: 'Първа година',
  [YearType.secondYear]: 'Втора година',
  [YearType.thirdYear]: 'Трета година',
  [YearType.fourthYear]: 'Четвърта година',
  [YearType.fifthYear]: 'Пета година',
};

export const CreditTypeEnumLocalization = {
  [CreditType.educationTaxes]: 'Заплащане на таксите за обучение',
  [CreditType.maintenance]: 'Издръжка',
}

export const EducationTypeEnumLocalization = {
  [EducationType.student]: 'Студент',
  [EducationType.doctoral]: 'Докторант',
}

export const ApplicationFourAttachedFileTypeEnumLocalizaiton = {
  [ApplicationFourAttachedFileType.debtStatementDocument]: 'Извлечение от сметка по партидата на кредитополучателя с посочване на главницата и дължимите лихви към датата на съставяне на извлечението',
  [ApplicationFourAttachedFileType.creditContractAndAnnexes]: 'Договор за кредит и анекс/и към него',
  [ApplicationFourAttachedFileType.documentInFavorOfTheBank]: 'Изпълнителен лист срещу кредитополучателя, издаден в полза на банката',
  [ApplicationFourAttachedFileType.documentPartialRepayment]: 'Документи, удостоверяващи частично погасяване на задължението, ако е извършено такова',
  [ApplicationFourAttachedFileType.deathCertificate]: 'Копие от акта за смърт на длъжника',
  [ApplicationFourAttachedFileType.permanentIncapacity]: 'Копие от експертните решения на ТЕЛК или НЕЛК, удостоверяващи трайна неработоспособност 70 или над 70 на сто на длъжника',
  [ApplicationFourAttachedFileType.birthCertificatesOrCourtDecisionsForAdoptions]: 'Копие от актовете за раждане/ от влезли в сила съдебни решения, с които е уважено искането за осиновяване, за две или повече деца'
}

export const SearchIdentificationEnumLocalization = {
  [SearchIdentificationType.UIN]: 'ЕГН',
  [SearchIdentificationType.ForeignerNumber]: 'ЛНЧ',
  [SearchIdentificationType.UAN]: 'ЕАН'
}

export const CommitStateEnumLocalization = {
  [CommitState.draft]: 'Чернова',
  [CommitState.modification]: 'Върнато за редакция',
  [CommitState.pending]: 'Изпратено за одобрение',
  [CommitState.approved]: 'Одобрено',
  [CommitState.archived]: 'Архивирано',
  [CommitState.denied]: 'Отхвърлен',
  [CommitState.approvedModification]: 'В процес на техническа редакция'
}

export const ApplicationHistoryEnumLocalization = {
  [ApplicationHistoryType.modification]: 'Върнато за редакция',
  [ApplicationHistoryType.edit]: 'Техническа редакция',
}

export const UserStatusEnumLocalization = {
  [UserStatus.active]: 'Активен',
  [UserStatus.inactive]: 'Неактивиран',
  [UserStatus.deactivated]: 'Деактивиран',
};

export const ApplicationFiveTypeEnumLocalization = {
  [ApplicationFiveType.totalDebtExposure]: 'Дължими суми по чл. 39, т. 1',
  [ApplicationFiveType.repaidCreditObligationsInTheRelevantYear]: 'Дължими суми по чл. 39, т. 2',
  [ApplicationFiveType.bankExpensesForTheEnforcementActions]: 'Дължими суми по чл. 39, т. 3'
}

export const YearPeriodEnumLocalization = {
  [YearPeriod.first]: '1во шестмесечие',
  [YearPeriod.second]: '2ро шестмесечие'
}
