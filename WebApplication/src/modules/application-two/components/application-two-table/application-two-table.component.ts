import { Component, Input, OnInit } from '@angular/core';
import { RecordEntryDto } from '../../models/record-entry.dto';

@Component({
  selector: 'app-application-two-table',
  templateUrl: 'application-two-table.component.html',
  styleUrls: ['application-two-table.component.css']
})

export class ApplicationTwoTableComponent {
  
  @Input() recordEntries: RecordEntryDto[];
  @Input() isNewApplicationTwo: boolean = false;
  @Input() id: string = '';
}