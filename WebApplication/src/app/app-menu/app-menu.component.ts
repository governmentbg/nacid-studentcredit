import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { IMenuItem } from 'src/infrastructure/interfaces/IMenuItem';

@Component({
  selector: 'app-menu',
  templateUrl: './app-menu.component.html',
  styleUrls: ['./app-menu.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AppMenuComponent {
  @Input() menuItems: IMenuItem[] = [];
  filePath: string;
  isCollapsed = false;

  constructor(
    public router: Router
  ) { }
}
