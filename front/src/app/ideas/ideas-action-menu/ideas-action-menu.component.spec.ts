import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IdeasActionMenuComponent } from './ideas-action-menu.component';

describe('IdeasActionMenuComponent', () => {
  let component: IdeasActionMenuComponent;
  let fixture: ComponentFixture<IdeasActionMenuComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IdeasActionMenuComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(IdeasActionMenuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
