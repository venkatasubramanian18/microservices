import { Component, OnInit,Input } from '@angular/core';
import {SharedService} from 'src/app/shared.service';

@Component({
  selector: 'app-add-edit-emp',
  templateUrl: './add-edit-emp.component.html',
  styleUrls: ['./add-edit-emp.component.css']
})
export class AddEditEmpComponent implements OnInit {

  constructor(private service:SharedService) { }

  @Input() emp:any;
  EmployeeId:0;
  FirstName:string;
  LastName: string;
  CreatedDate: string;
  PaycheckType: string;
  Salary: number;

  dependentsList: any = [];
  PaycheckList: any = [];

  ngOnInit(): void {
    this.refreshPaycheckList();
  }

  setNewPaycheckType(pcType){
    console.log(pcType.PaycheckType);
  }

  addEmployee(){
    var val = {
      EmployeeId: this.EmployeeId,
      FirstName: this.emp.FirstName,
      LastName: this.emp.LastName,
      PaycheckType: this.emp.PaycheckType.PaycheckType,
      Salary: this.emp.Salary
    };

    console.log(val.PaycheckType);
    this.service.addEmployee(val).subscribe((res : any) => {
    });
  }

  updateEmployee() {
    var val = {
      EmployeeId: this.emp.EmployeeId,
      FirstName: this.emp.FirstName,
      LastName: this.emp.LastName,
      CreatedDate: this.emp.CreatedDate,
      PaycheckType: this.emp.PaycheckType,
      Salary: this.emp.Salary
    };
    this.service.updateEmployee(val).subscribe(res=>{
    alert(res.toString());
    });
  }

  refreshPaycheckList() {
    this.service.getPaycheckList().subscribe(data => {
      this.PaycheckList = data;
    });
  }

}

