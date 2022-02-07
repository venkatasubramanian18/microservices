import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class SharedService {

  readonly GatewayAPIUrl = "https://localhost:5000/gateway";

  constructor(private http:HttpClient) { }

  /*
  * EMPLOYEES API CALLS
  */

  addEmployee(val: any) {
    return this.http.post(this.GatewayAPIUrl + '/employees', val);
  }

  updateEmployee(val: any) {

    return this.http.put(this.GatewayAPIUrl + '/employees/' + val.EmployeeId, val);
  }

  deleteEmployee(val: any) {
    return this.http.delete(this.GatewayAPIUrl + '/employees/' + val);
  }

  getEmpList(): Observable<any[]> {
    return this.http.get<any>(this.GatewayAPIUrl + '/employees');
  }

  getPaycheckList(): Observable<any[]> {
    return this.http.get<any>(this.GatewayAPIUrl + '/employees/PaycheckTypes');
  }

  /*
  * DEPENDENTS API CALLS
  */


  getDepList():Observable<any[]>{
    return this.http.get<any>(this.GatewayAPIUrl+'/dependents');
  }

  adddependent(val:any){
    return this.http.post(this.GatewayAPIUrl+'/dependents',val);
  }

  updatedependent(val:any){
    return this.http.put(this.GatewayAPIUrl + '/dependents/' + val.DependentId,val);
  }

  deletedependent(val:any){
    return this.http.delete(this.GatewayAPIUrl+'/dependents/'+val);
  }

  getDependentTypeList(): Observable<any[]>{
    return this.http.get<any>(this.GatewayAPIUrl+'/dependentsTypes');
  }

  getNoofDep(val: any) {
    return this.http.get<any>(this.GatewayAPIUrl + '/dependents/'+val.EmployeeId);
  }


  /*
  * BUSINESS API CALLS
  */

  calcDeductibles(val: any) {
    return this.http.get(this.GatewayAPIUrl + '/DeductionCalc/' + val);
  }


}
