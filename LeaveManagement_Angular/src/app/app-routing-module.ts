import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'leave',
    pathMatch: 'full'
  },
  {path:'leave', loadChildren: () =>
  import('./pages/leave-management/leave-management-module').then(
    (m) => m.LeaveManagementModule
  )}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
