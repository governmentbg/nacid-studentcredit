import { UserStatus } from "../enums/user-status.enum";

export class UserSearchResultDto {
  id: number;
  username: string;
  firstName: string;
  middleName: string;
  lastName: string;
  role: string;
  institutionName: string;
  status: UserStatus;
}