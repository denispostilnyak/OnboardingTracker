import { Component, OnInit } from '@angular/core';
@Component({
  selector: 'app-interview',
  templateUrl: './interview.component.html',
  styleUrls: ['./interview.component.scss']
})
export class InterviewComponent implements OnInit {

  title: string = "Interviews for Vacancy 1";
  interviewDate = "01.02.2020";
  interviewTimeSpan = "12.00-14.00";
  candidateName="Candidate.FirstName Candidate.LastName";
  constructor() { }

  ngOnInit(): void {
  }

}
