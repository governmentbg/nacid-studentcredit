import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { NgbModal } from "@ng-bootstrap/ng-bootstrap";
import { ToastrService } from "ngx-toastr";
import { finalize } from "rxjs";
import { ActionConfirmationModalComponent } from "src/infrastructure/components/action-confirmation-modal/action-confirmation-modal.component";
import { LoadingIndicatorService } from "src/infrastructure/components/loading-indicator/loading-indicator.service";
import { ContentTypes, UserRoleAliases } from "src/infrastructure/constants/constants";
import { RoleService } from "src/infrastructure/services/role.service";
import { SharedService } from "src/infrastructure/services/shared-service";
import { ApplicationFourDto } from "../../models/application-four.dto";
import { ApplicationFourResource } from "../../resources/application-four.resource";
import { CommitState } from "src/modules/application/common/enums/commit-state.enum";
import { CommitStateEnumLocalization } from "src/infrastructure/constants/enum-localization.const";
import { ApplicationHistoryDto } from "src/modules/application/common/models/application-history.dto";
import { ApplicationHistoryType } from "src/modules/application/common/enums/application-history-type.enum";

@Component({
  selector: 'app-application-four',
  templateUrl: './application-four.component.html'
})
export class ApplicationFourComponent implements OnInit {
  model: ApplicationFourDto = new ApplicationFourDto();
  applicationFourHistoryDto: ApplicationHistoryDto = new ApplicationHistoryDto();

  changeStateDescription: string;

  canSubmit = false;
  private forms: { [key: string]: string } = {};

  isAdminUser: boolean = false;
  isEditMode: boolean = false;
  isModificationMode: boolean = false;
  applicationFourId: number;
  parentRouteId: number;
  commitState = CommitState;
  applicationHistoryType = ApplicationHistoryType;

  contentTypes = ContentTypes;
  commitStateEnumLocalization = CommitStateEnumLocalization;

  originalModel: ApplicationFourDto = new ApplicationFourDto();

  constructor(
    private resource: ApplicationFourResource,
    private loadingIndicator: LoadingIndicatorService,
    private activatedRoute: ActivatedRoute,
    public sharedService: SharedService,
    private roleService: RoleService,
    private modal: NgbModal,
    private router: Router,
    private toastrService: ToastrService,
  ) {
    this.parentRouteId = this.activatedRoute.parent.snapshot.params['parentId'];

    this.isAdminUser = this.roleService.hasRole(UserRoleAliases.ADMINISTRATOR);
  }

  ngOnInit(): void {
    this.applicationFourId = +this.activatedRoute.snapshot.params.id;

    this.getById();
  }

  getById() {
    this.loadingIndicator.show();
    this.resource.getById(this.applicationFourId)
      .pipe(
        finalize(() => this.loadingIndicator.hide())
      )
      .subscribe((result: ApplicationFourDto) => {
        this.model = result;
      })
  }

  returnForModification(historyType: ApplicationHistoryType, state: CommitState) {
    const confirmationModal = this.modal.open(ActionConfirmationModalComponent, { backdrop: 'static' });
    state == CommitState.modification
      ? confirmationModal.componentInstance.confirmationMessage = "Сигурни ли сте, че искате да върнете приложението за редакция?"
      : confirmationModal.componentInstance.confirmationMessage = "Сигурни ли сте, че искате да започнете техническа редакция?";
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
          appHistoryDto.applicationHistoryType = historyType;

          this.resource.returnForModification(appHistoryDto, state)
            .pipe(finalize(() => this.loadingIndicator.hide()))
            .subscribe((model: ApplicationFourDto) => {
              this.model = model;

              this.toastrService.success('Приложението е върнато за редакция успешно');
            });
        }
      });
  }

  finishModification(state: CommitState): void {
    const confirmationModal = this.modal.open(ActionConfirmationModalComponent, { backdrop: 'static' });
    confirmationModal.componentInstance.confirmationMessage = 'Сигурни ли сте, че искате да запазите промените и да изпратите за одобрение?';

    confirmationModal.result
      .then((result: boolean) => {
        if (result) {
          this.loadingIndicator.show();

          this.resource.finishModification(this.model, state)
            .pipe(
              finalize(() => this.loadingIndicator.hide())
            )
            .subscribe(() => {
              this.toastrService.success('Приложението е редактирано и изпратено за одобрение успешно');
              this.router.navigate(['/credit']);
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
            .subscribe((model: ApplicationFourDto) => {
              this.model = model;

              this.toastrService.success('Приложението е одобрено успешно');
            });
        }
      })
  }

  deny() {
    const confirmationModal = this.modal.open(ActionConfirmationModalComponent, { backdrop: 'static' });
    confirmationModal.componentInstance.confirmationMessage = "Сигурни ли сте, че искате да отхвърлите приложението?";
    confirmationModal.componentInstance.showTextArea = true;
    confirmationModal.componentInstance.requireTextArea = true;
    confirmationModal.componentInstance.textAreaTitle = "Основание за отхвърляне";

    confirmationModal.componentInstance.passDescription.subscribe((description: string) => {
      this.changeStateDescription = description;
    });

    confirmationModal.result
      .then((result: boolean) => {
        if (result) {
          this.loadingIndicator.show();

          this.resource.denyApplication(this.model.id, this.changeStateDescription)
            .pipe(finalize(() => this.loadingIndicator.hide()))
            .subscribe((model: ApplicationFourDto) => {
              this.model = model;

              this.toastrService.success('Приложението е отхвърлено успешно');
            });
        }
      });
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
          this.isModificationMode = false;
        }
      });
  }

  changeFormValidStatus(form: string, status: string): void {
    this.forms[form] = status;
    this.canSubmit = Object.keys(this.forms).findIndex(e => this.forms[e] == 'INVALID') < 0;
  }
}