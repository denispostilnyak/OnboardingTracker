import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VacancyListItemComponent } from './vacancy-list-item.component';

describe('VacancyListItemComponent', () => {
  let component: VacancyListItemComponent;
  let fixture: ComponentFixture<VacancyListItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ VacancyListItemComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(VacancyListItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
