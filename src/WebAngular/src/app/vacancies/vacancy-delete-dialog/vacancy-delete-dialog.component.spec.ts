import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VacancyDeleteDialogComponent } from './vacancy-delete-dialog.component';

describe('VacancyDeleteDialogComponent', () => {
  let component: VacancyDeleteDialogComponent;
  let fixture: ComponentFixture<VacancyDeleteDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ VacancyDeleteDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(VacancyDeleteDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
