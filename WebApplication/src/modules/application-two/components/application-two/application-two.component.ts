import { Component, Input, OnInit } from '@angular/core';
import { ApplicationTwoDto } from '../../models/application-two.dto';
import { BankNomenclatureDto } from 'src/modules/nomenclature/common/models/bank-nomenclature.dto';
import { CommonFormComponent } from 'src/infrastructure/components/common-form.component';
import { SharedService } from 'src/infrastructure/services/shared-service';
import { RecordEntryDto } from '../../models/record-entry.dto';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { RecordEntryModalComponent } from '../record-entry-modal/record-entry-modal.component';
import { Configuration } from 'src/infrastructure/configuration/configuration';
import { RoleService } from 'src/infrastructure/services/role.service';
import { UserRoleAliases } from 'src/infrastructure/constants/constants';

@Component({
  selector: 'app-application-two',
  templateUrl: './application-two.component.html'
})
export class ApplicationTwoComponent extends CommonFormComponent<ApplicationTwoDto> implements OnInit {
  isBankUser: boolean = false;

  @Input() isNewApplicationTwo: boolean = false;
  @Input() isEditMode: boolean = false;

  constructor(
    public sharedService: SharedService,
    private modal: NgbModal,
    private configuration: Configuration,
    private roleService: RoleService
  ) {
    super();
    this.isBankUser = this.roleService.hasRole(UserRoleAliases.BANK_ACTIVE);
  }

  ngOnInit(): void {
    if (this.isBankUser == true && this.model.bank == null) {
      const userBank = JSON.parse(localStorage.getItem(this.configuration.bankProperty)) as BankNomenclatureDto;
      this.model.bank = userBank;
    }
  }

  setBankBulsat(bankDto: BankNomenclatureDto) {
    this.model.bank.bulstat = bankDto.bulstat;
  }

  addRecord() {
    if (!this.model.recordEntries) {
      this.model.recordEntries = [];
    }

    const resultModal = this.modal.open(RecordEntryModalComponent, { size: 'lg' });
    resultModal.componentInstance.applicationTwoId = this.model.id;
    resultModal.result
      .then((newRecord: RecordEntryDto | null) => {
        if (!newRecord) {
          return;
        }
        this.model.recordEntries.unshift(newRecord);
      });
  }
}