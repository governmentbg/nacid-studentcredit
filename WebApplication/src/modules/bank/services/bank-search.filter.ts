import { BaseSearchFilter } from "src/infrastructure/services/base-search.filter";

export class BankSearchFilter extends BaseSearchFilter {
  name: string;
  bulstat: string;

  constructor() {
    super(10);
  }
}