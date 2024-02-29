import { BankNomenclatureDto } from "src/modules/nomenclature/common/models/bank-nomenclature.dto";

export class UserLoginInfoDto {
	token: string;
	fullname: string;
	roleAlias: string;
	bank: BankNomenclatureDto;
}
