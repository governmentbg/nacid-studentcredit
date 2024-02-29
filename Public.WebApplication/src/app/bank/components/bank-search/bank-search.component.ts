import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { LoadingIndicatorService } from "src/infrastructure/components/loading-indicator/loading-indicator.service";
import { BaseSearchComponent } from "src/infrastructure/components/search-component/base-search.component";
import { BankDto } from "../../models/bank.dto";
import { BankResource } from "../../resources/bank.resource";
import { BankSearchFilter } from "../../services/bank-search.filter";

@Component({
  selector: 'app-bank-search',
  templateUrl: './bank-search.component.html',
  styleUrls: ['./bank-search.component.css']
})
export class BankSearchComponent extends BaseSearchComponent<BankDto> implements OnInit {

  constructor(
    private router: Router,
    public filter: BankSearchFilter,
    protected resource: BankResource,
    protected loadingIndicator: LoadingIndicatorService,
    private route: ActivatedRoute
  ) {
    super(filter, resource, loadingIndicator);
  }

  ngOnInit() {
    this.route.data.subscribe((data: { model: BankDto[] }) => this.model = data.model);
  }

  routeToBank(id: number) {
    this.router.navigate([`bank/${id}`]);
  }
}

