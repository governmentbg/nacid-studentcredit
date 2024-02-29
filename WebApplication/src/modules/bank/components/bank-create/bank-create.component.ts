import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { SharedService } from 'src/infrastructure/services/shared-service';
import { ContractType } from '../../enums/contract-type.enum';
import { BankDto } from '../../model/bank.dto';
import { BankResource } from '../../resources/bank.resource';

@Component({
  selector: 'app-bank-create',
  templateUrl: 'bank-create.component.html'
})

export class BankCreateComponent {

  model: BankDto = new BankDto();
  contractTypes = ContractType;

  constructor(
    private resource: BankResource,
    private router: Router,
    private toastrService: ToastrService,
    private translateService: TranslateService,
    public sharedService: SharedService
  ) {
  }
    
  create(): void {
    this.resource.create(this.model).subscribe(() => {
      this.router.navigate(['bank'])});
  }
}