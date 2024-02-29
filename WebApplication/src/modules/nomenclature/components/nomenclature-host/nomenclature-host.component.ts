import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-nomenclature-host',
  templateUrl: './nomenclature-host.component.html',
  styleUrls: ['./nomenclature-host.component.css']
})
export class NomenclatureHostComponent {
  links = [
    { fragment: 1, title: 'Шаблони', route: 'template' },
    { fragment: 2, title: 'Години', route: 'year' },
  ];

  constructor(
    public route: ActivatedRoute
  ) { }
}
