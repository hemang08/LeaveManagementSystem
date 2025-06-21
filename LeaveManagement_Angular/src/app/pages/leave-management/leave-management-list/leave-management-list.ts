import { Component, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { AddEditLeaveComponent } from '../add-edit-leave-list/add-edit-leave-list';
import { CommonService } from '../../../service/common/common';
import { ApiUrlHelper } from '../../../api/api-url';
import Swal from 'sweetalert2';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-leave-management-list',
  standalone: false,
  templateUrl: './leave-management-list.html',
  styleUrl: './leave-management-list.scss'
})
export class LeaveManagementList {
  displayedColumns = ['employeeName', 'leaveType', 'fromDate', 'status', 'actions'];
  dataSource: any[] = [];
  totalLeaves = 0;

  // Pagination + Sorting State
  pageIndex = 0;
  pageSize = 5;
  sortColumn = 'employeeName';
  sortDirection: 'asc' | 'desc' = 'asc';

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private dialog: MatDialog,
    private common: CommonService,
    private api: ApiUrlHelper,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit() {
    this.getList();
  }

  ngAfterViewInit() {
    // Sorting event
    this.sort.sortChange?.subscribe((sort: any) => {
      this.sortColumn = sort.active;
      this.sortDirection = sort.direction === '' ? 'asc' : sort.direction;
      this.pageIndex = 0; // Reset to first page
      this.getList();
    });

    // Paginator event
    this.paginator.page.subscribe((page: any) => {
      this.pageIndex = page.pageIndex;
      this.pageSize = page.pageSize;
      this.getList();
    });
  }

  addEditLeave(leaveId: any) {
    let data = {
      leaveId: leaveId
    }
    const dialogRef = this.dialog.open(AddEditLeaveComponent, {
      width: '600px',
      panelClass: 'custom-dialog-container',
      data: data
    });

    dialogRef.afterClosed().subscribe(result => {
      this.getList();
    });
  }

  getList() {
    const page = this.pageIndex + 1; // API expects 1-based index?
    const sort = this.sortColumn || 'employeeName'; // default sort
    const dir = this.sortDirection;

    const api = this.api.apiUrl.Leave.list
      .replace('{pageNumber}', page.toString())
      .replace('{pageSize}', this.pageSize.toString())
      .replace('{sort}', sort)
      .replace('{dir}', dir);
      
    this.common.doGet(api).subscribe({
      next: (response) => {
        if (response.success) {
          this.dataSource = response.data;
          if (response.data.length > 0) {
            this.totalLeaves = response.data[0].totalCount;
          }
          else {
            this.totalLeaves = 0;
          }
        }

      },
      error: (err) => {
        console.error(err)

      }
    });
  }

  onSortChange(sort: any) {
    this.sortColumn = sort.active;
    this.sortDirection = sort.direction as 'asc' | 'desc';
    this.pageIndex = 0;
    this.getList();
  }

  onPageChange(event: any) {
    this.pageIndex = event.pageIndex;
    this.pageSize = event.pageSize;
    this.getList();
  }

  confirmDelete(leaveId: number): void {
    Swal.fire({
      title: 'Are you sure?',
      text: 'This leave request will be permanently deleted.',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#d33',
      cancelButtonColor: '#3085d6',
      confirmButtonText: 'Yes, delete it!',
      cancelButtonText: 'Cancel'
    }).then((result) => {
      if (result.isConfirmed) {
        let apiUrl = this.api.apiUrl.Leave.deleteLeave.replace("{id}", leaveId.toString())
        this.common.doDelete(apiUrl, leaveId).subscribe({
          next: () => {
            Swal.fire('Deleted!', 'The leave request has been deleted.', 'success');
            if (this.dataSource && this.dataSource.length == 1 && this.pageIndex > 0) {
              this.pageIndex -= 1;
              this.paginator.pageIndex = this.pageIndex;

            }
            this.getList();

          },
          error: () => {
            Swal.fire('Oops!', 'Something went wrong. Try again.', 'error');
          }
        });
      }
    });
  }

}
