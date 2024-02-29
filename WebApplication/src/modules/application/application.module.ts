import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { TranslateModule } from '@ngx-translate/core';
import { CommonBootstrapModule } from 'src/infrastructure/common-bootstrap.module';
import { ApplicationOneResource } from './application-one/resources/application-one.resource';
import { ApplicationFourResource } from './application-four/resources/application-four.resource';
import { ApplicationRoutingModule } from './application-routing.module';
import { ApplicationOneNewComponent } from './application-one/components/application-one-new/application-one-new.component';
import { StudentSelectSearchFilter } from './application-one/services/student-select-search.filter';
import { ApplicationOneStudentSelectComponent } from './application-one/components/application-one-student-select/application-one-student-select.component';
import { SearchCreditFilter } from './credit/services/search-credit.filter';
import { CreditResource } from './credit/resources/credit-resource';
import { CreditSearchComponent } from './credit/credit-search/credit-search.component';
import { CreditInfoComponent } from './credit/credit-info/credit-info.component';
import { FormsModule } from '@angular/forms';
import { ApplicationFourComponent } from './application-four/components/application-four/application-four.component';
import { ApplicationOneComponent } from './application-one/components/application-one/application-one.component';
import { ApplicationService } from './common/services/application.service';
import { CreditAboutFormComponent } from './application-one/components/common/credit-about-form/credit-about-form.component';
import { BankFormComponent } from './application-one/components/common/bank-form/bank-form.component';
import { StudentInfoFormComponent } from './application-one/components/common/student-info-form/student-info-form.component';
import { ContractFormComponent } from './application-one/components/common/contract-form/contract-form.component';
import { GratisPeriodFormComponent } from './application-one/components/common/gratis-period-form/gratis-period-form.component';
import { RepaymentFormComponent } from './application-one/components/common/repayment-form/repayment-form.component';
import { PreNegoatiationFormComponent } from './application-one/components/common/pre-negoatiation-form/pre-negoatiation-form.component';
import { ActionOnLoanFormComponent } from './application-one/components/common/action-on-loan-form/action-on-loan-form.component';
import { ApplicationFourAboutFormComponent } from './application-four/components/common/application-four-about-form/application-four-about-form.component';
import { BankWithAccountFormComponent } from './application-four/components/common/bank-with-account-form/bank-with-account-form.component';
import { StudentInfoShortFormComponent } from './application-four/components/common/student-info-short-form/student-info-short-form.component';
import { CreditInfoShortFormComponent } from './application-four/components/common/credit-info-short-form/credit-info-short-form.component';
import { ApplicationFourAttachedFilesComponent } from './application-four/components/common/application-four-attached-files/application-four-attached-files.component';
import { ApplicationFourNewComponent } from './application-four/components/application-four-new/application-four-new.component';
import { ApplicationOneHistoryComponent } from './application-one/components/application-one-history.component.ts/application-one-history.component';
import { ApplicationFourHistoryComponent } from './application-four/components/application-four-history/application-four-history.component';
import { ApplicationTwoModule } from '../application-two/application-two.module';
import { CreditSearchTableComponent } from './credit/credit-search/credit-search-table/credit-search-table.component';
import { SearchApplicationFiveFilter } from './application-five/services/search-application-five.filter';
import { ApplicationFiveResource } from './application-five/resources/application-five.resource';
import { ApplicationFiveSearchComponent } from './application-five/components/application-five-search/application-five-search.component';
import { ApplicationFiveNewComponent } from './application-five/components/application-five-new/application-five-new.component';
import { ApplicationFiveComponent } from './application-five/components/application-five/application-five.component';
import { ApplicationFiveInfoComponent } from './application-five/components/application-five-info/application-five-info.component';
import { ApplicationFiveHistoryComponent } from './application-five/components/application-five-history/application-five-history.component';
import { ReportDividendComponent } from './application-five/components/report-dividend/report-dividend.component';


@NgModule({
  declarations: [
    ApplicationFourComponent,
    ApplicationFourNewComponent,
    ApplicationOneComponent,
    ApplicationOneNewComponent,
    ApplicationOneStudentSelectComponent,
    CreditSearchComponent,
    CreditInfoComponent,
    CreditAboutFormComponent,
    BankFormComponent,
    StudentInfoFormComponent,
    ContractFormComponent,
    GratisPeriodFormComponent,
    RepaymentFormComponent,
    PreNegoatiationFormComponent,
    ActionOnLoanFormComponent,
    ApplicationFourAboutFormComponent,
    BankWithAccountFormComponent,
    StudentInfoShortFormComponent,
    CreditInfoShortFormComponent,
    ApplicationFourAttachedFilesComponent,
    ApplicationOneHistoryComponent,
    ApplicationFourHistoryComponent,
    CreditSearchTableComponent,
    ApplicationFiveSearchComponent,
    ApplicationFiveNewComponent,
    ApplicationFiveComponent,
    ApplicationFiveInfoComponent,
    ApplicationFiveHistoryComponent,
    ReportDividendComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    CommonBootstrapModule,
    ApplicationRoutingModule,
    TranslateModule,
    ApplicationTwoModule
  ],
  providers: [
    ApplicationOneResource,
    ApplicationFourResource,
    StudentSelectSearchFilter,
    SearchCreditFilter,
    CreditResource,
    ApplicationService,
    SearchApplicationFiveFilter,
    ApplicationFiveResource
  ]
})
export class ApplicationModule { }
