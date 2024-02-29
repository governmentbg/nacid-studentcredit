import { Injectable } from "@angular/core";
import { Subject } from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class ChangeLanguageService {
  public changeLanguageSubject: Subject<string> = new Subject();
  public language: string

  constructor() {
  }

  setLanguage(language: string) {
    this.language = language;
    this.changeLanguageSubject.next(language);
  }
}