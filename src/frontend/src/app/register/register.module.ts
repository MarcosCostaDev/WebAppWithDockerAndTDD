import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { RegisterPageRoutingModule } from './register-routing.module';

import { RegisterPage } from './register.page';
import { AdvisorService } from '../services/advisor.service';
import { provideHttpClient } from '@angular/common/http';
import { MaskitoDirective } from '@maskito/angular';


@NgModule({
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    IonicModule,
    RegisterPageRoutingModule,
    MaskitoDirective
  ],
  providers:[
    AdvisorService,
    provideHttpClient()
  ],
  declarations: [RegisterPage]
})
export class RegisterPageModule {}
