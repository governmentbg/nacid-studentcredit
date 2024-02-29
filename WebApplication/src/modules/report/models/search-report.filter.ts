import { NomenclatureDto } from "src/infrastructure/models/nomenclature.dto";
import { BaseSearchFilter } from "src/infrastructure/services/base-search.filter";
import { CreditType } from "src/modules/application/common/enums/credit-type.enum";
import { ReportType } from "../enums/report-type.enum";
import { NewContractFilterType } from "../enums/new-contract-filter-type.enum";
import { LearnerType } from "../enums/learner-type.enum";
import { InstitutionOwnershipType } from "src/modules/nomenclature/enums/institution-ownership-type.enum";

export class SearchReportFilter extends BaseSearchFilter {
  reportType: ReportType;
  newContractFilterType: NewContractFilterType;
  learnerType: LearnerType;
  institutionOwnershipType: InstitutionOwnershipType;
  bank: NomenclatureDto;
  bankId: number | null;
  contractYear: number | null;
  creditType: CreditType | null = null;

  startDate: Date;
  endDate: Date;

  constructor() {
    super(100);
  }
}
