import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { finalize } from 'rxjs';
import { LoadingIndicatorService } from 'src/infrastructure/components/loading-indicator/loading-indicator.service';
import { CommitStateEnumLocalization, CourseTypeEnumLocalization, CreditTypeEnumLocalization, YearTypeEnumLocalization } from 'src/infrastructure/constants/enum-localization.const';
import { SharedService } from 'src/infrastructure/services/shared-service';
import { CreditType } from 'src/modules/application/common/enums/credit-type.enum';
import { CourseType } from '../../enums/course-type.enum';
import { EducationType } from '../../enums/education-type.enum';
import { YearType } from '../../enums/year-type.enum';
import { ApplicationOneDto } from '../../models/application-one.dto';
import { ApplicationOneResource } from '../../resources/application-one.resource';
import { ActionConfirmationModalComponent } from 'src/infrastructure/components/action-confirmation-modal/action-confirmation-modal.component';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ApplicationOneStatus } from '../../enums/application-one-status.enum';
import { ToastrService } from 'ngx-toastr';
import { RoleService } from 'src/infrastructure/services/role.service';
import { ContentTypes, UserRoleAliases } from 'src/infrastructure/constants/constants';
import { ApplicationHistoryType } from 'src/modules/application/common/enums/application-history-type.enum';
import { CommitState } from 'src/modules/application/common/enums/commit-state.enum';
import { ApplicationHistoryDto } from 'src/modules/application/common/models/application-history.dto';

@Component({
  selector: 'app-application-one',
  templateUrl: './application-one.component.html'
})
export class ApplicationOneComponent implements OnInit {
  model = new ApplicationOneDto();

  canSubmit = false;
  private forms: { [key: string]: string } = {};

  educationType = EducationType;
  courseType = CourseType;
  yearType = YearType;
  creditType = CreditType;
  applicationOneStatuses = ApplicationOneStatus;
  commitStates = CommitState;
  applicationHistoryType = ApplicationHistoryType;

  contentTypes = ContentTypes;
  courseTypeEnumLocalization = CourseTypeEnumLocalization;
  yearTypeEnumLocalization = YearTypeEnumLocalization;
  creditTypeEnumLocalization = CreditTypeEnumLocalization;
  commitStatesEnumLocalization = CommitStateEnumLocalization;

  isEditMode: boolean = false;
  parentRouteId: number;
  routeId: number;
  changeStateDescription: string;

  isAdminUser: boolean = false;
  isBankUser: boolean = false;

  originalModel: ApplicationOneDto = new ApplicationOneDto();

  constructor(
    private resource: ApplicationOneResource,
    private loadingIndicator: LoadingIndicatorService,
    public sharedService: SharedService,
    private activatedRoute: ActivatedRoute,
    private modal: NgbModal,
    private router: Router,
    private toastrService: ToastrService,
    private roleService: RoleService
  ) {
    this.parentRouteId = this.activatedRoute.parent.snapshot.params['parentId'];

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
      .pipe(finalize(() => this.loadingIndicator.hide()))
      .subscribe((result: ApplicationOneDto) => {
        this.model = result;
      });
  }

  returnForModification(historyType: ApplicationHistoryType, state: CommitState) {
    const confirmationModal = this.modal.open(ActionConfirmationModalComponent, { backdrop: 'static' });
    state == CommitState.modification
      ? confirmationModal.componentInstance.confirmationMessage = "Сигурни ли сте, че искате да върнете приложението за редакция?"
      : confirmationModal.componentInstance.confirmationMessage = "Сигурни ли сте, че искате да започнете редакция?";
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
            .subscribe((model: ApplicationOneDto) => {
              this.model = model;

              this.toastrService.success(state == CommitState.modification ? 'Приложението е върнато за редакция!' : 'Приложението е в процес на редакция!');
            });
        }
      });
  }

  finishModification(state: CommitState): void {
    const confirmationModal = this.modal.open(ActionConfirmationModalComponent, { backdrop: 'static' });
    state == CommitState.pending
      ? confirmationModal.componentInstance.confirmationMessage = 'Сигурни ли сте, че искате да изпратите приложението за одобрение?'
      : confirmationModal.componentInstance.confirmationMessage = 'Сигурни ли сте, че искате да завършите редакцията?';

    confirmationModal.result
      .then((result: boolean) => {
        if (result) {
          this.loadingIndicator.show();

          this.resource.finishModification(this.model, state)
            .pipe(
              finalize(() => this.loadingIndicator.hide())
            )
            .subscribe(() => {
              this.toastrService.success(state == CommitState.pending ? 'Приложението е изпратено за одобрение успешно' : 'Редакцията е завършена успешно!');
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
            .subscribe((model: ApplicationOneDto) => {
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

  cancelEdit() {
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