import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ApiUrlHelper {
  public apiUrl = {
    Leave:{
      list: 'leave?page={pageNumber}&pageSize={pageSize}&sort={sort}&dir={dir}',
      leavetype :'leave/leave-type',
      leaveStatus:'leave/leave-status',
      getLeave:'leave/{Id}',
      updateLeave : 'leave/{id}',
      addLeave : 'leave',
      deleteLeave : 'leave/{id}'
    }
  };
}