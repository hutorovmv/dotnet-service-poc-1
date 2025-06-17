import { Component, Input } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';

@Component({
  selector: 'app-login-form-actions',
  imports: [
    MatCardModule,
    MatButtonModule
  ],
  templateUrl: './login-form-actions.html',
  styleUrl: './login-form-actions.scss'
})
export class LoginFormActions {
  @Input() login!: () => void;
  @Input() register!: () => void;
}
