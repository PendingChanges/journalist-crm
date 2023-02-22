import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PitchesActionMenuComponent } from './pitches-action-menu.component';

describe('PitchesActionMenuComponent', () => {
  let component: PitchesActionMenuComponent;
  let fixture: ComponentFixture<PitchesActionMenuComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PitchesActionMenuComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PitchesActionMenuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
