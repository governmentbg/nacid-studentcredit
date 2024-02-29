import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login.component';
import { UserSearchComponent } from './components/user-search/user-search.component';
import { AuthGuard } from 'src/infrastructure/guards/auth-guard';
import { RoleGuard } from 'src/infrastructure/guards/role-guard';
import { UserRoleAliases } from 'src/infrastructure/constants/constants';
import { UserEditComponent } from './components/user-edit/user-edit.component';
import { UserCreateComponent } from './components/user-create/user-create.component';
import { UserActivationComponent } from './components/user-activation/user-activation.component';
import { UserTokenGuard } from './services/user-token.guard';
import { UserForgottenPasswordComponent } from './components/user-forgotten-password/user-forgotten-password.component';
import { UserPasswordRecoveryComponent } from './components/user-password-recovery/user-password-recovery.component';

const routes: Routes = [
	{
		path: 'login',
		component: LoginComponent,
	},
	{
		path: 'forgottenPassword',
		component: UserForgottenPasswordComponent
	},
	{
		path: 'user/activation',
		canActivate: [UserTokenGuard],
		component: UserActivationComponent
	},
	{
		path: 'passwordRecovery',
		component: UserPasswordRecoveryComponent,
		canActivate: [UserTokenGuard]
	},
	{
		path: 'user',
		canActivate: [AuthGuard, RoleGuard],
		data: { breadcrumb: 'Потребители', roles: [UserRoleAliases.ADMINISTRATOR] },
		children: [
			{
				path: '',
				component: UserSearchComponent,
				data: { breadcrumb: 'skip' }
			},
			{
				path: 'create',
				component: UserCreateComponent,
				data: { breadcrumb: 'Създаване на потребител' }
			},
			{
				path: ':id',
				component: UserEditComponent,
				data: { breadcrumb: 'Редакция на потребител' }
			},
		]
	},
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule]
})
export class UserRoutingModule { }
