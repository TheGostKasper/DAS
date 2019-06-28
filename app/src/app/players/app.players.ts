import { Component, OnInit } from '@angular/core';
import { DatePipe } from '@angular/common';
import { CRUDService } from '../services/app.crud';
import { Encryption } from '../services/encryption';


let $: any;

@Component({
    selector: 'app-player',
    templateUrl: './app.players.html',
    styleUrls: ['./../app.component.css']
})
export class PlayerComponent implements OnInit {
    p: number = 1;
    f_objs = [];
    bc_objs = [];
    teams = [];
    u_Obj = { name: '',nationality:'', birthDate: '',teamId:'' }
    curr_Obj = { id: '', ...this.u_Obj }
    admin=false;
    srch: String = "";
    constructor(private crudService: CRUDService, private encryption: Encryption, private datePipe: DatePipe) {
    }


    ngOnInit() {
        this.getPlayers();
        this.getTeams();
        this.admin=this.checkAdmin();
    }
    checkAdmin(){
        var crt=JSON.parse(localStorage.getItem('current_user'));
        return (crt.role.name==="Admin")?true:false;
    }

    getPlayers() {
        this.crudService.get({
            url: 'api/players'
        }).subscribe((res: any) => {
            this.displayError(res, _res => {
                this.f_objs=this.bc_objs = _res.data;
            });
        });
    }
    getTeams() {
        this.crudService.get({
            url: 'api/teams'
        }).subscribe((res: any) => {
            this.displayError(res, _res => {
                this.teams = res.data;
            });
        });
    }

    addPlayer(obj) {
        console.log(obj);
        const _obj = { ...obj };

        this.crudService.post({
            url: 'api/Players',
            body: _obj
        }).subscribe((res: any) => {
            this.displayError(res, _res => {
                let n_obj=res.data;
                n_obj.team = this.teams.filter(e => e.teamId == _obj.teamId)[0];
                this.f_objs.push(n_obj);
                document.getElementById('cancelED').click();
            });

        });

       
    }
    updatePlayer(obj) {
        const _obj = { ...obj };
        _obj.updatedDate = new Date().toUTCString();
        _obj.team = null;

        this.crudService.put({
            url: `api/Players/${obj.playerId}`,
            body: _obj
        }).subscribe((res: any) => {
            this.displayError(res, _res => {
                document.getElementById('cancleEditable').click();
                obj.team = this.teams.filter(e => e.teamId == _obj.teamId)[0];
            });

        });
    }
    confirmSelection(obj) {
        this.curr_Obj = obj;
        this.curr_Obj.birthDate = this.convertDate(obj.birthDate);

    }
    convertDate(date) {
        return this.datePipe.transform(date, 'yyyy-MM-dd')
    }

   
    deletePlayer() {
        this.crudService.delete({
            url: `api/Players/${this.curr_Obj.id}`,
        }).subscribe((res: any) => {
            this.displayError(res, _res => {
                this.f_objs = this.f_objs.filter(e => e.id != this.curr_Obj.id);
                document.getElementById('cancleModal').click();
            });
        });
    }

    filterData() {
        let arr_filter = this.bc_objs;
        this.f_objs = arr_filter.filter(sol => sol.name.includes(this.srch) || sol.team.name.includes(this.srch));

    }

    displayError(res, callback) {
        console.log(res);
        if (res.err) alert('Something went wrong, try again later');
        else callback(res);
    }

}
