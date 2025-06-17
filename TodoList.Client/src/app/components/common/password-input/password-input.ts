import { Component, signal } from '@angular/core';
import { ErrorStateMatcher } from '@angular/material/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { ReactiveFormsModule, FormControl, Validators } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-password-input',
  imports: [
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
  ],
  templateUrl: './password-input.html',
  styleUrls: ['./password-input.scss']
})
export class PasswordInput {
  passwordControl: FormControl = new FormControl('', [Validators.required, Validators.minLength(5)]);
  hidePassword = signal(true);

  constructor(public errorStateMatcher: ErrorStateMatcher) {}

  isPasswordInvalid(): boolean {
    return this.passwordControl.hasError('minlength') && !this.passwordControl.hasError('required');
  }

  isPasswordEmpty(): boolean {
    return this.passwordControl.hasError('required');
  }

  togglePasswordVisibility(event: MouseEvent) {
    this.hidePassword.set(!this.hidePassword());
    event.stopPropagation();
  }

  getPasswordVisibilityIconName(): string {
    return this.hidePassword() ? 'visibility_off' : 'visibility';
  }

  getPasswordInputType(): string {
    return this.hidePassword() ? 'password' : 'text';
  }
}
