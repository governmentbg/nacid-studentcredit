import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { CommonBootstrapModule } from 'src/infrastructure/common-bootstrap.module';
import { BankRoutingModule } from './bank-routing.module';
import { BankCreateComponent } from './components/bank-create/bank-create.component';
import { BankEditComponent } from './components/bank-edit/bank-edit.component';
import { BankComponent } from './components/bank.component';
import { BankResource } from './resources/bank.resource';
import { BankSearchFilter } from './services/bank-search.filter';
import { BankResolver } from './services/bank-resolver';

@NgModule({
  imports: [
    BankRoutingModule,
    CommonModule,
    FormsModule,
    CommonBootstrapModule,
    TranslateModule
  ],
  declarations: [
    BankComponent,
    BankCreateComponent,
    BankEditComponent
  ],
  providers: [
    BankResource,
    BankSearchFilter,
    BankResolver
  ],
})
export class BankModule { }
