import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { LoaderComponent } from './loader/loader.component';
import { MuroComponent } from './muro/muro.component';
import { LoginComponent } from './login/login.component';
import { UListComponent } from './usuario/u-list/u-list.component';
import { PListComponent } from './producto/p-list/p-list.component';
import { VListComponent } from './venta/v-list/v-list.component';
import { PGetComponent } from './producto/p-get/p-get.component';
import { RegisterComponent } from './usuario/register/register.component';
import { ProfileComponent } from './usuario/profile.component';
import { UpdateComponent } from './usuario/update.component';
import { VeditComponent } from './venta/vedit/vedit.component';
import { VupdateComponent } from './venta/vedit/vupdate.compoment';
import { PEditComponent } from './producto/p-edit/p-edit.component';
import { PAddComponent } from './producto/p-edit/p-add.component';
import { PitemDeleteComponent } from './venta/pitem-delete/pitem-delete.component';
import { PitemAddComponent } from './venta/pitem-add/pitem-add.component';

@NgModule({
  declarations: [
    AppComponent,
    LoaderComponent,
    MuroComponent,
    LoginComponent,
    UListComponent,
    PListComponent,
    VListComponent,
    PGetComponent,
    RegisterComponent,
    ProfileComponent,
    UpdateComponent,
    VeditComponent,
    VupdateComponent,
    PEditComponent,
    PAddComponent,
    PitemDeleteComponent,
    PitemAddComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: MuroComponent, pathMatch: 'full' },
      { path: 'login', component: LoginComponent },
      { path: 'register', component: RegisterComponent },
      { path: 'ulist', component: UListComponent },
      { path: 'profile', component: ProfileComponent },
      { path: 'uupdate/:id', component: UpdateComponent },
      { path: 'plist', component: PListComponent },
      { path: 'plist/:id', component: PGetComponent },
      { path: 'padd', component: PAddComponent },
      { path: 'pupdate/:id', component: PEditComponent },
      { path: 'vlist', component: VListComponent },
      { path: 'vlist', component: VListComponent },
      { path: 'vadd', component: VeditComponent },
      { path: 'vupdate/:id', component: VupdateComponent },
      { path: 'pitemdelete/:id', component: PitemDeleteComponent },
      { path: 'pitemadd/:id', component: PitemAddComponent },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
