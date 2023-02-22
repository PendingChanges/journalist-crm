import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IdeaActionMenuComponent } from './idea-action-menu.component';

describe('IdeaActionMenuComponent', () => {
  let component: IdeaActionMenuComponent;
  let fixture: ComponentFixture<IdeaActionMenuComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IdeaActionMenuComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(IdeaActionMenuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
