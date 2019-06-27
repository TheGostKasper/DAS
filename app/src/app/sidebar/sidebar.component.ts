import { Component, OnInit } from '@angular/core';

declare const $: any;

declare interface RouteInfo {
  path: string;
  title: string;
  icon: string;
  class: string;
}

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./../app.component.css']
})
export class SidebarComponent implements OnInit {
  menuItems: any[];
  current_user = {}
  admin=false;
  constructor() { }

  ngOnInit() {
    var items = [
      { role:'Admin User' ,path: 'players', title: 'Players', icon: 'weekend', class: 'active-pro' },
      { role:'Admin User' ,path: 'teams', title: 'Teams', icon: 'weekend', class: 'active-pro' },
      { role:'Admin' ,path: 'users', title: 'Users', icon: 'weekend', class: 'active-pro' },
      { role:'Admin' ,path: 'roles', title: 'Roles', icon: 'search', class: 'fa fa-search' }]
    
    this.current_user = localStorage.getItem('current_user');
    var crt=JSON.parse(localStorage.getItem('current_user'));

    this.menuItems=items.filter(e=>e.role.includes(crt.role.name));

  }
  
  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('current_user');
    window.location.href = '';
  }
  isMobileMenu() {

    // if ($(window).width() > 991) {
    //   return false;
    // }
    // return true;
  };
}
