import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SaveIdeaComponent } from './save-idea.component';

describe('AddIdeaComponent', () => {
  let component: SaveIdeaComponent;
  let fixture: ComponentFixture<SaveIdeaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
    imports: [SaveIdeaComponent]
})
    .compileComponents();

    fixture = TestBed.createComponent(SaveIdeaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
