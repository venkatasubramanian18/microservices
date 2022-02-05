import { Component, Input, OnInit } from '@angular/core';
import { SharedService } from 'src/app/shared.service';

@Component({
  selector: 'app-show-benefit-report',
  templateUrl: './show-benefit-report.component.html',
  styleUrls: ['./show-benefit-report.component.css']
})
export class ShowBenefitReportComponent implements OnInit {

  constructor(private service: SharedService) {
    console.log("constructor");
    this.clear();
  }

  @Input() emp: any;
  calc: any;
  employeeBenefitsDeductedPerPayCheck: number;
  employeeBenefitsDeductedPerYear: number;
  dependentsBenefitsDeductedPerPayCheck: number;
  dependentsBenefitsDeductedPerYear: number;
  totalBenefitsDeductedPerPayCheck: number;
  totalBenefitsDeductedPerPayYear: number;
  totalSalaryAfterDeducted: number;

  ngOnInit(): void {
    console.log("ShowBenefitReportComponent");
    this.refreshCalcReportList();
    
  }

  refreshCalcReportList() {
    console.log("this.emp.EmployeeId : " + this.emp.EmployeeId);
    this.service.calcDeductibles(this.emp.EmployeeId).subscribe(data => {
      this.employeeBenefitsDeductedPerPayCheck = data["employeeBenefitsDeductedPerPayCheck"];
      this.employeeBenefitsDeductedPerYear = data["employeeBenefitsDeductedPerYear"];
      this.dependentsBenefitsDeductedPerPayCheck = data["dependentsBenefitsDeductedPerPayCheck"];
      this.dependentsBenefitsDeductedPerYear = data["dependentsBenefitsDeductedPerYear"];
      this.totalBenefitsDeductedPerPayCheck = data["totalBenefitsDeductedPerPayCheck"];
      this.totalBenefitsDeductedPerPayYear = data["totalBenefitsDeductedPerPayYear"];
      this.totalSalaryAfterDeducted = data["totalSalaryAfterDeducted"];
    });
  }

  clear() {
    this.employeeBenefitsDeductedPerPayCheck = 0;
    this.employeeBenefitsDeductedPerYear = 0;
    this.dependentsBenefitsDeductedPerPayCheck = 0;
    this.dependentsBenefitsDeductedPerYear = 0;
    this.totalBenefitsDeductedPerPayCheck = 0;
    this.totalBenefitsDeductedPerPayYear = 0;
    this.totalSalaryAfterDeducted = 0;
  }
}
