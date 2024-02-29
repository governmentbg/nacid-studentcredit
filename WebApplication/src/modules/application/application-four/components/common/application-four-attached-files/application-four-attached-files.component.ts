import { Component } from "@angular/core";
import { CommonFormComponent } from "src/infrastructure/components/common-form.component";
import { ApplicationFourTypeAliases } from "src/infrastructure/constants/constants";
import { ApplicationFourAttachedFileTypeEnumLocalizaiton } from "src/infrastructure/constants/enum-localization.const";
import { ApplicationFourAttachedFileType } from "../../../enums/application-four-attached-file-type";
import { ApplicationFourDto } from "../../../models/application-four.dto";

@Component({
  selector: 'app-application-four-attached-files',
  templateUrl: 'application-four-attached-files.component.html'
})

export class ApplicationFourAttachedFilesComponent extends CommonFormComponent<ApplicationFourDto> {
  applicationFourAttachedFileTypeLocalization = ApplicationFourAttachedFileTypeEnumLocalizaiton;
  applicationFourAttachedFileType = ApplicationFourAttachedFileType;
  applicationFourTypeAliases = ApplicationFourTypeAliases;

  constructor() {
    super()
  }
}