import { Component, OnInit } from '@angular/core';
import { SharedService } from 'src/app/shared.service';
import { Guid } from "guid-typescript";

@Component({
  selector: 'app-show-emp',
  templateUrl: './show-emp.component.html',
  styleUrls: ['./show-emp.component.css']
})
export class ShowEmpComponent implements OnInit {

  constructor(private service:SharedService) { }

  EmployeeList: any = [];
  PaycheckList: any = [];
  NoofDep: 0;

  ModalTitle:string;
  ActivateAddEditEmpComp: boolean = false;
  ActivateShowCalcComp: boolean = false;
  ActivateAddDepComp: boolean = false;
  emp:any;

  ngOnInit(): void {
    this.getToken();
    this.refreshEmpList();
    this.refreshPaycheckList();
    this.refreshNoofDep();
  }

  addClick(){
    this.emp={
      EmployeeId: 0,
      FirstName:"",
      LastName: "",
      Salary: 0,
      PaycheckType:""
    }
    this.ModalTitle="Add Employee";
    this.ActivateAddEditEmpComp=true;

  }

  editClick(item){
    console.log(item);
    this.emp = item;
    console.log(this.emp);
    this.ModalTitle="Edit Employee";
    this.ActivateAddEditEmpComp=true;
  }

  deleteClick(item){
    if(confirm('Are you sure??')){
      this.service.deleteEmployee(item.EmployeeId).subscribe(data=>{
        this.refreshEmpList();
      })
    }
  }

  CalcClick(item) {
    console.log("CalcClick : " + item.EmployeeId);
    this.emp = item;
    this.ModalTitle = "Calulated Benefits deducted and Salary Report";
    this.ActivateShowCalcComp = true;
  }

  addDepClick(item) {
    console.log("addDepClick" + item);
    this.emp = item;
    console.log(this.emp);
    this.ModalTitle = "Add Dependent";
    this.ActivateAddDepComp = true;
  }

  closeClick(){
    this.ActivateAddEditEmpComp = false;
    this.ActivateAddDepComp = false;
    this.ActivateShowCalcComp = false;
    this.refreshEmpList();
  }

  getToken() {
    console.log("getToken");
    this.service.getOAuthToken().subscribe((res) => {
      console.log("getToken " + res);
      localStorage.setItem('_token', res["access_token"]);
      console.log(localStorage.getItem('_token'));
    });;
  }

  refreshEmpList() {
    console.log("refreshEmpList");
    this.service.getEmpList().subscribe(data=>{
      this.EmployeeList=data;
    });
  }

  refreshPaycheckList() {
    this.service.getPaycheckList().subscribe(data => {
      this.PaycheckList = data;
    });
  }

  refreshNoofDep() {
    this.service.getNoofDep("").subscribe(data => {
      this.NoofDep = data;
    });
  }

}

