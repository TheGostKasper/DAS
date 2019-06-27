import { Component, OnInit } from '@angular/core';
import { DatePipe } from '@angular/common';
import { CRUDService } from '../services/app.crud';
import { Encryption } from '../services/encryption';


let $: any;

@Component({
    selector: 'app-Team',
    templateUrl: './app.teams.html',
    styleUrls: ['./../app.component.css']
})
export class TeamComponent implements OnInit {
    p: number = 1;
    f_objs = [];
    bc_objs = [];
    teams = [];
    u_Obj = { name: '',country:'',coach:'', foundation: '' }
    curr_Obj = { teamId: '', ...this.u_Obj }
    admin=false;
    srch: String = "";
    constructor(private crudService: CRUDService, private encryption: Encryption, private datePipe: DatePipe) {
    }


    ngOnInit() {
        this.getTeams();
        this.admin=this.checkAdmin();
    }
    checkAdmin(){
        var crt=JSON.parse(localStorage.getItem('current_user'));
        return (crt.role.name==="Admin")?true:false;
    }

    getTeams() {
        this.crudService.get({
            url: 'api/teams'
        }).subscribe((res: any) => {
            this.displayError(res, _res => {
                this.f_objs=this.bc_objs = res.data;
            });
        });
    }
    convertDate(date) {
        return this.datePipe.transform(date, 'yyyy-MM-dd')
    }

    addTeam(obj) {
        const _obj = { ...obj };
        _obj.createdDate = new Date().toUTCString();

        this.crudService.post({
            url: 'api/teams',
            body: _obj
        }).subscribe((res: any) => {
            this.displayError(res, _res => {
                let n_obj=res.data;
                this.f_objs.push(n_obj);
                document.getElementById('cancelED').click();
            });

        });
    }
    updateTeam(obj) {
        const _obj = { ...obj };
        _obj.updatedDate = new Date().toUTCString();

        this.crudService.put({
            url: `api/teams/${obj.teamId}`,
            body: _obj
        }).subscribe((res: any) => {
            this.displayError(res, _res => {
                document.getElementById('cancleEditable').click();
            });

        });
    }
    confirmSelection(obj) {
        this.curr_Obj = obj;
        this.curr_Obj.foundation = this.convertDate(obj.foundation);
    }


   
    deleteTeam() {
        this.crudService.delete({
            url: `api/teams/${this.curr_Obj.teamId}`,
        }).subscribe((res: any) => {
            this.displayError(res, _res => {
                this.f_objs = this.f_objs.filter(e => e.teamId != this.curr_Obj.teamId);
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
