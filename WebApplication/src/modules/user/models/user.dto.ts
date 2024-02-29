import { UserStatus } from "../enums/user-status.enum";
import { Role } from "./role.dto";
import { BankDto } from "src/modules/bank/model/bank.dto";

export class UserDto {
  id: number | null;
  phone: string;
  firstName: string;
  middleName: string;
  lastName: string;
  email: string;
  status: UserStatus | null;

  roleId: number;
  role: Role;

  bankId: number;
  bank: BankDto;
}