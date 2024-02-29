import { NewContractFilterType } from "../enums/new-contract-filter-type.enum";
import { ReportType } from "../enums/report-type.enum";

export class ReportSearchResultItemDto {
  reportType: ReportType;
  newContractFilterType: NewContractFilterType;

  isStudents: boolean;
  isDoctorals: boolean;

  isSelfRepaid: boolean;
  isStateRepaid: boolean;

  bankName: string;
  institutionName: string;
  researchAreaName: string;
  nationalityName: string;
  ageAtContract: number;

  educationTaxesContractCount: number;
  educationTaxesSize: number;

  maintenanceContractCount: number;
  maintenanceSize: number;

  educationTaxesContractRefusedCount: number;
  maintenanceContractRefusedCount: number;

  selfRepaidCount: number;
  selfRepaidSize: number;

  deathCount: number;
  deathSize: number;

  birthOrFullAdoptationCount: number;
  birthOrFullAdoptationSize: number;

  permanentIncapacityCount: number;
  permanentIncapacitySize: number;

  bankClaimCount: number;
  bankClaimSize: number;

  earlyDellabilityCount: number;
  earlyDellabilitySize: number;

  renegotiationCount: number;
  renegotiationSize: number;

  stateCount: number;
  stateSize: number;

  privateCount: number;
  privateSize: number;

  currentTotalCount: number;
  currentTotalSize: number;
  currentTotalRefusedCount: number;
}