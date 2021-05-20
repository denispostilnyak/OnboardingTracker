import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RecruiterListItemComponent } from './recruiter-list-item.component';

describe('RecruiterListItemComponent', () => {
  let component: RecruiterListItemComponent;
  let fixture: ComponentFixture<RecruiterListItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RecruiterListItemComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RecruiterListItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
