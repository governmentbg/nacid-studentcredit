import { AttachedFile } from "src/infrastructure/models/attached-file.model";
import { ContractType } from "../enums/contract-type.enum";

export class BankContractDto {
  id: number;
  number: string;
  date: Date;
  type: ContractType;
  file: AttachedFile;

  constructor(type: ContractType) {
    this.type = type;
  }
}