import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddPitchButtonComponent } from './add-pitch-button.component';

describe('AddPitchButtonComponent', () => {
  let component: AddPitchButtonComponent;
  let fixture: ComponentFixture<AddPitchButtonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
    imports: [AddPitchButtonComponent]
})
    .compileComponents();

    fixture = TestBed.createComponent(AddPitchButtonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
