import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BankComponent } from './bank/components/bank/bank.component';
import { BankSearchResolver } from './bank/services/bank-search.resolver';
import { BankResolver } from './bank/services/bank.resolver';
import { HomeCompoennt } from './home/home.component';


const routes: Routes = [
  {
    path: '',
    redirectTo: 'home',
    pathMatch: 'full'
  },
  {
    path: 'bank/:id',
    component: BankComponent,
    resolve: {
      model: BankResolver
    }
  },
  {
    path: 'home',
    component: HomeCompoennt,
    resolve: {
      model: BankSearchResolver
    },
    pathMatch: 'full'
  },
  {
    path: '**',
    redirectTo: 'home'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
