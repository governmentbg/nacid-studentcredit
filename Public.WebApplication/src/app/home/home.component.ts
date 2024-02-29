import { AfterViewInit, Component } from "@angular/core";
import { Location } from '@angular/common';

@Component({
  selector: 'app-home',
  templateUrl: 'home.component.html',
  styleUrls: ['home.component.css']
})

export class HomeCompoennt implements AfterViewInit {
  section: string;
  constructor(private location: Location) {
  }

  ngAfterViewInit(): void {
    const state = this.location.getState() as any;
    this.section = state.section as string;
    if (this.section) {
      this.scrollToSection(this.section);
    }
  }

  scrollToSection(id: string): void {
    let el = document.getElementById(id);
    el.scrollIntoView({ behavior: "smooth" });
  }
}