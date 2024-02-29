import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { finalize } from 'rxjs';
import { LoadingIndicatorService } from 'src/infrastructure/components/loading-indicator/loading-indicator.service';
import { CommitStateEnumLocalization } from 'src/infrastructure/constants/enum-localization.const';
import { SharedService } from 'src/infrastructure/services/shared-service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { RoleService } from 'src/infrastructure/services/role.service';
import { UserRoleAliases } from 'src/infrastructure/constants/constants';
import { ApplicationHistoryType } from 'src/modules/application/common/enums/application-history-type.enum';
import { CommitState } from 'src/modules/application/common/enums/commit-state.enum';
import { ApplicationFiveDto } from '../../models/application-five.dto';
import { ApplicationFiveResource } from '../../resources/application-five.resource';
import { ApplicationHistoryDto } from 'src/modules/application/common/models/application-history.dto';
import { ActionConfirmationModalComponent } from 'src/infrastructure/components/action-confirmation-modal/action-confirmation-modal.component';

@Component({
  selector: 'app-application-five',
  templateUrl: './application-five.component.html'
})
export class ApplicationFiveComponent implements OnInit {
  model = new ApplicationFiveDto();

  canSubmit = false;
  private forms: { [key: string]: string } = {};

  commitState = CommitState;
  applicationHistoryType = ApplicationHistoryType;

  commitStateEnumLocalization = CommitStateEnumLocalization;

  isEditMode: boolean = false;
  routeId: number;
  changeStateDescription: string;

  isAdminUser: boolean = false;
  isBankUser: boolean = false;

  originalModel: ApplicationFiveDto = new ApplicationFiveDto();

  constructor(
    private resource: ApplicationFiveResource,
    private loadingIndicator: LoadingIndicatorService,
    public sharedService: SharedService,
    private activatedRoute: ActivatedRoute,
    private modal: NgbModal,
    private router: Router,
    private toastrService: ToastrService,
    private roleService: RoleService
  ) {
    this.isAdminUser = this.roleService.hasRole(UserRoleAliases.ADMINISTRATOR);
    this.isBankUser = this.roleService.hasRole(UserRoleAliases.BANK_ACTIVE);
  }

  ngOnInit(): void {
    this.routeId = +this.activatedRoute.snapshot.params.id;

    this.getById();
  }

  getById() {
    this.loadingIndicator.show();

    this.resource.getById(this.routeId)
      .pipe(
        finalize(() => this.loadingIndicator.hide())
      )
      .subscribe((result: ApplicationFiveDto) => {
        this.model = result;
      });
  }

  returnForModification() {
    const confirmationModal = this.modal.open(ActionConfirmationModalComponent, { backdrop: 'static' });
    confirmationModal.componentInstance.confirmationMessage = "Сигурни ли сте, че искате да върнете приложението за редакция?";
    confirmationModal.componentInstance.showTextArea = true;
    confirmationModal.componentInstance.requireTextArea = true;
    confirmationModal.componentInstance.textAreaTitle = "Основание за редакция";

    confirmationModal.componentInstance.passDescription.subscribe((description: string) => {
      this.changeStateDescription = description;
    });

    confirmationModal.result
      .then((result: boolean) => {
        if (result) {
          this.loadingIndicator.show();

          const appHistoryDto = new ApplicationHistoryDto();
          appHistoryDto.applicationId = this.model.id;
          appHistoryDto.description = this.changeStateDescription;
          appHistoryDto.applicationHistoryType = ApplicationHistoryType.modification;

          this.resource.returnForModification(appHistoryDto, CommitState.modification)
            .pipe(
              finalize(() => this.loadingIndicator.hide())
            )
            .subscribe((model: ApplicationFiveDto) => {
              this.model = model;

              this.toastrService.success('Приложението е върнато за редакция!');
            });
        }
      });
  }

  finishModification(): void {
    const confirmationModal = this.modal.open(ActionConfirmationModalComponent, { backdrop: 'static' });
    confirmationModal.componentInstance.confirmationMessage = 'Сигурни ли сте, че искате да изпратите приложението за одобрение?';

    confirmationModal.result
      .then((result: boolean) => {
        if (result) {
          this.loadingIndicator.show();

          this.resource.finishModification(this.model, CommitState.pending)
            .pipe(
              finalize(() => this.loadingIndicator.hide())
            )
            .subscribe(() => {
              this.toastrService.success('Приложението е изпратено за одобрение успешно');
              this.router.navigate(['/application-five']);
            });
        }
      });
  }

  approve(): void {
    const confirmationModal = this.modal.open(ActionConfirmationModalComponent, { backdrop: 'static' });
    confirmationModal.componentInstance.confirmationMessage = "Сигурни ли сте, че искате да одобрите приложението?";

    confirmationModal.result
      .then((result: boolean) => {
        if (result) {
          this.loadingIndicator.show();

          this.resource.approve(this.model.id)
            .pipe(
              finalize(() => this.loadingIndicator.hide())
            )
            .subscribe((model: ApplicationFiveDto) => {
              this.model = model;

              this.toastrService.success('Приложението е одобрено успешно!');
            });
        }
      })
  }

  enableModification() {
    const confirmationModal = this.modal.open(ActionConfirmationModalComponent, { backdrop: 'static' });
    confirmationModal.componentInstance.confirmationMessage = "Сигурни ли сте, че искате да редактирате приложението?";

    confirmationModal.result
      .then((result: boolean) => {
        if (result) {
          this.originalModel = JSON.parse(JSON.stringify(this.model));

          this.isEditMode = true;
        }
      });
  }

  cancelModification() {
    const confirmationModal = this.modal.open(ActionConfirmationModalComponent, { backdrop: 'static' });
    confirmationModal.componentInstance.confirmationMessage = "Сигурни ли сте, че искате да откажете редакцията?";

    confirmationModal.result
      .then((result: boolean) => {
        if (result) {
          this.model = JSON.parse(JSON.stringify(this.originalModel));

          this.isEditMode = false;
        }
      });
  }

  changeFormValidStatus(form: string, status: string): void {
    this.forms[form] = status;
    this.canSubmit = Object.keys(this.forms).findIndex(e => this.forms[e] == 'INVALID') < 0;
  }
}