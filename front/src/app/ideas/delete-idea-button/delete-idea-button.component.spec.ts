import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteIdeaButtonComponent } from './delete-idea-button.component';

describe('DeleteIdeaButtonComponent', () => {
  let component: DeleteIdeaButtonComponent;
  let fixture: ComponentFixture<DeleteIdeaButtonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeleteIdeaButtonComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeleteIdeaButtonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
