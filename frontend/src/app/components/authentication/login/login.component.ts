import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  public showLoading:boolean = false;

  constructor(
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      let callbackUrl = params['callbackUrl'];
      if (callbackUrl == null) {
        callbackUrl = '/';
      }
      console.log(callbackUrl);
    });
  }

  login(){
    this.showLoading = true;
    setTimeout(() =>{
      this.showLoading = false;
    }, 2000)
  }
}
