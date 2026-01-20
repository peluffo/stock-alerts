import { Component } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-watchlist',
  standalone: true,
  imports: [MatCardModule, MatTableModule, MatButtonModule],
  template: `
    <mat-card>
      <h2>Watchlist</h2>
      <table mat-table [dataSource]="items" class="mat-elevation-z1">
        <ng-container matColumnDef="symbol">
          <th mat-header-cell *matHeaderCellDef>Symbol</th>
          <td mat-cell *matCellDef="let item">{{ item.symbol }}</td>
        </ng-container>
        <ng-container matColumnDef="notes">
          <th mat-header-cell *matHeaderCellDef>Notes</th>
          <td mat-cell *matCellDef="let item">{{ item.notes }}</td>
        </ng-container>
        <tr mat-header-row *matHeaderRowDef="columns"></tr>
        <tr mat-row *matRowDef="let row; columns: columns"></tr>
      </table>
      <button mat-stroked-button color="primary">Add symbol</button>
    </mat-card>
  `,
  styles: [
    `
      table {
        width: 100%;
        margin-bottom: 16px;
      }
    `
  ]
})
export class WatchlistComponent {
  columns = ['symbol', 'notes'];
  items = [
    { symbol: 'AAPL', notes: 'Large cap watch' },
    { symbol: 'SPY', notes: 'Market benchmark' }
  ];
}
