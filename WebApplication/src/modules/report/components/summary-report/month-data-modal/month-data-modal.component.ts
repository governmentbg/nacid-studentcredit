import { Component, Input, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { finalize } from 'rxjs/operators';
import { LoadingIndicatorService } from 'src/infrastructure/components/loading-indicator/loading-indicator.service';
import { MonthDataDto } from '../../../models/month-data.dto';
import { SheetDto } from '../../../models/sheet.dto';
import { SummaryReportResource } from 'src/modules/report/resources/summary-report-resource';

@Component({
  selector: 'app-month-data-modal',
  templateUrl: './month-data-modal.component.html'
})
export class MonthDataModalComponent {
  @ViewChild('resultForm') resultForm: NgForm;

  @Input() model: MonthDataDto = new MonthDataDto();
  @Input() sheetId: number;

  months = {
    1: 'януари',
    2: 'февруари',
    3: 'март',
    4: 'април',
    5: 'май',
    6: 'юни',
    7: 'юли',
    8: 'август',
    9: 'септември',
    10: 'октомври',
    11: 'ноември',
    12: 'декември',
  };

  constructor(
    public modal: NgbActiveModal,
    private loadingIndicator: LoadingIndicatorService,
    private resource: SummaryReportResource
  ) { }

  save(): void {
    if (!this.resultForm.form.valid) {
      return;
    }

    this.loadingIndicator.show();

    this.resource.updateMonthData(this.sheetId, this.model)
      .pipe(
        finalize(() => this.loadingIndicator.hide())
      )
      .subscribe((sheet: SheetDto) => {
        this.modal.close(sheet);
      });
  }
}
