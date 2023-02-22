import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PitchListComponent } from './pitch-list.component';

describe('PitchListComponent', () => {
  let component: PitchListComponent;
  let fixture: ComponentFixture<PitchListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PitchListComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PitchListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
