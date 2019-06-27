import { Component, OnInit } from '@angular/core';
import { DatePipe } from '@angular/common';
import { CRUDService } from '../services/app.crud';
import { Encryption } from '../services/encryption';


let $: any;

@Component({
    selector: 'app-employee',
    templateUrl: './app.employee.html',
    styleUrls: ['./../app.component.css']
})
export class employeeComponent implements OnInit {
    p: number = 1;
    f_objs = [];
    bc_objs = [];
    roles = [];
    u_Obj = { userName: '', password: '', roleId: 1, createdDate: '' }
    curr_Obj = { userId: '', ...this.u_Obj }
    admin=false;
    srch: String = "";
    constructor(private crudService: CRUDService, private encryption: Encryption, private datePipe: DatePipe) {
    }


    ngOnInit() {
        this.getEmployees();
        this.getRoles();
        this.admin=this.checkAdmin();
    }
    checkAdmin(){
        var crt=JSON.parse(localStorage.getItem('current_user'));
        return (crt.role.name==="Admin")?true:false;
    }


    getEmployees() {
        this.crudService.get({
            url: 'api/users'
        }).subscribe((res: any) => {
            this.displayError(res, _res => {
                this.f_objs = this.bc_objs = res.data;
            });
        });
    }

    getRoles() {
        this.crudService.get({
            url: 'api/roles'
        }).subscribe((res: any) => {
            this.displayError(res, _res => {
                this.roles = res.data;
            });
        });
    }


    addEmployee(obj) {
        const _obj = { ...obj };
        _obj.createdDate = new Date().toUTCString();

        _obj.password = this.encryption.b64EncodeUnicode(obj.password);
        this.crudService.post({
            url: 'api/users',
            body: _obj
        }).subscribe((res: any) => {
            this.displayError(res, _res => {
                let n_obj=res.data;
                n_obj.role = this.roles.filter(e => e.id == _obj.roleId)[0];
                this.f_objs.push(n_obj);
                document.getElementById('cancelED').click();
            });

        });
    }
    updateEmployee(obj) {
        const _obj = { ...obj };
        _obj.updatedDate = new Date().toUTCString();
        _obj.password = this.encryption.b64EncodeUnicode(obj.password);

        _obj.role = null;

        this.crudService.put({
            url: `api/users/${obj.userId}`,
            body: _obj
        }).subscribe((res: any) => {
            this.displayError(res, _res => {
                document.getElementById('cancleEditable').click();
                obj.role = this.roles.filter(e => e.id == _obj.roleId)[0];
            });

        });
    }
    confirmSelection(obj) {
        this.curr_Obj = obj;
        this.curr_Obj.password = this.encryption.b64DecodeUnicode(obj.password);
    }


   
    deleteEmployee() {
        this.crudService.delete({
            url: `api/users/${this.curr_Obj.userId}`,
        }).subscribe((res: any) => {
            this.displayError(res, _res => {
                this.f_objs = this.f_objs.filter(e => e.userId != this.curr_Obj.userId);
                document.getElementById('cancleModal').click();
            });
        });
    }

    filterData() {
        let arr_filter = this.bc_objs;
        this.f_objs = arr_filter.filter(sol => sol.userName.includes(this.srch) || sol.role.name.includes(this.srch));

    }

    displayError(res, callback) {
        if (res.err) alert('Something went wrong, try again later');
        else callback(res);
    }

}
