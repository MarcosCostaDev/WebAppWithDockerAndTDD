import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { ListPageRoutingModule } from './list-routing.module';

import { ListPage } from './list.page';
import { AdvisorService } from '../services/advisor.service';
import { provideHttpClient } from '@angular/common/http';
@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    ListPageRoutingModule
  ],
  providers:[
    AdvisorService,
    provideHttpClient()
  ],
  declarations: [ListPage]
})
export class ListPageModule {}
