import { HttpClient, HttpClientModule } from '@angular/common/http';
import { APP_INITIALIZER, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { CommonBootstrapModule } from 'src/infrastructure/common-bootstrap.module';
import { Configuration, configurationFactory } from 'src/infrastructure/configuration/configuration';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AppFooterComponent } from './app-footer/app-footer.component';
import { AppHeaderComponent } from './app-header/app-header.component';
import { BankResource } from './bank/resources/bank.resource';
import { ChangeLanguageService } from './menu/change-language/change-language.service';
import { ChangeLanguageComponent } from './menu/change-language/change-language.component';
import { BankSearchComponent } from './bank/components/bank-search/bank-search.component';
import { BankSearchFilter } from './bank/services/bank-search.filter';
import { FormsModule } from '@angular/forms';
import { AboutCreditsComponent } from './utility/components/about-credits/about-creditscomponent';
import { BankComponent } from './bank/components/bank/bank.component';
import { HomeCompoennt } from './home/home.component';
import { ContactsComponent } from './utility/components/contacts/contacts.component';
import { BankSearchResolver } from './bank/services/bank-search.resolver';
import { BankResolver } from './bank/services/bank.resolver';

@NgModule({
  declarations: [
    AppComponent,
    BankSearchComponent,
    BankComponent,
    AboutCreditsComponent,
    ContactsComponent,
    AppFooterComponent,
    AppHeaderComponent,
    ChangeLanguageComponent,
    HomeCompoennt,
    ContactsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    CommonBootstrapModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient]
      }
    })
  ],
  providers: [
    Configuration,
    {
      provide: APP_INITIALIZER,
      useFactory: configurationFactory,
      deps: [Configuration],
      multi: true
    },
    BankResource,
    BankSearchFilter,
    ChangeLanguageService,
    BankSearchResolver,
    BankResolver
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http);
}