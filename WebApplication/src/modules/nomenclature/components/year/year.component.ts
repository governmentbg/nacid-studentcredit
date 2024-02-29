import { Component, OnInit } from '@angular/core';
import { Year } from '../../models/year.model';
import { BaseNomenclatureComponent } from '../../common/components/base-nomenclature.component';

@Component({
  selector: 'app-year',
  templateUrl: './../../common/components/base-nomenclature.component.html',
})
export class YearComponent extends BaseNomenclatureComponent<Year> implements OnInit {

  ngOnInit(): void {
    this.init(Year, 'Year');
  }

}
