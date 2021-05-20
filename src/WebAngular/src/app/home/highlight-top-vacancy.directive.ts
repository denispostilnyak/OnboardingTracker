import { Directive, ElementRef, Input, OnInit } from '@angular/core';

@Directive({
  selector: '[appHighlightTopVacancy]'
})
export class HighlightTopVacancyDirective implements OnInit{

  @Input() highlightClass!: string;
  @Input() vacancyIndex!: number;

  constructor(private el: ElementRef) { }

  ngOnInit(): void {
    if (this.vacancyIndex === 0) {
      this.el.nativeElement.classList.add(this.highlightClass);
    }
  }
}
