import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Configuration } from "src/infrastructure/configuration/configuration";
import { SearchResultItemDto } from "src/infrastructure/models/search-result-item.dto";
import { UserSearchResultDto } from "../models/user-search-result.dto";
import { BaseResource } from "src/infrastructure/services/base.resource";
import { UserSearchFilter } from "../services/user-search-filter.service";
import { UserStatus } from "../enums/user-status.enum";
import { UserDto } from "../models/user.dto";
import { UserChangePasswordDto } from "../models/user-change-password.dto";

@Injectable()
export class UserResource extends BaseResource {
  constructor(
    protected http: HttpClient,
    protected configuration: Configuration
  ) {
    super(http, configuration, 'User');
  }

  getFiltered(filter?: UserSearchFilter): Observable<SearchResultItemDto<UserSearchResultDto>> {
    return this.http.get<SearchResultItemDto<UserSearchResultDto>>(`${this.baseUrl}${this.composeQueryString(filter)}`);
  }

  getUserDtoById(id: number): Observable<UserDto> {
    return this.http.get<UserDto>(`${this.baseUrl}/${id}`);
  }

  changeUserActiveStatus(id: number): Observable<UserStatus> {
    return this.http.put<UserStatus>(`${this.baseUrl}/changeStatus`, id);
  }

  editUserData(model: UserDto): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}`, model);
  }

  create(model: UserDto): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}`, model)
  }

  changePassword(model: UserChangePasswordDto): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}/NewPassword`, model);
  }
}
