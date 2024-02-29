import { AfterViewInit, Directive, EventEmitter, Input, Output, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';

@Directive({})
export abstract class CommonFormComponent<TModel> implements AfterViewInit {
  @Input() model: TModel;
  @Output() modelChange: EventEmitter<TModel> = new EventEmitter();

  @Input() isEditMode: boolean;

  @Output() isValidForm: EventEmitter<string> = new EventEmitter();

  @ViewChild(NgForm) form: NgForm;

  ngAfterViewInit(): void {
    this.form.statusChanges
      .subscribe(() => {
        this.isValidForm.emit(this.form.status);
      });
  }
}
