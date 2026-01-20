import { Component } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { MatTableModule } from '@angular/material/table';

@Component({
  selector: 'app-signals',
  standalone: true,
  imports: [MatCardModule, MatTableModule, MatChipsModule],
  template: `
    <mat-card>
      <h2>Signals Feed</h2>
      <table mat-table [dataSource]="signals" class="mat-elevation-z1">
        <ng-container matColumnDef="symbol">
          <th mat-header-cell *matHeaderCellDef>Symbol</th>
          <td mat-cell *matCellDef="let signal">{{ signal.symbol }}</td>
        </ng-container>
        <ng-container matColumnDef="type">
          <th mat-header-cell *matHeaderCellDef>Type</th>
          <td mat-cell *matCellDef="let signal">{{ signal.type }}</td>
        </ng-container>
        <ng-container matColumnDef="confidence">
          <th mat-header-cell *matHeaderCellDef>Confidence</th>
          <td mat-cell *matCellDef="let signal">
            <mat-chip color="primary" selected>{{ signal.confidence }}%</mat-chip>
          </td>
        </ng-container>
        <ng-container matColumnDef="rationale">
          <th mat-header-cell *matHeaderCellDef>Explanation</th>
          <td mat-cell *matCellDef="let signal">{{ signal.rationale }}</td>
        </ng-container>
        <tr mat-header-row *matHeaderRowDef="columns"></tr>
        <tr mat-row *matRowDef="let row; columns: columns"></tr>
      </table>
    </mat-card>
  `,
  styles: [
    `
      table {
        width: 100%;
      }
    `
  ]
})
export class SignalsComponent {
  columns = ['symbol', 'type', 'confidence', 'rationale'];
  signals = [
    {
      symbol: 'AAPL',
      type: 'BUY_SIGNAL',
      confidence: 72,
      rationale: 'RSI oversold + volatility filter'
    },
    {
      symbol: 'SPY',
      type: 'WATCH',
      confidence: 58,
      rationale: 'Support bounce with mixed trend alignment'
    }
  ];
}
