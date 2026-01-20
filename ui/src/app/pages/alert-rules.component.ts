import { Component } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { ReactiveFormsModule, FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-alert-rules',
  standalone: true,
  imports: [
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    ReactiveFormsModule
  ],
  template: `
    <mat-card>
      <h2>Alert Rule Builder</h2>
      <form [formGroup]="form" class="form">
        <mat-form-field appearance="outline">
          <mat-label>Symbol</mat-label>
          <input matInput formControlName="symbol" />
        </mat-form-field>
        <mat-form-field appearance="outline">
          <mat-label>Timeframe</mat-label>
          <mat-select formControlName="timeframe">
            <mat-option value="1m">1m</mat-option>
            <mat-option value="15m">15m</mat-option>
            <mat-option value="1h">1h</mat-option>
            <mat-option value="1d">1d</mat-option>
          </mat-select>
        </mat-form-field>
        <mat-form-field appearance="outline">
          <mat-label>Strategy preset</mat-label>
          <mat-select formControlName="strategy">
            <mat-option value="bollinger_rsi_mean_reversion">Bollinger + RSI mean reversion</mat-option>
            <mat-option value="ema_trend_pullback">EMA trend pullback</mat-option>
            <mat-option value="breakout_volume_retest">Breakout + volume retest</mat-option>
            <mat-option value="support_resistance_bounce">Support/resistance bounce</mat-option>
            <mat-option value="covered_call_candidate">Covered call candidate</mat-option>
          </mat-select>
        </mat-form-field>
        <mat-form-field appearance="outline">
          <mat-label>Cooldown minutes</mat-label>
          <input matInput type="number" formControlName="cooldown" />
        </mat-form-field>
        <button mat-raised-button color="primary">Save rule</button>
      </form>
    </mat-card>
  `,
  styles: [
    `
      .form {
        display: grid;
        grid-template-columns: repeat(auto-fit, minmax(220px, 1fr));
        gap: 16px;
      }
    `
  ]
})
export class AlertRulesComponent {
  form = new FormGroup({
    symbol: new FormControl('SPY'),
    timeframe: new FormControl('1h'),
    strategy: new FormControl('bollinger_rsi_mean_reversion'),
    cooldown: new FormControl(60)
  });
}
