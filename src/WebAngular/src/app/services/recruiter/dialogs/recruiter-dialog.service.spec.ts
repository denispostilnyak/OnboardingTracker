import { TestBed } from '@angular/core/testing';

import { RecruiterDialogService } from './recruiter-dialog.service';

describe('RecruiterDialogService', () => {
  let service: RecruiterDialogService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RecruiterDialogService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
