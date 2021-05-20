import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteCandidateDialogComponent } from './delete-candidate-dialog.component';

describe('DeleteCandidateDialogComponent', () => {
  let component: DeleteCandidateDialogComponent;
  let fixture: ComponentFixture<DeleteCandidateDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeleteCandidateDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DeleteCandidateDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
