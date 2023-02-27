import { Component, OnInit } from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {AuthenticateService} from "../../../../services/generated/services/authenticate.service";
import {lastValueFrom} from "rxjs";
@Component({
  selector: 'app-confirm-email',
  templateUrl: './confirm-email.component.html',
  styleUrls: ['./confirm-email.component.scss']
})
export class ConfirmEmailComponent implements OnInit {

  public isLoading = false;
  public hasSucceeded = false;
  constructor(private route: ActivatedRoute,
              private authService: AuthenticateService) { }

  ngOnInit(): void {
    this.isLoading = true;
    this.route.queryParams.subscribe( params => {
      lastValueFrom(this.authService.apiAuthenticateConfirmEmailPost({
        token: params['token'],
        userId: params['userId']
      })).then(() => {
        this.hasSucceeded = true;
      }).catch(() => {
        this.hasSucceeded = false;
      });
      this.isLoading = false;
    });
  }
}



/*

http://localhost:4200/confirm-email?token=CfDJ8GoDThkWwhhAmaAFMiZ2SDomUcEm1Z9q9ApNV8c7NlnmQ92wx0gQBLrPlp0B0hQeBV9PD4s7w5pjr+Ta8OzzTGiVuMR4wgrA32bGJZ6HhfDU5apn6bOIIbhVt6sARdyDrrMGyxjybX4Fm4xU4oXzoPfqLZtamFz53hehUJrkzytlLhWte2pqxEe7QqWTk8VTihdrcuqUZGvcSQqTRUwXI+r+SLS3NSf0Tc14uXBbyq7bBMNBxb7CdrsIWgESgHIQ==&userId=5b68a4fe-cde9-44a7-884f-08db180466a6

http://localhost:4200/confirm-email?token=CfDJ8GoDThkWwhhAmaAFMiZ2SDomUcEm1Z9q9ApNV8c7NlnmQ92wx0gQBLrPlp0B0hQeBV9PD4s7w5pjr+Ta8OzzTGiVuMR4wgrA32bGJZ6HhfDU5apn6bOIIbhVt6sARdyDrrMGyxjybX4Fm4xU4oXzoPfqLZtamFz53hehUJrkzytlLhWte2pqxEe7QqWTk8VTihdrcuqUZGvcSQqTRUwXI+r+SLS3NSf0Tc14uXBbyq7bBMNBxb7CdrsIWgESgHIQ&userId=5b68a4fe-cde9-44a7-884f-08db180466a6

http://localhost:4200/confirm-email?token=testToken&userId=testUserId

http://localhost:4200/confirm-email?token=testToken

http://localhost:4200/confirm-email?token=testToken&userId=testUserId

 */
