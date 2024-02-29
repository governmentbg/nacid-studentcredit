import { ChangeDetectionStrategy, Component, Input } from '@angular/core';

@Component({
  selector: 'app-help-tooltip',
  templateUrl: './help-tooltip.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class HelpTooltipComponent {
  @Input() icon = 'question-circle';
  @Input() tooltipText: string;
}
