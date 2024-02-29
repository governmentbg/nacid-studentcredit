import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-part-panel',
  templateUrl: './part-panel.component.html'
})
export class PartPanelComponent {
  @Input() header: string;
  @Input() margin_top: string = "mt-3"
}
