import { ChangeDetectionStrategy, Component, HostBinding, Input } from '@angular/core';

@Component({
  selector: 'app-icon',
  templateUrl: './svg-icon.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class SvgIconComponent {
  @Input() width = 16;
  @Input() height = 16;
  @Input() icon: string;
  @Input() color: string;
  _centered: boolean;

  @Input()
  public get centered(): boolean {
    return this._centered;
  }
  public set centered(val: boolean) {
    this._centered = val;
    this.isCenteredBind = val;
  }

  @HostBinding('class.centered')
  public isCenteredBind: boolean = false; //default value
}