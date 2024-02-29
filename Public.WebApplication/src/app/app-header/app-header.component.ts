import { Component, OnInit } from "@angular/core";
import { TranslateService } from "@ngx-translate/core";
import { Configuration } from "src/infrastructure/configuration/configuration";

@Component({
  selector: 'app-header',
  templateUrl: 'app-header.component.html',
  styleUrls: ['app-header.component.css']
})
export class AppHeaderComponent implements OnInit {
  currentLanguage = null;

  constructor(public translate: TranslateService,
    public configuration: Configuration) { }

  ngOnInit(): void {
    this.currentLanguage = localStorage.getItem('currentLanguage') ?? this.configuration.defaultLanguage;
    localStorage.setItem('currentLanguage', this.currentLanguage);
    this.translate.setDefaultLang(this.currentLanguage);
    this.translate.use(this.currentLanguage);
  }

  switchLang(newLang: string) {
    this.translate.use(newLang);
    this.currentLanguage = newLang;
    localStorage.setItem('currentLanguage', this.currentLanguage);
  }

  scrollToTop(behavior: 'auto' | 'smooth' = 'smooth'): void {
    window.scrollTo({
      top: 0,
      left: 0,
      behavior
    });
  }

  scrollToSection(id: string): void {
    let el = document.getElementById(id);
    if (el) {
      el.scrollIntoView();
    }
  }
}
