import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClientActionMenuComponent } from './client-action-menu.component';

describe('ClientActionMenuComponent', () => {
  let component: ClientActionMenuComponent;
  let fixture: ComponentFixture<ClientActionMenuComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ClientActionMenuComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ClientActionMenuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
