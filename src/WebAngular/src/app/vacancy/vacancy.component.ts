import { Input } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { Vacancy } from '../models/vacancy/vacancy';

@Component({
  selector: 'app-vacancy',
  templateUrl: './vacancy.component.html',
  styleUrls: ['./vacancy.component.scss']
})
export class VacancyComponent implements OnInit {
  @Input() public vacancy!: Vacancy;

  constructor() { }

  ngOnInit(): void {

  }
}
