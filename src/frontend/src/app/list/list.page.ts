import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AlertController } from '@ionic/angular';
import { Advisor, HeathStatusEnum } from 'src/entities/advisor';
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

  }

  ionViewWillEnter() {
    this.loadRecords();
  }

  public getHealthStatusColor(status: HeathStatusEnum) {
    switch (status) {
      case HeathStatusEnum.Green:
        return "success";
      case HeathStatusEnum.Red:
        return "danger";
      case HeathStatusEnum.Yellow:
        return "warning";
    }
  }


  private loadRecords(name: string = '') {
    this.advisorService.getByName(name).then(result => {
      this.records = result.data.map(p => new Advisor(p.sin, p.name, p.phone, p.address, p.heathStatus));
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
          handler: async () => {
            await this.advisorService.delete(advisor);
            this.loadRecords();
          }
        }
      ]
    });

    await confirm.present();
  }

  searchBy(textChange: any) {
    this.loadRecords(textChange.detail.value)
  }

}
