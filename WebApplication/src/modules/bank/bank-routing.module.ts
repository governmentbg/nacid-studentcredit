import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BankCreateComponent } from './components/bank-create/bank-create.component';
import { BankEditComponent } from './components/bank-edit/bank-edit.component';
import { BankComponent } from './components/bank.component';
import { BankResolver } from './services/bank-resolver';
import { AuthGuard } from 'src/infrastructure/guards/auth-guard';
import { RoleGuard } from 'src/infrastructure/guards/role-guard';
import { UserRoleAliases } from 'src/infrastructure/constants/constants';

const routes: Routes = [
	{
		path: 'bank',
		canActivate: [AuthGuard, RoleGuard],
		data: { breadcrumb: 'Регистър на банки', roles: [UserRoleAliases.ADMINISTRATOR] },
		children: [
			{
				component: BankComponent,
				path: '',
				data: { breadcrumb: 'skip' }
			},
			{
				path: 'create',
				data: { breadcrumb: 'Добавяне на банка' },
				component: BankCreateComponent
			},
			{
				path: 'edit/:id',
				component: BankEditComponent,
				resolve: {
					bank: BankResolver
				},
				data: { breadcrumb: 'Редакция на банка' }

			}
		]
	},
	{
		path: 'bank-info',
		component: BankEditComponent,
		canActivate: [AuthGuard, RoleGuard],
		resolve: {
			bank: BankResolver
		},
		data: { breadcrumb: 'Данни на банка', roles: [UserRoleAliases.ADMINISTRATOR, UserRoleAliases.BANK_ACTIVE] }
	}
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule]
})
export class BankRoutingModule { }
