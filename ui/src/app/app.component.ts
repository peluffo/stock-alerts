import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, MatToolbarModule, MatButtonModule, MatSidenavModule, MatListModule],
  template: `
    <mat-toolbar color="primary">
      <span>Signal Alerting Platform</span>
      <span class="spacer"></span>
      <button mat-button routerLink="/login">Login</button>
      <button mat-button routerLink="/register">Register</button>
    </mat-toolbar>
    <mat-sidenav-container class="layout">
      <mat-sidenav mode="side" opened>
        <mat-nav-list>
          <a mat-list-item routerLink="/watchlist">Watchlist</a>
          <a mat-list-item routerLink="/alert-rules">Alert Rule Builder</a>
          <a mat-list-item routerLink="/signals">Signals Feed</a>
          <a mat-list-item routerLink="/notifications">Notification Settings</a>
          <a mat-list-item routerLink="/billing">Subscription & Billing</a>
        </mat-nav-list>
      </mat-sidenav>
      <mat-sidenav-content>
        <div class="page">
          <div class="disclaimer">Not financial advice. Educational/informational only.</div>
          <router-outlet></router-outlet>
        </div>
      </mat-sidenav-content>
    </mat-sidenav-container>
  `,
  styles: [
    `
      .layout {
        height: calc(100vh - 64px);
      }
      .spacer {
        flex: 1 1 auto;
      }
    `
  ]
})
export class AppComponent {}
