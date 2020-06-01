import { Injectable } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Applicant } from 'src/app/shared/models/applicant/applicant.model';
import { Observable } from 'rxjs';
import { MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition } from '@angular/material/snack-bar';
import { retry } from 'rxjs/operators';
import { Router } from '@angular/router';

const apiUrl = 'api/applicant';

@Injectable({
  providedIn: 'root'
})
export class ApplicantService {

  addEditForm: FormGroup;
  isEditMode = false;

  constructor(
    private formBuilder: FormBuilder,
    private httpClient: HttpClient,
    private snackBar: MatSnackBar,
    private router: Router
  ) { }


  get formFields() {
    return this.addEditForm.controls;
  }

  initAddEditMode(applicantId: number) {

    this.isEditMode = applicantId > 0;

    if (this.isEditMode) {
      this.getById(applicantId).subscribe(response => {
        this.addEditForm.patchValue(response);
      });
    }
  }

  initializeAddEditForm(): void {

    if (this.addEditForm) {
      this.addEditForm.clearValidators();
      this.addEditForm.updateValueAndValidity();
      this.addEditForm.reset();
    }

    this.addEditForm = this.formBuilder.group({
      applicantId: [0],
      name: ['', [Validators.required, Validators.minLength(5)]],
      familyName: ['', [Validators.required, Validators.minLength(5)]],
      emailAddress: ['', [Validators.required, Validators.email]],
      address: ['', [Validators.required, Validators.minLength(10)]],
      countryOfOrigin: ['', Validators.required],
      age: ['', [Validators.required, Validators.min(21), Validators.max(60)]],
      hired: [false],
    });
  }

  saveApplicant() {

    if (this.addEditForm.valid) {

      const applicant = this.addEditForm.value as Applicant;

      const saveSubscription = this.isEditMode ?
        this.updateApplicant(applicant).subscribe(() => {

          this.showNotification("Applicant Updated Successfully");
        }) :

        this.addApplicant(applicant).subscribe(() => {
          
          this.showNotification("Applicant created Successfully");
          this.navigateToList();
        });
    }
  }

  public navigateToList(){
    this.router.navigate(['/applicants/view']);
  }

  private addApplicant(applicant: Applicant): Observable<any> {

    return this.httpClient.post('api/applicant', applicant);

  }

  private updateApplicant(applicant: Applicant): Observable<any> {

    return this.httpClient.put(apiUrl, applicant);
  }

  private getById(applicantId: number): Observable<any> {

    return this.httpClient.get(`${apiUrl}/${applicantId}`);
  }

  public async getAll(): Promise<any> {

    return await this.httpClient.get(apiUrl).
      pipe(retry(2)).
      toPromise();
  }

  public async delete(applicantId: number): Promise<any> {

    const response = await this.httpClient.
      delete(`${apiUrl}/${applicantId}`).toPromise();

    this.showNotification("Record Deleted Successfully");

    return response;
  }

  public showNotification(msg: string) {
    this.snackBar.open(msg, "", {
      duration: 2000,
      horizontalPosition: 'center' as MatSnackBarHorizontalPosition,
      verticalPosition: 'top' as MatSnackBarVerticalPosition,
    });
  }
}
