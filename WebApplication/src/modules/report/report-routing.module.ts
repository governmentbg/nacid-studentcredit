import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ReportComponent } from './components/report/report.component';
import { SummaryReportComponent } from './components/summary-report/summary-report.component';
import { AuthGuard } from 'src/infrastructure/guards/auth-guard';
import { RoleGuard } from 'src/infrastructure/guards/role-guard';
import { UserRoleAliases } from 'src/infrastructure/constants/constants';
import { FulfillmentOfLimitComponent } from './components/fulfillmentOfLimits/fulfillment-of-limits.component';

const routes: Routes = [
  {
    path: 'reports',
    component: ReportComponent,
    canActivate: [AuthGuard, RoleGuard],
    data: { breadcrumb: 'Справки', roles: [UserRoleAliases.ADMINISTRATOR] }
  },
  {
    path: 'summary-report',
    component: SummaryReportComponent,
    canActivate: [AuthGuard, RoleGuard],
    data: { breadcrumb: 'Справка за броя, размера, състоянието и движението на предоставените кредити по реда на ЗКСД', roles: [UserRoleAliases.ADMINISTRATOR, UserRoleAliases.BANK_ACTIVE] }
  },
  {
    path: 'fulfillmentOfLimits',
    component: FulfillmentOfLimitComponent,
    canActivate: [AuthGuard, RoleGuard],
    data: { breadcrumb: 'Изпълнение на лимитите по гаранционните споразумения', roles: [UserRoleAliases.ADMINISTRATOR] }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ReportRoutingModule { }
