import { ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { BehaviorSubject, Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { Recruiter } from '../models/recruiter/recruiter';
import { RecruiterService } from '../services/recruiter/recruiter.service';
import { MatDialog } from '@angular/material/dialog';
import { faPlus } from '@fortawesome/free-solid-svg-icons';
import { RecruitersDialogComponent } from './recruiters-dialog/recruiters-dialog.component';
import { RecruiterDialogService } from '../services/recruiter/dialogs/recruiter-dialog.service';
import { NotificationService } from '../services/notification/notification.service';

@Component({
  selector: 'app-recruiters',
  templateUrl: './recruiters.component.html',
  styleUrls: ['./recruiters.component.scss'],
})
export class RecruitersComponent implements OnInit, OnDestroy {
  recruiters: Recruiter[] = new Array<Recruiter>();
  faPlus = faPlus;
  private unsubscribe$ = new Subject<void>();
  private recruitersDefaultPicture = 'https://bit.ly/3mamY6w';
  constructor(
    private recruiterService: RecruiterService,
    private dialogService : RecruiterDialogService,
    private notificationService: NotificationService,
    private cdRef: ChangeDetectorRef
  ) {}

  ngOnDestroy(): void {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }

  openDialog(): void {
    let dialogRef = this.dialogService.openCreateRecruiterDialog("Create Recruiter");

    dialogRef.subscribe((recruiterFormData) => {
      if (recruiterFormData == null) {
        return;
      }
      if (!recruiterFormData.pictureUrl) {
        recruiterFormData.pictureUrl = this.recruitersDefaultPicture;
      }
      this.recruiters.push(recruiterFormData);
      this.cdRef.detectChanges();
    });
  }
  deleteRecruiter(id: number):void{
    console.log(id);
    this.recruiterService.DeleteRecruiter(id).subscribe(result=>{
      this.recruiters.splice(this.recruiters.indexOf(result),1);
      this.cdRef.detectChanges();
    });
  }
  ngOnInit(): void {

    this.notificationService.registerCallbackFor("/recruiters",this,this.openDialog);

    this.recruiterService
      .GetRecruiters()
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe((data: any) => {
        this.recruiters = data.items.map((item: Recruiter) => {
          item.pictureUrl = item.pictureUrl ?? this.recruitersDefaultPicture;
          return item;
        });
        this.cdRef.detectChanges();
      });
  }
}
