import { AttachedFile } from "src/infrastructure/models/attached-file.model";
import { ApplicationFourAttachedFileType } from "../enums/application-four-attached-file-type";

export class ApplicationFourAttachedFileDto {
  type: ApplicationFourAttachedFileType;
  file: AttachedFile;

  constructor(type: ApplicationFourAttachedFileType) {
    this.type = type;
  }
}