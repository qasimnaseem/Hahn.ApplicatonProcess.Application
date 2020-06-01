import { Component, OnInit } from '@angular/core';
import { ApplicantService } from 'src/app/services/applicant/applicant.service';
import { Applicant } from 'src/app/shared/models/applicant/applicant.model';
import { Router } from '@angular/router';
import { ButtonRendererComponent } from '../../common/button-renderer/button-renderer/button-renderer.component';

@Component({
  selector: 'app-applicant-list',
  templateUrl: './applicant-list.component.html',
  styleUrls: ['./applicant-list.component.scss']
})

export class ApplicantListComponent implements OnInit {

  applicants: Applicant[] = [];
  columnDefs: any[] = [];
  frameworkComponents: any;

  constructor(
    private service: ApplicantService,
    private router: Router) {
    this.frameworkComponents = {
      buttonRenderer: ButtonRendererComponent,
    }
  }

  async ngOnInit() {
    this.columnDefs = this.gridColumns;
    await this.loadApplicants();
  }

  private async loadApplicants(){
    const data = await this.service.getAll();
    this.applicants = data.applicants as Applicant[];
  }

  private get gridColumns() {
    return [
      { headerName: 'Name', field: 'name', sortable: true, filter: true },
      { headerName: 'Family Name', field: 'familyName', sortable: true, filter: true },
      { headerName: 'Email', field: 'emailAddress', sortable: true, filter: true, width: 250 },
      { headerName: 'Age', field: 'age', sortable: true, filter: true },
      { headerName: 'Country', field: 'countryOfOrigin', sortable: true, filter: true },
      {
        headerName: 'Details',
        cellRenderer: 'buttonRenderer',
        cellRendererParams: {
          onClick: this.onViewBtnClick.bind(this),
          label: 'View',
          sortable: false,
          filter: false
        }
      },
      {
        headerName: 'Delete',
        cellRenderer: 'buttonRenderer',
        cellRendererParams: {
          onClick: this.onDeleteBtnClick.bind(this),
          label: 'Delete',
          sortable: false,
          filter: false
        }
      }
    ];
  }

  private onViewBtnClick(e) {
    const applicantId = e.rowData.applicantId as number;
    this.router.navigateByUrl(`/applicant/${applicantId}`);
  }

  private async onDeleteBtnClick(e){
    await this.service.delete(e.rowData.applicantId as number);
    await this.loadApplicants();
  }

  onAddNewClick(){
    this.router.navigate(['/applicant']);
  }
}
