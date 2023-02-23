import { TestBed } from '@angular/core/testing';

import { RoleGuardGuard } from './role.guard';

describe('RoleGuardGuard', () => {
  let guard: RoleGuardGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(RoleGuardGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
