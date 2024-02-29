import { Component, Input, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { Configuration } from 'src/infrastructure/configuration/configuration';
import { ChangeLanguageService } from './change-language.service';

@Component({
  selector: 'app-change-language',
  templateUrl: 'change-language.component.html',
  styleUrls: ['change-language-component.css']
})

export class ChangeLanguageComponent implements OnInit {
  @Input() isLoggedIn: boolean = false;
  @Input() isMobile: boolean;
  currentLanguage = null;

  constructor(
    public translate: TranslateService,
    public configuration: Configuration,
    private languageService: ChangeLanguageService
  ) {
    this.languageService.changeLanguageSubject.subscribe((language: string) => this.currentLanguage = language);
    this.currentLanguage = this.languageService.language;
  }

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
    this.languageService.setLanguage(newLang);
  }
}