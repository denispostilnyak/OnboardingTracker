import { EventEmitter, HostListener, Output } from '@angular/core';
import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { Candidate } from 'src/app/models/candidate/candidate';
import { Origin } from 'src/app/models/origin/origin';
import { CandidateService } from 'src/app/services/candidate/candidate.service';

@Component({
  selector: 'app-candidate-dialog',
  templateUrl: './candidate-dialog.component.html',
  styleUrls: ['./candidate-dialog.component.scss'],
})
export class CandidatesDialogComponent implements OnInit {

  private formData = new FormData();

  file!: File;
  profilePicFile!: File;
  previewImg!: any;
  uploadNotFinished: boolean = false;

  public origins = new Array<Origin>();
  public candidate!: Candidate;
  @Output() newCandidateEvent = new EventEmitter<Candidate>();
  public dialogFormGroup = this.formBuilder.group({
    firstName: ['', Validators.required],
    lastName: ['', Validators.required],
    email: ['', Validators.compose([Validators.required, Validators.email])],
    phoneNumber: ['', Validators.required],
    yearsOfExperience: ['', Validators.required],
    currentJobInformation: [''],
    origin: ['', Validators.required],
    profilePicture: [],
    cv: [],
  });

  constructor(
    public dialogRef: MatDialogRef<CandidatesDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private formBuilder: FormBuilder,
    private candidateService: CandidateService
  ) {}

  ngOnInit(): void {
    this.origins = this.data.origins;
    if (!this.data.candidate) {
      this.candidate = new Candidate();
    } else {
      this.candidate = Object.assign({}, this.data.candidate);
    }
  }

  updateCandidateProfilePicture($event:any){
    let files = $event.target.files;
    const file = files && files[0];
    this.profilePicFile = file;
    if (file != null) {
      var reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = (_event) => {
        this.previewImg = reader.result;
      };
    }
  }

  updateCandidateCv($event:any){
    let files = $event.target.files;
    const file = files && files[0];
    this.file = file;
  }

  closeWithPost(): void{

    this.formData.append('firstName',this.candidate.firstName);
    this.formData.append('lastName', this.candidate.lastName);
    this.formData.append('phoneNumber', this.candidate.phoneNumber);
    this.formData.append('email',this.candidate.email);
    this.formData.append('yearsOfExperience', this.candidate.yearsOfExperience.toString());
    this.formData.append('currentJobInformation', this.candidate.currentJobInformation || "");
    this.formData.append('cvFile', this.file || new Blob(), this.file?.name);
    this.formData.append('originId',this.origin?.value);
    this.formData.append('profilePicFile', this.profilePicFile || new Blob(), this.profilePicFile?.name);
    let candidate$ : Observable<Candidate>;
    this.uploadNotFinished = true;
    if(this.candidate.id){
      this.formData.append('id',this.candidate.id.toString());
      candidate$ = this.candidateService.UpdateCandidate(this.formData);
    }else{
      candidate$ = this.candidateService.CreateCandidate(this.formData);
    }
    candidate$.subscribe(newCandidate=>{
      this.uploadNotFinished = false;
      this.dialogRef.close(newCandidate);
    });

  }

  get firstName(): AbstractControl | null {
    return this.dialogFormGroup.get('firstName');
  }
  get lastName(): AbstractControl | null {
    return this.dialogFormGroup.get('lastName');
  }
  get email(): AbstractControl | null {
    return this.dialogFormGroup.get('email');
  }
  get phoneNumber(): AbstractControl | null {
    return this.dialogFormGroup.get('phoneNumber');
  }
  get yearsOfExperience(): AbstractControl | null {
    return this.dialogFormGroup.get('yearsOfExperience');
  }
  get currentJobInformation(): AbstractControl | null {
    return this.dialogFormGroup.get('currentJobInformation');
  }
  get origin(): AbstractControl | null {
    return this.dialogFormGroup.get('origin');
  }
  get profilePicture(): AbstractControl | null {
    return this.dialogFormGroup.get('profilePicture');
  }

  get cv():AbstractControl | null{
    return this.dialogFormGroup.get('cv');
  }
}
