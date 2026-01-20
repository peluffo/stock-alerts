import { Component } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-billing',
  standalone: true,
  imports: [MatCardModule, MatButtonModule],
  template: `
    <mat-card>
      <h2>Subscription & Billing</h2>
      <p>Manage your Stripe subscription and payment method.</p>
      <button mat-raised-button color="primary">Manage subscription</button>
    </mat-card>
  `
})
export class BillingComponent {}
