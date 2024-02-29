import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { finalize } from 'rxjs';
import { LoadingIndicatorService } from 'src/infrastructure/components/loading-indicator/loading-indicator.service';
import { CourseTypeEnumLocalization, YearTypeEnumLocalization, CreditTypeEnumLocalization, CommitStateEnumLocalization } from 'src/infrastructure/constants/enum-localization.const';
import { SharedService } from 'src/infrastructure/services/shared-service';
import { ApplicationService } from '../../common/services/application.service';
import { CourseType } from '../../application-one/enums/course-type.enum';
import { EducationType } from '../../application-one/enums/education-type.enum';
import { YearType } from '../../application-one/enums/year-type.enum';
import { ApplicationOneDto } from '../../application-one/models/application-one.dto';
import { ApplicationOneResource } from '../../application-one/resources/application-one.resource';
import { CreditType } from '../../common/enums/credit-type.enum';
import { CreditInfo } from '../models/credit-info.dto';
import { CreditResource } from '../resources/credit-resource';
import { ApplicationOneTypeAliases, UserRoleAliases } from 'src/infrastructure/constants/constants';
import { CommitState } from '../../common/enums/commit-state.enum';
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';
import { RoleService } from 'src/infrastructure/services/role.service';
import { CreditSelectFilterDto } from '../../common/models/credit-select-filter.dto';
import { ActionConfirmationModalComponent } from 'src/infrastructure/components/action-confirmation-modal/action-confirmation-modal.component';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ApplicationFourResource } from '../../application-four/resources/application-four.resource';
import { ApplicationType } from '../../common/enums/application-type.enum';
import { ApplicationOneCreditSelectDto } from '../../application-one/models/application-one-credit-select.dto';
import { CreditSelectDto } from '../../common/models/credit-select.dto';
import { ExistingEnum } from '../../common/enums/existing.enum';

@Component({
  selector: 'app-credit-info',
  templateUrl: './credit-info.component.html'
})
export class CreditInfoComponent implements OnInit {
  model: CreditInfo = new CreditInfo();
  creditId: number;

  educationType = EducationType;
  courseType = CourseType;
  yearType = YearType;
  creditType = CreditType;
  commitState = CommitState;
  existingEnum = ExistingEnum;

  courseTypeEnumLocalization = CourseTypeEnumLocalization;
  yearTypeEnumLocalization = YearTypeEnumLocalization;
  creditTypeEnumLocalization = CreditTypeEnumLocalization;
  commitStateEnumLocalization = CommitStateEnumLocalization;
  applicationOneAliases = ApplicationOneTypeAliases;

  existRefuseContract: boolean = false;
  canPayByApplicationFour: boolean = false;

  isAdmin: boolean = false;
  isBankUser: boolean = false;

  specialityMissing = 'Специалността липсва в регистъра и е въведена чрез свободен текст';
  researchAreaMissing = 'Направлението липсва в регистъра и е въведено чрез свободен текст';

  constructor(
    private resource: CreditResource,
    private activatedRoute: ActivatedRoute,
    private loadingService: LoadingIndicatorService,
    public sharedService: SharedService,
    private router: Router,
    private applicationOneResource: ApplicationOneResource,
    private applicationFourResource: ApplicationFourResource,
    private applicationService: ApplicationService,
    private toastrService: ToastrService,
    private translateService: TranslateService,
    private roleService: RoleService,
    private modal: NgbModal,
    private cdr: ChangeDetectorRef,
  ) {
    this.isBankUser = this.roleService.hasRole(UserRoleAliases.BANK_ACTIVE);
    this.isAdmin = this.roleService.hasRole(UserRoleAliases.ADMINISTRATOR);
  }

  ngOnInit(): void {
    this.creditId = +this.activatedRoute.snapshot.params.parentId;

    this.loadingService.show();

    this.resource.getCreditInfo(this.creditId)
      .pipe(
        finalize(() => this.loadingService.hide())
      )
      .subscribe((result: CreditInfo) => {
        this.model = result;

        this.checkExistRefuseContract();
        this.checkCanPayByAppFour();
      });

    this.cdr.detectChanges();
  }

  createApplicationOne(): void {
    if (this.model.commitState != this.commitState.approved) {
      this.toastrService.error(this.translateService.instant('toastrError.ApplicationOneNotApproved'));
    } else {
      this.applicationService.openApplicationNewPage<ApplicationOneCreditSelectDto>(this.applicationOneResource, ApplicationType.applicationOne, this.model.uin, this.model.creditType, this.model.creditNumber, this.model.bank.id, this.model.id);
    }
  }

  createApplicationFour(): void {
    this.applicationService.openApplicationNewPage<CreditSelectDto>(this.applicationFourResource, ApplicationType.applicationFour, this.model.uin, this.model.creditType, this.model.creditNumber, this.model.bank.id, this.model.id);
  }

  readFile = (e) => {
    if (this.model.commitState != this.commitState.approved) {
      this.toastrService.error(this.translateService.instant('toastrError.ApplicationOneNotApproved'));
    } else {
      const file = e.target.files[0];
      if (!file) {
        return;
      }
      const reader = new FileReader();

      reader.onload = (evt) => {
        const xmlData: string = (evt as any).target.result;
        this.applicationOneResource.importFromXml(xmlData).subscribe((result: ApplicationOneDto) => {
          result.isNewCredit = false;

          this.router.navigate(
            ['/credit', 'one', 'new'],
            { state: { applicationOne: result, isFromXml: true } }
          );
        })
      };
      reader.readAsText(file);
    }

  }

  payCreditByApplicationFour() {
    const confirmationModal = this.modal.open(ActionConfirmationModalComponent, { backdrop: 'static' });
    confirmationModal.componentInstance.confirmationMessage = 'Сигурни ли сте, че искате да активирате държавна гаранция?';

    confirmationModal.result
      .then((result: boolean) => {
        if (result) {
          this.loadingService.show();

          let creditSelectDto = new CreditSelectFilterDto(this.model.uin, this.model.creditType, this.model.creditNumber, this.model.bank.id, this.model.id);
          this.resource.payCreditByApplicationFour(this.creditId, creditSelectDto)
            .pipe(
              finalize(() => this.loadingService.hide())
            )
            .subscribe((creditInfo: CreditInfo) => {
              this.model = creditInfo;

              this.toastrService.success("Успешно активирана държавна гаранция!");
            });
        }
      });

  }

  private checkExistRefuseContract() {
    this.model.applicationsOne.forEach(applicationOne => {
      if (applicationOne.applicationType.alias === this.applicationOneAliases.REFUSE_CONTRACT) {
        this.existRefuseContract = true;
      }
    });
  }

  private checkCanPayByAppFour() {
    this.canPayByApplicationFour = this.model.applicationsOne.every(app => app.commitState == CommitState.approved)
      && this.model.applicationsFour.length > 0
      && this.model.applicationsFour.some(app => app.commitState == CommitState.approved);
  }
}