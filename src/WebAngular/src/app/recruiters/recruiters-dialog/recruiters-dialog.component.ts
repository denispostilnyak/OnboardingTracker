import {
  Component,
  EventEmitter,
  HostListener,
  Inject,
  Input,
  OnDestroy,
  OnInit,
  Output,
} from '@angular/core';
import { AbstractControl, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { BehaviorSubject, Subject, SubscriptionLike } from 'rxjs';
import { Recruiter } from 'src/app/models/recruiter/recruiter';
import { RecruiterService } from 'src/app/services/recruiter/recruiter.service';
@Component({
  selector: 'app-recruiters-dialog',
  templateUrl: './recruiters-dialog.component.html',
  styleUrls: ['./recruiters-dialog.component.scss'],
})
export class RecruitersDialogComponent implements OnInit {
  file!: File;
  formData = new FormData();
  previewImg!: any;
  uploadNotFinished: boolean = false;
  public recruiter!: Recruiter;

  constructor(
    public dialogRef: MatDialogRef<RecruitersDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private formBuilder: FormBuilder,
    private recruiterService: RecruiterService
  ) {}

  public dialogFormGroup = this.formBuilder.group({
    email: ['', Validators.compose([Validators.required, Validators.email])],
    firstName: ['', Validators.required],
    lastName: ['', Validators.required],
    picture: [],
  });

  ngOnInit(): void {
    this.recruiter = Object.assign<Recruiter, Recruiter>(
      new Recruiter(),
      this.data.recruiter
    );
  }
  @HostListener('change', ['$event.target.files']) emitFile(event: FileList) {
    const file = event && event[0];
    this.file = file;
    if (file != null) {
      var reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = (_event) => {
        this.previewImg = reader.result;
      };
    }
  }

  closeWithPost(): void {
    let update: boolean = false;
    if (this.recruiter.id) {
      update = true;
      this.formData.append('id', this.recruiter.id.toString());
    }
    this.formData.append('firstName', this.recruiter.firstName);
    this.formData.append('lastName', this.recruiter.lastName);
    this.formData.append('email', this.recruiter.email);
    this.formData.append('picture', this.file || new Blob(), this.file?.name);
    this.uploadNotFinished = true;
    if (!update) {
      this.recruiterService
        .CreateRecruiter(this.formData)
        .subscribe((recruiter) => {
          this.uploadNotFinished = false;
          this.dialogRef.close(recruiter);
        });
    }else{
      this.recruiterService
        .UpdateRecruiter(this.formData)
        .subscribe((recruiter) => {
          this.uploadNotFinished = false;
          this.dialogRef.close(recruiter);
        });
    }

  }
  closeWithoutSaving(): void {
    this.dialogRef.close();
  }

  get email(): AbstractControl | null {
    return this.dialogFormGroup.get('email');
  }
  get firstName(): AbstractControl | null {
    return this.dialogFormGroup.get('firstName');
  }
  get lastName(): AbstractControl | null {
    return this.dialogFormGroup.get('lastName');
  }
}
