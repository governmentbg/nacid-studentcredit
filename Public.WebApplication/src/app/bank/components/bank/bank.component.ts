import { Component } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { finalize } from "rxjs";
import { LoadingIndicatorService } from "src/infrastructure/components/loading-indicator/loading-indicator.service";
import { BankDto } from "../../models/bank.dto";
import { BankResource } from "../../resources/bank.resource";

@Component({
  selector: 'app-bank',
  templateUrl: './bank.component.html',
  styleUrls: ['./bank.component.css']
})
export class BankComponent {
  model: BankDto;
  src: string;

  constructor(
    protected resource: BankResource,
    protected loadingIndicator: LoadingIndicatorService,
    private activatedRoute: ActivatedRoute,
  ) {
  }

  ngOnInit(): void {
    this.activatedRoute.data.subscribe((data: { model: BankDto }) => this.model = data.model);

    if (this.model.id % 2 === 0) {
      this.src = './../assets/fibank.png';
    } else {
      this.src = './../assets/dsk.jpg';
    }
  }
}