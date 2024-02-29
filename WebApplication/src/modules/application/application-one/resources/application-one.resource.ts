import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Configuration } from 'src/infrastructure/configuration/configuration';
import { XmlApplicationImportDto } from '../../credit/models/xml-application-import.dto';
import { ApplicationOneDto } from '../models/application-one.dto';
import { IApplicationResource } from '../../common/interfaces/application.resource.interface';
import { ApplicationResource } from '../../common/services/application.resource';

@Injectable()
export class ApplicationOneResource extends ApplicationResource<ApplicationOneDto> implements IApplicationResource {
  constructor(
    protected http: HttpClient,
    protected configuration: Configuration
  ) {
    super(http, configuration, 'ApplicationOne');
  }

  importFromXml(xmlData: string): Observable<ApplicationOneDto> {
    const xml = new XmlApplicationImportDto();
    xml.documentXml = xmlData;

    return this.http.post<ApplicationOneDto>(`${this.baseUrl}/xml`, xml);
  }
}
