import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { finalize } from 'rxjs/operators';
import { ActionConfirmationModalComponent } from 'src/infrastructure/components/action-confirmation-modal/action-confirmation-modal.component';
import { LoadingIndicatorService } from 'src/infrastructure/components/loading-indicator/loading-indicator.service';
import { ApplicationOneDto } from '../../models/application-one.dto';
import { StudentSelectDto } from '../../../../student/models/student-select.dto';
import { ApplicationOneResource } from '../../resources/application-one.resource';
import { Location } from '@angular/common';
import { ApplicationOneCreditSelectDto } from '../../models/application-one-credit-select.dto';
import { mapper } from 'src/infrastructure/mappings/mapper';
import { ExistingEnum } from 'src/modules/application/common/enums/existing.enum';

@Component({
  selector: 'app-application-one-new',
  templateUrl: './application-one-new.component.html'
})
export class ApplicationOneNewComponent implements OnInit {
  model = new ApplicationOneDto();
  isNewCredit: boolean = true;
  canSubmit = false;
  importFromXml: boolean = false;

  private forms: { [key: string]: string } = {};

  constructor(
    private resource: ApplicationOneResource,
    private router: Router,
    private modal: NgbModal,
    private loadingIndicator: LoadingIndicatorService,
    private toastrService: ToastrService,
    private location: Location
  ) { }

  ngOnInit(): void {
    const state = this.location.getState() as any;
    const credit = state.credit as ApplicationOneCreditSelectDto;
    const applicationOne = state.applicationOne as ApplicationOneDto;
    const isFromXml = state.isFromXml as boolean;

    if (isFromXml) {
      this.importFromXml = true;
    }

    if (credit) {
      this.isNewCredit = false;
      this.setCredit(credit);
    }

    if (applicationOne) {
      this.isNewCredit = applicationOne.isNewCredit;
      this.model = applicationOne;
    }
  }

  setStudent(student: StudentSelectDto): void {
    this.model = mapper.map(student, StudentSelectDto, ApplicationOneDto);

    this.model.specialityEnum = ExistingEnum.existing;
    this.model.researchAreaEnum = ExistingEnum.existing;
  }

  setCredit(credit: ApplicationOneCreditSelectDto): void {
    this.model = mapper.map(credit, ApplicationOneCreditSelectDto, ApplicationOneDto);
  }

  setApplicationOne(applicationOne: ApplicationOneDto): void {
    this.model = applicationOne;
    this.isNewCredit = applicationOne.isNewCredit;
  }

  createApplicationOne(): void {
    this.model.isNewCredit = this.isNewCredit;

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
          this.location.back();
        }
      });
  }

  setIsFromXml(isFromXml: boolean) {
    this.importFromXml = isFromXml;
  }

  changeFormValidStatus(form: string, status: string): void {
    this.forms[form] = status;
    this.canSubmit = Object.keys(this.forms).findIndex(e => this.forms[e] == 'INVALID') < 0;
  }
}