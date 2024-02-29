import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Configuration } from "src/infrastructure/configuration/configuration";
import { BaseResource } from "src/infrastructure/services/base.resource";
import { StudentFilterDto } from "../models/student-filter.dto";
import { StudentSearchResult } from "../models/student-search-result.dto";
import { StudentSelectSearchFilter } from "src/modules/application/application-one/services/student-select-search.filter";
import { StudentSelectDto } from "src/modules/student/models/student-select.dto";

@Injectable()
export class StudentResource extends BaseResource {
  constructor(
    protected http: HttpClient,
    protected configuration: Configuration
  ) {
    super(http, configuration, 'Student');
  }

  getFiltered(filter?: StudentFilterDto): Observable<StudentSearchResult> {
    return this.http.get<StudentSearchResult>(`${this.baseUrl}` + this.composeQueryString(filter));
  }

  selectStudent(filter: StudentSelectSearchFilter): Observable<StudentSelectDto> {
    return this.http.get<StudentSelectDto>(`${this.baseUrl}/selectstudent` + this.composeQueryString(filter));
  }
}
