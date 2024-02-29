import { Component, Input, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { finalize } from 'rxjs/operators';
import { LoadingIndicatorService } from 'src/infrastructure/components/loading-indicator/loading-indicator.service';
import { NomenclatureDto } from 'src/infrastructure/models/nomenclature.dto';
import { SheetDto } from 'src/modules/report/models/sheet.dto';
import { SummaryReportResource } from 'src/modules/report/resources/summary-report-resource';

@Component({
  selector: 'app-limit-modal',
  templateUrl: './limit-modal.component.html'
})
export class LimitModalComponent {
  @ViewChild('resultForm') resultForm: NgForm;

  @Input() year: NomenclatureDto;
  @Input() yearLimit: number;

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
    this.resource.changeYearLimit(this.yearLimit, this.year.id)
      .pipe(
        finalize(() => this.loadingIndicator.hide())
      )
      .subscribe((sheet: SheetDto) => {
        this.modal.close(sheet);
      });
  }
}
