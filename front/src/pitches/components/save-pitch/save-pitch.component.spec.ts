import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddPitchComponent } from './save-pitch.component';

describe('AddPitchComponent', () => {
  let component: AddPitchComponent;
  let fixture: ComponentFixture<AddPitchComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
    imports: [AddPitchComponent]
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
