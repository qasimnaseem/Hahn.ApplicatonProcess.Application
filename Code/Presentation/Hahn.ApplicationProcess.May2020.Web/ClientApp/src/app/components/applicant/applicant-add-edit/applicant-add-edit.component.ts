import { Component, OnInit } from '@angular/core';
import { ApplicantService } from 'src/app/services/applicant/applicant.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-applicant-add-edit',
  templateUrl: './applicant-add-edit.component.html',
  styleUrls: ['./applicant-add-edit.component.scss']
})
export class ApplicantAddEditComponent implements OnInit {

  constructor(
    public service: ApplicantService,
    private route: ActivatedRoute
  ) {
    this.route.paramMap.subscribe(params => {
      this.service.initAddEditMode(Number(params.get("id")));
    });
  }

  ngOnInit() {
    this.service.initializeAddEditForm();
  }

  onBackClick() {
    this.service.navigateToList();
  }
}