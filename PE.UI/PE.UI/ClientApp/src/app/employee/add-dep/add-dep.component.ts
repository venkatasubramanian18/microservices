import { Component, Input, OnInit } from '@angular/core';
import { SharedService } from '../../shared.service';

@Component({
  selector: 'app-add-dep',
  templateUrl: './add-dep.component.html',
  styleUrls: ['./add-dep.component.css']
})
export class AddDepComponent implements OnInit {

  constructor(private service: SharedService) { }

  @Input() emp: any;
  dep: any;
  EmployeeId: 0;
  DependentId: 0;
  FirstName: string;
  LastName: string;
  CreatedDate: string;
  DependentType: string;
  DependentTypeId: 0;

  DependentTypeList: any = [];

  ngOnInit(): void {
    this.refreshDependentTypeList();
    this.EmployeeId = this.emp.EmployeeId;
  }

  adddependent() {
    var val = {
      EmployeeId: this.EmployeeId,
      FirstName: this.FirstName,
      LastName: this.LastName,
      DependentTypeId: this.DependentType["DependentTypeId"]
    };

    console.log(val.DependentTypeId);
    this.service.adddependent(val).subscribe((res: any) => {
    });
  }

  setNewDependentType(pcType) {
    console.log(pcType.PaycheckType);
  }

  refreshDependentTypeList() {
    console.log("EmployeeId" + this.emp.EmployeeId);
    this.service.getDependentTypeList().subscribe(data => {
      this.DependentTypeList = data;
      console.log(this.DependentTypeList);
    });
  }

}
