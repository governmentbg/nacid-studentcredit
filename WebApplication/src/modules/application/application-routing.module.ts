import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ApplicationFourNewComponent } from './application-four/components/application-four-new/application-four-new.component';
import { ApplicationFourComponent } from './application-four/components/application-four/application-four.component';
import { ApplicationOneNewComponent } from './application-one/components/application-one-new/application-one-new.component';
import { ApplicationOneComponent } from './application-one/components/application-one/application-one.component';
import { CreditInfoComponent } from './credit/credit-info/credit-info.component';
import { CreditSearchComponent } from './credit/credit-search/credit-search.component';
import { ApplicationOneHistoryComponent } from './application-one/components/application-one-history.component.ts/application-one-history.component';
import { AuthGuard } from 'src/infrastructure/guards/auth-guard';
import { ApplicationFourHistoryComponent } from './application-four/components/application-four-history/application-four-history.component';
import { ApplicationFiveSearchComponent } from './application-five/components/application-five-search/application-five-search.component';
import { ApplicationFiveNewComponent } from './application-five/components/application-five-new/application-five-new.component';
import { ApplicationFiveComponent } from './application-five/components/application-five/application-five.component';
import { ApplicationFiveHistoryComponent } from './application-five/components/application-five-history/application-five-history.component';
import { ReportDividendComponent } from './application-five/components/report-dividend/report-dividend.component';
import { RoleGuard } from 'src/infrastructure/guards/role-guard';
import { UserRoleAliases } from 'src/infrastructure/constants/constants';

const routes: Routes = [
  {
    path: 'credit',
    data: { breadcrumb: 'Кредити', roles: [UserRoleAliases.ADMINISTRATOR, UserRoleAliases.BANK_ACTIVE] },
    canActivate: [AuthGuard, RoleGuard],
    children: [
      {
        component: CreditSearchComponent,
        path: '',
        data: { breadcrumb: 'skip' }
      },
      {
        path: ':parentId',
        data: { breadcrumb: 'Информация за кредит' },
        children: [
          {
            path: '',
            component: CreditInfoComponent,
            data: { breadcrumb: 'skip' }
          },
          {
            path: 'one/:id',
            data: { breadcrumb: 'Приложение 1' },
            children: [
              {
                path: '',
                component: ApplicationOneComponent,
                data: { breadcrumb: 'skip' }
              },
              {
                path: 'history/:rootId',
                component: ApplicationOneHistoryComponent,
                data: { breadcrumb: 'История' }
              }
            ]
          },
          {
            path: 'four/:id',
            data: { breadcrumb: 'Приложение 4' },
            children: [
              {
                path: '',
                component: ApplicationFourComponent,
                data: { breadcrumb: 'skip' }
              },
              {
                path: 'history/:rootId',
                component: ApplicationFourHistoryComponent,
                data: { breadcrumb: 'История' }
              }
            ]
          },
        ]
      },
      {
        path: 'four/new',
        component: ApplicationFourNewComponent,
        canActivate: [AuthGuard],
        data: { breadcrumb: 'Искане за плащане (Пр. №4)' }
      },
      {
        path: 'one/new',
        component: ApplicationOneNewComponent,
        canActivate: [AuthGuard],
        data: { breadcrumb: 'Вписване на данни (Пр. №1)' }
      }
    ]
  },
  {
    path: 'application-five',
    data: { breadcrumb: 'Приложение 5', roles: [UserRoleAliases.ADMINISTRATOR, UserRoleAliases.BANK_ACTIVE] },
    canActivate: [AuthGuard, RoleGuard],
    children: [
      {
        component: ApplicationFiveSearchComponent,
        path: '',
        data: { breadcrumb: 'skip' }
      },
      {
        path: 'new',
        component: ApplicationFiveNewComponent,
        data: { breadcrumb: 'Създаване на Пр. №5' }
      },
      {
        path: ':id',
        data: { breadcrumb: 'Информация за Пр. №5' },
        children: [
          {
            path: '',
            component: ApplicationFiveComponent,
            data: { breadcrumb: 'skip' }
          },
          {
            path: 'history/:rootId',
            component: ApplicationFiveHistoryComponent,
            data: { breadcrumb: 'История' }
          }
        ]
      },
    ]
  },
  {
    path: 'report-dividend',
    component: ReportDividendComponent,
    canActivate: [AuthGuard, RoleGuard],
    data: { breadcrumb: 'Справка за приложение 5', roles: [UserRoleAliases.ADMINISTRATOR] }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ApplicationRoutingModule { }
