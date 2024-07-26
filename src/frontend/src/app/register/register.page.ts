import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MaskitoElementPredicate, MaskitoOptions } from '@maskito/core';
import { Advisor } from 'src/entities/advisor';
import { AdvisorService } from '../services/advisor.service';
import { AlertController } from '@ionic/angular';
import { ApiResponse } from 'src/entities/apiResponse';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.page.html',
  styleUrls: ['./register.page.scss'],
})
export class RegisterPage implements OnInit {


  public advisorForm: FormGroup = new FormGroup({});
  validation_messages = {
    'sin': [
      { type: 'required', message: 'SIN is required.' },
      { type: 'minlength', message: 'SIN must be at least 11 characters long.' },
      { type: 'maxlength', message: 'SIN cannot be more than 11 characters long.' },
      { type: 'pattern', message: 'Your SIN must be provider in the format ###-###-###.' }
    ],
    'name': [
      { type: 'required', message: 'Name is required.' },
      { type: 'maxlength', message: 'Name cannot be more than 255 characters long.' },
    ],
    'phone': [
      { type: 'minlength', message: 'Phone must be at least 9 characters long.' },
      { type: 'maxlength', message: 'Phone cannot be more than 9 characters long.' },
      { type: 'pattern', message: 'Your Phone must be provider in the format ####-####.' }
    ],
    'address': [
      { type: 'maxlength', message: 'Address cannot be more than 255 characters long.' }
    ]
  }

  public existedRecord: boolean = true;
  public advisor = {} as Advisor;
  readonly maskPredicate: MaskitoElementPredicate = async (el) => (el as HTMLIonInputElement).getInputElement();
  public readonly sinMaskOption: MaskitoOptions = {
    mask: [
      ...Array(3).fill(/\d/),
      '-',
      ...Array(3).fill(/\d/),
      '-',
      ...Array(3).fill(/\d/)
    ]
  };

  public readonly phoneMaskOption: MaskitoOptions = {
    mask: [
      ...Array(4).fill(/\d/),
      '-',
      ...Array(4).fill(/\d/),
    ]
  };

  constructor(
    private router: Router, 
    private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    private advisorService: AdvisorService,
    private alertController: AlertController) { 

      this.route.queryParams.subscribe(params => {
        if (this.router.getCurrentNavigation()?.extras.state) {
          this.advisor = this.router.getCurrentNavigation()?.extras.state as Advisor;
          this.existedRecord = true;
        }
         else{
          this.existedRecord = false;
         }
      });
    }

  ngOnInit() {

    this.advisorForm = this.formBuilder.group({
      sin: ['', Validators.compose([
        Validators.required,
        Validators.minLength(11),
        Validators.maxLength(11),
        Validators.pattern("^\\d{3}-\\d{3}-\\d{3}$")
      ])],
      name: [
        '',
        Validators.compose([
          Validators.required,
          Validators.maxLength(255),
        ]),
      ],
      phone: ['', Validators.compose([
        Validators.minLength(9),
        Validators.maxLength(9),
        Validators.pattern('^\\d{4}-\\d{4}$')
      ])],
      address: ['', Validators.compose([
        Validators.maxLength(255),
      ])],
    });
  }

  ionViewDidEnter(){
    if(!this.existedRecord){
      this.advisorForm.reset();
      return;
    }

    this.advisorForm.setValue({
        sin: this.advisor.getMaskedSin(),
        name: this.advisor.name,
        phone: this.advisor.getMaskedPhone(),
        address: this.advisor.address,
    });
  }



  async save() {
    if (!this.advisorForm.valid) return;

    var saveSuccessfullyOnSaveDialog = await this.alertController.create({
      header: "Saved!",
      message: "Your record has been saved successfully!",
      buttons: [
        {
          text: "Ok",
          handler: () => {
            this.router.navigate(['/list']);
          }
        }
      ]
    });

    try {
      let result: ApiResponse<Advisor>;
      let request:Advisor = new Advisor(this.advisorForm.value["sin"] as string, 
                                        this.advisorForm.value["name"] as string, 
                                        this.advisorForm.value["phone"] as string, 
                                        this.advisorForm.value["address"] as string);
      if (this.existedRecord) {
        result = await this.advisorService.update(request);
      }
      else {
        result = await this.advisorService.create(request);
      }
      await saveSuccessfullyOnSaveDialog.present();
    } catch (e:any) {
      console.log(e);
      var errorOnSaveDialog = await this.alertController.create({
        header: "Error on save",
        message: "Something went wrong.",
        buttons: [
          {
            text: "Ok",
            role: "cancel",
          }
        ]
      });

      console.log(e);
      await errorOnSaveDialog.present();
    }

  }

}
