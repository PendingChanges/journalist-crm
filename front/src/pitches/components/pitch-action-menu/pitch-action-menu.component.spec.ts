import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PitchActionMenuComponent } from './pitch-action-menu.component';

describe('PitchActionMenuComponent', () => {
  let component: PitchActionMenuComponent;
  let fixture: ComponentFixture<PitchActionMenuComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ PitchActionMenuComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PitchActionMenuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
