import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteRecruiterDialogComponent } from './delete-recruiter-dialog.component';

describe('DeleteRecruiterDialogComponent', () => {
  let component: DeleteRecruiterDialogComponent;
  let fixture: ComponentFixture<DeleteRecruiterDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeleteRecruiterDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DeleteRecruiterDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
