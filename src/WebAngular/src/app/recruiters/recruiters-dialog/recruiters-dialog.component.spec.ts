import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RecruitersDialogComponent } from './recruiters-dialog.component';

describe('RecruitersDialogComponent', () => {
  let component: RecruitersDialogComponent;
  let fixture: ComponentFixture<RecruitersDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RecruitersDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RecruitersDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
