import { Component, ContentChild, ElementRef, HostListener, Input, TemplateRef } from '@angular/core';
import { Observable, Observer, Subscription } from 'rxjs';
import { Configuration } from 'src/infrastructure/configuration/configuration';
import * as FileSaver from 'file-saver';
import { ReportExportDto } from 'src/modules/report/models/report-export-dto.model';
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-export',
  template: `
		<ng-container [ngTemplateOutlet]="exportBtnTemplate"
									[ngTemplateOutletContext]="{ $implicit: this }">
		</ng-container>
	`
})
export class ExportComponent {
  loading = false;

  @Input() restController: string;
  @Input() filename: string;
  @Input() label: string;
  @Input() filter: any;
  @Input() contentType: string;
  @Input() model: any;

  @ContentChild('exportBtnTemplate', { static: true }) exportBtnTemplate: TemplateRef<ElementRef>;

  @HostListener('click', ['$event']) exportToExcel(): Subscription {
    this.loading = true;
    return this.export().subscribe(blob => {
      FileSaver.saveAs(blob, this.filename);
      this.loading = false;
    });
  }

  constructor(
    private configuration: Configuration,
    private toastr: ToastrService,
    private translateService: TranslateService
  ) { }


  private export(): Observable<Blob> {
    return new Observable((observer: Observer<Blob>) => {
      const accessToken = localStorage.getItem(this.configuration.tokenProperty);

      const xhr = new XMLHttpRequest();
      xhr.open('POST', `${this.configuration.restUrl}/${this.restController}`, true);
      xhr.setRequestHeader('Content-type', 'application/json');
      xhr.setRequestHeader('Authorization', `Bearer ${accessToken}`);
      xhr.responseType = 'arraybuffer';

      this.loading = true;
      xhr.onreadystatechange = () => {
        if (xhr.readyState === 4) {
          if (xhr.status === 200) {
            const blob = new Blob([xhr.response], { type: this.contentType });
            observer.next(blob);
            observer.complete();
          } else if (xhr.status === 422) {
            this.toastr.error(this.translateService.instant("toastrError.System_ExportProblem"));
          }
        }
      };
      xhr.send(JSON.stringify(this.model));
    });
  }
}
