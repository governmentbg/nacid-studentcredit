import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { TranslateModule } from '@ngx-translate/core';
import { CommonBootstrapModule } from 'src/infrastructure/common-bootstrap.module';
import { FormsModule } from '@angular/forms';
import { ApplicationTwoRoutingModule } from './application-two-routing.module';
import { ApplicationTwoSearchComponent } from './components/application-two-search/application-two-search.component';
import { SearchApplicationTwoFilter } from './services/search-application-two.filter';
import { ApplicationTwoResource } from './resources/application-two-resource';
import { ApplicationTwoNewComponent } from './components/application-two-new/application-two-new.component';
import { ApplicationTwoComponent } from './components/application-two/application-two.component';
import { ApplicationTwoEditComponent } from './components/application-two-edit/application-two-edit.component';
import { ApplicationTwoResolver } from './services/application-two-resolver';
import { RecordEntryModalComponent } from './components/record-entry-modal/record-entry-modal.component';
import { ApplicationTwoTableComponent } from './components/application-two-table/application-two-table.component';


@NgModule({
  declarations: [
    ApplicationTwoSearchComponent,
    ApplicationTwoNewComponent,
    ApplicationTwoComponent,
    ApplicationTwoEditComponent,
    ApplicationTwoTableComponent,
    RecordEntryModalComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    CommonBootstrapModule,
    TranslateModule,
    ApplicationTwoRoutingModule
  ],
  exports: [
    ApplicationTwoTableComponent
  ],
  providers: [
    SearchApplicationTwoFilter,
    ApplicationTwoResource,
    ApplicationTwoResolver
  ]
})
export class ApplicationTwoModule { }
