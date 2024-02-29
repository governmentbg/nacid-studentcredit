import { Component } from "@angular/core";
import { Observable } from "rxjs";
import { Breadcrumb } from "./breadcrumb.dto";
import { BreadcrumbService } from "./breadcrumb.service";

@Component({
  selector: 'app-breadcrumb',
  templateUrl: './breadcrumb.component.html',
  styleUrls: ['./breadcrumb.component.css']
})
export class BreadcrumbComponent {
  breadcrumbs$: Observable<Breadcrumb[]>;
  breadcrumbs: Breadcrumb[] = [];
  constructor(private readonly breadcrumbService: BreadcrumbService) {
    this.breadcrumbs$ = breadcrumbService.breadcrumbs$;
    breadcrumbService.breadcrumbs$.subscribe((result) => {
      this.breadcrumbs = result;
    })
  }
} 