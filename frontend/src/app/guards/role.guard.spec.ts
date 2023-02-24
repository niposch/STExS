import { TestBed } from '@angular/core/testing';
import { RoleGuard } from './role.guard';
import {HttpClientTestingModule} from "@angular/common/http/testing";

describe('RoleGuard', () => {
  let guard: RoleGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
      ],
    });
    guard = TestBed.inject(RoleGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
