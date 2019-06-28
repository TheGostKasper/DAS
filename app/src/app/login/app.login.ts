import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../services/app.authentication';
import { Encryption } from '../services/encryption';

@Component({
    selector: 'app-login',
    templateUrl: './app.login.html',
    styleUrls: ['./app.login.css']
})
export class LoginComponent implements OnInit {
    user: any = {
        username: '', password: '',roleId:1
    };
    
    auth=false;
    constructor(private authenticationService: AuthenticationService, private encryption: Encryption) { }
    ngOnInit() {
    }

    login(user) {
        this.authenticationService.logIn(this.retUserEnc(user)).subscribe((res: any) => {
            this.fellowLog(res);
        });
    }

    signUp(user) {
        user.roleId=2;
        this.authenticationService.signUp(this.retUserEnc(user)).subscribe((res: any) => {
            this.fellowLog(res);
        });
    }

    private retUserEnc(user){
        alert('Your request being proccessing');
        const _user = { ...user };
        _user.password = this.encryption.b64EncodeUnicode(user.password);
        return _user;
    }

    private fellowLog(res){
        if (res.data == "") {
            alert(res.message);
        } else {
            localStorage.setItem('token', res.data.token);
            localStorage.setItem('current_user', JSON.stringify(res.data));
            window.location.href = '';
        }
    }
}
