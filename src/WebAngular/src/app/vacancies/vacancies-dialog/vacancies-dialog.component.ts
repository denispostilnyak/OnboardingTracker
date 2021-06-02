import {
  Component,
  HostListener,
  Inject,
  OnInit,
} from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { JobType } from 'src/app/models/common/job-type';
import { SeniorityLevel } from 'src/app/models/common/seniority-level';
import { VacancyStatus } from 'src/app/models/common/vacancy-status';
import { Recruiter } from 'src/app/models/recruiter/recruiter';
import { Vacancy } from 'src/app/models/vacancy/vacancy';
import { VacancyService } from 'src/app/services/vacancy/vacancy.service';

@Component({
  selector: 'app-vacancies-dialog',
  templateUrl: './vacancies-dialog.component.html',
  styleUrls: ['./vacancies-dialog.component.scss'],
})
export class VacanciesDialogComponent implements OnInit {
  vacanciesForm = new FormGroup({});
  file!: File;
  jobTypes = JobType;
  previewImg: any;
  vacancyStatuses = VacancyStatus;
  seniorityLevels = SeniorityLevel;
  recruiters!: Recruiter[];
  isLoading = false;
  isCreateVacancy = false;
  formData = new FormData();
  editVacancy!: Vacancy;
  defaultPicture = 'https://previews.123rf.com/images/arcady31/arcady311508/arcady31150800034/44235258-job-vacancy-rubber-stamp.jpg';

  constructor(
    private formBuilder: FormBuilder,
    @Inject(MAT_DIALOG_DATA) private data: any,
    private dialogRef: MatDialogRef<VacanciesDialogComponent>,
    private vacancyService: VacancyService
  ) {
    this.recruiters = data.recruiters;
    this.editVacancy = data.vacancy;
    this.isCreateVacancy = data.isCreate;
  }
  ngOnInit(): void {
    this.initVacanciesForm();
  }

  @HostListener('change', ['$event.target.files']) emitFile(eventFiles: FileList): void {
    const file = eventFiles && eventFiles[0];
    if (file != null) {
      this.file = file;
      const reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = (event) => {
        this.previewImg = reader.result;
      };
    }
  }

  initVacanciesForm(): void {
    if (this.isCreateVacancy === true) {
      this.initCreateVacancyForm();
    } else {
      this.initEditVacancyForm();
    }
  }

  initEditVacancyForm(): void {
    const assignedRecruiters = this.recruiters.find((recruiter: Recruiter) => recruiter.id === this.editVacancy.assignedRecruiterId);
    this.previewImg = this.editVacancy.vacancyPictureUrl;
    
    this.vacanciesForm = this.formBuilder.group({
      title: [this.editVacancy.title, Validators.required],
      description: [this.editVacancy.description, Validators.required],
      maxSalary: [this.editVacancy.maxSalary, [Validators.min(0), Validators.required]],
      assignedRecruiter: [assignedRecruiters, Validators.required],
      workExperience: [this.editVacancy.workExperience, [Validators.required, Validators.min(0)]],
      vacancyStatus: [this.vacancyStatuses[this.editVacancy.vacancyStatusId], Validators.required],
      seniorityLevel: [this.seniorityLevels[this.editVacancy.seniorityLevelId], Validators.required],
      jobType: [this.jobTypes[this.editVacancy.jobTypeId], Validators.required],
      picture: []
    });
  }

  initCreateVacancyForm(): void {
    this.vacanciesForm = this.formBuilder.group({
      title: ['', Validators.required],
      description: ['', Validators.required],
      maxSalary: ['', [Validators.min(0), Validators.required]],
      assignedRecruiter: ['', Validators.required],
      workExperience: ['', [Validators.required, Validators.min(0)]],
      vacancyStatus: ['', Validators.required],
      seniorityLevel: ['', Validators.required],
      jobType: ['', Validators.required],
      picture: []
    });
  }

  async initFormDataVacancy(): Promise<void> {
    if (this.isCreateVacancy === false) {
      this.formData.append('id', this.editVacancy.id + '');
    }
    let uploadFileName = this.file?.name || this.editVacancy?.vacancyPictureUrl;

    let blob;
    if(!uploadFileName) {
      uploadFileName = this.defaultPicture;
      blob = await fetch(this.defaultPicture).then(r => r.blob());
    }

    this.formData.append('title', this.form.title?.value);
    this.formData.append('description', this.form.description?.value);
    this.formData.append('maxSalary', this.form.maxSalary?.value);
    this.formData.append('workExperience', this.form.workExperience?.value);
    this.formData.append('vacancyPicture', this.file || blob || new Blob(), uploadFileName || '');
    this.formData.append('assignedRecruiterId', this.form.assignedRecruiter?.value.id);
    let jobType = this.jobTypes[this.form.jobType?.value];
    this.formData.append('jobTypeId', jobType);

    let vacancyStatus = this.vacancyStatuses[this.form.vacancyStatus?.value];
    this.formData.append('vacancyStatusId', vacancyStatus);

    let seniorityLevel = this.seniorityLevels[this.form.seniorityLevel?.value];
    this.formData.append('seniorityLevelId', seniorityLevel);
  }

  submitActionVacancy(): void {
    this.initFormDataVacancy();
    this.isLoading = true;
    if (this.isCreateVacancy === true) {
      this.vacancyService.createVacancy(this.formData).subscribe((vacancy: Vacancy) => {
        this.isLoading = false;
        this.dialogRef.close(vacancy);
      },
      (e: any) => {
        this.isLoading = false;
      });
    } else {
      this.vacancyService.updateVacancy(this.formData).subscribe((vacancy: Vacancy) => {
        this.isLoading = false;
        this.dialogRef.close(vacancy);
      },
      (e: any) => {
        this.isLoading = false;
      });
    }
  }

  get form(): any {
    return this.vacanciesForm.controls;
  }
}
