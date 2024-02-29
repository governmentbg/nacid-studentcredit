import { CreditType } from "../enums/credit-type.enum";

export class CreditSelectFilterDto {
  uin: string;
  creditType: CreditType;
  creditNumber: string;
  bankId: number;
  applicationOneId: number;

  constructor(uin: string, creditType: CreditType, creditNumber: string, bankId: number, applicationOneId: number) {
    this.uin = uin;
    this.creditType = creditType;
    this.creditNumber = creditNumber;
    this.bankId = bankId;
    this.applicationOneId = applicationOneId;
  }
}