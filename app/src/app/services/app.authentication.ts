import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';

@Injectable()
export class AuthenticationService {
    ApiUrl = 'https://localhost:5001/api/users';
    // 'http://iisnode.local.com';
    constructor(
        private http: HttpClient
    ) { }
    logIn(user) {
        return new Observable(observer => {
            this.http.post(`${this.ApiUrl}/authenticate`, JSON.stringify(user), this.getHeader())
                .subscribe(
                    (response: Response) => {
                        observer.next(response);
                        observer.complete();
                    },
                    error => {
                        alert('Username or password is incorrect');
                        observer.error(error);
                    });
        });

    }
    signUp(user) {
        return new Observable(observer => {
            this.http.post(`${this.ApiUrl}`, JSON.stringify(user), this.getHeader())
                .subscribe(
                    (response: Response) => {
                        observer.next(response);
                        observer.complete();
                    },
                    error => {
                        observer.error(error);
                    });
        });
    }
    
    getToken() {
        return localStorage.getItem('token');
    }

    private getHeader() {
        const headers = new HttpHeaders({
            'Content-Type': 'application/json',
            'Access-Control-Allow-Origin': '*',
            // 'Authorization':`Bearer ${localStorage.getItem('token')}`,
            'Accept':'application/json',
            'Access-Control-Allow-Headers': 'access-control-allow-headers,access-control-allow-origin,content-type,authorization'
        });
        return { headers: headers };
    }
}
