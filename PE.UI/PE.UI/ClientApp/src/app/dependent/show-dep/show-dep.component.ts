import { Component, OnInit } from '@angular/core';
import {SharedService} from 'src/app/shared.service';

@Component({
  selector: 'app-show-dep',
  templateUrl: './show-dep.component.html',
  styleUrls: ['./show-dep.component.css']
})
export class ShowDepComponent implements OnInit {

  constructor(private service:SharedService) { }

  dependentList:any=[];

  ModalTitle:string;
  ActivateAddEditDepComp:boolean=false;
  dep:any;

  employeeIdFilter:string="";
  dependentNameFilter:string="";
  dependentListWithoutFilter:any=[];

  ngOnInit(): void {
    this.refreshDepList();
  }

  addClick(){
    this.dep={
      dependentId:0,
      dependentName:""
    }
    this.ModalTitle="Add Dependent";
    this.ActivateAddEditDepComp=true;

  }

  editClick(item){
    this.dep=item;
    this.ModalTitle="Edit Dependent";
    this.ActivateAddEditDepComp=true;
  }

  deleteClick(item){
    if(confirm('Are you sure??')){
      this.service.deletedependent(item.DependentId).subscribe(data=>{
        this.refreshDepList();
      })
    }
  }

  closeClick(){
    this.ActivateAddEditDepComp=false;
    this.refreshDepList();
  }


  refreshDepList(){
    this.service.getDepList().subscribe(data=>{
      this.dependentList=data;
      this.dependentListWithoutFilter=data;
    });
  }

  FilterFn(){
    var employeeIdFilter = this.employeeIdFilter;

    this.dependentList = this.dependentListWithoutFilter.filter(function (el){
        return el.EmployeeId.toString().toLowerCase().includes(
          employeeIdFilter.toString().trim().toLowerCase()
        )
    });
  }

  sortResult(prop,asc){
    this.dependentList = this.dependentListWithoutFilter.sort(function(a,b){
      if(asc){
          return (a[prop]>b[prop])?1 : ((a[prop]<b[prop]) ?-1 :0);
      }else{
        return (b[prop]>a[prop])?1 : ((b[prop]<a[prop]) ?-1 :0);
      }
    })
  }

}
