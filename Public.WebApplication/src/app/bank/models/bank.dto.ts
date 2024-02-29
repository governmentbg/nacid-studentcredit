import { Nomenclature } from "src/infrastructure/models/nomenclature.model";
import { ContractType } from "../enums/contract-type.enum";
import { BankContractDto } from "./bank-contract.dto";
import { TermsDto } from "./terms.dto";

export class BankDto extends Nomenclature {
  bulstat: string;
  address: string;
  terms: TermsDto;

  contracts: BankContractDto[] = [];

  constructor() {
    super();

    this.contracts.push(new BankContractDto(ContractType.concluded));

    this.terms = new TermsDto();
  }
}