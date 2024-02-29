import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApplicationTwoDto } from '../../models/application-two.dto';
import { ContentTypes, UserRoleAliases } from 'src/infrastructure/constants/constants';
import { RoleService } from 'src/infrastructure/services/role.service';
import { ApplicationTwoResource } from '../../resources/application-two-resource';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ActionConfirmationModalComponent } from 'src/infrastructure/components/action-confirmation-modal/action-confirmation-modal.component';
import { LoadingIndicatorService } from 'src/infrastructure/components/loading-indicator/loading-indicator.service';
import { finalize } from 'rxjs';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-application-two-edit',
  templateUrl: './application-two-edit.component.html'
})
export class ApplicationTwoEditComponent implements OnInit {
  model = new ApplicationTwoDto();
  isBankUser: boolean = false;

  canSubmit = false;
  isEditMode: boolean = false;

  contentTypes = ContentTypes;

  private forms: { [key: string]: string } = {};
  private originalModel: ApplicationTwoDto;

  constructor(
    private activatedRoute: ActivatedRoute,
    private roleService: RoleService,
    private resource: ApplicationTwoResource,
    private modal: NgbModal,
    private loadingIndicator: LoadingIndicatorService,
    private toastrService: ToastrService
  ) {
    this.isBankUser = this.roleService.hasRole(UserRoleAliases.BANK_ACTIVE);
  }

  ngOnInit(): void {
    this.activatedRoute.data
      .subscribe((data: { model: ApplicationTwoDto }) => {
        this.model = data.model;
      });
  }

  changeFormValidStatus(form: string, status: string): void {
    this.forms[form] = status;
    this.canSubmit = Object.keys(this.forms).findIndex(e => this.forms[e] == 'INVALID') < 0;
  }

  edit() {
    this.originalModel = JSON.parse(JSON.stringify(this.model));
    this.isEditMode = true;
  }

  cancelEdit() {
    this.model = JSON.parse(JSON.stringify(this.originalModel));
    this.originalModel = null;
    this.isEditMode = false;
  }

  save() {
    const confirmationModal = this.modal.open(ActionConfirmationModalComponent, { backdrop: 'static' });
    confirmationModal.componentInstance.confirmationMessage = "Сигурни ли сте, че искате да запазите промените?";

    confirmationModal.result
      .then((result: boolean) => {
        if (result) {
          this.loadingIndicator.show();

          this.resource.edit(this.model)
            .pipe(
              finalize(() => this.loadingIndicator.hide())
            )
            .subscribe(() => {
              this.isEditMode = false;
              this.originalModel = null;

              this.toastrService.success('Вие успешно редактирахте приложението!');
            });
        }
      });
  }
}