import { Component, Inject } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from '@angular/material/dialog';
import { CommonService } from '../../../service/common/common';
import { ApiUrlHelper } from '../../../api/api-url';
import { ToastrService } from 'ngx-toastr';


@Component({
  selector: 'app-add-edit-leave-list',
  standalone: false,
  templateUrl: './add-edit-leave-list.html',
  styleUrl: './add-edit-leave-list.scss'
})

export class AddEditLeaveComponent {
  leaveForm: FormGroup;
  leaveTypes:any = [];
  statuses:any = [];
  leaveId:any;
  submitted:boolean = false;

  constructor(private fb: FormBuilder,
    private dialog:MatDialog,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private commonService:CommonService,
    private api:ApiUrlHelper,
    private toaster : ToastrService
  ) {
    this.leaveForm = this.fb.group({
      employeeName: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(128)]],
      leaveType: ['', Validators.required],
      status: ['', Validators.required],
      fromDate: ['', Validators.required],
      toDate: ['', Validators.required],
    });
    this.leaveId = data.leaveId;
    if(this.leaveId > 0 ){
      this.patchLeaveData();
    }
    this.getLeaveType();
    this.getLeaveStatus();
    
  }

  submit() {
    this.submitted = true;
    console.log(this.submitted);
    if (this.leaveForm.valid) {
      if(this.leaveForm.value.fromDate > this.leaveForm.value.toDate){
        this.toaster.error("To date is must be greater then from date.")
        return;
      }
      this.submitted = false;
      const leaveData = this.leaveForm.value;
      let requestedModel = {
          leaveRequestId: this.leaveId,
          employeeName: leaveData.employeeName,
          leaveTypeId: leaveData.leaveType,
          statusId: leaveData.status,
          fromDate: this.formateDate(leaveData.fromDate),
          toDate: this.formateDate(leaveData.toDate),
      }
      if(this.leaveId == 0){
        let api = this.api.apiUrl.Leave.addLeave;
         this.commonService.doPost(api, requestedModel).pipe().subscribe({
          next:(response:any)=>{
            if(response.success){
              this.toaster.success(response.message);
              this.dialog.closeAll();
            }else{
              this.toaster.success(response.message);
            }
          }
        })
      }
      else{
        let api = this.api.apiUrl.Leave.updateLeave.replace("{id}", this.leaveId);
         this.commonService.doPut(api, requestedModel).pipe().subscribe({
          next:(response:any)=>{

            if(response.success){
              this.toaster.success(response.message);
              this.dialog.closeAll();
            }else{
              this.toaster.success(response.message);
            }
          }
        })
      }
    }
  }

  cancel() {
    this.dialog.closeAll();
  }

  patchLeaveData(){
    let api = this.api.apiUrl.Leave.getLeave.replace('{Id}',this.leaveId);
    this.commonService.doGet(api).pipe().subscribe({
      next:(response:any)=>{
        if(response.success){
          this.leaveForm.patchValue({
            leaveRequestId: this.leaveId,
            employeeName: response.data.employeeName,
            leaveType: response.data.leaveTypeId,
            status: response.data.statusId,
            fromDate: response.data.fromDate,
            toDate: response.data.toDate,
          });

        }
      }
    })
  }

  getLeaveType(){
    let api = this.api.apiUrl.Leave.leavetype
    this.commonService.doGet(api).pipe().subscribe({
      next:(response)=>{
        this.leaveTypes = response.data;
      }
    })
  }

  getLeaveStatus(){
    let api = this.api.apiUrl.Leave.leaveStatus
    this.commonService.doGet(api).pipe().subscribe({
      next:(response)=>{
        this.statuses = response.data;
      }
    })
  }

  formateDate(date:any){
    const d = new Date(date)
    return new Date(d.getFullYear(), d.getMonth(), d.getDate(), d.getHours(), d.getMinutes() - d.getTimezoneOffset()).toISOString();
  }

}
