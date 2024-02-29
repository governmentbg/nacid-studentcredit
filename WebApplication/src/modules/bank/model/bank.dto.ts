import { Nomenclature } from "src/modules/nomenclature/common/models/nomenclature.model";
import { ContractType } from "../enums/contract-type.enum";
import { BankContractDto } from "./bank-contract.dto";
import { TermsFileDto } from "./terms-file.dto";

export class BankDto extends Nomenclature {
  bulstat: string;
  address: string;
  description: string;
  terms: TermsFileDto;
  
  contracts: BankContractDto[] = [];

  constructor() {
    super();

    this.contracts.push(new BankContractDto(ContractType.concluded));

    this.terms = new TermsFileDto();
  }
}