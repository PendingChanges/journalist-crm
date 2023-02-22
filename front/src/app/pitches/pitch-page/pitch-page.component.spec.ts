import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PitchPageComponent } from './pitch-page.component';

describe('PitchPageComponent', () => {
  let component: PitchPageComponent;
  let fixture: ComponentFixture<PitchPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PitchPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PitchPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
