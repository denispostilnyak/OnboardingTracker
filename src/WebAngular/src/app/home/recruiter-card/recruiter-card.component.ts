import { animate, state, style, transition, trigger } from '@angular/animations';
import { Component, Input } from '@angular/core';
import { Recruiter } from '../../models/recruiter/recruiter';
@Component({
  selector: 'app-recruiter-card',
  templateUrl: './recruiter-card.component.html',
  styleUrls: ['./recruiter-card.component.scss'],

})
export class RecruiterCardComponent  {
  @Input() recruiter! : Recruiter;
  recruitersDefaultPicture = 'https://bit.ly/3mamY6w';
  constructor() { }
}
