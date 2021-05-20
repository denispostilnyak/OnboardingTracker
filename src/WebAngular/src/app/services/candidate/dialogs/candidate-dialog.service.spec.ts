import { TestBed } from '@angular/core/testing';

import { CandidateDialogService } from './candidate-dialog.service';

describe('CandidateDialogService', () => {
  let service: CandidateDialogService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CandidateDialogService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
