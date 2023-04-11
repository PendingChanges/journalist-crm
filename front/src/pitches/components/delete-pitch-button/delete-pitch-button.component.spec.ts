import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeletePitchButtonComponent } from './delete-pitch-button.component';

describe('DeletePitchButtonComponent', () => {
  let component: DeletePitchButtonComponent;
  let fixture: ComponentFixture<DeletePitchButtonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ DeletePitchButtonComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeletePitchButtonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
