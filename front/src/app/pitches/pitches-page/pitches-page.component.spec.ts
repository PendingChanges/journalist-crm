import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PitchesPageComponent } from './pitches-page.component';

describe('PitchesPageComponent', () => {
  let component: PitchesPageComponent;
  let fixture: ComponentFixture<PitchesPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PitchesPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PitchesPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
