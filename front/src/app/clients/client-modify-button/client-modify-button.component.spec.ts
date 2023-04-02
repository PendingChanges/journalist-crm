import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClientModifyButtonComponent } from './client-modify-button.component';

describe('ClientRenameButtonComponent', () => {
  let component: ClientModifyButtonComponent;
  let fixture: ComponentFixture<ClientModifyButtonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ClientModifyButtonComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ClientModifyButtonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
