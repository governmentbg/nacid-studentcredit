import { Component, Input, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { finalize } from 'rxjs/operators';
import { RecordEntryDto } from '../../models/record-entry.dto';
import { LoadingIndicatorService } from 'src/infrastructure/components/loading-indicator/loading-indicator.service';
import { ApplicationTwoResource } from '../../resources/application-two-resource';
import { ToastrService } from 'ngx-toastr';
import { ActionConfirmationModalComponent } from 'src/infrastructure/components/action-confirmation-modal/action-confirmation-modal.component';

@Component({
  selector: 'app-record-entry-modal',
  templateUrl: './record-entry-modal.component.html'
})
export class RecordEntryModalComponent {
  model = new RecordEntryDto();

  @ViewChild('resultForm') resultForm: NgForm;

  @Input() applicationTwoId: number;

  public validationTexts: { key: string, value: string }[] = [
    { key: 'required', value: 'Полето е задължително' }
  ];

  constructor(
    public activeModal: NgbActiveModal,
    private loadingIndicator: LoadingIndicatorService,
    private resource: ApplicationTwoResource,
    private toastrService: ToastrService,
    private modal: NgbModal,
  ) { }

  save(): void {
    if (!this.resultForm.form.valid) {
      return;
    }

    this.model.applicationTwoId = this.applicationTwoId;

    if (this.model.isRepaid == null) {
      this.model.isRepaid = false;
    }

    const confirmationModal = this.modal.open(ActionConfirmationModalComponent, { backdrop: 'static' });
    confirmationModal.componentInstance.confirmationMessage = 'Сигурни ли сте, че искате да добавите кредита към приложение № 2?';
    confirmationModal.result
      .then((result: boolean) => {
        if (result) {
          this.loadingIndicator.show();
          this.resource.addRecordEntry(this.model)
            .pipe(
              finalize(() => this.loadingIndicator.hide())
            )
            .subscribe(() => {
              this.activeModal.close(this.model);
              this.toastrService.success("Успешно добавен кредит!");
            });
        }
      });
  }
}
