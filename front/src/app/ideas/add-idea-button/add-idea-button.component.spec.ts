import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddIdeaButtonComponent } from './add-idea-button.component';

describe('AddIdeaButtonComponent', () => {
  let component: AddIdeaButtonComponent;
  let fixture: ComponentFixture<AddIdeaButtonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
    imports: [AddIdeaButtonComponent]
})
    .compileComponents();

    fixture = TestBed.createComponent(AddIdeaButtonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
