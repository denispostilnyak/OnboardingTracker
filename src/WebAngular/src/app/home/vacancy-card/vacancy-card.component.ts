import { Component, OnInit, Input } from '@angular/core';
import { Vacancy } from '../../models/vacancy/vacancy';
@Component({
  selector: 'app-vacancy-card',
  templateUrl: './vacancy-card.component.html',
  styleUrls: ['./vacancy-card.component.scss']
})
export class VacancyCardComponent implements OnInit {
  @Input() public vacancy!: Vacancy;
  defaultPicture = 'https://bit.ly/2KBf9c0';
  constructor() { }

  ngOnInit(): void {
  }

}
