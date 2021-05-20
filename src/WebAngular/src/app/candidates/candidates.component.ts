import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { faPlus } from '@fortawesome/free-solid-svg-icons';
import { Observable } from 'rxjs';
import { map } from 'rxjs/internal/operators/map';
import { takeUntil } from 'rxjs/internal/operators/takeUntil';
import { switchMap } from 'rxjs/operators';
import { Candidate } from '../models/candidate/candidate';
import { Origin } from '../models/origin/origin';
import { CandidateService } from '../services/candidate/candidate.service';
import { CandidateDialogService } from '../services/candidate/dialogs/candidate-dialog.service';
import { NotificationService } from '../services/notification/notification.service';
import { OriginService } from '../services/origin/origin.service';

@Component({
  selector: 'app-candidates',
  templateUrl: './candidates.component.html',
  styleUrls: ['./candidates.component.scss'],
})
export class CandidatesComponent implements OnInit {
  candidates = new Array<Candidate>();
  candidateDefaultPicture = 'https://bit.ly/3mamY6w';
  faPlus = faPlus;
  constructor(
    private candidateService: CandidateService,
    private originService: OriginService,
    private dialogService: CandidateDialogService,
    private notificationService: NotificationService,
    private cdRef: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.notificationService.registerCallbackFor("/candidates", this, this.openCreateCandidateDialog);

    this.candidateService
      .GetCandidates()
      .pipe(
        map((item) => {
          item.items.map((candidate) => {
            candidate.profilePicture = candidate.profilePicture ?? this.candidateDefaultPicture;
            this.candidates.push(candidate);
          });
        })
      )
      .subscribe(() => {
        this.cdRef.detectChanges();
      });
  }

  openCreateCandidateDialog(): void {
    let origins = new Array<Origin>();
    let newCandidate$ = this.originService.GetOrigins().pipe(
      switchMap((response, index) => {
        origins = response.items;
        return this.dialogService.openCreateCandidateDialog("Create Candidate",origins);
      })
    );
    newCandidate$
      .pipe(takeUntil(this.dialogService.unsubscribe$))
      .subscribe((candidate) => {
        if (candidate) {
          if(!candidate.cvUrl){
            candidate.cvUrl = this.candidateDefaultPicture;
          }
          this.candidates.push(candidate);
          this.cdRef.detectChanges();
        }
      });
  }

  deleteCandidate(id: number):void{
    this.candidateService.DeleteCandidate(id).subscribe(result=>{
      this.candidates.splice(this.candidates.indexOf(result),1);
      this.cdRef.detectChanges();
    });
  }
}
