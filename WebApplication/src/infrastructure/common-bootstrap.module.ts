import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbDateAdapter, NgbDateParserFormatter, NgbDatepickerI18n, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { TranslateModule } from '@ngx-translate/core';
import { AppRoutingModule } from 'src/app/app-routing.module';
import { ActionConfirmationModalComponent } from './components/action-confirmation-modal/action-confirmation-modal.component';
import { AsyncSelectComponent } from './components/async-select/async-select.component';
import { BreadcrumbComponent } from './components/breadcrumb/breadcrumb.component';
import { FileUploadComponent } from './components/file-upload/file-upload.component';
import { HelpTooltipComponent } from './components/help-tooltip/help-tooltip.component';
import { LoadingIndicatorComponent } from './components/loading-indicator/loading-indicator.component';
import { LoadingIndicatorService } from './components/loading-indicator/loading-indicator.service';
import { PartPanelComponent } from './components/part-panel/part-panel.component';
import { ScrollToTopBtnComponent } from './components/scroll-to-top-btn/scroll-to-top-btn.component';
import { SvgIconComponent } from './components/svg-icon/svg-icon.component';
import { ValidDateDirective } from './directives/valid-date.directive';
import { FilterPipe } from './pipes/filter.pipe';
import { CustomDatepickerI18n } from './services/datepicker-i18n.service';
import { MomentDateFormatter } from './services/moment-date.formatter';
import { StringDateAdapter } from './services/string-date.adapter';
import { ExportComponent } from './components/export/export.component';
import { OrderByPipe } from './pipes/order-by.pipe';
import { RoleService } from './services/role.service';


@NgModule({
  imports: [
    NgbModule,
    CommonModule,
    FormsModule,
    TranslateModule,
    ReactiveFormsModule,
    AppRoutingModule
  ],
  declarations: [
    LoadingIndicatorComponent,
    ScrollToTopBtnComponent,
    SvgIconComponent,
    ActionConfirmationModalComponent,
    AsyncSelectComponent,
    ValidDateDirective,
    HelpTooltipComponent,
    FilterPipe,
    FileUploadComponent,
    PartPanelComponent,
    BreadcrumbComponent,
    ExportComponent,
    OrderByPipe
  ],
  exports: [
    LoadingIndicatorComponent,
    ScrollToTopBtnComponent,
    SvgIconComponent,
    ActionConfirmationModalComponent,
    AsyncSelectComponent,
    ValidDateDirective,
    NgbModule,
    HelpTooltipComponent,
    FilterPipe,
    FileUploadComponent,
    PartPanelComponent,
    BreadcrumbComponent,
    ExportComponent,
    OrderByPipe
  ],
  providers: [
    LoadingIndicatorService,
    RoleService,
    { provide: NgbDatepickerI18n, useClass: CustomDatepickerI18n },
    { provide: NgbDateAdapter, useClass: StringDateAdapter },
    { provide: NgbDateParserFormatter, useClass: MomentDateFormatter }
  ]
})
export class CommonBootstrapModule { }
