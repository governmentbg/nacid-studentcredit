import { Component, Input, OnInit } from '@angular/core';
import { CommonFormComponent } from 'src/infrastructure/components/common-form.component';
import { ApplicationOneDto } from '../../../models/application-one.dto';
import { BankNomenclatureDto } from 'src/modules/nomenclature/common/models/bank-nomenclature.dto';
import { RoleService } from 'src/infrastructure/services/role.service';
import { RegExps, UserRoleAliases } from 'src/infrastructure/constants/constants';
import { Configuration } from 'src/infrastructure/configuration/configuration';

@Component({
  selector: 'app-bank-form',
  templateUrl: './bank-form.component.html'
})
export class BankFormComponent extends CommonFormComponent<ApplicationOneDto> implements OnInit {
  @Input() isNewAppOne: boolean = false;

  isBankUser: boolean = false;

  cyrillicNumberRegExp = RegExps.CYRILLIC_AND_NUMBER_REGEX;
  numberRegExp = RegExps.NUMBER_REGEX;

  constructor(
    private roleService: RoleService,
    private configuration: Configuration
  ) {
    super()
    this.isBankUser = this.roleService.hasRole(UserRoleAliases.BANK_ACTIVE);
  }

  ngOnInit(): void {
    if (this.isBankUser == true) {
      const bank = JSON.parse(localStorage.getItem(this.configuration.bankProperty)) as BankNomenclatureDto;
      const fullname = localStorage.getItem(this.configuration.fullnameProperty);

      this.model.bank = bank;
      this.model.bulstat = bank.bulstat;

      if (this.isNewAppOne) {
        this.model.contactPerson = fullname;
      }
    }
  }

  setBankBulsat(bankDto: BankNomenclatureDto) {
    this.model.bulstat = bankDto.bulstat;
  }
}
