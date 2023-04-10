import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModifyIdeaButtonComponent } from './modify-idea-button.component';

describe('ModifyIdeaButtonComponent', () => {
  let component: ModifyIdeaButtonComponent;
  let fixture: ComponentFixture<ModifyIdeaButtonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ ModifyIdeaButtonComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ModifyIdeaButtonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
