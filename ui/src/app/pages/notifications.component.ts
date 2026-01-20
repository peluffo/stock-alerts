import { Component } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { ReactiveFormsModule, FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-notifications',
  standalone: true,
  imports: [
    MatCardModule,
    MatSlideToggleModule,
    MatFormFieldModule,
    MatInputModule,
    ReactiveFormsModule
  ],
  template: `
    <mat-card>
      <h2>Notification Settings</h2>
      <form [formGroup]="form" class="form">
        <mat-slide-toggle formControlName="email">Email alerts</mat-slide-toggle>
        <mat-slide-toggle formControlName="sms">SMS alerts</mat-slide-toggle>
        <mat-slide-toggle formControlName="inApp">In-app alerts</mat-slide-toggle>
        <mat-form-field appearance="outline">
          <mat-label>Quiet hours</mat-label>
          <input matInput formControlName="quietHours" placeholder="22:00-06:00" />
        </mat-form-field>
      </form>
    </mat-card>
  `,
  styles: [
    `
      .form {
        display: grid;
        gap: 16px;
      }
    `
  ]
})
export class NotificationsComponent {
  form = new FormGroup({
    email: new FormControl(true),
    sms: new FormControl(false),
    inApp: new FormControl(true),
    quietHours: new FormControl('22:00-06:00')
  });
}
