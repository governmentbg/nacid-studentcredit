import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  currentYear: number;

  constructor(private translate: TranslateService) {
    this.translate.setDefaultLang('bg');
    this.translate.use('bg');
    this.currentYear = new Date().getFullYear();
  }
}
