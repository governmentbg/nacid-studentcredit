import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { finalize } from 'rxjs';
import { LoadingIndicatorService } from 'src/infrastructure/components/loading-indicator/loading-indicator.service';
import { ContractType } from '../../enums/contract-type.enum';
import { BankContractDto } from '../../model/bank-contract.dto';
import { BankDto } from '../../model/bank.dto';
import { BankResource } from '../../resources/bank.resource';
import { RoleService } from 'src/infrastructure/services/role.service';
import { UserRoleAliases } from 'src/infrastructure/constants/constants';
import { SharedService } from 'src/infrastructure/services/shared-service';

@Component({
  selector: 'app-bank-edit',
  templateUrl: 'bank-edit.component.html'
})

export class BankEditComponent implements OnInit {
  model: BankDto = new BankDto();
  contractTypes = ContractType;
  isEditMode: boolean = false;
  newContractChange: boolean = false;
  contractTerminate: boolean = false;
  isTerminated: boolean = false;
  isBankUser: boolean = false;

  private originalModel: BankDto;

  constructor(
    private resource: BankResource,
    private loadingIndicator: LoadingIndicatorService,
    private activatedRoute: ActivatedRoute,
    private roleService: RoleService,
    private cdr: ChangeDetectorRef,
    public sharedService: SharedService
  ) {
    this.isBankUser = this.roleService.hasRole(UserRoleAliases.BANK_ACTIVE);
  }

  ngOnInit(): void {
    this.activatedRoute.data
      .subscribe((data: { bank: BankDto }) => {
        this.model = data.bank;

        this.isTerminated = this.model.contracts.some(s => s.type === this.contractTypes.terminated);
      });
    this.cdr.detectChanges();
  }

  save(): void {
    this.resource.edit(this.model)
      .subscribe(() => {
        this.finishEdit();
        this.ngOnInit();
      });
  }

  edit(): void {
    this.originalModel = JSON.parse(JSON.stringify(this.model));
    this.isEditMode = true;
  }

  cancelEdit(): void {
    this.model = JSON.parse(JSON.stringify(this.originalModel));
    this.finishEdit();
  }

  changeStatus(): void {
    this.loadingIndicator.show();

    this.resource.changeStatus(this.model.id)
      .pipe(
        finalize(() => this.loadingIndicator.hide())
      )
      .subscribe(isActive => this.model.isActive = isActive);
  }

  addContract(type: ContractType): void {
    this.model.contracts.push(new BankContractDto(type));

    this.cdr.detectChanges();
    if (type === this.contractTypes.changed) {
      this.newContractChange = true;
    } else if (type === this.contractTypes.terminated) {
      this.contractTerminate = true
    }

  }

  cancelChange(): void {
    this.newContractChange = false;
    this.model.contracts.pop();
  }

  cancelTerminate(): void {
    this.contractTerminate = false;
    this.model.contracts.pop();
  }

  private finishEdit(): void {
    this.originalModel = null;
    this.isEditMode = false;
    this.newContractChange = false;
    this.contractTerminate = false;
  }
}