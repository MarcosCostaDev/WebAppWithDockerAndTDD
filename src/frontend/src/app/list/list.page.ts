import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AlertController } from '@ionic/angular';
import { Advisor } from 'src/entities/advisor';
import { AdvisorService } from '../services/advisor.service';

@Component({
  selector: 'app-list',
  templateUrl: './list.page.html',
  styleUrls: ['./list.page.scss'],
})
export class ListPage implements OnInit {

  constructor(private route: Router,
    private alertController: AlertController,
    private advisorService: AdvisorService) { }

  public records: Array<Advisor> = [];

  ngOnInit() {
    this.advisorService.get().then(result => {
      this.records = result.data;
    })
  }

  public edit(advisor: Advisor) {
    this.route.navigate(['/register'], {
      state: advisor
    });
  }

  public newRecord() {
    this.route.navigate(['/register']);
  }

  public async delete(advisor: Advisor) {
    var confirm = await this.alertController.create({
      header: "Delete?",
      message: "Are you sure you want to delete this record?",
      buttons: [
        {
          text: "Cancel",
          role: "cancel"
        },
        {
          text: "Delete it!",
          handler: () => {
            console.log("deleting....");
            setTimeout(() => console.log("deleted"), 5000);

          }
        }
      ]
    });

    await confirm.present();
  }

  searchBy(textChange: any) {
    this.advisorService.getByName(textChange.detail.value).then(result => {
      this.records = result.data;
    })
  }

}
