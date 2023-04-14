import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PitchModifyButtonComponent } from './pitch-modify-button.component';

describe('PitchModifyButtonComponent', () => {
  let component: PitchModifyButtonComponent;
  let fixture: ComponentFixture<PitchModifyButtonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ PitchModifyButtonComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PitchModifyButtonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
