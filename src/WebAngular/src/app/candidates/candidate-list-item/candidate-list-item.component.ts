import {
  ChangeDetectorRef,
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
} from '@angular/core';
import { switchMap, takeUntil, tap } from 'rxjs/operators';
import { Candidate } from 'src/app/models/candidate/candidate';
import { Origin } from 'src/app/models/origin/origin';
import { CandidateDialogService } from 'src/app/services/candidate/dialogs/candidate-dialog.service';
import { OriginService } from 'src/app/services/origin/origin.service';

@Component({
  selector: 'app-candidate-list-item',
  templateUrl: './candidate-list-item.component.html',
  styleUrls: ['./candidate-list-item.component.scss'],
})
export class CandidateListItemComponent implements OnInit {
  @Input() candidate!: Candidate;
  @Output() deleteCandidateEvent = new EventEmitter<number>();

  public origins = new Array<Origin>();

  constructor(
    private originService: OriginService,
    private dialogService: CandidateDialogService,
    private cdRef: ChangeDetectorRef
  ) {}

  openEditDialog(): void {
    let origins = new Array<Origin>();
    let updatedCandidate$ = this.originService.GetOrigins().pipe(
      switchMap((response, index) => {
        this.origins = response.items;
        return this.dialogService.openEditCandidateDialog(
          'Update Candidate',
          this.origins,
          this.candidate
        );
      })
    );
    updatedCandidate$
      .pipe(takeUntil(this.dialogService.unsubscribe$))
      .subscribe((candidate) => {
        if (candidate != null) {
          this.candidate = Object.assign(this.candidate, candidate);
          this.cdRef.detectChanges();
        }
      });
  }

  openDeleteDialog(): void {
    this.dialogService
      .openDeleteCandidateDialog(this.candidate)
      .subscribe((value) => {
        if (value) {
          this.deleteCandidateEvent.emit(this.candidate.id);
        }
      });
  }
  ngOnInit(): void {
    this.originService.GetOrigins().subscribe(response=>this.origins = response.items);
  }
}
