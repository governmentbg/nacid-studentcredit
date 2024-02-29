import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { finalize } from 'rxjs/operators';
import { ActionConfirmationModalComponent } from 'src/infrastructure/components/action-confirmation-modal/action-confirmation-modal.component';
import { LoadingIndicatorService } from 'src/infrastructure/components/loading-indicator/loading-indicator.service';
import { Location } from '@angular/common';
import { ApplicationFiveDto } from '../../models/application-five.dto';
import { ApplicationFiveResource } from '../../resources/application-five.resource';
import { RoleService } from 'src/infrastructure/services/role.service';
import { UserRoleAliases } from 'src/infrastructure/constants/constants';
import { Configuration } from 'src/infrastructure/configuration/configuration';
import { NomenclatureDto } from 'src/infrastructure/models/nomenclature.dto';

@Component({
  selector: 'app-application-five-new',
  templateUrl: './application-five-new.component.html'
})
export class ApplicationFiveNewComponent implements OnInit {
  model = new ApplicationFiveDto();
  canSubmit = false;

  isBankUser: boolean = false;

  private forms: { [key: string]: string } = {};

  constructor(
    private resource: ApplicationFiveResource,
    private router: Router,
    private modal: NgbModal,
    private loadingIndicator: LoadingIndicatorService,
    private toastrService: ToastrService,
    private location: Location,
    private roleService: RoleService,
    private configuration: Configuration
  ) {
    this.isBankUser = this.roleService.hasRole(UserRoleAliases.BANK_ACTIVE);
  }

  ngOnInit(): void {
    if (this.isBankUser == true) {
      this.model.bank = JSON.parse(localStorage.getItem(this.configuration.bankProperty)) as NomenclatureDto;
    }
  }

  createApplicationFive(): void {
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
              this.toastrService.success('Приложението е изпратено успешно');
              this.router.navigate(['/application-five']);
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
          this.location.back();
        }
      });
  }

  changeFormValidStatus(form: string, status: string): void {
    this.forms[form] = status;
    this.canSubmit = Object.keys(this.forms).findIndex(e => this.forms[e] == 'INVALID') < 0;
  }
}