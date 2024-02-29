import { Component } from '@angular/core';
import { LoadingIndicatorService } from 'src/infrastructure/components/loading-indicator/loading-indicator.service';
import { BaseSearchComponent } from 'src/infrastructure/components/search-component/base-search.component';
import { Configuration } from 'src/infrastructure/configuration/configuration';
import { SearchApplicationTwoFilter } from '../../services/search-application-two.filter';
import { ApplicationTwoResource } from '../../resources/application-two-resource';
import { ApplicationTwoSearchResultItemDto } from '../../models/application-two-search-result-item.dto';
import { NomenclatureDto } from 'src/infrastructure/models/nomenclature.dto';
import { RoleService } from 'src/infrastructure/services/role.service';
import { UserRoleAliases } from 'src/infrastructure/constants/constants';
import { finalize } from 'rxjs';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-application-two-search',
  templateUrl: './application-two-search.component.html'
})
export class ApplicationTwoSearchComponent extends BaseSearchComponent<ApplicationTwoSearchResultItemDto> {
  isBankUser: boolean = false;
  isAdminUser: boolean = false;

  constructor(
    protected loadingIndicator: LoadingIndicatorService,
    private configuration: Configuration,
    public filter: SearchApplicationTwoFilter,
    protected resource: ApplicationTwoResource,
    private roleService: RoleService,
    private toastService: ToastrService
  ) {
    super(filter, resource, loadingIndicator);
    this.isBankUser = this.roleService.hasRole(UserRoleAliases.BANK_ACTIVE);
    this.isAdminUser = this.roleService.hasRole(UserRoleAliases.ADMINISTRATOR);
  }

  ngOnInit(): void {
    if (this.isBankUser == true) {
      const userBank = JSON.parse(localStorage.getItem(this.configuration.bankProperty)) as NomenclatureDto;

      this.filter.bank = userBank;
      this.filter.bankId = userBank.id;
    } else {
      this.filter.bank = null;
      this.filter.bankId = null;
    }

    this.search();
  }

  clearFilter(): void {
    if (this.isBankUser) {
      this.filter.fromDate = null;
      this.filter.toDate = null;
      super.search();
    } else {
      super.clearFilter();
    }
  }

  importExcel = (e) => {
    const file = e.target.files[0];

    if (!file) {
      return;
    }

    this.loadingIndicator.show();

    const formData: FormData = new FormData();
    formData.append('file', file, file.name);

    this.resource.importExcel(formData)
      .pipe(
        finalize(() => this.loadingIndicator.hide())
      )
      .subscribe(() => {
        super.search();
        this.toastService.success("Успешно импортирахте Приложение 2 от Excel");
      });
  }
}