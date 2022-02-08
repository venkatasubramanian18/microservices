import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';


@Injectable({
  providedIn: 'root'
})
export class SharedService {

  readonly GatewayAPIUrl = environment.GatewayAPIUrl;
  readonly AuthOAPIUrl = environment.AuthOAPIUrl;
  access_token: string;
  expires_in: number;
  token_type: string;

  AuthOres:any = {
  access_token: "",
  expires_in: 0,
  token_type: ""
  };

  constructor(private http: HttpClient) {
 
  }

  requestOptions = new HttpHeaders()
    .set('Authorization', 'Bearer ' + localStorage.getItem('_token'));  

  /*
   * AUTHO 
   */
  getOAuthToken(): Observable<any> {
    console.log("getOAuthToken");
    let val = new Object({
      client_id: environment.client_id,
      client_secret: environment.client_secret,
      audience: environment.audience,
      grant_type: environment.grant_type
    });

    const body = JSON.stringify(val);
    const headers = new HttpHeaders()
      .set('content-type', 'application/json');

    return this.http.post(this.AuthOAPIUrl, body, { 'headers': headers });
  }

  /*
  * EMPLOYEES API CALLS
  */

  addEmployee(val: any) {
    return this.http.post(this.GatewayAPIUrl + '/employees', val, { 'headers': this.requestOptions });
  }

  updateEmployee(val: any) {

    return this.http.put(this.GatewayAPIUrl + '/employees/' + val.EmployeeId, val, { 'headers': this.requestOptions });
  }

  deleteEmployee(val: any) {
    return this.http.delete(this.GatewayAPIUrl + '/employees/' + val, { 'headers': this.requestOptions });
  }

  getEmpList(): Observable<any[]> {  
    return this.http.get<any>(this.GatewayAPIUrl + '/employees', { 'headers': this.requestOptions });
  }

  getPaycheckList(): Observable<any[]> {
    return this.http.get<any>(this.GatewayAPIUrl + '/employees/PaycheckTypes', { 'headers': this.requestOptions });
  }

  /*
  * DEPENDENTS API CALLS
  */


  getDepList():Observable<any[]>{
    return this.http.get<any>(this.GatewayAPIUrl + '/dependents', { 'headers': this.requestOptions });
  }

  adddependent(val:any){
    return this.http.post(this.GatewayAPIUrl + '/dependents', val, { 'headers': this.requestOptions });
  }

  updatedependent(val:any){
    return this.http.put(this.GatewayAPIUrl + '/dependents/' + val.DependentId, val, { 'headers': this.requestOptions });
  }

  deletedependent(val:any){
    return this.http.delete(this.GatewayAPIUrl + '/dependents/' + val, { 'headers': this.requestOptions });
  }

  getDependentTypeList(): Observable<any[]>{
    return this.http.get<any>(this.GatewayAPIUrl + '/dependentsTypes', { 'headers': this.requestOptions });
  }

  getNoofDep(val: any) {
    return this.http.get<any>(this.GatewayAPIUrl + '/dependents/' + val.EmployeeId, { 'headers': this.requestOptions });
  }


  /*
  * BUSINESS API CALLS
  */

  calcDeductibles(val: any) {
    return this.http.get(this.GatewayAPIUrl + '/DeductionCalc/' + val, { 'headers': this.requestOptions });
  }

}
