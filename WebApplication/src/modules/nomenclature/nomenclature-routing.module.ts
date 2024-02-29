import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NomenclatureHostComponent } from './components/nomenclature-host/nomenclature-host.component';
import { AuthGuard } from 'src/infrastructure/guards/auth-guard';
import { RoleGuard } from 'src/infrastructure/guards/role-guard';
import { FileTemplateComponent } from './components/file-template/file-template.component';
import { YearComponent } from './components/year/year.component';
import { UserRoleAliases } from 'src/infrastructure/constants/constants';

const routes: Routes = [
  {
    path: 'nomenclature',
    component: NomenclatureHostComponent,
    canActivate: [AuthGuard, RoleGuard],
    data: { roles: [UserRoleAliases.ADMINISTRATOR] },
    children: [
      {
        path: '',
        redirectTo: 'template',
        pathMatch: 'full'
      },
      {
        path: 'template',
        component: FileTemplateComponent
      },
      {
        path: 'year',
        component: YearComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class NomenclatureRoutingModule { }
