import { ChangeDetectionStrategy, Component, EventEmitter, Input, Output, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-action-confirmation-modal',
  templateUrl: './action-confirmation-modal.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ActionConfirmationModalComponent {
  @ViewChild('resultForm') resultForm: NgForm;

  description: string = "";
  @Input() showTextArea: boolean = false;

  @Input() confirmationMessage: string;

  @Input() textAreaTitle: string = "Допълнителна информация";

  @Input() requireTextArea: boolean = false;

  @Output() passDescription: EventEmitter<string> = new EventEmitter();

  constructor(public modal: NgbActiveModal) { }

  closeModal() {
    this.passDescription.emit(this.description);
    this.modal.close(true);
  }
}
