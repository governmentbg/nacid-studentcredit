import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Configuration } from 'src/infrastructure/configuration/configuration';
import { BaseResource } from 'src/infrastructure/services/base.resource';
import { SheetDto } from '../models/sheet.dto';
import { MonthDataDto } from '../models/month-data.dto';

@Injectable()
export class SummaryReportResource extends BaseResource {

  constructor(
    protected http: HttpClient,
    protected configuration: Configuration
  ) {
    super(http, configuration, 'SummaryReport');
  }

  getSummaryReportByBank(yearId: number, bankId: number): Observable<SheetDto> {
    return this.http.get<SheetDto>(`${this.baseUrl}/reportByBank?yearId=${yearId}&bankId=${bankId}`);
  }

  getSummaryReport(yearId: number): Observable<SheetDto> {
    return this.http.get<SheetDto>(`${this.baseUrl}/summaryReport?yearId=${yearId}`);
  }

  importExcel(file: FormData): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}/importExcel`, file);
  }

  updateMonthData(sheetId: number, dto: MonthDataDto): Observable<SheetDto> {
    return this.http.put<SheetDto>(`${this.baseUrl}/monthData?sheetId=${sheetId}`, dto,);
  }

  changeYearLimit(yearLimit: number, yearId: number): Observable<SheetDto> {
    return this.http.put<SheetDto>(`${this.baseUrl}/yearLimit?yearId=${yearId}`, yearLimit);
  }
}
