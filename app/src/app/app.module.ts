import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';
import { AppRoutingModule } from './app.routes';
import { FormsModule } from '@angular/forms';
import { Container } from './container.modules';
import {NgxPaginationModule} from 'ngx-pagination';

import { AppComponent } from './app.component';

import 'rxjs/add/operator/do';
const ctr = new Container();


import { SidebarModule } from './sidebar/sidebar.module';

@NgModule({
  declarations: ctr.declarations,
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    NgxPaginationModule,
    SidebarModule,
    RouterModule,
    AppRoutingModule,
  ],
  exports: [RouterModule],
  providers: ctr.providers,
  bootstrap: [AppComponent]
})
export class AppModule { }
