import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LeaveManagementList } from './leave-management-list/leave-management-list';

const routes: Routes = [{ path:'', component : LeaveManagementList}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LeaveManagementRoutingModule { }
