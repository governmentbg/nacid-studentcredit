import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { finalize } from 'rxjs/operators';
import { CommonFormComponent } from 'src/infrastructure/components/common-form.component';
import { RegExps } from 'src/infrastructure/constants/constants';
import { UserRecoveryPasswordDto } from '../../models/user-recovery-password.dto';
import { UserForgottenPasswordResource } from '../../resources/user-forgotten-password.resource';
import { LoadingIndicatorService } from 'src/infrastructure/components/loading-indicator/loading-indicator.service';

@Component({
  selector: 'app-user-password-recovery',
  templateUrl: './user-password-recovery.component.html'
})
export class UserPasswordRecoveryComponent extends CommonFormComponent<UserRecoveryPasswordDto> implements OnInit {
  model: UserRecoveryPasswordDto = new UserRecoveryPasswordDto();
  passwordRegex = RegExps.PASSWORD_REGEX;

  constructor(
    private toastrService: ToastrService,
    private loadingIndicator: LoadingIndicatorService,
    private resource: UserForgottenPasswordResource,
    private route: ActivatedRoute,
    private router: Router,
    private translateService: TranslateService
  ) {
    super();

  }

  ngOnInit(): void {
    this.model.token = this.route.snapshot.queryParams.token;
  }

  recoverPassword() {
    this.loadingIndicator.show();

    this.resource.recoverPassword(this.model)
      .pipe(finalize(() => this.loadingIndicator.hide()))
      .subscribe(() => {
        this.toastrService.success(this.translateService.instant('toastr.userChangePassword'));
        this.router.navigate(['login']);
      });
  }
}
