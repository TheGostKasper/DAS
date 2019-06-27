import { Component, OnInit } from '@angular/core';
import { DatePipe } from '@angular/common';
import { CRUDService } from '../services/app.crud';
import { Encryption } from '../services/encryption';


let $: any;

@Component({
    selector: 'app-Role',
    templateUrl: './app.Roles.html',
    styleUrls: ['./../app.component.css']
})
export class RoleComponent implements OnInit {
    p: number = 1;
    f_objs = [];
    bc_objs = [];
    roles = [];
    u_Obj = { name: '', createdDate: '' }
    curr_Obj = { id: '', ...this.u_Obj }
    admin=false;
    srch: String = "";
    constructor(private crudService: CRUDService, private encryption: Encryption, private datePipe: DatePipe) {
    }


    ngOnInit() {
        this.getRoles();
        this.admin=this.checkAdmin();
    }
    checkAdmin(){
        var crt=JSON.parse(localStorage.getItem('current_user'));
        return (crt.role.name==="Admin")?true:false;
    }
    getRoles() {
        this.crudService.get({
            url: 'api/roles'
        }).subscribe((res: any) => {
            this.displayError(res, _res => {
                this.f_objs=this.bc_objs = res.data;
            });
        });
    }


    addRole(obj) {
        const _obj = { ...obj };
        _obj.createdDate = new Date().toUTCString();

        this.crudService.post({
            url: 'api/roles',
            body: _obj
        }).subscribe((res: any) => {
            this.displayError(res, _res => {
                let n_obj=res.data;
                this.f_objs.push(n_obj);
                document.getElementById('cancelED').click();
            });

        });
    }
    updateRole(obj) {
        const _obj = { ...obj };
        _obj.updatedDate = new Date().toUTCString();

        this.crudService.put({
            url: `api/roles/${obj.id}`,
            body: _obj
        }).subscribe((res: any) => {
            this.displayError(res, _res => {
                document.getElementById('cancleEditable').click();
            });

        });
    }
    confirmSelection(obj) {
        this.curr_Obj = obj;
    }


   
    deleteRole() {
        this.crudService.delete({
            url: `api/roles/${this.curr_Obj.id}`,
        }).subscribe((res: any) => {
            this.displayError(res, _res => {
                this.f_objs = this.f_objs.filter(e => e.id != this.curr_Obj.id);
                document.getElementById('cancleModal').click();
            });
        });
    }

    filterData() {
        let arr_filter = this.bc_objs;
        this.f_objs = arr_filter.filter(sol => sol.name.includes(this.srch));

    }

    displayError(res, callback) {
        if (res.err) alert('Something went wrong, try again later');
        else callback(res);
    }

}
