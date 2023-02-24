import { Component, OnInit } from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {AuthenticateService} from "../../../../services/generated/services/authenticate.service";
@Component({
  selector: 'app-confirm-email',
  templateUrl: './confirm-email.component.html',
  styleUrls: ['./confirm-email.component.scss']
})
export class ConfirmEmailComponent implements OnInit {

  constructor(private route: ActivatedRoute,
              private authService: AuthenticateService) { }

  ngOnInit(): void {
    /*
    this.route.params.subscribe(async params => {
      console.log(params['token'])
      await lastValueFrom(this.authService.CONFIRMEMAIL({
        token: params['token']
      }))
    });
    */
  }


}
