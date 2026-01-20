import { Routes } from '@angular/router';
import { LoginComponent } from './pages/login.component';
import { RegisterComponent } from './pages/register.component';
import { WatchlistComponent } from './pages/watchlist.component';
import { AlertRulesComponent } from './pages/alert-rules.component';
import { SignalsComponent } from './pages/signals.component';
import { NotificationsComponent } from './pages/notifications.component';
import { BillingComponent } from './pages/billing.component';

export const appRoutes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'signals' },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'watchlist', component: WatchlistComponent },
  { path: 'alert-rules', component: AlertRulesComponent },
  { path: 'signals', component: SignalsComponent },
  { path: 'notifications', component: NotificationsComponent },
  { path: 'billing', component: BillingComponent }
];
