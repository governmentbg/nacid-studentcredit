import { Component } from "@angular/core";
import { CommonFormComponent } from "src/infrastructure/components/common-form.component";
import { ApplicationFourDto } from "../../../models/application-four.dto";

@Component({
  selector: 'app-bank-with-account-form',
  templateUrl: 'bank-with-account-form.component.html'
})

export class BankWithAccountFormComponent extends CommonFormComponent<ApplicationFourDto> {

  constructor() {
    super()
  }
}