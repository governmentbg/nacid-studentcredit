import { HttpClient, HttpClientModule } from '@angular/common/http';
import { APP_INITIALIZER, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { ToastrModule } from 'ngx-toastr';
import { CommonBootstrapModule } from 'src/infrastructure/common-bootstrap.module';
import { Configuration, configurationFactory } from 'src/infrastructure/configuration/configuration';
import { InterceptorsModule } from 'src/infrastructure/interceptors/interceptors.module';
import { ApplicationModule } from 'src/modules/application/application.module';
import { UserModule } from 'src/modules/user/user.module';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule } from '@angular/forms';
import { SharedService } from 'src/infrastructure/services/shared-service';
import { BankModule } from 'src/modules/bank/bank.module';
import { AppMenuComponent } from './app-menu/app-menu.component';
import { AppUserComponent } from './app-user/app-user.component';
import { ReportModule } from 'src/modules/report/report.module';
import { StudentModule } from 'src/modules/student/student.module';
import { AuthGuard } from 'src/infrastructure/guards/auth-guard';
import { RoleGuard } from 'src/infrastructure/guards/role-guard';
import { ApplicationTwoModule } from 'src/modules/application-two/application-two.module';
import { NomenclatureModule } from 'src/modules/nomenclature/nomenclature.module';
import { DefaultComponent } from './default/default.component';

@NgModule({
  declarations: [
    AppComponent,
    AppMenuComponent,
    AppUserComponent,
    DefaultComponent
  ],
  imports: [
    BrowserModule,
    UserModule,
    FormsModule,
    AppRoutingModule,
    CommonBootstrapModule,
    HttpClientModule,
    ToastrModule.forRoot(),
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient]
      }
    }),
    ApplicationModule,
    BrowserAnimationsModule,
    BankModule,
    ReportModule,
    StudentModule,
    ApplicationTwoModule,
    NomenclatureModule
  ],
  providers: [
    Configuration,
    {
      provide: APP_INITIALIZER,
      useFactory: configurationFactory,
      deps: [Configuration],
      multi: true
    },
    InterceptorsModule,
    SharedService,
    AuthGuard,
    RoleGuard
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http);
}
