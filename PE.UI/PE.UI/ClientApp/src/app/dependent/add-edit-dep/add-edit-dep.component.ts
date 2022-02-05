import { Component, OnInit,Input } from '@angular/core';
import {SharedService} from 'src/app/shared.service';

@Component({
  selector: 'app-add-edit-dep',
  templateUrl: './add-edit-dep.component.html',
  styleUrls: ['./add-edit-dep.component.css']
})
export class AddEditDepComponent implements OnInit {

  constructor(private service:SharedService) { }

  @Input() dep:any;
  EmployeeId: 0;
  DependentId: 0;
  DependentTypeId: 0;
  FirstName: string;
  LastName: string;
  CreatedDate: string;

  ngOnInit(): void {
    this.EmployeeId = this.dep.EmployeeId;
    this.DependentId = this.dep.DependentId;
    this.FirstName = this.dep.FirstName;
    this.LastName = this.dep.LastName;
    this.CreatedDate = this.dep.CreatedDate;
    this.DependentTypeId = this.dep.DependentTypeId;
  }

  updatedependent(){
    var val = {
      EmployeeId: this.EmployeeId,
      DependentId: this.DependentId,
      DependentTypeId: this.DependentTypeId,
      FirstName: this.FirstName,
      CreatedDate: this.CreatedDate,
      LastName: this.LastName
    };
    this.service.updatedependent(val).subscribe(res=>{
    alert(res.toString());
    });
  }

}
