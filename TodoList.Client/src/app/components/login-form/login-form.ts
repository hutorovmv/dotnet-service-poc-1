import { Component, Input, signal } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { ReactiveFormsModule } from '@angular/forms';
import { EmailInput } from '../common/email-input/email-input';
import { PasswordInput } from '../common/password-input/password-input';
import { LoginFormActions } from './login-form-actions/login-form-actions';

@Component({
  selector: 'app-login-form',
  imports: [
    MatCardModule,
    ReactiveFormsModule,
    EmailInput,
    PasswordInput,
    LoginFormActions,
  ],
  templateUrl: './login-form.html',
  styleUrl: './login-form.scss'
})
export class LoginForm {
  @Input() login!: () => void;
  @Input() register!: () => void;
}
