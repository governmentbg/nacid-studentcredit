import { Component } from "@angular/core";
import { ParentCommonFormComponent } from "src/infrastructure/components/parent-common-form.component";
import { UserDto } from "../../models/user.dto";
import { ActionConfirmationModalComponent } from "src/infrastructure/components/action-confirmation-modal/action-confirmation-modal.component";
import { Router } from "@angular/router";
import { NgbModal } from "@ng-bootstrap/ng-bootstrap";
import { UserResource } from "../../resources/user.resource";
import { LoadingIndicatorService } from "src/infrastructure/components/loading-indicator/loading-indicator.service";
import { ToastrService } from "ngx-toastr";
import { finalize } from "rxjs";
import { TranslateService } from "@ngx-translate/core";

@Component({
  selector: 'user-create',
  templateUrl: 'user-create.component.html'
})

export class UserCreateComponent extends ParentCommonFormComponent {
  model: UserDto = new UserDto();

  constructor(
    private router: Router,
    private modal: NgbModal,
    private userResource: UserResource,
    private loadingIndicator: LoadingIndicatorService,
    private toastrService: ToastrService,
    private translateService: TranslateService
  ) {
    super()
  }

  create() {
    this.loadingIndicator.show();
    this.userResource.create(this.model)
      .pipe(
        finalize(() => this.loadingIndicator.hide())
      )
      .subscribe(() => {
        this.router.navigate(['user']);
        this.toastrService.success(this.translateService.instant('toastr.userCreatedLinkSent'));
      })

  }

  cancel() {
    const confirmationModal = this.modal.open(ActionConfirmationModalComponent, { backdrop: 'static' });
    const confirmationMessage = "Сигурни ли сте, че искате да откажете създаването на нов потребител?";
    confirmationModal.componentInstance.confirmationMessage = confirmationMessage;

    confirmationModal.result
      .then((result: boolean) => {
        if (result) {
          this.router.navigate(['user']);
        }
      });
  }
}