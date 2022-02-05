import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class SharedService {
  readonly EmployeeServiceAPIUrl = "https://localhost:5001/api";
  readonly DependentServiceAPIUrl = "https://localhost:5002/api";
  readonly BusinessServiceAPIUrl = "https://localhost:5003/api";

  readonly GatewayAPIUrl = "https://localhost:5000/gateway";

  constructor(private http:HttpClient) { }

  getDepList():Observable<any[]>{
    return this.http.get<any>(this.GatewayAPIUrl+'/dependents');
  }

  adddependent(val:any){
    return this.http.post(this.DependentServiceAPIUrl+'/dependents',val);
  }

  updatedependent(val:any){
    return this.http.put(this.DependentServiceAPIUrl + '/dependents/' + val.DependentId,val);
  }

  deletedependent(val:any){
    return this.http.delete(this.DependentServiceAPIUrl+'/dependents/'+val);
  }

  getEmpList():Observable<any[]>{
    return this.http.get<any>(this.GatewayAPIUrl+'/employees');
  }

  getPaycheckList(): Observable<any[]>{
    return this.http.get<any>(this.GatewayAPIUrl+'/employees/PaycheckTypes');
  }

  getDependentTypeList(): Observable<any[]>{
    return this.http.get<any>(this.DependentServiceAPIUrl+'/dependents/Types');
  }

  getNoofDep(val: any) {
    return this.http.get<any>(this.DependentServiceAPIUrl + '/dependents/'+val.EmployeeId);
  }


  addEmployee(val:any){
    return this.http.post(this.EmployeeServiceAPIUrl +'/employees',val);
  }

  updateEmployee(val: any) {

    return this.http.put(this.EmployeeServiceAPIUrl+'/employees/'+val.EmployeeId,val);
  }

  deleteEmployee(val:any){
    return this.http.delete(this.EmployeeServiceAPIUrl +'/employees/'+val);
  }

  calcDeductibles(val: any) {
    return this.http.get(this.GatewayAPIUrl + '/DeductionCalc/' + val);
  }


}
