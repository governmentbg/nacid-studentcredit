import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { finalize } from 'rxjs/operators';
import { LoadingIndicatorService } from 'src/infrastructure/components/loading-indicator/loading-indicator.service';
import { LoginDto } from '../models/login.dto';
import { UserLoginInfoDto } from '../models/user-login-info.dto';
import { UserLoginResource } from '../resources/user-login.resource';
import { UserLoginService } from '../services/user-login.service';

@Component({
  selector: 'user-login',
  templateUrl: 'login.component.html'
})

export class LoginComponent implements OnInit {
  model: LoginDto = new LoginDto();

  constructor(
    private router: Router,
    private resource: UserLoginResource,
    private userLoginService: UserLoginService,
    private loadingIndicator: LoadingIndicatorService,
  ) { }

  ngOnInit(): void {
    if (this.userLoginService.isLogged) {
      this.router.navigate(['']);
    }
  }

  login(): void {
    this.loadingIndicator.show();
    this.resource.login(this.model)
      .pipe(
        finalize(() => this.loadingIndicator.hide())
      )
      .subscribe(
        (userLoginInfoDto: UserLoginInfoDto) => {
          this.userLoginService.login(userLoginInfoDto);
          this.router.navigate(['']);
        }
      );
  }
}
