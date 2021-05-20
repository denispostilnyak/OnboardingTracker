import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VacanciesDialogComponent } from './vacancies-dialog.component';

describe('VacanciesDialogComponent', () => {
  let component: VacanciesDialogComponent;
  let fixture: ComponentFixture<VacanciesDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ VacanciesDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(VacanciesDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
