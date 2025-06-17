import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoginFormActions } from './login-form-actions';

describe('LoginFormActions', () => {
  let component: LoginFormActions;
  let fixture: ComponentFixture<LoginFormActions>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LoginFormActions]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LoginFormActions);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
