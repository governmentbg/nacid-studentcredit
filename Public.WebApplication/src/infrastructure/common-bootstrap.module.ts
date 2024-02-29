import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { TranslateModule } from "@ngx-translate/core";
import { LoadingIndicatorComponent } from "./components/loading-indicator/loading-indicator.component";
import { LoadingIndicatorService } from "./components/loading-indicator/loading-indicator.service";
import { SvgIconComponent } from "./components/svg-icon/svg-icon.component";

@NgModule({
  imports: [
    CommonModule,
    TranslateModule,
    FormsModule
  ],
  declarations: [
    LoadingIndicatorComponent,
    SvgIconComponent
  ],
  exports: [
    LoadingIndicatorComponent,
    SvgIconComponent
  ],
  providers: [
    LoadingIndicatorService,
  ]
})
export class CommonBootstrapModule { }