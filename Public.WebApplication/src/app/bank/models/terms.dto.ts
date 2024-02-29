import { AttachedFile } from "src/infrastructure/models/attached-file.model";

export class TermsDto {
  id: number;
  description: string;
  file: AttachedFile;
}