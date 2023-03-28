import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IdeaSelectorComponent } from './idea-selector.component';

describe('IdeaSelectorComponent', () => {
  let component: IdeaSelectorComponent;
  let fixture: ComponentFixture<IdeaSelectorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IdeaSelectorComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(IdeaSelectorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
