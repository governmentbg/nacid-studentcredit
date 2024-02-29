import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/infrastructure/guards/auth-guard';
import { ApplicationTwoSearchComponent } from './components/application-two-search/application-two-search.component';
import { ApplicationTwoNewComponent } from './components/application-two-new/application-two-new.component';
import { ApplicationTwoEditComponent } from './components/application-two-edit/application-two-edit.component';
import { ApplicationTwoResolver } from './services/application-two-resolver';
import { RoleGuard } from 'src/infrastructure/guards/role-guard';
import { UserRoleAliases } from 'src/infrastructure/constants/constants';

const routes: Routes = [
  {
    path: 'application-two',
    canActivate: [AuthGuard, RoleGuard],
    data: { breadcrumb: 'Отчети за размера на кредитните задължения (Пр. № 2)', roles: [UserRoleAliases.ADMINISTRATOR, UserRoleAliases.BANK_ACTIVE] },
    children: [
      {
        component: ApplicationTwoSearchComponent,
        path: '',
        data: { breadcrumb: 'skip' }
      },
      {
        path: 'new',
        data: { breadcrumb: 'Вписване на данни по Приложение № 2' },
        component: ApplicationTwoNewComponent
      },
      {
        path: ':id',
        component: ApplicationTwoEditComponent,
        resolve: {
          model: ApplicationTwoResolver
        },
        data: { breadcrumb: 'Данни по Приложение № 2' }

      }
    ]
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ApplicationTwoRoutingModule { }
