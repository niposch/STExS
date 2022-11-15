import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  public showLoading:boolean = false;
  public agreedToTOS: boolean = false;

  constructor() { }

  ngOnInit(): void {
  }

  register() {
    this.showLoading = true;
    setTimeout(() =>{
      this.showLoading = false;
    }, 2000)
  }
}
