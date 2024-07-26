import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MaskitoOptions } from '@maskito/core';
import { Advisor } from 'src/entities/advisor';
import { AdvisorService } from '../services/advisor.service';
import { AlertController } from '@ionic/angular';
import { ApiResponse } from 'src/entities/apiResponse';

@Component({
  selector: 'app-register',
  templateUrl: './register.page.html',
  styleUrls: ['./register.page.scss'],
})
export class RegisterPage implements OnInit {


  public advisor = {} as Advisor;
  public existedRecord: boolean = true;

  public readonly sinMaskOption: MaskitoOptions = {
    mask: /^\d{3}-\d{2}-\d{4}$/,
  };

  public readonly phoneMaskOption: MaskitoOptions = {
    mask: /^\d{4}-\d{4}$/,
  };

  constructor(private router: Router,
    private advisorService: AdvisorService,
    private alertController: AlertController) { }

  ngOnInit() {
    this.advisor = (this.router.getCurrentNavigation()?.extras.state || {}) as Advisor;
    this.existedRecord = this.router.getCurrentNavigation()?.extras.state != undefined;
  }


  async save() {
    try {

      var savedSuccessfully = await this.alertController.create({
        header: "Saved!",
        message: "Are you sure you want to delete this record?",
        buttons: [
          {
            text: "Ok",
            handler: () => {
              this.router.navigate(['/list']);
            }
          }
        ]
      });
      let result: ApiResponse<Advisor>;
      if (this.existedRecord) {
        result = await this.advisorService.update(this.advisor);
      }
      else {
        result = await this.advisorService.create(this.advisor);
      }
      await savedSuccessfully.present();
    } catch (e) {
      var savedSuccessfully = await this.alertController.create({
        header: "Error on save",
        message: `${JSON.stringify(e)}`,
        buttons: [
          {
            text: "Ok",
            role: "cancel",
          }
        ]
      });

    }

  }

}
