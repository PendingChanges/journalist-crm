import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClientsActionMenuComponent } from './clients-action-menu.component';

describe('ClientsActionMenuComponent', () => {
  let component: ClientsActionMenuComponent;
  let fixture: ComponentFixture<ClientsActionMenuComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ClientsActionMenuComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ClientsActionMenuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
