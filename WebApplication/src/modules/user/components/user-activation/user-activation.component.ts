import { Component, OnInit } from "@angular/core";
import { UserActivationDto } from "../../models/user-activation.dto";
import { ActivatedRoute, Router } from "@angular/router";
import { UserActivationResource } from "../../resources/user-activation.resource";
import { TranslateService } from "@ngx-translate/core";
import { ToastrService } from "ngx-toastr";

@Component({
  selector: 'user-activation',
  templateUrl: 'user-activation.component.html'
})

export class UserActivationComponent implements OnInit {
  model: UserActivationDto = new UserActivationDto();

  constructor(
    private route: ActivatedRoute,
    private resource: UserActivationResource,
    private toastrService: ToastrService,
    private translateService: TranslateService,
    private router: Router
  ) {

  }

  ngOnInit(): void {
    this.model.token = this.route.snapshot.queryParams.token;
  }

  activate(): void {
    this.resource.activateUser(this.model).subscribe({
      complete: () => {
        this.toastrService.success(this.translateService.instant('toastr.userActivateProfile'));
        setTimeout(() => {
          this.router.navigate(['login']);
        }, 2000);
      }
    });
  }
}