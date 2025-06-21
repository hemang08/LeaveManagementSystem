import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LeaveManagementList } from './leave-management-list';

describe('LeaveManagementList', () => {
  let component: LeaveManagementList;
  let fixture: ComponentFixture<LeaveManagementList>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [LeaveManagementList]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LeaveManagementList);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
