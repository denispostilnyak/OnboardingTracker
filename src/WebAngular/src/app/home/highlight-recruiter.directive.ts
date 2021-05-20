import { Directive, Input, OnInit, ElementRef } from '@angular/core';

@Directive({
  selector: '[appHighlightRecruiter]',
})
export class HighlightRecruiterDirective implements OnInit {
  @Input() highlightClass!: string;
  @Input() highlightCondition!: boolean;

  constructor(private element : ElementRef)
  {}

  ngOnInit(): void {
    if(this.highlightCondition){
      this.element.nativeElement.classList.add(this.highlightClass);
    }
  }
}
