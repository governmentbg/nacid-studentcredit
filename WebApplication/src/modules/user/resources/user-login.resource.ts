import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Configuration } from 'src/infrastructure/configuration/configuration';
import { LoginDto } from '../models/login.dto';
import { UserLoginInfoDto } from '../models/user-login-info.dto';

@Injectable()
export class UserLoginResource {
	constructor(
		private http: HttpClient,
		private configuration: Configuration
	) { }

	login(model: LoginDto): Observable<UserLoginInfoDto> {
		return this.http.post<UserLoginInfoDto>(`${this.configuration.restUrl}/Login`, model);
	}
}
