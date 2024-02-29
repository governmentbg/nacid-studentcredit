import { Component, OnInit } from '@angular/core';
import { LoadingIndicatorService } from 'src/infrastructure/components/loading-indicator/loading-indicator.service';
import { SharedService } from 'src/infrastructure/services/shared-service';
import { SheetDto } from '../../models/sheet.dto';
import { NomenclatureDto } from 'src/infrastructure/models/nomenclature.dto';
import { finalize } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { MonthDataModalComponent } from './month-data-modal/month-data-modal.component';
import { MonthDataDto } from '../../models/month-data.dto';
import { RoleService } from 'src/infrastructure/services/role.service';
import { ContentTypes, UserRoleAliases } from 'src/infrastructure/constants/constants';
import { LimitModalComponent } from './limit-modal/limit-modal.component';
import { SummaryReportResource } from '../../resources/summary-report-resource';
import { SummaryReportType } from '../../enums/summary-report-type.enum';
import { Configuration } from 'src/infrastructure/configuration/configuration';
import { SummaryReportSearchDto } from '../../models/summary-report-search-dto';

@Component({
  selector: 'app-summary-report',
  templateUrl: 'summary-report.component.html',
  styleUrls: ['summary-report.component.css']
})
export class SummaryReportComponent implements OnInit {
  sheet: SheetDto = new SheetDto();
  year: NomenclatureDto;
  currentYear = new Date().getFullYear();
  bank: NomenclatureDto;

  contentTypes = ContentTypes;

  summaryReportTypes = SummaryReportType;
  summaryReportType: SummaryReportType;

  isAdmin: boolean = false;
  isBankUser: boolean = false;
  showTable: boolean = false;
  showLimitHint: boolean = false;

  currentDay: number = new Date().getDate();

  searchDto: SummaryReportSearchDto = new SummaryReportSearchDto();

  constructor(
    public sharedService: SharedService,
    protected resource: SummaryReportResource,
    protected loadingIndicator: LoadingIndicatorService,
    private toastrService: ToastrService,
    private modal: NgbModal,
    private roleService: RoleService,
    private configuration: Configuration
  ) {
    this.isAdmin = this.roleService.hasRole(UserRoleAliases.ADMINISTRATOR);
    this.isBankUser = this.roleService.hasRole(UserRoleAliases.BANK_ACTIVE);
  }

  ngOnInit(): void {
    if (this.isBankUser) {
      const userBank = JSON.parse(localStorage.getItem(this.configuration.bankProperty)) as NomenclatureDto;

      this.summaryReportType = SummaryReportType.bank;
      this.bank = userBank;
    }
  }

  search() {
    this.loadingIndicator.show();

    if (this.summaryReportType == SummaryReportType.summary) {
      this.resource.getSummaryReport(this.year.id)
        .pipe(
          finalize(() => this.loadingIndicator.hide())
        )
        .subscribe((sheet: SheetDto) => {
          this.sheet = sheet;
          this.showTable = true;
          sheet.limit == null ? this.showLimitHint = true : this.showLimitHint = false;
        });
    } else {
      this.resource.getSummaryReportByBank(this.year.id, this.bank.id)
        .pipe(
          finalize(() => this.loadingIndicator.hide())
        )
        .subscribe((sheet: SheetDto) => {
          this.sheet = sheet;
          this.showTable = true;
          sheet.limit == null ? this.showLimitHint = true : this.showLimitHint = false;
        });
    }
  }

  importExcel = (e) => {
    const file = e.target.files[0];

    if (!file) {
      return;
    }

    this.loadingIndicator.show();

    const formData: FormData = new FormData();
    formData.append('file', file, file.name);

    this.resource.importExcel(formData)
      .pipe(
        finalize(() => this.loadingIndicator.hide())
      )
      .subscribe(() => {
        this.toastrService.success("Успешно импортирана справка!");
      });
  }

  edit(monthData: MonthDataDto) {
    const copiedMonthData = JSON.parse(JSON.stringify(monthData));

    const resultModal = this.modal.open(MonthDataModalComponent, { size: 'lg' });
    resultModal.componentInstance.model = copiedMonthData;
    resultModal.componentInstance.sheetId = this.sheet.id;

    resultModal.result
      .then((sheet: SheetDto) => {
        if (!sheet) {
          return;
        }

        this.sheet = sheet;
        this.sheet.sheetList.find(e => e.id == monthData.sheetYearId).isExpand = true;

        this.toastrService.success("Успешно редактирахте данните!");
      });
  }

  changeLimit() {
    const yearLimit = JSON.parse(JSON.stringify(this.sheet.limit));

    const resultModal = this.modal.open(LimitModalComponent, { size: 'sm' });
    resultModal.componentInstance.year = this.year;
    resultModal.componentInstance.yearLimit = yearLimit;

    resultModal.result
      .then((sheet: SheetDto) => {
        if (!sheet) {
          return;
        }

        this.showLimitHint = false;

        this.sheet = sheet;

        this.toastrService.success("Успешно редактирахте данните!");
      });
  }

  clearFilter() {
    this.bank = null;
    this.year = null;
    this.showTable = false;
    this.showLimitHint = false;
  }
}
