import { Component } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { ReactiveFormsModule, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    ReactiveFormsModule
  ],
  template: `
    <mat-card>
      <h2>Create account</h2>
      <form [formGroup]="form" class="form">
        <mat-form-field appearance="outline">
          <mat-label>Display name</mat-label>
          <input matInput formControlName="displayName" />
        </mat-form-field>
        <mat-form-field appearance="outline">
          <mat-label>Email</mat-label>
          <input matInput formControlName="email" type="email" />
        </mat-form-field>
        <mat-form-field appearance="outline">
          <mat-label>Password</mat-label>
          <input matInput formControlName="password" type="password" />
        </mat-form-field>
        <button mat-raised-button color="primary" [disabled]="form.invalid">Register</button>
      </form>
    </mat-card>
  `,
  styles: [
    `
      .form {
        display: grid;
        gap: 16px;
      }
      mat-card {
        max-width: 520px;
      }
    `
  ]
})
export class RegisterComponent {
  form = new FormGroup({
    displayName: new FormControl('', Validators.required),
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', Validators.required)
  });
}
