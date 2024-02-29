import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { CommonBootstrapModule } from 'src/infrastructure/common-bootstrap.module';
import { LoginComponent } from './components/login.component';
import { UserActivationResource } from './resources/user-activation.resource';
import { UserLoginResource } from './resources/user-login.resource';
import { UserTokenGuard } from './services/user-token.guard';
import { UserRoutingModule } from './user-routing.module';
import { UserResource } from './resources/user.resource';
import { UserSearchComponent } from './components/user-search/user-search.component';
import { UserEditComponent } from './components/user-edit/user-edit.component';
import { UserCreateComponent } from './components/user-create/user-create.component';
import { UserCommonComponent } from './components/user-common/user-common.component';
import { UserActivationComponent } from './components/user-activation/user-activation.component';
import { UserForgottenPasswordComponent } from './components/user-forgotten-password/user-forgotten-password.component';
import { UserPasswordRecoveryComponent } from './components/user-password-recovery/user-password-recovery.component';
import { UserChangePasswordModalComponent } from './components/user-change-password-modal/user-change-password-modal.component';

@NgModule({
  declarations: [
    LoginComponent,
    UserSearchComponent,
    UserEditComponent,
    UserCreateComponent,
    UserCommonComponent,
    UserActivationComponent,
    UserForgottenPasswordComponent,
    UserPasswordRecoveryComponent,
    UserChangePasswordModalComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    CommonBootstrapModule,
    UserRoutingModule,
    TranslateModule
  ],
  providers: [
    UserLoginResource,
    UserActivationResource,
    UserResource,
    UserTokenGuard
  ]
})
export class UserModule { }
