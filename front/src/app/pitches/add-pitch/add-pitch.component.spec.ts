import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddPitchComponent } from './add-pitch.component';

describe('AddPitchComponent', () => {
  let component: AddPitchComponent;
  let fixture: ComponentFixture<AddPitchComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddPitchComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddPitchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
