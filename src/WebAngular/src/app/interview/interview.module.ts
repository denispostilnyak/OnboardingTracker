import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InterviewComponent } from './interview.component';
import {MatButtonModule} from '@angular/material/button';
import {MatSliderModule} from '@angular/material/slider';
import {MatCardModule} from '@angular/material/card';
import {MatDividerModule} from '@angular/material/divider';
@NgModule({
  declarations: [InterviewComponent],
  imports: [
    CommonModule,
    MatButtonModule,
    MatSliderModule,
    MatCardModule,
    MatDividerModule,
  ],
  exports:[
    InterviewComponent
  ]
})
export class InterviewModule { }
