import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { finalize } from 'rxjs/operators';
import { LoadingIndicatorService } from 'src/infrastructure/components/loading-indicator/loading-indicator.service';
import { ApplicationTwoDto } from '../../models/application-two.dto';
import { ApplicationTwoResource } from '../../resources/application-two-resource';
import { ActionConfirmationModalComponent } from 'src/infrastructure/components/action-confirmation-modal/action-confirmation-modal.component';

@Component({
  selector: 'app-application-two-new',
  templateUrl: './application-two-new.component.html'
})
export class ApplicationTwoNewComponent {
  model = new ApplicationTwoDto();

  canSubmit = false;

  private forms: { [key: string]: string } = {};

  constructor(
    private router: Router,
    private modal: NgbModal,
    private loadingIndicator: LoadingIndicatorService,
    private toastrService: ToastrService,
    private resource: ApplicationTwoResource
  ) { }

  createApplicationTwo(): void {
    const confirmationModal = this.modal.open(ActionConfirmationModalComponent, { backdrop: 'static' });
    confirmationModal.componentInstance.confirmationMessage = 'Сигурни ли сте, че искате да създадете прил. № 2?';
    confirmationModal.result
      .then((result: boolean) => {
        if (result) {
          this.loadingIndicator.show();
          this.resource.createApplicationTwo(this.model)
            .pipe(
              finalize(() => this.loadingIndicator.hide())
            )
            .subscribe((id: number) => {
              this.toastrService.success('Приложението е създадено успешно!');
              this.router.navigate(['application-two', id]);
            });
        }
      });
  }

  changeFormValidStatus(form: string, status: string): void {
    this.forms[form] = status;
    this.canSubmit = Object.keys(this.forms).findIndex(e => this.forms[e] == 'INVALID') < 0;
  }
}