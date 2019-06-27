// Services
import { AuthenticationService } from './services/app.authentication';
import { CRUDService } from './services/app.crud';
import { Encryption } from './services/encryption';

// Components
import { AppComponent } from './app.component';

import { employeeComponent } from './employee/app.employee';
import { RoleComponent } from './roles/app.roles';
import { PlayerComponent } from './players/app.players';
import { TeamComponent } from './teams/app.teams';
import { TMPlayerComponent } from './TMPlayer/app.TMPlayer';

import { LoginComponent } from './login/app.login';
import { HTTP_INTERCEPTORS } from '@angular/common/http';

import { AuthInterceptor } from './services/interceptor';
import { DatePipe } from '@angular/common';

export class Container {
    declarations = [
        AppComponent,
        LoginComponent,
        employeeComponent,
        RoleComponent,
        PlayerComponent,
        TeamComponent,
        TMPlayerComponent
    ];
    providers = [{
        provide: HTTP_INTERCEPTORS,
        useClass: AuthInterceptor,
        multi: true,
    }, AuthenticationService, CRUDService, Encryption, DatePipe];
}
