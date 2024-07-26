import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { RegisterPageRoutingModule } from './register-routing.module';

import { RegisterPage } from './register.page';
import { MaskitoDirective } from '@maskito/angular';
import { AdvisorService } from '../services/advisor.service';
import { provideHttpClient } from '@angular/common/http';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    MaskitoDirective, 
    IonicModule,
    RegisterPageRoutingModule
  ],
  providers:[
    AdvisorService,
    provideHttpClient()
  ],
  declarations: [RegisterPage]
})
export class RegisterPageModule {}
