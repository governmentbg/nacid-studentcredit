import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Configuration } from 'src/infrastructure/configuration/configuration';
import { ApplicationFourDto } from '../models/application-four.dto';
import { IApplicationResource } from '../../common/interfaces/application.resource.interface';
import { ApplicationResource } from '../../common/services/application.resource';

@Injectable()
export class ApplicationFourResource extends ApplicationResource<ApplicationFourDto> implements IApplicationResource {
  constructor(
    protected http: HttpClient,
    protected configuration: Configuration
  ) {
    super(http, configuration, 'ApplicationFour');
  }

  denyApplication(applicationFourId: number, changeStateDescription: string): Observable<ApplicationFourDto> {
    return this.http.post<ApplicationFourDto>(`${this.baseUrl}/${applicationFourId}/deny?changeStateDescription=${changeStateDescription}`, null)
  }
}
