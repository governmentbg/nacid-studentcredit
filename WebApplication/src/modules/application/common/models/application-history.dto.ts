import { ApplicationHistoryType } from "../enums/application-history-type.enum";

export class ApplicationHistoryDto {
  description: string;
  applicationId: number;
  modificationDate: Date;
  userFullName: string;
  applicationHistoryType: ApplicationHistoryType;
}