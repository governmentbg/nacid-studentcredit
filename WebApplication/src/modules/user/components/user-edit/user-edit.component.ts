import { Component, OnInit } from "@angular/core";
import { RegExps, UserRoleAliases } from "src/infrastructure/constants/constants";
import { UserResource } from "../../resources/user.resource";
import { ActivatedRoute, Params, Router } from "@angular/router";
import { finalize } from "rxjs";
import { LoadingIndicatorService } from "src/infrastructure/components/loading-indicator/loading-indicator.service";
import { SharedService } from "src/infrastructure/services/shared-service";
import { UserStatus } from "../../enums/user-status.enum";
import { NgbModal } from "@ng-bootstrap/ng-bootstrap";
import { ActionConfirmationModalComponent } from "src/infrastructure/components/action-confirmation-modal/action-confirmation-modal.component";
import { ToastrService } from "ngx-toastr";
import { TranslateService } from "@ngx-translate/core";
import { UserDto } from "../../models/user.dto";
import { ParentCommonFormComponent } from "src/infrastructure/components/parent-common-form.component";
import { UserActivationResource } from "../../resources/user-activation.resource";

@Component({
  selector: 'user-edit',
  templateUrl: 'user-edit.component.html'
})
export class UserEditComponent extends ParentCommonFormComponent implements OnInit {
  model: UserDto = new UserDto();
  private originalModel: UserDto;


  userStatus = UserStatus;

  constructor(
    public sharedService: SharedService,
    private route: ActivatedRoute,
    private router: Router,
    private userResource: UserResource,
    private activationResource: UserActivationResource,
    private loadingIndicator: LoadingIndicatorService,
    private modal: NgbModal,
    private toastrService: ToastrService,
    private translateService: TranslateService
  ) {
    super()
  }

  ngOnInit(): void {
    this.route.params.subscribe((params: Params) => {
      const userId = +params.id;
      if (isNaN(userId) || !userId) {
        this.router.navigate(['']);
      }

      this.loadingIndicator.show();
      this.userResource.getUserDtoById(userId)
        .pipe(
          finalize(() => this.loadingIndicator.hide())
        )
        .subscribe((model: UserDto) => {
          this.model = model;
        });
    });
  }

  changeUserActiveStatus() {
    const confirmationModal = this.modal.open(ActionConfirmationModalComponent, { backdrop: 'static' });
    confirmationModal.componentInstance.confirmationMessage = this.model.status == this.userStatus.active ?
      "Сигурни ли сте, че искате да деактивирате потребителя?" : "Сигурни ли сте, че искате да активирате потребителя?";

    confirmationModal.result
      .then((result: boolean) => {
        if (result) {
          this.loadingIndicator.show();
          this.userResource.changeUserActiveStatus(this.model.id)
            .pipe(
              finalize(() => this.loadingIndicator.hide())
            )
            .subscribe((status: UserStatus) => {
              this.model.status = status;
              this.finishEdit();
            });
        }
      });
  }

  sendActivationLink(userId: number) {
    this.activationResource.sendActivationLink(userId).subscribe(() => {
      this.toastrService.success(this.translateService.instant('toastr.userActivationLinkSent'));
    })
  }

  edit() {
    this.originalModel = JSON.parse(JSON.stringify(this.model));
    this.isEditMode = true;
  }

  save() {
    this.loadingIndicator.show();
    this.userResource.editUserData(this.model)
      .pipe(
        finalize(() => this.loadingIndicator.hide())
      )
      .subscribe(() => {
        this.toastrService.success(this.translateService.instant('toastr.userEditSuccess'));
        this.finishEdit();
      });
  }

  cancelEdit() {
    this.model = JSON.parse(JSON.stringify(this.originalModel));
    this.finishEdit();
  }

  private finishEdit(): void {
    this.originalModel = null;
    this.isEditMode = false;
  }
}