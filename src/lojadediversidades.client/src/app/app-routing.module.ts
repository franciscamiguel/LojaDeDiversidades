import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './auth/login/login.component';
import { ProdutosComponent } from './pages/produtos/produtos.component';
import { AuthGuard } from './core/auth/auth.guard';
import { VendasComponent } from './pages/vendas/vendas.component';
import { CadastroComponent } from './pages/cadastro/cadastro.component';

// import { AuthGuard } from './guards/auth.guard'; // caso já tenha

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'produtos', component: ProdutosComponent , canActivate: [AuthGuard], data: { role: 'Administrador' } },
  { path: 'vendas', component: VendasComponent , canActivate: [AuthGuard], data: { role: 'Cliente' } },
  { path: 'cadastro', component: CadastroComponent },
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: '**', redirectTo: '/login' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
