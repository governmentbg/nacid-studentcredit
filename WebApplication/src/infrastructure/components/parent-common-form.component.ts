import { Directive, Input } from "@angular/core";

@Directive({})
export abstract class ParentCommonFormComponent {

  @Input() isEditMode: boolean;

  private forms: { [key: string]: string } = {};

  canSubmit = false;

  changeFormValidStatus(form: string, status: string): void {
    this.forms[form] = status;
    this.canSubmit = Object.keys(this.forms).findIndex(e => this.forms[e] == 'INVALID') < 0;
  }
}