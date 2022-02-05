import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import {EmployeeComponent} from './employee/employee.component';
import {dependentComponent} from './dependent/dependent.component';


const routes: Routes = [
{path:'employee',component:EmployeeComponent},
{path:'dependent',component:dependentComponent}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
