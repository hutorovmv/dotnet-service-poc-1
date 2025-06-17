import { Component } from '@angular/core';
import { ErrorStateMatcher } from '@angular/material/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { ReactiveFormsModule, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-email-input',
  imports: [
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
  ],
  templateUrl: './email-input.html',
  styleUrl: './email-input.scss'
})
export class EmailInput {
  emailControl: FormControl = new FormControl('', [Validators.required, Validators.email]);

  constructor(public errorStateMatcher: ErrorStateMatcher) {}

  isEmailEmpty(): boolean {
    return this.emailControl.hasError('required');
  }

  isEmailInvalid(): boolean {
    return this.emailControl.hasError('email') && !this.emailControl.hasError('required');
  }
}
