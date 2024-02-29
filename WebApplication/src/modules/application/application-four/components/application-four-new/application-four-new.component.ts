import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { NgbModal } from "@ng-bootstrap/ng-bootstrap";
import { ToastrService } from "ngx-toastr";
import { finalize } from "rxjs";
import { ActionConfirmationModalComponent } from "src/infrastructure/components/action-confirmation-modal/action-confirmation-modal.component";
import { LoadingIndicatorService } from "src/infrastructure/components/loading-indicator/loading-indicator.service";
import { SharedService } from "src/infrastructure/services/shared-service";
import { ApplicationFourDto } from "../../models/application-four.dto";
import { CreditSelectDto } from "../../../common/models/credit-select.dto";
import { ApplicationFourResource } from "../../resources/application-four.resource";
import { Location } from '@angular/common';
import { mapper } from 'src/infrastructure/mappings/mapper';

@Component({
  selector: 'app-application-four-new',
  templateUrl: './application-four-new.component.html'
})
export class ApplicationFourNewComponent implements OnInit {
  model: ApplicationFourDto = new ApplicationFourDto();

  canSubmit = false;
  private forms: { [key: string]: string } = {};

  constructor(
    private resource: ApplicationFourResource,
    private loadingIndicator: LoadingIndicatorService,
    private router: Router,
    private modal: NgbModal,
    private toastrService: ToastrService,
    public sharedService: SharedService,
    private location: Location
  ) {
  }

  ngOnInit(): void {
    const state = this.location.getState() as any;
    const credit = state.credit as CreditSelectDto;

    if (!credit) {
      this.router.navigate(['/credit'])
    }

    if (credit) {
      this.setCredit(credit);
    }
  }

  setCredit(credit: CreditSelectDto): void {
    this.model = mapper.map(credit, CreditSelectDto, ApplicationFourDto);
  }

  createApplicationFour(): void {
    const confirmationModal = this.modal.open(ActionConfirmationModalComponent, { backdrop: 'static' });
    confirmationModal.componentInstance.confirmationMessage = 'Сигурни ли сте, че искате да изпратите приложението за одобрение?';
    confirmationModal.result
      .then((result: boolean) => {
        if (result) {
          this.loadingIndicator.show();
          this.resource.create(this.model)
            .pipe(
              finalize(() => this.loadingIndicator.hide())
            )
            .subscribe(() => {
              this.toastrService.success('Приложението е изпратено успешно!');
              this.router.navigate(['/credit']);
            });
        }
      });
  }

  cancel() {
    const confirmationModal = this.modal.open(ActionConfirmationModalComponent, { backdrop: 'static' });
    confirmationModal.componentInstance.confirmationMessage = 'Сигурни ли сте, че искате да излезете от страницата?';
    confirmationModal.result
      .then((result: boolean) => {
        if (result) {
          this.router.navigate(['/credit']);
        }
      });
  }

  changeFormValidStatus(form: string, status: string): void {
    this.forms[form] = status;
    this.canSubmit = Object.keys(this.forms).findIndex(e => this.forms[e] == 'INVALID') < 0;
  }
}