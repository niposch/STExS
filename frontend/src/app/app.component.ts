import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})

export class AppComponent implements OnInit{
  title = 'STExS';

  public temperatures:Array<number> = [];
  constructor(private readonly http: HttpClient,
              ) {
  }

  ngOnInit(): void {
  }
}
