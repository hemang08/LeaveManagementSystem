import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddEditLeaveList } from './add-edit-leave-list';

describe('AddEditLeaveList', () => {
  let component: AddEditLeaveList;
  let fixture: ComponentFixture<AddEditLeaveList>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AddEditLeaveList]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddEditLeaveList);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
