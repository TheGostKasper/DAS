import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { employeeComponent } from './employee/app.employee';
import { RoleComponent } from './roles/app.roles';
import { PlayerComponent } from './players/app.players';
import { TeamComponent } from './teams/app.teams';
import { LoginComponent } from './login/app.login';
import { TMPlayerComponent } from './TMPlayer/app.TMPlayer';


const routes: Routes = [
    { path: '', redirectTo: '/players', pathMatch: 'full' },
    { path: 'users', component: employeeComponent },
    { path: 'roles', component: RoleComponent },
    { path: 'players', component: PlayerComponent },
    { path: 'teams/:id', component: TMPlayerComponent },
    { path: 'teams', component: TeamComponent },
    { path: 'login', component: LoginComponent },
    { path: '**', redirectTo: '/players', pathMatch: 'full' }
];
@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
