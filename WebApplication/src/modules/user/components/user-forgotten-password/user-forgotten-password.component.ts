import { Component } from "@angular/core";
import { CommonFormComponent } from "src/infrastructure/components/common-form.component";
import { ForgottenPasswordDto } from "../../models/forgotten-password.dto";
import { RegExps } from "src/infrastructure/constants/constants";
import { UserForgottenPasswordResource } from "../../resources/user-forgotten-password.resource";
import { ToastrService } from "ngx-toastr";
import { LoadingIndicatorService } from "src/infrastructure/components/loading-indicator/loading-indicator.service";
import { TranslateService } from "@ngx-translate/core";
import { Router } from "@angular/router";
import { finalize } from "rxjs";
import { handleDomainError } from "src/infrastructure/utils/domain-error-handler.util";

@Component({
  selector: 'app-user-forgotten-password',
  templateUrl: './user-forgotten-password.component.html'
})
export class UserForgottenPasswordComponent extends CommonFormComponent<ForgottenPasswordDto>{
  model: ForgottenPasswordDto = new ForgottenPasswordDto();

  emailRegex = RegExps.EMAIL_REGEX;

  constructor(
    private resource: UserForgottenPasswordResource,
    private toastrService: ToastrService,
    private loadingIndicator: LoadingIndicatorService,
    private translateService: TranslateService,
    private router: Router
  ) {
    super();
  }

  sendForgottenPassword(): void {
    this.loadingIndicator.show();

    this.resource.sendForgottenPassword(this.model)
      .pipe(
        finalize(() => this.loadingIndicator.hide())
      )
      .subscribe(() => {
        this.toastrService.success(this.translateService.instant('toastr.userLinkForActivation'));
        this.router.navigate(['/login']);

        this.model = new ForgottenPasswordDto();
      });
  }
}